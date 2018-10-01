using System;

namespace BlogStarWars.Infrastructure.Data.Dapper.Queries.ObterUltimosCincoPosts
{
    public class ObterUltimosCincoPostsResult
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Conteudo { get; set; }
        public int QuantidadeLikes { get; set; }
        public int QuantidadeViews { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}