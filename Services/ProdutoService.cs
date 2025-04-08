using E_Commerce_API.Data;
using E_Commerce_API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API.Services
{
    public class ProdutoService
    {
        private readonly AppDbContext _context;

        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> ObterTodos()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto?> ObterPorId(int id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task<Produto> Adicionar(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> Atualizar(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                throw new ArgumentException("IDs não correspondem");
            }

            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task Remover(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
