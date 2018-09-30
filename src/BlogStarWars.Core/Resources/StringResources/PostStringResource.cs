using System.Collections.Generic;

namespace BlogStarWars.Core.Resources.StringResourse {
    public static class PostStringResource {
        /*
        Poderia desenvolver toda uma infra para StringResourse?
        Poderia, mas seria realmente necessario? Acredito que não.
        */
        private static readonly IDictionary<string, string> _stringResources =
            new Dictionary<string, string> {
                {
                "TituloNull",
                "O post deve conter um título, no momento o mesmo está vazio!"
                },
                {
                "Titulo",
                "O tamanho do título está inválido, ultrapassou o limite de 100 caracteres!"
                },
                {
                "DescricaoNull",
                "O post deve conter uma descrição, no momento o mesmo está vazio!"
                },
                {
                "Descricao",
                "O tamanho da descrição está inválida, ultrapassou o limite de 350 caracteres!"
                },
                {
                "ConteudoNull",
                "O post deve conter conteudo, no momento o mesmo está vazio!"
                },
                {
                "Conteudo",
                "O tamanho do conteúdo está inválido, ultrapassou o limite de 50000 caracteres!"
                }
            };

        public static string Get (string propertyName) => _stringResources[propertyName];
    }
}