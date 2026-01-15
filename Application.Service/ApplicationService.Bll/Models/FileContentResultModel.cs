namespace ApplicationService.BLL.Models
{
    public record FileContentResultModel(
        Stream Stream,
        string ContentType,
        string FileName
        );
}
