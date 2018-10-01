using System.Collections.Generic;
using System.Threading.Tasks;
using BlogStarWars.Infrastructure.Data.Dapper.DapperConnection;
using Dapper;

namespace BlogStarWars.Infrastructure.Data.Dapper.Queries.ObterUltimosCincoPosts
{
    public class ObterUltimosCincoPostsQuery : QueryBase<ObterUltimosCincoPostsParameter, IEnumerable<ObterUltimosCincoPostsResult>>
    {
        public ObterUltimosCincoPostsQuery(IDapperConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public override async Task<IEnumerable<ObterUltimosCincoPostsResult>> HandleAsync(ObterUltimosCincoPostsParameter parameter)
        {
            const string query = @"
            SELECT 
                Titulo, 
                Descricao, 
                Conteudo, 
                QuantidadeLikes, 
                QuantidadeViews, 
                DataCriacao 
                FROM POST
                ORDER BY 
                datetime(DataCriacao) DESC, 
                QuantidadeLikes DESC 
                Limit 5";

            using (var connection = ConnectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<ObterUltimosCincoPostsResult>(query);
            }
        }
    }
}