using AdBoard.Host.Api;
using AdBoard.Infrastructure.MapProfiles;
using AutoMapper;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));

// Add DbContext
builder.Services.AddDbContextWithConfigurations();

// Add repository
builder.Services.AddRepositories();

// Add Services
builder.Services.AddServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo() { Title = "AdBoard API", Version = "v1"});
    options.IncludeXmlComments(Path.Combine(Path.Combine(AppContext.BaseDirectory, "Documentation.xml")));
    options.IncludeXmlComments(Path.Combine(Path.Combine(AppContext.BaseDirectory, "Contracts.xml")));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHsts();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static MapperConfiguration GetMapperConfiguration()
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<AdvertProfile>();
        cfg.AddProfile<CategoryProfile>();
        cfg.AddProfile<UserProfile>();
        cfg.AddProfile<ImageProfile>();
    });
    config.AssertConfigurationIsValid();
    return config;
}