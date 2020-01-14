using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Template.NuGet
{
    public static class SecurityHelper
    {
        // Fields
        private const string Key = "TMPAY888";

        // Methods
        public static string AesDecrypt(string input, string key)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            byte[] inputBuffer = Convert.FromBase64String(input);
            RijndaelManaged managed1 = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] buffer2 = managed1.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            return Encoding.UTF8.GetString(buffer2);
        }

        public static string AesEncrypt(string input, string key)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            RijndaelManaged managed1 = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] inArray = managed1.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
            return Convert.ToBase64String(inArray, 0, inArray.Length);
        }

        public static bool CheckData(string Value, string SMD)
        {
            string appConfig = ConfigHelper.GetAppConfig("SecurityKey");
            if (appConfig == null)
            {
                appConfig = "";
            }
            if (string.IsNullOrEmpty(SMD))
            {
                SMD = string.Empty;
            }
            string str2 = GetMD5(Value + appConfig).ToUpper();
            return (SMD.ToUpper() == str2);
        }

        public static string Decrypt3DES(string a_strString, string a_strKey)
        {
            string str;
            try
            {
                ICryptoTransform transform = new TripleDESCryptoServiceProvider
                {
                    Key = Encoding.ASCII.GetBytes(a_strKey),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                }.CreateDecryptor();
                byte[] inputBuffer = Convert.FromBase64String(a_strString);
                str = Encoding.Default.GetString(transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch (Exception exception)
            {
                throw new Exception("TripleDES解密异常:" + exception.Message, exception);
            }
            return str;
        }

        public static string DecryptDES(string str, string key = "TMPAY888")
        {
            try
            {
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider
                {
                    Key = Encoding.ASCII.GetBytes(key.Substring(0, 8)),
                    IV = Encoding.ASCII.GetBytes(key.Substring(0, 8))
                };
                byte[] buffer = new byte[str.Length / 2];
                for (int i = 0; i < (str.Length / 2); i++)
                {
                    int num2 = Convert.ToInt32(str.Substring(i * 2, 2), 0x10);
                    buffer[i] = (byte)num2;
                }
                MemoryStream stream = new MemoryStream();
                CryptoStream stream1 = new CryptoStream((Stream)stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
                stream1.Write(buffer, 0, buffer.Length);
                stream1.FlushFinalBlock();
                stream.Close();
                return Encoding.UTF8.GetString(stream.ToArray());
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string DESDecrypt(string encryptedValue, string key) =>
            DESDecrypt(encryptedValue, key, key);

        public static string DESDecrypt(string encryptedValue, string key, string IV)
        {
            key = key + "12345678";
            IV = IV + "12345678";
            key = key.Substring(0, 8);
            IV = IV.Substring(0, 8);
            ICryptoTransform transform = new DESCryptoServiceProvider
            {
                Key = Encoding.UTF8.GetBytes(key),
                IV = Encoding.UTF8.GetBytes(IV)
            }.CreateDecryptor();
            byte[] buffer = Convert.FromBase64String(encryptedValue);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream1 = new CryptoStream((Stream)stream, transform, CryptoStreamMode.Write);
            stream1.Write(buffer, 0, buffer.Length);
            stream1.FlushFinalBlock();
            stream1.Close();
            return Encoding.UTF8.GetString(stream.ToArray());
        }

        public static string DESEncrypt(string originalValue, string key) =>
            DESEncrypt(originalValue, key, key);

        public static string DESEncrypt(string originalValue, string key, string IV)
        {
            IV = IV + "12345678";
            key = key.Substring(0, 8);
            IV = IV.Substring(0, 8);
            ICryptoTransform transform = new DESCryptoServiceProvider
            {
                Key = Encoding.UTF8.GetBytes(key),
                IV = Encoding.UTF8.GetBytes(IV)
            }.CreateEncryptor();
            byte[] bytes = Encoding.UTF8.GetBytes(originalValue);
            MemoryStream stream1 = new MemoryStream();
            CryptoStream stream2 = new CryptoStream((Stream)stream1, transform, CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            stream2.Close();
            return Convert.ToBase64String(stream1.ToArray());
        }

        public static string Encrypt3DES(string a_strString, string a_strKey)
        {
            string str;
            try
            {
                TripleDESCryptoServiceProvider provider1 = new TripleDESCryptoServiceProvider
                {
                    Key = Encoding.ASCII.GetBytes(a_strKey),
                    Mode = CipherMode.ECB
                };
                byte[] bytes = Encoding.Default.GetBytes(a_strString);
                str = Convert.ToBase64String(provider1.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length));
            }
            catch (Exception exception)
            {
                throw new Exception("TripleDES加密异常:" + exception.Message, exception);
            }
            return str;
        }

        public static string EncryptDES(string str, string key = "TMPAY888")
        {
            try
            {
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider
                {
                    Key = Encoding.ASCII.GetBytes(key.Substring(0, 8)),
                    IV = Encoding.ASCII.GetBytes(key.Substring(0, 8))
                };
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                MemoryStream stream = new MemoryStream();
                CryptoStream stream1 = new CryptoStream((Stream)stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
                stream1.Write(bytes, 0, bytes.Length);
                stream1.FlushFinalBlock();
                StringBuilder builder = new StringBuilder();
                foreach (byte num2 in stream.ToArray())
                {
                    builder.AppendFormat("{0:X2}", (byte)num2);
                }
                stream.Close();
                return builder.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string Encrypted(string Value)
        {
            string appConfig = ConfigHelper.GetAppConfig("SecurityKey");
            if (appConfig == null)
            {
                appConfig = "";
            }
            return GetMD5(Value + appConfig).ToUpper();
        }

        public static string GetMD5(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] buffer2 = MD5.Create().ComputeHash(bytes);
            StringBuilder builder = new StringBuilder(40);
            for (int i = 0; i < buffer2.Length; i++)
            {
                builder.Append(((byte)buffer2[i]).ToString("x2"));
            }
            return builder.ToString();
        }

        public static string GetMd5Hash(string input) =>
            Enumerable.Aggregate<byte, string>(MD5.Create().ComputeHash(Encoding.Unicode.GetBytes(input)), "", delegate (string current, byte t) {
                return current + ((byte)t).ToString("x");
            });

        public static string SHA1Encrypt(string data)
        {
            new SHA1Managed().Clear();
            SHA1 sha = null;
            return BitConverter.ToString(sha.ComputeHash(Encoding.ASCII.GetBytes(data))).Replace("-", "");
        }

        public static string SHA256Encrypt(string data)
        {
            new SHA256Managed().Clear();
            SHA256 sha = null;
            return BitConverter.ToString(sha.ComputeHash(Encoding.ASCII.GetBytes(data))).Replace("-", "");
        }

        public static string SHA512Encrypt(string data)
        {
            new SHA512Managed().Clear();
            SHA512 sha = null;
            return BitConverter.ToString(sha.ComputeHash(Encoding.ASCII.GetBytes(data))).Replace("-", "");
        }

        public static bool VerifyMd5Hash(string input, string hash)
        {
            string x = GetMd5Hash(input);
            return (StringComparer.OrdinalIgnoreCase.Compare(x, hash) == 0);
        }

    //    // Nested Types
    //    [Serializable, CompilerGenerated]
    //    private sealed class <>c
    //{
    //    // Fields
    //    public static readonly SecurityHelper.<>c<>9 = new SecurityHelper.<>c();
    //    public static Func<string, byte, string> <>9__7_0;

    //    // Methods
    //    internal string <GetMd5Hash>b__7_0(string current, byte t) =>
    //        (current + ((byte)t).ToString("x"));
    //}
}

}
