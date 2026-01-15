namespace Application.Service.HttpClients.Endpoints
{
    public class ParticipantEndpoints
    {
        public const string Base = "api/participants";
        public static string ById(Guid id) => $"{Base}/{id}";
    }
}
