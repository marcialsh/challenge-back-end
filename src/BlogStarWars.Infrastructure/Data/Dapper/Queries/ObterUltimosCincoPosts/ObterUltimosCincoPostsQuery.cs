using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace BlogStarWars.Infrastructure.Data.Dapper.Queries.ObterUltimosCincoPosts
{
    public class ObterUltimosCincoPostsQuery : QueryBase<ObterUltimosCincoPostsParameter, IEnumerable<ObterUltimosCincoPostsResult>>
    {
        public override async Task<IEnumerable<ObterUltimosCincoPostsResult>> Handle(ObterUltimosCincoPostsParameter parameter)
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