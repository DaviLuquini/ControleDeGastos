var builder = WebApplication.CreateBuilder(args);

// Adiciona apenas os controladores da API
builder.Services.AddControllers();

var app = builder.Build();

// Configura o pipeline de requisição HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

// Mapeia as rotas apenas para os controladores da API
app.MapControllers();

app.Run();
