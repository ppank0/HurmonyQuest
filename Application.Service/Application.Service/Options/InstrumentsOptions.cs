namespace Application.Service.Options
{
    public class InstrumentsOptions
    {
        public const string Section = "Instruments";
        public string BaseUrl { get; set; } = default!;
        public string? ApiKey { get; set; }
    }
}
