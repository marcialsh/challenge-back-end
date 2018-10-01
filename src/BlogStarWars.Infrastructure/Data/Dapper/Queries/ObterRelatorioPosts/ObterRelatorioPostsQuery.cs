using System.Linq;
using System.Threading.Tasks;
using BlogStarWars.Infrastructure.Data.Dapper.DapperConnection;
using Dapper;

namespace BlogStarWars.Infrastructure.Data.Dapper.Queries.ObterRelatorioPosts
{
    public class ObterRelatorioPostsQuery : QueryBase<ObterRelatorioPostsParameter, ObterRelatorioPostsResult>
    {
        public ObterRelatorioPostsQuery(IDapperConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public override async Task<ObterRelatorioPostsResult> HandleAsync(ObterRelatorioPostsParameter parameter)
        {
            const string query = @"
            SELECT 
                QuantidadeLikes, 
                QuantidadeViews
                FROM POST";

            using (var connection = ConnectionFactory.CreateConnection())
            {
                var queryResult = await connection.QueryAsync(query);
                var queryResultList = queryResult.ToList();

                var quantidadeTotalLikes = queryResultList.Sum(q => (int)q.QuantidadeTotalLikes);
                var quantidadeTotalViews = queryResultList.Sum(q => (int)q.QuantidadeTotalViews);

                var resumoPontuacaoPosts = queryResultList.Select(q => 
                {
                    var porcentagemLikes = (((int)q.QuantidadeTotalLikes * 100) / quantidadeTotalLikes);
                    var porcentagemViews = (((int)q.QuantidadeTotalLikes * 100) / quantidadeTotalLikes);

                    return new ResumoPontuacaoPost
                    {
                        PorcentagemLikes = porcentagemLikes,
                        PorcentagemViews = porcentagemViews
                    };
                });

                var obterRelatorioPostsResult = new ObterRelatorioPostsResult
                {
                    QuantidadeTotalLikes = quantidadeTotalLikes,
                    QuantidadeTotalViews = quantidadeTotalViews,
                    ResumoPontuacaoPosts = resumoPontuacaoPosts
                };

                return obterRelatorioPostsResult;
            }
        }
    }
}