using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleGame.Services
{
    public static class Validator
    {
        public static bool IsCharacterNameValid(string name)
        {
            // false if name is null or empty
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            // false if name contains any ~!@#$%^&*()_+` etc
            if (name.Any(char.IsDigit))
            {
                return false;
            }

            // false if name is less than 3 or more than 40 characters
            if (name.Length < 3 || name.Length > 40)
            {
                // Tiberius, The Great And Masculine
                return false;
            }
            return true;
        }

    }
}
