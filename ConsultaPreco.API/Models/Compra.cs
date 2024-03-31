namespace ConsultaPreco.API.Models
{
    public class Compra
    {
        public int Id { get; set; }
        public DateTime DataCompra { get; set; }
        public string LocalCompra { get; set; }
        public string CodigoBarras { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public int Quantidade { get; set; }
        public Produto? Produto { get; set; }
    }
}
