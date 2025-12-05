using ApplicationService.BLL.CQRS.DTOs;
using ApplicationService.DAL.Enum;
using MediatR;

namespace ApplicationService.BLL.CQRS.Command.ApplicationCommands.Update
{
    public record UpdateApplicationCommand(Guid id, ApplicationStatus status) : IRequest<ApplicationDto>;
}
