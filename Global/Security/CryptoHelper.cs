using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Global.Security
{
    ///<summary>
    /// Handles cryptographic actions on strings (encrypt and decrypt).
    ///</summary>
    public class CryptoHelper
    {
        // RTAIAPAURLDKMLPP

        /// <summary>
        /// Local variable for setting/getting the encoding of the text to be encrypted/decrypted.
        /// </summary>
        private Encoding _textEncoding;
        /// <summary>
        /// Sets and gets the encoding of the text to be encrypted/decrypted. Defaults to UTF8.
        /// </summary>
        public Encoding TextEncoding
        {
            get { return _textEncoding ?? (_textEncoding = Encoding.UTF8); } 
            set { _textEncoding = value; }
        }

        /// <summary>
        /// Local variable for setting/getting the encoding of the encryptor strings (initialization vector and secret key).
        /// </summary>
        private Encoding _encryptorEncoding;
        /// <summary>
        /// Sets and gets the encoding of the encryptor strings (initialization vector and secret key). Defaults to ASCII.
        /// </summary>
        public Encoding EncryptorEncoding
        {
            get { return _encryptorEncoding ?? (_encryptorEncoding = Encoding.ASCII); }
            set { _encryptorEncoding = value; }
        }

        /// <summary>
        /// Local variable for setting/getting the initialization vector string when using the encryptor class.
        /// </summary>
        private string _initializationVectorString;
        /// <summary>
        /// Sets and gets the initialization vector string when using the encryptor class. A default value is set if none is provided.
        /// </summary>
        public string InitializationVectorString
        {
            get { return _initializationVectorString ?? (_initializationVectorString = "RTAIAPAURLDKMLPP"); }
            set { _initializationVectorString = value; }
        }

        /// <summary>
        /// Local variable for setting/getting the secret key string when using the encryptor class.
        /// </summary>
        private string _secretKeyString;
        /// <summary>
        /// Sets and gets the secret key string when using the encryptor class. A random value is set if none is provided.
        /// </summary>
        public string SecretKeyString
        {
            get { return _secretKeyString ?? (_secretKeyString = Common.Random(32, true)); }
            set { _secretKeyString = value; }
        }

        /// <summary>
        /// Encrypts a string using the Rijndael crypto algorithm.
        /// </summary>
        /// <param name="toEncrypt">The specified string to be encrypted.</param>
        /// <returns>A Base64 encrypted string.</returns>
        public string Encrypt(string toEncrypt)
        {
            return Encrypt(toEncrypt, null, null);
        }

        /// <summary>
        /// Encrypts a string using the Rijndael crypto algorithm.
        /// </summary>
        /// <param name="toEncrypt">The specified string to be encrypted.</param>
        ///<param name="keySize">The key size.</param>
        ///<param name="blockSize">The block size.</param>
        /// <returns>A Base64 encrypted string.</returns>
        public string Encrypt(string toEncrypt, int? keySize, int? blockSize)
        {
            byte[] secretKey;
            return Encrypt(toEncrypt, out secretKey, keySize, blockSize);
        }

        /// <summary>
        /// Encrypts a string using the Rijndael crypto algorithm.
        /// </summary>
        /// <param name="toEncrypt">The specified string to be encrypted.</param>
        /// <param name="secretKey">The secret key byte array to be used when encrypting the string.</param>
        /// <returns>A Base64 encrypted string.</returns>
        public string Encrypt(string toEncrypt, out byte[] secretKey)
        {
            return Encrypt(toEncrypt, out secretKey, null, null);
        }

        /// <summary>
        /// Encrypts a string using the Rijndael crypto algorithm.
        /// </summary>
        /// <param name="toEncrypt">The specified string to be encrypted.</param>
        /// <param name="secretKey">The secret key byte array to be used when encrypting the string.</param>
        ///<param name="keySize">The key size.</param>
        ///<param name="blockSize">The block size.</param>
        /// <returns>A Base64 encrypted string.</returns>
        public string Encrypt(string toEncrypt, out byte[] secretKey, int? keySize, int? blockSize)
        {
            var bytesToEncrypt = TextEncoding.GetBytes(toEncrypt);
            var rijn = Create(keySize, blockSize);
            using (var ms = new MemoryStream())
            {
                var initializationVector = EncryptorEncoding.GetBytes(InitializationVectorString);
                secretKey = EncryptorEncoding.GetBytes(SecretKeyString);

                using (var cs = new CryptoStream(ms, rijn.CreateEncryptor(secretKey, initializationVector), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToEncrypt, 0, bytesToEncrypt.Length);
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        ///<summary>
        /// Decrypts a string using the Rijndael crypto algorithm. It will use the SecretKeyString property to retrieve the secretKey byte array. 
        /// If the SecretKeyString is not set it will throw an exception.
        ///</summary>
        /// <param name="toDescrypt">A Base64 string to be decrypted.</param>
        /// <returns>The decrypted string of the provided encrypted string.</returns>
        public string Decrypt(string toDescrypt)
        {
            return Decrypt(toDescrypt, null, null);
        }

        /// <summary>
        /// Decrypts a string using the Rijndael crypto algorithm.
        /// </summary>
        /// <param name="toDescrypt">A Base64 string to be decrypted.</param>
        ///<param name="keySize">The key size.</param>
        ///<param name="blockSize">The block size.</param>
        /// <returns>The decrypted string of the provided encrypted string.</returns>
        public string Decrypt(string toDescrypt, int? keySize, int? blockSize)
        {
            var secretKey = EncryptorEncoding.GetBytes(SecretKeyString);
            return Decrypt(toDescrypt, secretKey, keySize, blockSize);
        }

        /// <summary>
        /// Decrypts a string using the Rijndael crypto algorithm.
        /// </summary>
        /// <param name="toDescrypt">A Base64 string to be decrypted.</param>
        /// <param name="secretKey">The secret key to be used when decrypting the string.</param>
        /// <returns>The decrypted string of the provided encrypted string.</returns>
        public string Decrypt(string toDescrypt, byte[] secretKey)
        {
            return Decrypt(toDescrypt, secretKey, null, null);
        }

        /// <summary>
        /// Decrypts a string using the Rijndael crypto algorithm.
        /// </summary>
        /// <param name="toDescrypt">A Base64 string to be decrypted.</param>
        /// <param name="secretKey">The secret key to be used when decrypting the string.</param>
        ///<param name="keySize">The key size.</param>
        ///<param name="blockSize">The block size.</param>
        /// <returns>The decrypted string of the provided encrypted string.</returns>
        public string Decrypt(string toDescrypt, byte[] secretKey, int? keySize, int? blockSize)
        {
            var bytesToDescrypt = Convert.FromBase64String(toDescrypt);
            using (var ms = new MemoryStream())
            {

                var rijn = Create(keySize, blockSize);
                var initializationVector = EncryptorEncoding.GetBytes(InitializationVectorString);
                using (var cs = new CryptoStream(ms, rijn.CreateDecryptor(secretKey, initializationVector), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToDescrypt, 0, bytesToDescrypt.Length);
                }
                return TextEncoding.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// Creates an instance of the rijndael class.
        /// </summary>
        /// <param name="keySize">The key size.</param>
        /// <param name="blockSize">The block size.</param>
        /// <returns>An instance of the rijndael class.</returns>
        private static Rijndael Create(int? keySize, int? blockSize)
        {
            var rjd = Rijndael.Create();
            if (keySize != null && keySize > 0) rjd.KeySize = (int)keySize;
            if (blockSize != null && blockSize > 0) rjd.BlockSize = (int)blockSize;
            return rjd;
        }
    }
}