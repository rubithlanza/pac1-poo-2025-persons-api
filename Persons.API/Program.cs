using Microsoft.EntityFrameworkCore;
using Persons.API.Database;
using Persons.API.Helpers;
using Persons.API.Services;
using Persons.API.Services.Interfaces;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 10 de frebrero

builder.Services.AddDbContext<PersonsDbContext>(options => options.UseSqlite(builder.Configuration
    .GetConnectionString("DefaultConnection")));

//19 de febrero
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


builder.Services.AddTransient<IPersonsService, PersonsService>();//12/02
//La funcion es cuando no quiero en controller colocar mas datos de los necesarios 

//25 de febrero paises
builder.Services.AddTransient<ICountriesService, CountriesService>();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
