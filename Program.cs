using contratosimples_api.Data;
using contratosimples_api.Repositories.Implementation;
using contratosimples_api.Repositories.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ContratoSimplesConnectionString"));
});

builder.Services.AddScoped<ICentroDeCustoRepository, CentroDeCustoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
	app.UseSwaggerUI( options =>
	{
		options.SwaggerEndpoint("/openapi/v1.json", "V1");
	});
}

app.UseHttpsRedirection();

app.UseCors(options =>
{
	options.AllowAnyHeader();
	options.AllowAnyOrigin();
	options.AllowAnyMethod();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
