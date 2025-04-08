using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_API.Models.Entities
{
    public class CarrinhoItem
    {
        [Key]
        public int Id { get; set; }

        public int CarrinhoId { get; set; }

        [ForeignKey("Produto")]
        public int ProdutoId { get; set; }
        public Carrinho? Carrinho { get; set; }

        public Produto? Produto { get; set; }

        public int Quantidade { get; set; }
    }
}
