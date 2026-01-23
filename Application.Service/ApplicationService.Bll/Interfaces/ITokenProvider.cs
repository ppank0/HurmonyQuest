namespace ApplicationService.BLL.Interfaces
{
    public interface ITokenProvider
    {
        Task<string> GetAccessTokenAsync(string downstream, CancellationToken ct);
    }
}
