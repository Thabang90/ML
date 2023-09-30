using System.Text;

namespace QuestionExplorer.Helpers
{
    public static class PasswordGenerator
    {
        public static string GenerateRandomPassword(string passwordChars)
        {
            const int passwordLength = 6;
            StringBuilder password = new StringBuilder();
            Random random = new Random();

            for(int i = 0; i< passwordLength; i++)
            {
                int index = random.Next(passwordChars.Length);
                password.Append(passwordChars[index]);
            }

            return password.ToString();
        }
    }
}
