using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nabeghe.Application.DTOs.CourseStatus;
using Nabeghe.Application.Features.CourseStatus.Request.Commands;
using Nabeghe.Application.Features.CourseStatus.Request.Queries;
using Nabeghe.Application.Response;
using Nabeghe.Domain.Models.Course;

namespace Nabeghe.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CourseStatusController : ControllerBase
	{
		private readonly IMediator _mediator;

		public CourseStatusController(IMediator mediator)
		{
			_mediator = mediator;
		}
		// GET: api/<CourseStatusController>
		[HttpPut]
		public async Task<ActionResult<CourseStatusFilterDto>> GetAll(CourseStatusFilterDto filterDto)
		{
			var courseStatus = await _mediator.Send(new GetCourseStatusListRequest() { CourseStatusFilterDto = filterDto });
			return Ok(courseStatus);
		}

		// GET api/<CourseStatusController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<CourseStatusDto>> Get(int id)
		{
			var courseStatus = await _mediator.Send(new GetCourseStatusDetailRequest() { Id = id });
			return Ok(courseStatus);
		}

		// POST api/<CourseStatusController>
		[HttpPost]
		public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateCourseStatusDto createCourseStatusDto)
		{
			var command = new CreateCourseStatusCommand() { CreateCourseStatusDto = createCourseStatusDto };
			var response = await _mediator.Send(command);
			return Ok(response);
		}

		// PUT api/<CourseStatusController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult<BaseCommandResponse>> Put(int id, [FromBody] UpdateCourseStatusDto updateCourseStatusDto)
		{
			var command = new UpdateCourseStatusCommand() { UpdateCourseStatusDto = updateCourseStatusDto };
			var response = await _mediator.Send(command);
			return Ok(response);
		}

		// DELETE api/<CourseStatusController>/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var command = new DeleteCourseStatusCommand() { Id = id };
			await _mediator.Send(command);
			return NoContent();
		}
	}
}
