using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persons.API.Database;
using Persons.API.Extensions;
using Persons.API.Filters;
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

//Modificaion del 17 de marzo 
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ValidateModelAttribute));
});

//Que ya no queremos usar los modelos de defecto si no el que hemos creado 
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


builder.Services.AddTransient<IPersonsService, PersonsService>();//12/02
//La funcion es cuando no quiero en controller colocar mas datos de los necesarios 

//25 de febrero paises
builder.Services.AddTransient<ICountriesService, CountriesService>();

builder.Services.AddTransient<IStatisticsService, StatisticsService>();

builder.Services.AddCorsConfiguration(builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
