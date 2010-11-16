using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace Global.Security
{
    /// <summary>
    /// Provide hashing features 
    /// </summary>
    public class HashingHelper
    {
        private readonly static RSACryptoServiceProvider Rsa = new RSACryptoServiceProvider(new CspParameters { Flags = CspProviderFlags.UseMachineKeyStore });
        private readonly static RSAParameters PublicParams = Rsa.ExportParameters(false); //Only public key.
        private readonly static RSAParameters PrivateParams = Rsa.ExportParameters(true); //Complete key pairs.

        /// <summary>
        /// Encrypt an input string 
        /// </summary>
        /// <param name="input">the input string to be encrpty</param>
        /// <param name="signature">the private key signature</param>
        /// <returns>A Base 64 encrypted string or empty if can't encrpyt</returns>
        public static string Encrpyt(string input, out byte[] signature)
        {
            signature = null;
            try
            {
                if (string.IsNullOrEmpty(input)) return string.Empty;
                var provider = new RSACryptoServiceProvider(new CspParameters { Flags = CspProviderFlags.UseMachineKeyStore });
                provider.ImportParameters(PublicParams);

                var buffer = Encoding.ASCII.GetBytes(input);
                var encryptedbuffer = provider.Encrypt(buffer, false);

                var hash = new SHA1Managed();
                provider.ImportParameters(PrivateParams);
                var hashedData = hash.ComputeHash(encryptedbuffer);
                signature = provider.SignHash(hashedData, CryptoConfig.MapNameToOID("SHA1"));

                var stringBuilder = new StringBuilder();
                stringBuilder.Append(Convert.ToBase64String(encryptedbuffer));

                return stringBuilder.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Decrypt a base 64 string hash
        /// </summary>
        /// <param name="hash">The encrypted hash</param>
        /// <param name="signature">the private key signature</param>
        /// <returns>If is valid return decrypt hash, else return empty string</returns>
        public static string Decrypt(string hash, byte[] signature)
        {
            if (!VerifyHash(hash, signature)) return string.Empty;
            try
            {
                var provider = new RSACryptoServiceProvider();
                provider.ImportParameters(PrivateParams);

                var encryptedbuffer = Convert.FromBase64String(hash);

                var decryptedbuffer = provider.Decrypt(encryptedbuffer, false);
                var arrayList = new ArrayList();

                arrayList.AddRange(decryptedbuffer);
                var converted = arrayList.ToArray(typeof(byte)) as byte[];

                if (converted != null) return Encoding.ASCII.GetString(converted);

                return string.Empty;
            }
            catch 
            {
                return string.Empty;
            }
        }

        private static bool VerifyHash(string signedHash, byte[] signature)
        {
            try
            {
                var provider = new RSACryptoServiceProvider();
                var hash = new SHA1Managed();

                provider.ImportParameters(PublicParams);
                var encryptedbuffer = Convert.FromBase64String(signedHash);
                var hashedData = hash.ComputeHash(encryptedbuffer);
                return provider.VerifyHash(hashedData, CryptoConfig.MapNameToOID("SHA1"), signature);
            }
            catch
            {
                return false;
            }
        }
    }
}
