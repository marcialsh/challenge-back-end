using BlogStarWars.Core.Resources.StringResourse;
using Flunt.Notifications;
using Flunt.Validations;

namespace BlogStarWars.Core.ValueObjects
{
    public class AlteracaoPost : Notifiable
    {
        public AlteracaoPost(
            string titulo, 
            string descricao, 
            string conteudo)
        {
            AddNotifications(
                new Contract()
                    .IsNotNullOrEmpty(
                        titulo,
                        "TituloNull",
                        PostStringResource.Get("TituloNull"))
                    .HasMaxLen(
                        titulo, 
                        100, 
                        nameof(Titulo), 
                        PostStringResource.Get(nameof(Titulo)))
                    .IsNotNullOrEmpty(
                        descricao,
                        "DescricaoNull",
                        PostStringResource.Get("DescricaoNull"))
                    .HasMaxLen(
                        descricao, 
                        350, 
                        nameof(Descricao), 
                        PostStringResource.Get(nameof(Descricao)))
                    .IsNotNullOrEmpty(
                        conteudo,
                        "ConteudoNull",
                        PostStringResource.Get("ConteudoNull"))
                    .HasMaxLen(
                        conteudo, 
                        50000, 
                        nameof(Conteudo), 
                        PostStringResource.Get(nameof(Conteudo))));
            
            Titulo = titulo;
            Descricao = descricao;
            Conteudo = conteudo;
        }

        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Conteudo { get; private set; }
    }
}