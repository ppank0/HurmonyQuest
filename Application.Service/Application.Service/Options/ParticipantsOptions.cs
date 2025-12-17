namespace Application.Service.Options
{
    public class ParticipantsOptions
    {
        public const string Section = "Participants";
        public string BaseUrl { get; set; } = default!;
        public string? ApiKey { get; set; }
    }
}
