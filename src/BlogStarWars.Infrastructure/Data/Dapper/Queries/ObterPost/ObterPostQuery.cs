using System.Threading.Tasks;
using BlogStarWars.Infrastructure.Data.Dapper.DapperConnection;
using Dapper;

namespace BlogStarWars.Infrastructure.Data.Dapper.Queries.ObterPost
{
    public class ObterPostQuery : QueryBase<ObterPostParameter, ObterPostResult>
    {
        public ObterPostQuery(IDapperConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public override async Task<ObterPostResult> HandleAsync(ObterPostParameter parameter)
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
                WHERE Id = @Id";

            var parameters = new {Id = parameter.Id};
            
            using (var connection = ConnectionFactory.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync(query, parameters);
            }
        }
    }
}