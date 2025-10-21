using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories.CategoryRepositories;
using server.Repositories.CategoryRepositories.Interfaces;
using server.Repositories.TransactionRepositories;
using server.Repositories.TransactionRepositories.Interfaces;
using server.Repositories.UserRepositories;
using server.Repositories.UserRepositories.Interfaces;
using server.Services.CategoryServices;
using server.Services.CategoryServices.Interfaces;
using server.Services.TransactionServices;
using server.Services.TransactionServices.Interface;
using server.Services.UserServices;
using server.Services.UserServices.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");  

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => 
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; 
    options.JsonSerializerOptions.WriteIndented = true; 
});
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>(); 
builder.Services.AddScoped<IUserRepository, UserRepository>(); 
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();   
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(); 
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString)); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.MapGet("/", () => "Hello from minimal API");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
