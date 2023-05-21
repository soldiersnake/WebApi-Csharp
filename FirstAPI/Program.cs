using FirstAPI.Data;
using System.Data;
using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore; //importacion del paquete de data base en memoria

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connString = builder.Configuration.GetConnectionString("DefaultConnection"); // la conexcion a SQLServer, el string de defaul esta en appsettings.json
builder.Services.AddSingleton<IDbConnection>((sp) => new SqlConnection(connString)); //configuracion de conexcion (proceso estandart) sp=service provider

builder.Services.AddScoped<IBookRepository, BookRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<BooksDb>(opt => opt.UseInMemoryDatabase("Booklist")); //configuracion de base de datos en memoria, de esta manera tambien se agrega una instancia a la dependencia


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
