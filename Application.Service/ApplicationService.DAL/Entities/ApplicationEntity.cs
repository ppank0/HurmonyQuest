using ApplicationService.DAL.Enum;

namespace ApplicationService.DAL.Entities
{
    public class ApplicationEntity : BaseEntity
    {
        public Guid ParticipantId { get; set; }
        public Guid NominationId { get; set; }
        public Guid InstrumentId { get; set; }

        public ApplicationStatus Status { get; set; } = ApplicationStatus.Review;
        public Guid VideoId { get; set; }
        public VideoEntity? Video { get; set; }
    }
}