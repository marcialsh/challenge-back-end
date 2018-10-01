using System.Collections.Generic;
using System.Threading.Tasks;
using BlogStarWars.Api.Models;
using BlogStarWars.Core.Entities;
using BlogStarWars.Core.ValueObjects;
using BlogStarWars.Infrastructure.Data.Dapper.DapperConnection;
using BlogStarWars.Infrastructure.Data.Dapper.Queries.ObterPost;
using BlogStarWars.Infrastructure.Data.Dapper.Queries.ObterRelatorioPosts;
using BlogStarWars.Infrastructure.Data.Dapper.Queries.ObterUltimosCincoPosts;
using BlogStarWars.Infrastructure.Data.EntityFramework.Context;
using Microsoft.AspNetCore.Mvc;

namespace BlogStarWars.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly BlogStarWarsContext _blogStarWarsDbContext;
        private readonly IDapperConnectionFactory _dapperConnectionFactory;

        public PostController(BlogStarWarsContext blogStarWarsDbContext, IDapperConnectionFactory dapperConnectionFactory)
        {
            _blogStarWarsDbContext = blogStarWarsDbContext;
            _dapperConnectionFactory = dapperConnectionFactory;
        }

        [HttpPost]
        public async Task Postar(PostModel postModel)
        {
            var post = new Post(postModel.Titulo, postModel.Descricao, postModel.Conteudo);
            
            await _blogStarWarsDbContext
                .Post
                .AddAsync(post);

            await _blogStarWarsDbContext.SaveChangesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ObterPostResult> Obter(long id)
        {
            var obterPostQuery = new ObterPostQuery(_dapperConnectionFactory);
            var obterPostParameter = new ObterPostParameter(id);
            
            return await obterPostQuery.HandleAsync(obterPostParameter);
        }

        [HttpGet("Relatorio")]
        public async Task<ObterRelatorioPostsResult> ObterRelatorio()
        {
            var obterRelatorioPostsQuery = new ObterRelatorioPostsQuery(_dapperConnectionFactory);
            var obterRelatorioPostsParameter = new ObterRelatorioPostsParameter();
            
            return await obterRelatorioPostsQuery.HandleAsync(obterRelatorioPostsParameter);
        }

        [HttpGet]
        public async Task<IEnumerable<ObterUltimosCincoPostsResult>> Obter()
        {
            var obterUltimosCincoPostsQuery = new ObterUltimosCincoPostsQuery(_dapperConnectionFactory);
            var obterUltimosCincoPostsParameter = new ObterUltimosCincoPostsParameter();
            
            return await obterUltimosCincoPostsQuery.HandleAsync(obterUltimosCincoPostsParameter);
        }

        [HttpPut("{id}/alterar")]
        public async Task Editar(long id, PostModel postModel)
        {
            var post = await _blogStarWarsDbContext.Post.FindAsync(id);

            var alteracaoPost = new AlteracaoPost(postModel.Titulo, post.Descricao, post.Conteudo);
            post.AlterarDados(alteracaoPost);

            await _blogStarWarsDbContext.SaveChangesAsync();
        }

        [HttpDelete("{id}/deletar")]
        public async Task Deletar(long id)
        {
            var post = await _blogStarWarsDbContext.Post.FindAsync(id);

            _blogStarWarsDbContext
                .Entry(post)
                .Property("EstaDeletado")
                .CurrentValue = 1;

            await _blogStarWarsDbContext.SaveChangesAsync();
        }

        [HttpPut("{id}/ToLike")]
        public async Task ToLike(long id)
        {
            var post = await _blogStarWarsDbContext.Post.FindAsync(id);
            post.ToLike();

            await _blogStarWarsDbContext.SaveChangesAsync();
        }

        [HttpPut("{id}/ToView")]
        public async Task ToView(long id)
        {
            var post = await _blogStarWarsDbContext.Post.FindAsync(id);
            post.ToView();

            await _blogStarWarsDbContext.SaveChangesAsync();
        }
    }
}