using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRAS.Testovoe.Main.Services.NewsServices;
using PRAS.Testovoe.Main.ViewModels.Request;

namespace PRAS.Testovoe.Main.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NewsController : ControllerBase
{
    private readonly INewsService _newsService;

    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }

    [HttpPost]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Create([FromForm] CreateNewsRequest createNewsRequest)
    {
        Console.WriteLine(createNewsRequest.Image.ContentType.ToString());
        var file = Request.Form.Files.FirstOrDefault();

        if (file == null || file.Length < 1)
        {
            return BadRequest("fail nepr");
        }

        createNewsRequest.Image = file;

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.Root.Errors.Select(e => e.ErrorMessage));
        }

        await _newsService.CreateAsync(createNewsRequest);

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var news = await _newsService.GetAsync(id);

        return Ok(news);
    }

    [HttpGet("Paged")]
    public async Task<IActionResult> Get([FromQuery]int page, [FromQuery]int size)
    {
        var news = await _newsService.GetPagedAsync(page, size);

        return Ok(news);
    }

    [HttpPut("{id}/Image")]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromForm] UpdateNewsImageRequest request)
    {
        await _newsService.UpdateImageAsync(id, request);

        return Ok();
    }

    [HttpPut("{id}")]
    [HttpPost("{id}")]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromForm] UpdateNewsRequest request)
    {
        await _newsService.UpdateAsync(id, request);

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _newsService.DeleteAsync(id);

        return Ok();
    }
}