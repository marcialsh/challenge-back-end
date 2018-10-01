using BlogStarWars.Core.Resources.StringResources;
using Flunt.Notifications;
using Flunt.Validations;

namespace BlogStarWars.Core.ValueObjects
{
    public class Like : Contabilizador
    {
        public Like(int pontuacao) : base(pontuacao)
        {
        }
    }
}