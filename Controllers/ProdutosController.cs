using Microsoft.AspNetCore.Mvc;
using E_Commerce_API.Data;
using E_Commerce_API.Models.Entities;
using E_Commerce_API.Services;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoService _produtoService;

        public ProdutosController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> Get()
        {
            return await _produtoService.ObterTodos();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            var produto = await _produtoService.ObterPorId(id);
            if (produto == null) return NotFound();
            return produto;
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Post(Produto produto)
        {
            var novoProduto = await _produtoService.Adicionar(produto);
            return CreatedAtAction(nameof(Get), new { id = novoProduto.Id }, novoProduto);
        }
    }
}