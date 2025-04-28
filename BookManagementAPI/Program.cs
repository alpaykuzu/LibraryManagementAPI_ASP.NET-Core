using BookManagementAPI.Data;
using BookManagementAPI.Extensions;
using BookManagementAPI.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Connection String'i alýyoruz
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// DbContext'i servislere ekliyoruz
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(connectionString));

// AutoMapper ekle
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Servisleri ve Repository'leri ekliyoruz
builder.Services.AddRepositories();
builder.Services.AddServices();

// Controller'larý ekle
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Library API",
        Version = "v1",
        Description = "Kütüphane Yönetim Sistemi API",
        Contact = new OpenApiContact
        {
            Name = "Alpay Kuzu",
            Email = "alpaykuzu0@gmail.com"
        }
    });

    var xmlFile = Path.Combine(AppContext.BaseDirectory, "BookManagementAPI.xml");
    if (File.Exists(xmlFile))
    {
        c.IncludeXmlComments(xmlFile);
    }
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API v1");
        c.RoutePrefix = "swagger"; // Swagger için özel bir URL belirtin
    });
}

// HTTP'de kalmasý için HTTPS yönlendirmesini kaldýr
// app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Veritabanýný oluþtur
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<LibraryDbContext>();
        dbContext.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Veritabaný oluþturulurken hata oluþtu.");
    }
}

app.Run();