using ApplicationService.BLL.CQRS.DTOs;
using ApplicationService.BLL.Interfaces;
using MediatR;

namespace ApplicationService.BLL.CQRS.Command.ApplicationCommands.Create
{
    public class CreateApplicationHandler(IApplicationService applicationService) : IRequestHandler<CreateApplicationCommand, ApplicationDto>
    {
        public async Task<ApplicationDto> Handle(CreateApplicationCommand request, CancellationToken ct)
        {
            return await applicationService.CreateAsync(request, ct);
        }
    }
}
