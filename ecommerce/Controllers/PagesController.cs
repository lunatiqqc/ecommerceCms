using AutoMapper;
using Castle.Core.Resource;
using cms.Models;
using ecommerce.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
//using NSwag.Annotations;

namespace cms.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PagesController : ControllerBase
    {
        private readonly MyDbContext _context;

        private readonly IMapper _mapper;

        private readonly IConfiguration _configuration;

        public PagesController(MyDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        [Produces("application/json")]
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
        public async Task<ActionResult<Page>> Get(string url)
        {
            var pages = await _context.Pages.ToListAsync();
            var decodedUrl = Uri.UnescapeDataString(url);
            var page = pages.Find(p => p.Url == decodedUrl);
            if (page == null)
            {
                return NotFound();
            }
            return Ok(page);
        }
        [HttpGet]
        [Route("all")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Page>>> GetAll()
        {
            var pages = await _context.Pages.ToListAsync();
            var rootPages = pages.Where(page => page.ParentPage == null);
            return Ok(rootPages);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Page updatedPage)
        {
            //_context.Entry(updatedPage).State = EntityState.Detached;

            //_context.ChangeTracker.LazyLoadingEnabled = false;
            //var existingPage = await _context.Pages.FirstOrDefaultAsync(p => p.Id == id);
            //
            //if (existingPage == null) { return NotFound(); }

            _context.Update(updatedPage);
            //_context.Entry(existingPage).State = EntityState.Detached;
            //_context.Entry(existingPage.GridRows[0]).State = EntityState.Detached;
            //_mapper.Map(updatedPage, existingPage);
            //_context.Update(existingPage);
            //_context.Update(existingPage);


            // Update the GridRows collection
            //existingPage.GridRows.Find((gridRow) =>  gridRow.Id == id);
            //existingPage.GridRows.AddRange(updatedPage.GridRows);

            //foreach (var updatedGridRow in updatedPage.GridRows)
            //{
            //    var existingGridRow = existingPage.GridRows.FirstOrDefault(g => g.Id == updatedGridRow.Id);
            //
            //    if (existingGridRow != null)
            //    {
            //        _context.Entry(existingGridRow).CurrentValues.SetValues(updatedGridRow);
            //    }
            //    else
            //    {
            //        existingPage.GridRows.Add(updatedGridRow);
            //    }
            //}

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
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







