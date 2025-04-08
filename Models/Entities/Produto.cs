namespace E_Commerce_API.Models.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        public ICollection<CarrinhoItem> CarrinhoItens { get; set; } = new List<CarrinhoItem>();
    }
}