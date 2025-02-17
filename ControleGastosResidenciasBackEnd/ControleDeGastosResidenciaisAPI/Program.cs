using ControleGastosResidenciaisAPI.Application;
using ControleGastosResidenciaisAPI.Domain.Models.PessoaModel;
using ControleGastosResidenciaisAPI.Domain.Models.TransacaoModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<PessoaFactory>();
builder.Services.AddSingleton<TransacaoFactory>();

builder.Services.AddScoped<PessoaAppService>();
builder.Services.AddScoped<TransacaoAppService>();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowLocalhost",
        policy => policy.WithOrigins("http://localhost:5173")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalhost");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
