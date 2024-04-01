using API_MongoDB.Models;
using API_MongoDB.Services.Menus;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMenusService _menusService;

        public MenusController(IMenusService menusService)
        {
            _menusService = menusService;
        }
        // GET: api/<MenusController>
        [HttpGet]
        public ActionResult<List<Menu>> Get()
        {
            return _menusService.Get();
        }

        // GET api/<MenusController>/5
        [HttpGet("{id}")]
        public ActionResult<Menu> Get(string id)
        {
            try
            {
                var menu = _menusService.GetById(id);

                if (menu == null)
                {
                    // Nếu không tìm thấy menu với id cụ thể, trả về 404 Not Found
                    return NotFound($"{id} không được tìm thấy, vui lòng nhập lại");
                }

                // Nếu tìm thấy menu, trả về menu đó
                return menu;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu cần
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Lỗi trong quá trình xử lý yêu cầu");
            }
        }


        // POST api/<MenusController>
        [HttpPost]
        public ActionResult<Menu> Post([FromBody] Menu menu)
        {
            try
            {
                menu.Id = null;
                // Kiểm tra xem menu có phải là kiểu dữ liệu Menu không
                if (menu.GetType() != typeof(Menu))
                {
                    return BadRequest("Kiểu dữ liệu của menu không đúng");
                }

                // Tiếp tục thêm dữ liệu vào cơ sở dữ liệu
                _menusService.Create(menu);
                return CreatedAtAction(nameof(Get), new { id = menu.Id }, menu);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu cần
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT api/<MenusController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Menu menu)
        {
            try
            {
                var menu1 = _menusService.GetById(id);

                if (menu1 == null)
                {
                    // Nếu không tìm thấy menu với id cụ thể, trả về 404 Not Found
                    return NotFound($"{id} không được tìm thấy, vui lòng nhập lại");
                }

                // Kiểm tra xem menu có phải là kiểu dữ liệu Menu không
                if (menu.GetType() != typeof(Menu))
                {
                    return BadRequest("Kiểu dữ liệu của menu không đúng");
                }

                // Tiếp tục thêm dữ liệu vào cơ sở dữ liệu
                _menusService.Update(id, menu);
                return Ok("Update thành công");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE api/<MenusController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                var menu = _menusService.GetById(id);

                if (menu == null)
                {
                    // Nếu không tìm thấy menu với id cụ thể, trả về 404 Not Found
                    return NotFound($"{id} không được tìm thấy, vui lòng nhập lại");
                }

                _menusService.Remove(id);
                return Ok($"Menu with Id = {id} deleted!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
