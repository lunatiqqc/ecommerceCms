using AutoMapper;
using Castle.Core.Resource;
using cms.Models;
using ecommerce.Helpers;
using ecommerce.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
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

	    var options = new JsonSerializerOptions();

	    options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
	    options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

	    var serialized = System.Text.Json.JsonSerializer.Serialize(page, options);

	    var filteredSerialized = serialized;

	    // Remove null value items from arrays
	    ////filteredSerialized = Regex.Replace(filteredSerialized, @",\s*null\s*", string.Empty);
	    ////filteredSerialized = Regex.Replace(filteredSerialized, @"(?<=\[)\s*null\s*,", string.Empty);
	    ////filteredSerialized = Regex.Replace(filteredSerialized, @"\[\s*null\s*\]", "[]");

	    return new ContentResult
	    {
		Content = filteredSerialized,
		ContentType = "application/json",
		//StatusCode = 200
	    };

        }
        [HttpGet]
        [Route("all")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Page>>> GetAll()
        {
            var pages = await _context.Pages.ToListAsync();
            var rootPages = pages.Where(page => page.ParentPage == null);

            var options = new JsonSerializerOptions();

	    options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
	    options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
	    options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;


	    var serialized = System.Text.Json.JsonSerializer.Serialize(rootPages, options);

	    var filteredSerialized = serialized;

	    // Remove null value items from arrays
	    filteredSerialized = Regex.Replace(filteredSerialized, @",\s*null\s*", string.Empty);
	    filteredSerialized = Regex.Replace(filteredSerialized, @"(?<=\[)\s*null\s*,", string.Empty);
	    filteredSerialized = Regex.Replace(filteredSerialized, @"\[\s*null\s*\]", "[]");


	    return new ContentResult
            {
                Content = filteredSerialized,
		ContentType = "application/json",
		//StatusCode = 200
	    };
            //return Ok(rootPages);
	}

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Page updatedPage)
        {

	    _context.Update(updatedPage);

            _context.Entry(updatedPage.GridRows[0].Columns[0].GridRows[0]).State = EntityState.Detached;

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







public class CustomJsonResult : ActionResult
{
    private readonly object _data;
    private readonly bool _useNewtonsoftJson;

    public CustomJsonResult(object data, bool useNewtonsoftJson = false)
    {
	_data = data;
	_useNewtonsoftJson = useNewtonsoftJson;
    }

    public override async Task ExecuteResultAsync(ActionContext context)
    {
	var response = context.HttpContext.Response;

	if (_useNewtonsoftJson)
	{
	    response.ContentType = "application/json";
	    var serializedData = JsonConvert.SerializeObject(_data);
	    await response.WriteAsync(serializedData);
	}
	else
	{
	    response.ContentType = "application/json";
            var options = new JsonSerializerOptions
            {
               
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
		    ReferenceHandler = ReferenceHandler.IgnoreCycles


	    // Add other options as needed
	};
	    var serializedData = System.Text.Json.JsonSerializer.Serialize(_data, options);
	    await response.WriteAsync(serializedData);
	}
    }
}