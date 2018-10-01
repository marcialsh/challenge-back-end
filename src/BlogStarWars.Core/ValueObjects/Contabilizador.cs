using BlogStarWars.Core.Resources.StringResources;
using Flunt.Notifications;
using Flunt.Validations;

namespace BlogStarWars.Core.ValueObjects
{
    public class Contabilizador : Notifiable
    {
        public Contabilizador(int pontuacao) 
        {
            AddNotifications(
                new Contract()
                .IsGreaterOrEqualsThan(
                    pontuacao, 
                    0, 
                    nameof(Pontuacao), 
                    ContabilizadorStringResource.Get(nameof(Pontuacao))));
            
            Pontuacao = pontuacao;
        }

        public int Pontuacao { get; private set; }

        public void Contabilizar() => Pontuacao++;
    }
}