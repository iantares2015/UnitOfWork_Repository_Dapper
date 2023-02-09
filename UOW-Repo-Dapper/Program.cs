using System.Data;
using Npgsql;
using UOW_Repo_Dapper.Repositories;
using UOW_Repo_Dapper.Repositories.Interfaces;
using UOW_Repo_Dapper.Repositories.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped((s) => new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IDbTransaction>(s =>
{
    NpgsqlConnection connection = s.GetRequiredService<NpgsqlConnection>();
    connection.Open();
    return connection.BeginTransaction();
});
// Add services to the container.

builder.Services.AddScoped<ITransferRepository, TransferRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitofWork, UnitOfWork>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UOW-Repo-Dapper v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();