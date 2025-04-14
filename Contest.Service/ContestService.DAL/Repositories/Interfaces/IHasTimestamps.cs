namespace ContestService.DAL.Repositories.Interfaces
{
    internal interface IHasTimestamps
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}
