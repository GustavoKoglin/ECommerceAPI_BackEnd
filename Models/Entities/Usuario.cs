using Microsoft.AspNetCore.Identity;

namespace E_Commerce_API.Models.Entities
{
    public class Usuario : IdentityUser
    {
        public string? NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }

        public Carrinho? Carrinho { get; set; }
    }
}
