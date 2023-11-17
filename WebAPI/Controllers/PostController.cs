using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain;
using Shared.DTOs;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostLogic _postLogic;
    
    public PostController(IPostLogic postLogic)
    {
        _postLogic = postLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<Post>> CreateAsync([FromBody] PostCreationDTO dto)
    {
        try
        {
            Post post = await _postLogic.CreateAsync(dto);
            return Created($"/posts/{post.PostId}", post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromQuery] string? userName,
        [FromQuery] string? postTitle)
    {
        try
        {
            PostSearchParametersDTO searchParameters = new(userName, postTitle);
            var posts = await _postLogic.GetAsync(searchParameters);
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult> UpdateAsync ([FromBody] PostUpdateDTO dto)
    {
        try
        {
            await _postLogic.UpdateAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}