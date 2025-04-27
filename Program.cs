using contratosimples_api.Application.Core.Data;
using contratosimples_api.Application.Core.Repositories;
using contratosimples_api.Application.Management.Data;
using contratosimples_api.Application.Management.Middlewares;
using contratosimples_api.Application.Management.Models.Entities;
using contratosimples_api.Application.Management.Repositories;
using contratosimples_api.Application.Management.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ContratoSimplesDbContext>(options => {
	options.UseNpgsql(builder.Configuration.GetConnectionString("ContratoSimplesConnectionString"));
});

builder.Services.AddDbContext<AppManagementDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("AppManagementConnectionString"));
});

builder.Services.AddScoped<ContratoSimplesUnitOfWork>();
builder.Services.AddScoped<AppManagementUnitOfWork>();
builder.Services.AddScoped<TokenRepository>();
builder.Services.AddScoped<TenantService>();

builder.Services.AddIdentityCore<Usuario>()
	.AddRoles<IdentityRole>()
	.AddTokenProvider<DataProtectorTokenProvider<Usuario>>("ContratoSimples")
	.AddEntityFrameworkStores<AppManagementDbContext>()
	.AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 6;
	options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			AuthenticationType = "Jwt",
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
		};
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.UseSwaggerUI(options =>
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

app.UseAuthentication();
app.UseAuthorization();
app.UseTenantAuthorization();

app.MapControllers();

app.Run();
