using BlogSystem.Application.DTOs.Categories;
using BlogSystem.Application.Features.Categories.Commands;
using BlogSystem.Application.Features.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : Controller
    {

        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(CategoryCommands commands)
        {
            var result = await _mediator.Send(commands);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Editor")]
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(List<CategoryDto>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCategoryQuery());
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Editor")]
        [HttpGet("detail/{id}")]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(id));
            return Ok(result);
        }
        [Authorize(Roles ="Admin")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryCommand command)
        {
            
            if(id != command.Id)
            {
                return BadRequest("Id Category Missmatch");
            }
            var result =await _mediator.Send(command);
            if(!result)
            {
                return NotFound("Can not update category");
            }
            return Ok(new {
                success=result,
                message= "Category undated successfully"
            });


        }
    }
}
