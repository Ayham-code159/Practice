using CRUDPractice.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/// set up 
var cs = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("connection string doesn't exist");

if(builder.Environment.IsDevelopment())
{
    if (!cs.Contains("Connection Timeout", StringComparison.OrdinalIgnoreCase))
        cs += ";Connection Timeout=3";

    if (!cs.Contains("Default Command Timeout", StringComparison.OrdinalIgnoreCase))
        cs += ";Default Command Timeout=5";
}

builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
{
    var ver = new MySqlServerVersion(new Version(8, 0, 36));
    options.UseMySql(cs, ver, mySql =>
    {
        if(builder.Environment.IsProduction())
            mySql.EnableRetryOnFailure(5,TimeSpan.FromSeconds(3),null);
        else
            mySql.EnableRetryOnFailure(1, TimeSpan.FromMilliseconds(200),null);

    });
});

/// fluent validation 

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly,includeInternalTypes: true);


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
