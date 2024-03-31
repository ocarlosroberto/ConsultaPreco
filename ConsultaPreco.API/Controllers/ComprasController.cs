using ConsultaPreco.API.Data;
using ConsultaPreco.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsultaPreco.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ComprasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compra>>> GetCompras()
        {
            return await _context.Compras.Include(c => c.Produto).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Compra>> PostCompra(Compra compra)
        {
            // Verifica se o produto já existe na tabela de produtos
            var produto = await _context.Produtos.FindAsync(compra.CodigoBarras);

            // Se não existir, adiciona um novo produto com o código de barras e nome em branco
            if (produto == null)
            {
                produto = new Produto { CodigoBarras = compra.CodigoBarras, Nome = "" };
                _context.Produtos.Add(produto);
            }
            compra.Produto = produto;

            // Adiciona a compra
            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCompras), new { id = compra.Id }, compra);
        }
    }
}