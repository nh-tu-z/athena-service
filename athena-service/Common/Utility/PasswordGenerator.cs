using System.Security.Cryptography;

namespace AthenaService.Common.Utility
{
    /// <summary>
    /// Password generator class refactored from the code located in
    /// https://stackoverflow.com/questions/38995379/alternative-to-system-web-security-membership-generatepassword-in-aspnetcore-ne
    /// </summary>
    public static class PasswordGenerator
    {
        public static readonly char[] PUNCTUATIONS = "!@#$%^&*()_-+[{]}:>|./?".ToCharArray();

        public static string Generate(int length, int numberOfNonAlphanumericCharacters)
        {
            if (length < 1 || length > 128)
            {
                throw new ArgumentException(null, nameof(length));
            }

            if (numberOfNonAlphanumericCharacters > length || numberOfNonAlphanumericCharacters < 0)
            {
                throw new ArgumentException(null, nameof(numberOfNonAlphanumericCharacters));
            }

            using var randomNumberGenerator = RandomNumberGenerator.Create();
            var randomByteBuffer = new byte[length];

            randomNumberGenerator.GetBytes(randomByteBuffer);

            var count = 0;
            var characterBuffer = new char[length];

            for (var index = 0; index < length; index++)
            {
                var counter = randomByteBuffer[index] % 87;
                if (counter < 10)
                {
                    characterBuffer[index] = (char)('0' + counter);
                }
                else if (counter < 36)
                {
                    characterBuffer[index] = (char)('A' + counter - 10);
                }
                else if (counter < 64)
                {
                    characterBuffer[index] = (char)('a' + counter - 36);
                }
                else
                {
                    characterBuffer[index] = PUNCTUATIONS[counter - 64];
                    count++;
                }
            }

            if (count >= numberOfNonAlphanumericCharacters)
            {
                return new string(characterBuffer);
            }

            var rand = new Random();

            for (var index = 0; index < numberOfNonAlphanumericCharacters - count; index++)
            {
                int randomNumber;
                do
                {
                    randomNumber = rand.Next(0, length);
                }
                while (!char.IsLetterOrDigit(characterBuffer[randomNumber]));

                characterBuffer[randomNumber] = PUNCTUATIONS[rand.Next(0, PUNCTUATIONS.Length)];
            }

            return new string(characterBuffer);
        }
    }
}
