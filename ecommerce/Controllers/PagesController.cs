using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cms.Models;
using NSwag.Annotations;
using Microsoft.AspNetCore.Components.Web;
using System.Runtime.InteropServices;

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

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        public async Task<ActionResult<Page>> Post(string title, int? parentId)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("Title is required.");
            }


            var newPage = new Page
            {
                Title = title,
                VisibleInMenu = false,
                Url = title
            };

            if (parentId != null)
            {
                var parentPage = await _context.Pages.FindAsync(parentId);

                // Update the parent page's children collection

                if (
                    parentPage != null)
                {
                    newPage.ParentPage = parentPage;
                    parentPage.Children ??= new List<Page>();
                    parentPage.Children.Add(newPage);
                }

            }

            _context.Pages.Add(newPage);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = newPage.Id }, newPage);
        }


        [HttpGet]
        [Produces("application/json")]
        [SwaggerResponse(typeof(IEnumerable<Page>))]
        [SwaggerResponse(typeof(Page))]
        public async Task<ActionResult<IEnumerable<Page>>> Get(string? url)
        {
            var pages = await _context.Pages.ToListAsync();

            if (url != null)
            {
                var decodedUrl = Uri.UnescapeDataString(url);
                var page = pages.Find(p => p.Url == decodedUrl);
                if (page == null)
                {
                    return NotFound();
                }

                return Ok(page);
            }

            var rootPages = pages.Where(page => page.ParentPage == null).ToList();
            return Ok(rootPages);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                return NotFound("Page not found.");
            }


            // Remove the page from its parent's children collection, if it has a parent
            if (page.ParentPage != null)
            {
                page.ParentPage.Children?.Remove(page);
            }

            await DeletePageAndChildren(page);

            _context.Pages.Remove(page);
            await _context.SaveChangesAsync();

            return NoContent();

            async Task DeletePageAndChildren(Page page)
            {
                if (page.Children != null && page.Children.Any())
                {
                    foreach (var childPage in page.Children.ToList())
                    {
                        await DeletePageAndChildren(childPage);
                    }
                }

                _context.Pages.Remove(page);
            }
        }
    }
}







