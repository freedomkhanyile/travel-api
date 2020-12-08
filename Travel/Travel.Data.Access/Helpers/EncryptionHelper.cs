﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
namespace Travel.Data.Access.Helpers
{
    using BCrypt = BCrypt.Net.BCrypt;
    public static class EncryptionHelper
    {
        public static string WithBCrypt(this string text)
        {
            var result = BCrypt.HashPassword(text);
            return result;
        }
        public static bool VerifyWithBCrypt(this string hashedPassword, string plainText)
        {
            var result = BCrypt.Verify(plainText, hashedPassword);
            return result;
        }
    }
}
