namespace Application.Service.Dtos
{
    public record CreateApplicationApiRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly Birthday { get; set; }
        public Guid MusicalInstrumentId { get; set; }
        public Guid NominationId { get; set; }
        public IFormFile File { get; set; }
    }
}
