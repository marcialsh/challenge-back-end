using System.Collections.Generic;

namespace BlogStarWars.Infrastructure.Data.Dapper.Queries.ObterRelatorioPosts
{
    public class ObterRelatorioPostsResult
    {
        public IEnumerable<ResumoPontuacaoPost> ResumoPontuacaoPosts { get; set; }
        public int QuantidadeTotalLikes { get; set; }
        public int QuantidadeTotalViews { get; set; }
    }

    public class ResumoPontuacaoPost
    {
        public int PorcentagemLikes { get; set; }
        public int PorcentagemViews { get; set; }
    }
}