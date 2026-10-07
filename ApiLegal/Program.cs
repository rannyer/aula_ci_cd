using ApiLegal.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/saude", () => Results.Ok(new { status = "ok", horario = DateTime.UtcNow}));

app.MapGet("/versao",() => Results.Ok(new 
{ 
    versao = Environment.GetEnvironmentVariable("APP_VERSAO") ?? "local",
}));

app.MapGet("/desconto", (decimal valorOriginal, string? cupom, DescontoService descontoService) =>
{
    try
    {
        var valorFinal = descontoService.CalcularDesconto(valorOriginal, cupom);
        return Results.Ok(new { 
            valorOiginal = valorOriginal,
            cupom = cupom,
            desconto = valorOriginal - valorFinal,
            valorFinal = valorFinal
        });
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});
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
