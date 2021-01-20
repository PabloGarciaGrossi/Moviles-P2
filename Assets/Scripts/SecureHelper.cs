using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;

public class SecureHelper
{
    public static string Hash(string data)
    {
        byte[] textToBytes = Encoding.UTF8.GetBytes(data);
        SHA256Managed sha = new SHA256Managed();

        byte[] hashValue = sha.ComputeHash(textToBytes);

        return GetHexStringFromHash(hashValue);
    }

    private static string GetHexStringFromHash(byte[] hash)
    {
        string hexString = string.Empty;

        foreach(byte b in hash)
        {
            hexString += b.ToString("x2");
        }
        return hexString;
    }
}
