namespace UsersService.Domain.Exceptions
{
    public class BadRequestException(string message) : Exception(message) { }
}
