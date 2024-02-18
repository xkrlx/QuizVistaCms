using QuizVistaApi.Middlewares;
using QuizVistaApiInfrastructureLayer.Extensions;
using QuizVistaApiBusinnesLayer.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using AngleSharp;
using QuizVistaApiBusinnesLayer.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.ConfigureDatabaseConnection(configuration);
builder.Services.AddRepositories();

builder.Services.AddServices();
builder.Services.AddHealthChecks();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

var myOrigins = "_myOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(myOrigins,
        builder =>
        {
            builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
        });
});

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("AppSettings:Token").Value!))
    };
});

builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddTransient<AntiXssMiddleware>();

builder.Services.AddTransient<IMailService, MailService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<AntiXssMiddleware>();

//app.UseCors("ui-cors");


app.UseHttpsRedirection();

app.UseCors(myOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health-check");

app.Run();
