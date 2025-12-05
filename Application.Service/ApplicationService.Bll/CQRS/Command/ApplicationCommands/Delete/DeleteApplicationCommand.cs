using MediatR;

namespace ApplicationService.BLL.CQRS.Command.ApplicationCommands.Delete
{
    public record DeleteApplicationCommand(Guid id) : IRequest;
}
