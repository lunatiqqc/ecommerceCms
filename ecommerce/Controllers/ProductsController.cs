using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cms.ecommerce.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NSwag.Annotations;

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

        [HttpPost]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status201Created, typeof (Product))]
        public async Task<ActionResult<Product>> Post([BindRequired, FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _context.Products
                .ToListAsync();

            return Ok(products);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }
    }
}
