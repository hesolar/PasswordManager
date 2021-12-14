using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace PassManager;

public static class Encryptor
{
    public static string EncryptStringPassword(string publicKey, string privateKey)
    {

        byte[] clearBytes = Encoding.Unicode.GetBytes(publicKey);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new(privateKey, new byte[] { 0x76, 0x49, 0x61, 0x20, 0x4d, 0x65, 0x64, 0x65, 0x76, 0x65, 0x64, 0x76, 0x6e });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using MemoryStream ms = new();
            using (CryptoStream cs = new(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(clearBytes, 0, clearBytes.Length);
                cs.Close();
            }
            publicKey = Convert.ToBase64String(ms.ToArray());
        }
        return TruncateLongString(publicKey, 25);
    }
    public static string EncryptNumberPassword(string publicKey, string privateKey)
    {


        String text = EncryptStringPassword(publicKey, privateKey);


        long sum = 0;
        byte overflow;
        for (int i = 0; i < text.Length; i++)
        {
            sum = (long)((16 * sum) ^ Convert.ToUInt32(text[i]));
            overflow = (byte)(sum / 4294967296);
            sum -= overflow * 4294967296;
            sum ^= overflow;
        }

        if (sum > 2147483647)
            sum -= 4294967296;
        else if (sum >= 32768 && sum <= 65535)
            sum -= 65536;
        else if (sum >= 128 && sum <= 255)
            sum -= 256;

        sum = Math.Abs(sum);

        return sum.ToString();


    }




    public static string TruncateLongString(string str, int maxLength) => str.Length < 25 ? str.PadRight(25, '0') : str[..Math.Min(str.Length, maxLength)];


}
