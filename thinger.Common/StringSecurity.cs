using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


class StringSecurity {
    public static string Sha1Encrypt(string source, Encoding encoding = null)
    {
        if (encoding == null) encoding = Encoding.UTF8;
        byte[] byteArray = encoding.GetBytes(source);
        using (HashAlgorithm hashAlgorithm = new SHA1CryptoServiceProvider())
        {
            byteArray = hashAlgorithm.ComputeHash(byteArray);
            StringBuilder stringBuilder = new StringBuilder(256);
            foreach (byte item in byteArray)
            {
                stringBuilder.AppendFormat("{0:x2}", item);
            }
            hashAlgorithm.Clear();
            return stringBuilder.ToString();
        }

    }

    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="text"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string EncryptString(string text, string key)
    {
        using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(key);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(textBytes, 0, textBytes.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }
    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="cipherText"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string DecryptString(string cipherText, string key)
    {
        using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(key);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
    }
}