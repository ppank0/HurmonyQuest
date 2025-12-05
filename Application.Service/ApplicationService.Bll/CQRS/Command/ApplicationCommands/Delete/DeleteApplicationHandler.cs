using ApplicationService.BLL.Interfaces;
using MediatR;

namespace ApplicationService.BLL.CQRS.Command.ApplicationCommands.Delete
{
    public class DeleteApplicationHandler(IApplicationService application) : IRequestHandler<DeleteApplicationCommand>
    {
        public async Task Handle(DeleteApplicationCommand command, CancellationToken cancellationToken)
        {
            await application.DeleteAsync(command, cancellationToken);
        }
    }
}
