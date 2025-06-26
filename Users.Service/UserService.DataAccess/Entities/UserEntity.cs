namespace UserService.DataAccess.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public string? UserPictureUrl { get; set; }
    }
}
