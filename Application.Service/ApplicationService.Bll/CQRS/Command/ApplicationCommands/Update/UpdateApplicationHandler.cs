using ApplicationService.BLL.CQRS.DTOs;
using ApplicationService.BLL.Interfaces;
using MediatR;

namespace ApplicationService.BLL.CQRS.Command.ApplicationCommands.Update
{
    public class UpdateApplicationHandler(IApplicationService application)
        : IRequestHandler<UpdateApplicationCommand, ApplicationDto>
    {
        public async Task<ApplicationDto> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
        {
            return await application.UpdateAsync(request, cancellationToken);
        }
    }
}
