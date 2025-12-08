using ApplicationService.DAL.Enum;

namespace Application.Service.Dtos
{
    public class ApplicationDto
    {
        public Guid Id { get; set; }
        public string ParticipantName { get; set; } = string.Empty;
        public string ParticipantSurname { get; set; } = string.Empty;
        public DateOnly ParticipantBirthday { get; set; }
        public string InstrumentName { get; set; } = string.Empty;
        public string NominationName { get; set; } = string.Empty;
        public ApplicationStatus Status { get; set; }
    }
}
