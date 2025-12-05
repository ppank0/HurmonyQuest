using ApplicationService.BLL.CQRS.DTOs;
using MediatR;

namespace ApplicationService.BLL.CQRS.Command.ApplicationCommands.Create
{
    public record CreateApplicationCommand(
    string Name,
    string Surname,
    DateOnly Birthday,
    Guid MusicalInstrumentId,
    Guid NominationId,
    Guid VideoId) : IRequest<ApplicationDto>;
}
