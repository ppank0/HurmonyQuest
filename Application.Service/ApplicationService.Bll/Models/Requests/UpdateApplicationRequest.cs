using ApplicationService.DAL.Enum;

namespace ApplicationService.BLL.Models.Requests
{
    public record UpdateApplicationRequest(Guid id, ApplicationStatus status);
}
