using System;
using System.Security.Cryptography;
using System.Web.Security;

namespace Global.Security
{
    public class EncryptDecrypt
    {
        
    }

    ///<summary>
    /// Provides some basic security features for a web application.
    ///</summary>
    public class SecurityHelper
    {
        ///<summary>
        /// Creates a password salt. The default size is 32.
        ///</summary>
        ///<returns>The salt to be used with the password.</returns>
        public static string CreateSalt()
        {
            return CreateSalt(32);
        }

        ///<summary>
        /// Creates a password salt.
        ///</summary>
        ///<param name="size">The size of the byte array.</param>
        ///<returns>The salt to be used with the password.</returns>
        public static string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

        ///<summary>
        /// Hashes the password. SHA1 is the default password format.
        ///</summary>
        ///<param name="password">The specified password.</param>
        ///<param name="passwordSalt">The specified password salt.</param>
        ///<returns></returns>
        public static string CreatePasswordHash(string password, string passwordSalt)
        {
            return CreatePasswordHash(password, passwordSalt, "SHA1");
        }

        ///<summary>
        /// Hashes the password.
        ///</summary>
        ///<param name="password">The specified password.</param>
        ///<param name="passwordSalt">The specified password salt.</param>
        ///<param name="passwordFormat">The specified password format. Can be SHA1 or MD5.</param>
        ///<returns></returns>
        public static string CreatePasswordHash(string password, string passwordSalt, string passwordFormat)
        {
            var saltAndPwd = String.Concat(password, passwordSalt);
            var hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, passwordFormat);
            
            return hashedPwd;
        }
    }
}
