namespace SharedModels.Application
{
    public interface IApplication
    {
        string? UserId { get; set; }
        string? TargetGroup { get; set; }
        public ActionType ActionToApplication { get; set; }
        public ApplicationData applicationData { get; set; }
    }

    public record ApplicationData(string ParticipantName, string ParticipantSurname,
        string ApplicationStatus, string NominationName);
}
