using BlogStarWars.Core.Entities;
using BlogStarWars.Infrastructure.Data.EntityFramework.Mappings;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace BlogStarWars.Infrastructure.Data.EntityFramework.Context
{
    public class BlogStarWarsContext : DbContext
    {
        public BlogStarWarsContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new PostMapping(modelBuilder.Entity<Post>());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Post> Post {get; set;}
    }
}