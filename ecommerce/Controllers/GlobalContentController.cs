using cms.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace cms.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class GlobalContentController : ControllerBase
    {
	private readonly MyDbContext _context;

	public GlobalContentController(MyDbContext context)
	{
	    _context = context;
	}

	[HttpGet("header")]
	public async Task<ActionResult<GlobalHeader>> GetHeader()
	{
	    var globalHeader = await _context.GlobalHeaders.FirstOrDefaultAsync();

	    if (globalHeader != null)
	    {
		return Ok(globalHeader);
	    }



	    return NoContent();
	    
	}

	[HttpGet("footer")]
	public async Task<ActionResult<GlobalHeader>> GetFooter()
	{
	    var globalFooter = await _context.GlobalFooters.FirstOrDefaultAsync();

	    if (globalFooter != null)
	    {
		return Ok(globalFooter);
	    }


	    return NoContent();

	}

	[HttpPut("header")]
	public async Task<ActionResult> UpdateHeader([FromBody] GlobalHeader updatedHeader)
	{

	    _context.Update(updatedHeader);
	    await _context.SaveChangesAsync();

	    return Ok();



	}

	[HttpPut("footer")]
	public async Task<ActionResult> UpdateFooter([FromBody] GlobalFooter updatedFooter)
	{



	    _context.Update(updatedFooter);
	    await _context.SaveChangesAsync();

	    return Ok();

	}
    }
}