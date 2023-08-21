using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.OpenApi.Models;
using TodoApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ToDoDBContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenPolicy", policy =>
                          {
                              policy.WithOrigins("http://localhost:3000","https://todoserver-6h1q.onrender.com")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo API", Description = "Keep track of your tasks", Version = "v1" });
});
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("OpenPolicy");
app.MapGet("/", () => "Hello World!");

app.MapGet("/item/", async (ToDoDBContext context) => { return await context.Items.ToListAsync(); });

app.MapPost("/item/", async (Item item, ToDoDBContext context) =>
{
    context.Items.Add(item);
    await context.SaveChangesAsync();
    return item;
});
app.MapPut("/item/{id}/{iscom}", async (int id, bool isCom, ToDoDBContext context) =>
{
    var item = context.Items.Find(id);
    item.IsComplete = isCom;
    context.Items.Update(item);
    await context.SaveChangesAsync();
    return item;
});
app.MapDelete("/item/{id}", async (int id, ToDoDBContext context) =>
{
    var item = context.Items.Find(id);
    context.Items.Remove(item);
    await context.SaveChangesAsync();
});

app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.Run();
