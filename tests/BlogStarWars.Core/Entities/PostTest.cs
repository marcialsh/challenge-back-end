using System;
using System.Linq;
using BlogStarWars.Core.Entities;
using BlogStarWars.Core.ValueObjects;
using FluentAssertions;
using Flunt.Notifications;
using Xunit;

namespace tests.BlogStarWars.Core.Entities {
    public class PostTest {
        [Fact]
        public void Deve_Criar_Um_Post_Em_Um_Estado_Valido () {
            //Arrange
            var payloadTitulo = new char[100];
            var titulo = new string (payloadTitulo, 0, 100);

            var payloadDescricao = new char[350];
            var descricao = new string (payloadDescricao, 0, 350);

            var payloadConteudo = new char[50000];
            var conteudo = new string (payloadConteudo, 0, 5000);

            //Act
            var post = new Post (titulo, descricao, conteudo);

            //Assert
            //Valida a criação do objeto.
            post
                .Invalid
                .Should ()
                .BeFalse ();

            post
                .Should ()
                .NotBeNull ();

            post
                .Should ()
                .BeOfType (typeof (Post));

            //Valida o estado das propriedades.
            post
                .Titulo
                .Should()
                .NotBeNull();
            post
                .Titulo
                .Should ()
                .Be (titulo);
            
            post
                .Descricao
                .Should()
                .NotBeNull();
            post
                .Descricao
                .Should ()
                .Be (descricao);

            post
                .Conteudo
                .Should()
                .NotBeNull();
            post
                .Conteudo
                .Should ()
                .Be (conteudo);
        }

        [Theory]
        [InlineData (150)]
        [InlineData (5000)]
        [InlineData (18123)]
        public void Deve_Ser_Invalido_Informar_Um_Titulo_Com_Comprimento_Maior_Que_Cem_Caracteres (int comprimentoTitulo) {
            //Arrange
            var payloadTitulo = new char[comprimentoTitulo];
            var titulo = new string (payloadTitulo, 0, comprimentoTitulo);

            var payloadDescricao = new char[350];
            var descricao = new string (payloadDescricao, 0, 350);

            var payloadConteudo = new char[50000];
            var conteudo = new string (payloadConteudo, 0, 5000);

            //Act
            var post = new Post (titulo, descricao, conteudo);

            //Assert
            post
                .Invalid
                .Should ()
                .BeTrue ();

            var notificationTitulo =
                new Notification (
                    "Titulo",
                    "O tamanho do titulo esta invalido, ultrapassou o limite de 100 caracteres!");
            post
                .Notifications
                .Select (n =>
                    n
                    .Should ()
                    .Be (notificationTitulo));
        }

        [Theory]
        [InlineData (679)]
        [InlineData (8912)]
        [InlineData (12311)]
        public void Deve_Ser_Invalido_Informar_A_Descricao_Com_Comprimento_Maior_Que_Trezentos_E_Cinquenta_Caracteres (int comprimentoDescricao) {
            //Arrange
            var payloadTitulo = new char[100];
            var titulo = new string (payloadTitulo, 0, 100);

            var payloadDescricao = new char[comprimentoDescricao];
            var descricao = new string (payloadDescricao, 0, comprimentoDescricao);

            var payloadConteudo = new char[50000];
            var conteudo = new string (payloadConteudo, 0, 5000);

            //Act
            var post = new Post (titulo, descricao, conteudo);

            //Assert
            post
                .Invalid
                .Should ()
                .BeTrue ();

            var notificationDescricao =
                new Notification (
                    "Descricao",
                    "O tamanho da descricao esta invalido, ultrapassou o limite de 350 caracteres!");

            post
                .Notifications
                .Select (n =>
                    n
                    .Should ()
                    .Be (notificationDescricao));
        }

        [Theory]
        [InlineData (505050505)]
        [InlineData (93939393)]
        [InlineData (11919199)]
        public void Deve_Ser_Invalido_Informar_O_Conteudo_Com_Comprimento_Maior_Que_Cinquenta_Mil_Caracteres (int comprimentoConteudo) {
            //Arrange
            var payloadTitulo = new char[100];
            var titulo = new string (payloadTitulo, 0, 100);

            var payloadDescricao = new char[350];
            var descricao = new string (payloadDescricao, 0, 350);

            var payloadConteudo = new char[comprimentoConteudo];
            var conteudo = new string (payloadConteudo, 0, comprimentoConteudo);

            //Act
            var post = new Post (titulo, descricao, conteudo);

            //Assert
            post
                .Invalid
                .Should ()
                .BeTrue ();

            var notificacaoConteudo =
                new Notification (
                    "Conteudo",
                    "O tamanho do conteudo esta invalido, ultrapassou o limite de 50000 caracteres!");

            post
                .Notifications
                .Select (n =>
                    n
                    .Should ()
                    .Be (notificacaoConteudo));
        }

        [Fact]
        public void Deve_Ser_Alterar_Os_Dados_De_Um_Post_Quando_Houver_Modificacao_Nos_Mesmos()
        {
            //Arrange
            //TODO: Futuramente desenvolver um Builder para Post
            var titulo = "What is Lorem Ipsum?";
            var descricao = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.";
            var conteudo = "It is a long established fact that a reader will be distracted by the readable content.";
            
            var post = new Post (titulo, descricao, conteudo);

            //Construindo a estrutura para alteração do post.
            var tituloAlteracao = "What is Gilbertagem Alada?";
            var descricaoAlteracao = "Gilbertagem Alada heheheheahahhah ehehehehahahah";
            var conteudoAlteracao = "blablablablablablablablablablablablablablabla";

            var alteracaoPost = new AlteracaoPost(tituloAlteracao, descricaoAlteracao, conteudoAlteracao);

            //Act
            post.AlterarDados(alteracaoPost);

            //Assert
            post
                .Invalid
                .Should ()
                .BeFalse ();

            //Valida o estado das propriedades.
            post
                .Titulo
                .Should()
                .NotBeNull();
            post
                .Titulo
                .Should ()
                .Be (tituloAlteracao);

            post
                .Descricao
                .Should()
                .NotBeNull();
            post
                .Descricao
                .Should ()
                .Be (descricaoAlteracao);
            
            post
                .Conteudo
                .Should()
                .NotBeNull();
            post
                .Conteudo
                .Should ()
                .Be (conteudoAlteracao);
        }

        [Fact]
        public void Deve_Manter_Inalterado_Os_Dados_De_Um_Post_Quando_Nao_Houver_Modificacao_Nos_Mesmos()
        {
            //Arrange
            //TODO: Futuramente desenvolver um Builder para Post
            var titulo = "What is Lorem Ipsum?";
            var descricao = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.";
            var conteudo = "It is a long established fact that a reader will be distracted by the readable content.";
            
            var post = new Post (titulo, descricao, conteudo);

            //Construindo a estrutura para alteração do post.
            var tituloAlteracao = "What is Lorem Ipsum?";
            var descricaoAlteracao = "Gilbertagem Alada heheheheahahhah ehehehehahahah";
            var conteudoAlteracao = "blablablablablablablablablablablablablablabla";
            
            var alteracaoPost = new AlteracaoPost(tituloAlteracao, descricaoAlteracao, conteudoAlteracao);

            //Act
            post.AlterarDados(alteracaoPost);

            //Assert
            post
                .Invalid
                .Should ()
                .BeFalse ();

            //Valida o estado das propriedades.
            post
                .Titulo
                .Should()
                .NotBeNull();
            post
                .Titulo
                .Should ()
                .Be (titulo);

            post
                .Descricao
                .Should()
                .NotBeNull();
            post
                .Descricao
                .Should ()
                .Be (descricaoAlteracao);
            
            post
                .Conteudo
                .Should()
                .NotBeNull();
            post
                .Conteudo
                .Should ()
                .Be (conteudoAlteracao);
        }

        [Theory]
        [InlineData(131)]
        [InlineData(212)]
        [InlineData(111)]
        public void Deve_Contabilizar_Um_Ponto_Por_Like(int quantidadeLikes)
        {
            //Arrange
            var payloadTitulo = new char[100];
            var titulo = new string (payloadTitulo, 0, 100);

            var payloadDescricao = new char[350];
            var descricao = new string (payloadDescricao, 0, 350);

            var payloadConteudo = new char[50000];
            var conteudo = new string (payloadConteudo, 0, 5000);

            var post = new Post (titulo, descricao, conteudo, quantidadeLikes, 0);
            var quantidadeLikesContabilizado = (quantidadeLikes + 1);
            
            //Act
            post.ToLike();

            //Assert
            post
                .QuantidadeLikes
                .Should()
                .NotBe(quantidadeLikes);
            post
                .QuantidadeLikes
                .Should()
                .Be(quantidadeLikesContabilizado);
        }

        [Theory]
        [InlineData(131)]
        [InlineData(212)]
        [InlineData(111)]
        public void Deve_Contabilizar_Um_Ponto_Por_View(int quantidadeViews)
        {
            //Arrange
            var payloadTitulo = new char[100];
            var titulo = new string (payloadTitulo, 0, 100);

            var payloadDescricao = new char[350];
            var descricao = new string (payloadDescricao, 0, 350);

            var payloadConteudo = new char[50000];
            var conteudo = new string (payloadConteudo, 0, 5000);

            var post = new Post (titulo, descricao, conteudo, 0, quantidadeViews);
            var quantidadeViewsContabilizado = (quantidadeViews + 1);
            
            //Act
            post.ToView();

            //Assert
            post
                .QuantidadeViews
                .Should()
                .NotBe(quantidadeViews);
            post
                .QuantidadeViews
                .Should()
                .Be(quantidadeViewsContabilizado);
        }
    }
}