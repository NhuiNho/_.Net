using API_MongoDB.Models;
using API_MongoDB.Services.Categories;
using API_MongoDB.Services.Menus;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IMenusService _menusService;

        public CategoriesController(ICategoriesService categoriesService, IMenusService menusService)
        {
            _categoriesService = categoriesService;
            _menusService = menusService;
        }
        // GET: api/<MenusController>
        [HttpGet]
        public ActionResult<List<Category>> Get()
        {
            return _categoriesService.Get();
        }

        // GET api/<MenusController>/5
        [HttpGet("{id}")]
        public ActionResult<Category> Get(string id)
        {
            try
            {
                var category = _categoriesService.GetById(id);

                if (category == null)
                {
                    // Nếu không tìm thấy menu với id cụ thể, trả về 404 Not Found
                    return NotFound($"{id} không được tìm thấy, vui lòng nhập lại");
                }

                // Nếu tìm thấy menu, trả về menu đó
                return category;
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
        public ActionResult<Category> Post([FromBody] Category category)
        {
            try
            {
                category.Id = null;
                var menu = _menusService.GetById(category.Menu_Id);
                if(menu == null)
                {
                    return BadRequest("Menu_id không đúng");
                }
                // Kiểm tra xem menu có phải là kiểu dữ liệu Menu không
                if (category.GetType() != typeof(Category))
                {
                    return BadRequest("Kiểu dữ liệu của menu không đúng");
                }

                // Tiếp tục thêm dữ liệu vào cơ sở dữ liệu
                _categoriesService.Create(category);
                return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
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
        public ActionResult Put(string id, [FromBody] Category category)
        {
            try
            {
                var category1 = _categoriesService.GetById(id);
                var menu1 = _menusService.GetById(category.Menu_Id);

                if (category1 == null || menu1 == null)
                {
                    // Nếu không tìm thấy menu với id cụ thể, trả về 404 Not Found
                    return NotFound($"{id} không được tìm thấy, vui lòng nhập lại");
                }

                // Kiểm tra xem menu có phải là kiểu dữ liệu Menu không
                if (category1.GetType() != typeof(Category))
                {
                    return BadRequest("Kiểu dữ liệu của menu không đúng");
                }

                // Tiếp tục thêm dữ liệu vào cơ sở dữ liệu
                _categoriesService.Update(id, category);
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
                var category = _categoriesService.GetById(id);

                if (category == null)
                {
                    // Nếu không tìm thấy menu với id cụ thể, trả về 404 Not Found
                    return NotFound($"{id} không được tìm thấy, vui lòng nhập lại");
                }

                _categoriesService.Remove(id);
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
