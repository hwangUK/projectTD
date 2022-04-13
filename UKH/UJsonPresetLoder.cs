using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UKH
{  
    public static class JsonETileEncryptIO
    {
        private static readonly string privateKey   = "1718hy9dsf0jsdlfjds0pa9ids78ahgf81h32re";
        private static string path_preset           = $"{Application.streamingAssetsPath}/PRESET/";

        private static string Encrypt(string data)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
            RijndaelManaged rm = CreateRijndaelManaged();
            ICryptoTransform ct = rm.CreateEncryptor();
            byte[] results = ct.TransformFinalBlock(bytes, 0, bytes.Length);
            return System.Convert.ToBase64String(results, 0, results.Length);
        }

        private static string Decrypt(string data)
        {
            byte[] bytes = System.Convert.FromBase64String(data);
            RijndaelManaged rm = CreateRijndaelManaged();
            ICryptoTransform ct = rm.CreateDecryptor();
            byte[] resultArray = ct.TransformFinalBlock(bytes, 0, bytes.Length);
            return System.Text.Encoding.UTF8.GetString(resultArray);
        }


        private static RijndaelManaged CreateRijndaelManaged()
        {
            byte[] keyArray = System.Text.Encoding.UTF8.GetBytes(privateKey);
            RijndaelManaged result = new RijndaelManaged();

            byte[] newKeysArray = new byte[16];
            System.Array.Copy(keyArray, 0, newKeysArray, 0, 16);

            result.Key = newKeysArray;
            result.Mode = CipherMode.ECB;
            result.Padding = PaddingMode.PKCS7;
            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////////////

    }

}