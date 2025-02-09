using Microsoft.AspNetCore.Identity;

namespace TrainingApp.Shared.Helpers
{
    public static class PasswordHelper
    {
        public static string HashPassword(string username, string password)
        {
            var passwordHasher = new PasswordHasher<string>();

            return passwordHasher.HashPassword(username, password);
        }

        public static PasswordVerificationResult VerifyPassword(string username, string password)
        {
            var passwordHasher = new PasswordHasher<string>();

            var hashedPassword = HashPassword(username, password);

            return passwordHasher.VerifyHashedPassword(username, hashedPassword, password);
        }
    }
}
