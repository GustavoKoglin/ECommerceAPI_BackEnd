using System.Collections.Generic;

namespace E_Commerce_API.Models.Entities
{
    public class Carrinho
    {
        public int Id { get; set; }
        public string? UsuarioId { get; set; }  // Relaciona com IdentityUser (Usuario.Id)
        public List<CarrinhoItem> Itens { get; set; } = new();
    }
}
