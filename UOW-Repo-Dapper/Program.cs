using UOW_Repo_Dapper.Models;
using UOW_Repo_Dapper.Repositories.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);


// UnitOfWork
builder.Services.AddScoped<IUnitOfWork>(_ => new UnitOfWork(builder.Configuration.GetConnectionString("DefaultConnection")));

// IOptionsMonitor
builder.Services.Configure<Numb>(builder.Configuration.GetSection(nameof(Numb)));


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