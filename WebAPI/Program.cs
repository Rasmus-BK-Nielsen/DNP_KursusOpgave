// Auto generated code start -------------------------------------------------------------------------------------------

using Application.DAOInterfaces;
using Application.Logic;
using Application.LogicInterfaces;
using FileData;
using FileData.DAOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Code I have added: **************************************************************************************************
builder.Services.AddScoped<FileContext>();
builder.Services.AddScoped<IUserDAO, UserFileDAO>();
builder.Services.AddScoped<IUserLogic, UserLogic>();

builder.Services.AddScoped<IPostDAO, PostFileDAO>();
builder.Services.AddScoped<IPostLogic, PostLogic>();
// End of code I have added ********************************************************************************************

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
// Auto generated code end ---------------------------------------------------------------------------------------------