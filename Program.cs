using Microsoft.EntityFrameworkCore;
using myAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<myAPIDBContext>();

builder.Services.AddSwaggerGen();
var app = builder.Build();

if (builder.Environment.IsDevelopment()){
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();