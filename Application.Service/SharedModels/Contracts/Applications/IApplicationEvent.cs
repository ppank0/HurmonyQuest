using SharedModels.Contracts.Applications.Data;

namespace SharedModels.Contracts.Applications
{
    public interface IApplicationEvent
    {
        string? UserId { get; set; }
        string? TargetGroup { get; set; }
        public ActionType ActionToApplication { get; set; }
        public ApplicationData applicationData { get; set; }
    }
}
