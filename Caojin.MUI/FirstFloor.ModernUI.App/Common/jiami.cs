using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace CJComLibrary
{
  public  class DESjiami
    {
        //默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
 /**//**//**//// <summary>
/// DES加密字符串 keleyi.com
/// </summary>
/// <param name="encryptString">待加密的字符串</param>
 /// <param name="encryptKey">加密密钥,要求为8位</param>
 /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
 public static string EncryptDES(string encryptString, string encryptKey)
 {
     try
     {
         byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
         byte[] rgbIV = Keys;
         byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
         DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
         MemoryStream mStream = new MemoryStream();
         CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
         cStream.Write(inputByteArray, 0, inputByteArray.Length);
         cStream.FlushFinalBlock();
         return Convert.ToBase64String(mStream.ToArray());
     }
     catch
     {
         return encryptString;
     }
 }
 /**//**//**//// <summary>
 /// DES解密字符串 keleyi.com
 /// </summary>
 /// <param name="decryptString">待解密的字符串</param>
 /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
 /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
 public static string DecryptDES(string decryptString, string decryptKey)
 {
     try
     {
         byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
         byte[] rgbIV = Keys;
         byte[] inputByteArray = Convert.FromBase64String(decryptString);
         DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
         MemoryStream mStream = new MemoryStream();
         CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
         cStream.Write(inputByteArray, 0, inputByteArray.Length);
         cStream.FlushFinalBlock();
         return Encoding.UTF8.GetString(mStream.ToArray());
     }
     catch
     {
         return decryptString;
     }
 } 
  }
  public class MD5jiami
  {
      public static string MD5Encrypt(string strText)
      {
          var md5 = new MD5CryptoServiceProvider();
          string resualt = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(strText)), 4, 8);
          resualt = resualt.Replace("-", "");
          return resualt;
      }

      //获取指定长度的随机字符串（不重复）
      private static int rep = 0;
      public static string GenerateCheckCode(int codeCount)
      {
          
          string str = string.Empty;
          long num = DateTime.Now.Ticks + rep;
          rep++;
          Random random=new Random(((int)(((ulong)num) &0xffffffffL)) | ((int)(num >>rep)));
          for (int i = 0; i < codeCount; i++)
          {
              char ch;
              int num2 = random.Next();
              if ((num2 % 2) == 0)
              {
                  ch = (char)(0x30 + ((ushort)(num2 % 10)));
              }
              else 
              {
                  ch = (char)(0x41 + ((ushort)(num2 % 0x1a)));
              }
              str = str + ch.ToString();
          }
          return str;
      }
  
  }
}
