using System.Linq;
using Microsoft.AspNetCore.Mvc;

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
                public IActionResult GetAll()
                {
                        var products = _context.Products.ToList();
                        return Ok(products);
                }
        }
}
