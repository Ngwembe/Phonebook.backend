using Absa.Phonebook.Contactservice.DAL;
using Absa.Phonebook.Contactservice.Handlers;
using Absa.Phonebook.Contactservice.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Reflection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


//  Uncomment To apply ApiConventions on Assembly level
//[assembly: ApiConventionType(typeof(DefaultApiConventions))];

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PhonebookContext>(options =>
    options.UseSqlServer(connectionString));

//builder.Services.AddDbContext<PhonebookContext>(options => { 
//    options.UseSqlServer("")
//});


builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IContactHandler, ContactHandler>();
builder.Services.AddScoped<IPersonHandler, PersonHandler>();

builder.Services.AddControllers(options =>
{
    //  Globally adds InternalServerError as potential response of all controllers
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));

    //  Ensures the consumer does not specify unsupported type and the API still return back json [application/json]
    options.ReturnHttpNotAcceptable = true;

    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
    //options.OutputFormatters.Add(new SystemTextJsonOutputFormatter(new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = false}));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFileFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    options.IncludeXmlComments(xmlCommentsFileFullPath);
});

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
