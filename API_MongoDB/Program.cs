using API_MongoDB.Models.Database;
using API_MongoDB.Services.Categories;
using API_MongoDB.Services.Menus;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<TCHStoreDatabaseSettings>(
    builder.Configuration.GetSection("TCHStoreDatabaseSettings"));

builder.Services.AddSingleton<ITCHStoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<TCHStoreDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration.GetValue<string>("TCHStoreDatabaseSettings:ConnectionString")));
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IMenusService, MenusService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
