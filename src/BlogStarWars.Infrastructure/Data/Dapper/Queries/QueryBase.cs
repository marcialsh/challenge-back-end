using System.Threading.Tasks;
using BlogStarWars.Infrastructure.Data.Dapper.DapperConnection;

namespace BlogStarWars.Infrastructure.Data.Dapper.Queries
{
    public abstract class QueryBase<TParameter, TResult> where TResult : class
    {
        protected IDapperConnectionFactory ConnectionFactory;
        public abstract Task<TResult> Handle(TParameter parameter);
    }
}