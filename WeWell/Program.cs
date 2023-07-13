using DataAccess;
using Domain.AutoMapper;
using DataAccess.Repositories;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using WeWell.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));
builder.Services.AddAutoMapper(typeof(AppMappingDtoDalProfile));
builder.Services.AddAutoMapper(typeof(AppMappingDtoViewProfile));

builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<MeetingRepository>();
builder.Services.AddScoped<MeetingStatusRepository>();
builder.Services.AddScoped<MeetingTypeRepository>();
builder.Services.AddScoped<PlaceRepository>();
builder.Services.AddScoped<PreferenceRepository>();

// Add services to the container.
builder.Services.AddControllers();

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
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalhost");

app.UseAuthorization();

app.MapControllers();

app.Run();
