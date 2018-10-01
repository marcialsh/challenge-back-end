using BlogStarWars.Core.ValueObjects;
using FluentAssertions;
using Xunit;

namespace tests.BlogStarWars.Core.ValueObjects
{
    public class ContabilizadorTest
    {
        [Theory] 
        [InlineData(0)]
        [InlineData(19)]
        [InlineData(304)]
        public void Deve_Criar_Um_Like_Em_Um_Estado_Valido(int pontuacaoInicial)
        {
            //Arrange
            //Act
            var contabilizador = new Contabilizador(pontuacaoInicial);

            //Assert
            contabilizador
                .Valid
                .Should()
                .BeTrue();

            contabilizador
                .Pontuacao
                .Should()
                .BeGreaterOrEqualTo(0);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(19)]
        [InlineData(304)]
        public void Deve_Contabilizar_Um_Like_A_Cada_Vez(int pontuacaoInicial)
        {
            //Arrange
            var contabilizador = new Contabilizador(pontuacaoInicial);
            var pontuacaoContabilizada = (pontuacaoInicial + 1);

            //Act
            contabilizador.Contabilizar();

            //Assert
            contabilizador
                .Pontuacao
                .Should()
                .BeGreaterThan(pontuacaoInicial);
            contabilizador
                .Pontuacao
                .Should()
                .Be(pontuacaoContabilizada);
        }
    }
}