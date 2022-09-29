using Microsoft.EntityFrameworkCore;
using ServerAnime.Data.DataContext;
using ServerAnime.Data.Repositories;
using ServerAnime.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<serveranimedbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MysqlConnection"), ServerVersion.Parse("8.0.30-mysql"))
);
builder.Services.AddScoped<IGenericRepository<Categorium>, CategoriaRepository>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
