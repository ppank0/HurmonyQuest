namespace ContestService.BLL.Exceptions;
public class NotFoundException (string message) : Exception (message)
{
    public NotFoundException(Guid id) : this($"Entity with this id: {id} was not found") { }
};
