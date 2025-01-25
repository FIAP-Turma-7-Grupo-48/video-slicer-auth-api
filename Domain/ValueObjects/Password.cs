using Domain.Helpers;
using Domain.ValueObjects.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public struct Password
    {
        public Password(string password)
        {
            if (!IsValidPassword(password))
                throw new InvalidPasswordException(password);

            Hash = PasswordHelper.HashPassword(password);
        }
        public string Hash { get; }

        public static implicit operator Password(string password)
        {
            return new Password(password);
        }

        public static implicit operator string(Password email)
        {
            return email.Hash;
        }

        private bool IsValidPassword(string password)
        {
            var input = password;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Senha não pode ser vazia");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public override bool Equals(object? obj)
        {
            return obj is Password pass &&
                   Hash == pass.Hash;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Hash);
        }
    }
}
