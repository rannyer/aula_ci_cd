namespace ApiLegal.Services
{
    public class DescontoService
    {
        public decimal CalcularDesconto(decimal valorOriginal, string? cupom)
        {
           if (valorOriginal <= 0)
            {
                throw new ArgumentException("O valorr original não pode ser negativo.");
            }
            decimal desconto = 0;
            decimal percentual = cupom?.Trim().ToUpper() switch
            {
                "ALUNO10" => 0.10m,
                "BLACKFRIDAY" => 0.032m,
                _ => 0
            };
            return Math.Round(valorOriginal * (1 - percentual), 2);
        }
    }
}
