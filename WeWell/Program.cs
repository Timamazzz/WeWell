using DataAccess;
using DataAccess.Repositories;
using Domain.AutoMapper;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using WeWell.AutoMapper;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using OfficeOpenXml;
using WeWell.Services;

var builder = WebApplication.CreateBuilder(args);

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

//EF
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

//Mapping
builder.Services.AddAutoMapper(typeof(AppMappingDtoToDataAccessModelsProfile), typeof(AppMappingDtoToPresentationModelsProfile));


//Services
builder.Services.AddScoped<PreferenceService>();
builder.Services.AddScoped<MeetingTypeService>();
builder.Services.AddScoped<MeetingStatusService>();
builder.Services.AddScoped<MeetingService>();
builder.Services.AddScoped<PlaceService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<Random>();
builder.Services.AddScoped<SmsService>();

//Repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<MeetingRepository>();
builder.Services.AddScoped<MeetingTypeRepository>();
builder.Services.AddScoped<PlaceRepository>();
builder.Services.AddScoped<PreferenceRepository>();

// Add services to the container.
builder.Services.AddControllers();

//Http
builder.Services.AddHttpContextAccessor();
builder.Services.AddLogging(builder =>
{
    builder.AddConsole(); // Добавьте провайдеры логирования по вашему выбору
});

builder.Services.AddHostedService<MeetingArchiveService>();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeWell API", Version = "v1" });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeWell API v1");
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads")),
    RequestPath = "/uploads"
});

app.UseCors("AllowLocalhost");

app.UseAuthorization();

app.MapControllers();

app.Run();
