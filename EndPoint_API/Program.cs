using Microsoft.EntityFrameworkCore;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:7239")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseContext>(option =>
    option.UseSqlServer(@"Data Source=localHost\DEVINSTANCE; Initial Catalog=NursingService; User Id=sa; Password=aA123456; Encrypt=false;")
);

builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
