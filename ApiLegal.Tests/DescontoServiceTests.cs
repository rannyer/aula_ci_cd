using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiLegal.Services;

namespace ApiLegal.Tests
{
    public class DescontoServiceTests
    {
        private readonly DescontoService _service = new();

        [Fact]
        public void SemCupom_NaoDaDesconto()
        {
            var resultado =  _service.CalcularDesconto(100, null );

            Assert.Equal(100m, resultado);
        }

        [Theory]
        [InlineData("ALUNO10", 100, 90)]
        [InlineData("aluno10", 100, 90)]
        [InlineData("     ALUNO10", 100, 90)]
        [InlineData("BLACKFRIDAY", 200, 140)]
        [InlineData("CUPOMFALSE", 100, 100)]
        public void CuponsConhecidos_AplicamDesconto(string cupom, int valorOriginal, int valorEsperado)
        {
            var resultado = _service.CalcularDesconto(valorOriginal, cupom);
            Assert.Equal(valorEsperado, resultado);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-50)]
        public void ValorOriginalNegativo_LancaArgumentException(int valorOriginal)
        {
            Assert.Throws<ArgumentException>(() => _service.CalcularDesconto(valorOriginal, "ALUNO10"));
        }
    }
}
