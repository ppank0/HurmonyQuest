namespace UsersService.Domain.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public string? UserPictureUrl { get; set; }
        public required string AuthId { get; set; }
    }
}
