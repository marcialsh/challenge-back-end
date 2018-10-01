using System.Collections.Generic;

namespace BlogStarWars.Core.Resources.StringResources
{
    public static class ContabilizadorStringResource
    {
        private static readonly IDictionary<string, string> _stringResources =
            new Dictionary<string, string> {
                {
                "Pontuacao",
                "A pontuação não pode ser negativa!"
                }
            };
            
        public static string Get (string propertyName) => _stringResources[propertyName];
    }
}