using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cms.ecommerce.Models;

namespace cms.ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ProductsController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products
                .ToListAsync();

            return Ok(products);
        }
    }
}
