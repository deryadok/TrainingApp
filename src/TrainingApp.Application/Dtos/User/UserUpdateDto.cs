namespace TrainingApp.Application.Dtos.User
{
    public class UserUpdateDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
    }
}
