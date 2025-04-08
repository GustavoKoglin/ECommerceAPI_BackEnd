using Microsoft.AspNetCore.Mvc;
using E_Commerce_API.Models.Entities;
using E_Commerce_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using E_Commerce_API.Data;

namespace E_Commerce_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarrinhoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Carrinho>> Get()
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var carrinho = await _context.Carrinhos
                .Include(c => c.Itens)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);

            return carrinho ?? new Carrinho { UsuarioId = usuarioId };
        }

        [HttpPost("adicionar/{produtoId}")]
        public async Task<ActionResult> AdicionarItem(int produtoId, int quantidade = 1)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var carrinho = await _context.Carrinhos
                .Include(c => c.Itens)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);

            if (carrinho == null)
            {
                carrinho = new Carrinho { UsuarioId = usuarioId };
                _context.Carrinhos.Add(carrinho);
            }

            var item = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == produtoId);
            if (item == null)
            {
                item = new CarrinhoItem
                {
                    ProdutoId = produtoId,
                    Quantidade = quantidade
                };
                carrinho.Itens.Add(item);
            }
            else
            {
                item.Quantidade += quantidade;
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
