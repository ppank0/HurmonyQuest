using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersService.Application.CQRS.Commands.UserCommands.CreateUser;
using UsersService.Application.CQRS.Commands.UserCommands.DeleteUser;
using UsersService.Application.CQRS.Commands.UserCommands.UpdateUser;
using UsersService.Application.CQRS.Queries.GetUser;
using UsersService.Application.CQRS.Queries.GetUsers;
using UsersService.Application.DTOs;


namespace UsersService.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await mediator.Send(new GetUsersQuery());

            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await mediator.Send(new GetUserQuery(id));

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserDto userDto)
        {
            var newUser = await mediator.Send(new CreateUserCommand(userDto));

            return Ok(newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateUserDto userDto)
        {
            await mediator.Send(new UpdateUserCommand(id, userDto));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await mediator.Send(new DeleteUserCommand(id));

            return NoContent();
        }
    }
}
