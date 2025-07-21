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
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var users = await mediator.Send(new GetUsersQuery(), cancellationToken);

            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await mediator.Send(new GetUserQuery(id), cancellationToken);

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserDto userDto, CancellationToken cancellationToken)
        {
            var newUser = await mediator.Send(new CreateUserCommand(userDto), cancellationToken);

            return Ok(newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateUserDto userDto, CancellationToken cancellationToken)
        {
            await mediator.Send(new UpdateUserCommand(id, userDto), cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteUserCommand(id), cancellationToken);

            return NoContent();
        }
    }
}
