
using desafio_dotnet.Contexto;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var conexao = Environment.GetEnvironmentVariable("DATABASE_DESAFIO");
if(conexao is null) conexao = "Server=localhost;Database=database_desafio;Uid=root;";

builder.Services.AddDbContext<DbContexto>(options =>
    {
        options.UseMySql(conexao, ServerVersion.AutoDetect(conexao));
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
