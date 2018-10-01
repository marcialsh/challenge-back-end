namespace BlogStarWars.Infrastructure.Data.Dapper.Queries.ObterPost {
    public class ObterPostParameter {
        public ObterPostParameter (long id) {
            this.Id = id;

        }
        public long Id { get; set; }
    }
}