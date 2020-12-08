using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Travel.Security.Auth
{
    public static class RSAKeyHelper
    {
        public static RSAParameters GenerateKey()
        {
            using var key = new RSACryptoServiceProvider(2048);
            return key.ExportParameters(true);
        }
    }
}
