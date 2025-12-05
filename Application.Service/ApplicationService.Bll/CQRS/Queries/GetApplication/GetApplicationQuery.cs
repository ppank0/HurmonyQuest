using ApplicationService.BLL.CQRS.DTOs;
using MediatR;

namespace ApplicationService.BLL.CQRS.Queries.GetApplication
{
    public record GetApplicationQuery(Guid id) : IRequest<ApplicationDto>;
}
