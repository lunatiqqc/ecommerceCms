using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using cms;
using cms.models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
	private readonly IWebHostEnvironment _hostingEnvironment;
	private readonly MyDbContext _context;

	public FileController(IWebHostEnvironment hostingEnvironment, MyDbContext context)
	{
	    _hostingEnvironment = hostingEnvironment;
	    _context = context;
	}

	[RequestSizeLimit(10737418240)] // 10GB
	[HttpPost("upload")]
	public async Task<IActionResult> Upload(IEnumerable<IFormFile> files, string FileFolderName)
	{


	    if (files == null || files.Count() <= 0)
		return BadRequest("No file was uploaded.");


	    foreach (var file in files)
	    {
		try
		{
		    var fileName = Path.GetFileName(file.FileName);

		    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

		    var fileExtension = Path.GetExtension(fileName);

		    // Set the upload folder location using environment variables
		    var uploadsFolder = _hostingEnvironment.WebRootPath;

		    // Generate a unique file name by appending an incremented integer value to the original file name
		    var uniqueFileNameWithoutExtension = GetUniqueFileNameWithoutExtension(fileNameWithoutExtension, fileExtension);

		    var fileFolderEntity = await GetOrCreateFileFolderEntityAsync(FileFolderName, null);


		    var imageWidths = Enum.GetValues(typeof(ImageSizes))
		    .Cast<ImageSizes>()
			.Select(s => (int)s)
			.ToList();


		    ImageFile imageFile = new ImageFile
		    {
			FileName = $"{uniqueFileNameWithoutExtension}{fileExtension}",
			FileFolder = fileFolderEntity,
			Sizes = new List<ImageSizes>()
			{

			}
		    };

		    using (var image = Image.Load(file.OpenReadStream()))
		    {
			imageFile.AspectRatio = (float)image.Width / image.Height;


			foreach (var newWidth in imageWidths)
			{

			    if (image.Width > newWidth)
			    {
				var resizedImage = image.Clone(x => x.Resize(new ResizeOptions
				{
				    Size = new Size(newWidth, 0),
				    Mode = ResizeMode.Max
				}));

				var newFileName = $"{uniqueFileNameWithoutExtension}_w{newWidth}{fileExtension}";
				var newImagePath = Path.Combine(uploadsFolder, newFileName);
				resizedImage.Save(newImagePath);

				imageFile.Sizes.Add((ImageSizes)newWidth);

			    }
			}

		    }
		    // Save the image
		    var newImageFileName = $"{uniqueFileNameWithoutExtension}{fileExtension}";
		    var newImageFilepath = Path.Combine(uploadsFolder, newImageFileName);
		    file.CopyTo(new FileStream(newImageFilepath, FileMode.Create));


		    _context.Files.Add(imageFile);
		    await _context.SaveChangesAsync();
		}
		catch (Exception ex)
		{
		    return StatusCode(500, $"An error occurred: {ex.Message}");
		}


	    }
	    return NoContent();
	}

	private string GetUniqueFileNameWithoutExtension(string fileNameWithoutExtension, string fileExtension)
	{

	    var count = 1;
	    while (FileExists(fileNameWithoutExtension, fileExtension))
	    {
		fileNameWithoutExtension = $"{fileNameWithoutExtension}_{count++}";
	    }

	    return fileNameWithoutExtension;
	}

	private bool FileExists(string fileNameWithoutExtension, string fileExtension)
	{
	    return _context.Files.Any(f => f.FileName == $"{fileNameWithoutExtension}{fileExtension}");
	}

	private async Task<FileFolder> GetOrCreateFileFolderEntityAsync(string fileFolderName, int? fileFolderId)
	{
	    FileFolder existingFileFolder;

	    if (fileFolderId.HasValue)
	    {
		existingFileFolder = await _context.FileFolders.FindAsync(fileFolderId);
		if (existingFileFolder != null)
		    return existingFileFolder;
	    }

	    existingFileFolder = await _context.FileFolders.FirstOrDefaultAsync(c => c.Name == fileFolderName);

	    if (existingFileFolder != null)
		return existingFileFolder;

	    var newFileFolder = new FileFolder
	    {
		Name = fileFolderName
	    };

	    _context.FileFolders.Add(newFileFolder);

	    try
	    {

		await _context.SaveChangesAsync();
	    }
	    catch (Exception ex)
	    {

	    }

	    return newFileFolder;
	}


	[HttpGet("filefolders")]
	public async Task<ActionResult<IEnumerable<FileFolder>>> GetFileFolders()
	{
	    try
	    {
		var folders = await _context.FileFolders.ToListAsync();

		return Ok(folders);
	    }
	    catch (Exception ex)
	    {
		return StatusCode(500, $"An error occurred: {ex.Message}");
	    }
	}
    }


}