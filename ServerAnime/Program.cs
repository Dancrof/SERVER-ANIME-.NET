using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ServerAnime;
using ServerAnime.Data.DataContext;
using ServerAnime.Data.Repositories;
using ServerAnime.Model;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<serveranimedbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MysqlConnection"), ServerVersion.Parse("8.0.30-mysql"))
);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IGenericRepository<Categorium>, CategoriaRepository>();
builder.Services.AddControllers().AddJsonOptions(x => { 
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    x.JsonSerializerOptions.WriteIndented = true;
    x.JsonSerializerOptions.Converters.Add(new CustomJsonConverterForType());
});

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
