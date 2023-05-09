using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cms.Models;

namespace cms.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PagesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public PagesController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Page>>> GetPages()
        {
            var pages = await _context.Pages.ToListAsync();

            var rootPages = pages.Where(p => p.Parent == null);

            return Ok(rootPages);
        }
    }
}







