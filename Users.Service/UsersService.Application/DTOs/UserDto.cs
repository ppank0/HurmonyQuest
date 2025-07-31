namespace UsersService.Application.DTOs
{
    public record UserDto(Guid Id, string Email, string UserPictureUrl, string AuthId);
}
