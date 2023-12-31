using Microsoft.EntityFrameworkCore;
using StockWallet.Db.Mysql;
using StockWallet.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//StockWallet.Utils.ServicesHelper.cs
builder.Services.RegisterRepositories();
builder.Services.RegisterServices();

string connectionString = builder.Configuration["ConnectionStrings:Default"] ?? "Server=localhost; User ID=root; Password=pass; Database=blog";
connectionString = connectionString.Replace("\"", string.Empty);
builder.Services.AddDbContext<StockWalletContext>(x => x.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

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