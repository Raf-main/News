using Microsoft.AspNetCore.Mvc;
using PRAS.Testovoe.Main.Services.Image;
using Raf.Utils.Shared.MimeTypes;

namespace PRAS.Testovoe.Main.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpGet("{imageName}")]
    public IActionResult GetImage(string imageName)
    {
        if(string.IsNullOrEmpty(imageName))
        {
            return BadRequest();
        }

        var extension = Path.GetExtension(imageName);

        if(string.IsNullOrEmpty(extension))
        {
            return BadRequest();
        }

        var mimeType = MimeTypeUtils.GetMimeType(extension);

        return PhysicalFile(_imageService.GetImagePath(imageName), mimeType);
    }
}