// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Security.Cryptography;
using System.IO;

namespace App.Model
{
    #region UCrypto
    [System.Diagnostics.DebuggerStepThrough]
    public class UCrypto
    {
        public static string Encrypt(string s)
        {
            return CryptoString.Encrypt(s);
        }

        public static string Decrypt(string s)
        {
            return CryptoString.Decrypt(s);
        }

        #region Base64
        public static string ToBase64(string data)
        {
            try
            {
                return Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(data));
            }
            catch (Exception e)
            {
                throw new Exception("Error in Encrypt" + e.Message);
            }
        }

        public static string FromBase64(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in Decrypt" + e.Message);
            }
        }
        #endregion
    }
    #endregion

    #region CryptoString Class
    [System.Diagnostics.DebuggerStepThrough]
    public sealed class CryptoString
    {
        private CryptoString() { }

        private static byte[] savedKey = null;
        private static byte[] savedIV = null;

        public static byte[] Key
        {
            get { return savedKey; }
            set { savedKey = value; }
        }

        public static byte[] IV
        {
            get { return savedIV; }
            set { savedIV = value; }
        }

        private static void RdGenerateSecretKey(RijndaelManaged rdProvider)
        {
            if (savedKey == null)
            {
                savedKey = App.Model.Config.Key;

                rdProvider.KeySize = 256;
                //rdProvider.GenerateKey();
                rdProvider.Key = savedKey;
            }
        }

        private static void RdGenerateSecretInitVector(RijndaelManaged rdProvider)
        {
            if (savedIV == null)
            {
                savedIV = App.Model.Config.IV;

                //rdProvider.GenerateIV();
                rdProvider.IV = savedIV;
            }
        }

        public static string Encrypt(string originalStr)
        {
            // Encode data string to be stored in memory.
            byte[] originalStrAsBytes = Encoding.ASCII.GetBytes(originalStr);
            byte[] originalBytes = { };

            // Create MemoryStream to contain output.
            using (MemoryStream memStream = new
            MemoryStream(originalStrAsBytes.Length))
            {
                using (RijndaelManaged rijndael = new RijndaelManaged())
                {
                    // Generate and save secret key and init vector.
                    RdGenerateSecretKey(rijndael);
                    RdGenerateSecretInitVector(rijndael);

                    if (savedKey == null || savedIV == null)
                    {
                        throw (new NullReferenceException(
                        "savedKey and savedIV must be non-null."));
                    }

                    // Create encryptor and stream objects.
                    using (ICryptoTransform rdTransform =
                    rijndael.CreateEncryptor((byte[])savedKey.
                    Clone(), (byte[])savedIV.Clone()))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memStream,
                        rdTransform, CryptoStreamMode.Write))
                        {
                            // Write encrypted data to the MemoryStream.
                            cryptoStream.Write(originalStrAsBytes, 0,
                            originalStrAsBytes.Length);
                            cryptoStream.FlushFinalBlock();
                            originalBytes = memStream.ToArray();
                        }
                    }
                }
            }
            // Convert encrypted string.
            string encryptedStr = Convert.ToBase64String(originalBytes);
            return (encryptedStr);
        }

        public static string Decrypt(string encryptedStr)
        {
            // Unconvert encrypted string.
            byte[] encryptedStrAsBytes = Convert.FromBase64String(encryptedStr);
            byte[] initialText = new Byte[encryptedStrAsBytes.Length];

            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                using (MemoryStream memStream = new MemoryStream(encryptedStrAsBytes))
                {
                    // Generate and save secret key and init vector.
                    RdGenerateSecretKey(rijndael);
                    RdGenerateSecretInitVector(rijndael);

                    if (savedKey == null || savedIV == null)
                    {
                        throw (new NullReferenceException(
                        "savedKey and savedIV must be non-null."));
                    }

                    // Create decryptor, and stream objects.
                    using (ICryptoTransform rdTransform =
                    rijndael.CreateDecryptor((byte[])savedKey.
                    Clone(), (byte[])savedIV.Clone()))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memStream,
                        rdTransform, CryptoStreamMode.Read))
                        {
                            // Read in decrypted string as a byte[].
                            cryptoStream.Read(initialText, 0, initialText.Length);
                        }
                    }
                }
            }

            // Convert byte[] to string.
            string decryptedStr = Encoding.ASCII.GetString(initialText);
            return decryptedStr.Replace("\0", "");
        }
    }
    #endregion
}