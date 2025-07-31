namespace UsersService.Application.DTOs
{
    public record CreateUserDto(string Email, string UserPictureUrl, string AuthId);
}
