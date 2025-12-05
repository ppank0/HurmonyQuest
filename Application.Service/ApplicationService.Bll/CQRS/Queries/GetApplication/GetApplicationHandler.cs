using ApplicationService.BLL.CQRS.DTOs;
using ApplicationService.BLL.Interfaces;
using MediatR;

namespace ApplicationService.BLL.CQRS.Queries.GetApplication
{
    public class GetApplicationHandler(IApplicationService application) : IRequestHandler<GetApplicationQuery, ApplicationDto>
    {
        public async Task<ApplicationDto> Handle(GetApplicationQuery request, CancellationToken cancellationToken)
        {
            var appDto = await application.GetByIdAsync(request.id, cancellationToken);
            return appDto;
        }
    }
}
