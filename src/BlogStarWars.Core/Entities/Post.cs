using System;
using BlogStarWars.Core.Resources.StringResources;
using BlogStarWars.Core.ValueObjects;
using Flunt.Notifications;
using Flunt.Validations;

namespace BlogStarWars.Core.Entities {
    public class Post : Notifiable
    {
        //Construtor usado para criar um post a primeira vez.
        public Post (
            string titulo,
            string descricao,
            string conteudo) 
        {
            AddNotifications(
                new Contract()
                    .HasMaxLen(
                        titulo, 
                        100, 
                        nameof(Titulo), 
                        PostStringResource.Get(nameof(Titulo)))
                    .HasMaxLen(
                        descricao, 
                        350, 
                        nameof(Descricao), 
                        PostStringResource.Get(nameof(Descricao)))
                    .HasMaxLen(
                        conteudo, 
                        50000, 
                        nameof(Conteudo), 
                        PostStringResource.Get(nameof(Conteudo))));

            Titulo = titulo;
            Descricao = descricao;
            Conteudo = conteudo;
            _like = new Like (0);
            _view = new View (0);
        }

        //Construtor utilizado para quando o post já existe.
        public Post(
            string titulo, 
            string descricao, 
            string conteudo, 
            int quantidadeLikes, 
            int quantidadeViews)
        {
            Titulo = titulo;
            Descricao = descricao;
            Conteudo = conteudo;
            _like = new Like (quantidadeLikes);
            _view = new View (quantidadeViews);
        }
        
        //Construtor
        public long Id { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Conteudo { get; private set; }
        public int QuantidadeLikes => _like.Pontuacao;
        public int QuantidadeViews => _view.Pontuacao;
        private View _view;
        private Like _like;

        public void AlterarDados(AlteracaoPost alteracaoPost)
        {
            AddNotifications(
                new Contract()
                    .IsNotNull(
                        alteracaoPost, 
                        nameof(alteracaoPost), 
                        ""));
            
            /*
            Conforme a classe fosse crescendo ou em classe maiores, 
            realmente, tornaria algo inviavel, 
            tendo buscando de buscar outras estrategias.
            */
            if(Titulo != alteracaoPost.Titulo)
                Titulo = alteracaoPost.Titulo;
            if(Descricao != alteracaoPost.Descricao)
                Descricao = alteracaoPost.Descricao;
            if(Conteudo != alteracaoPost.Conteudo)
                Conteudo = alteracaoPost.Conteudo;
        }

        public void ToLike() => _like.Contabilizar();
        public void ToView() => _view.Contabilizar();
    }
}