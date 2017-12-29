// -----------------------------------------------------------------------
//  <copyright file="AesHelper.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2017 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2017-12-29 23:17</last-date>
// -----------------------------------------------------------------------

using System;
using System.IO;
using System.Security.Cryptography;

using OSharp.Utility.Extensions;


namespace OSharp.Utility.Secutiry
{
    /// <summary>
    /// AEA加密解密辅助类
    /// </summary>
    public class AesHelper
    {
        private readonly string _key;

        /// <summary>
        /// 初始化一个<see cref="AesHelper"/>类型的新实例
        /// </summary>
        /// <param name="key">加密密钥</param>
        public AesHelper(string key)
        {
            _key = key;
        }

        #region 实例方法

        /// <summary>
        /// 加密字节数组
        /// </summary>
        public byte[] Encrypt(byte[] decodeBytes)
        {
            return Encrypt(decodeBytes, _key);
        }

        /// <summary>
        /// 解密字节数组
        /// </summary>
        public byte[] Decrypt(byte[] encodeBytes)
        {
            return Decrypt(encodeBytes, _key);
        }

        /// <summary>
        /// 加密字符串，输出为Base64编码的字符串
        /// </summary>
        public string Encrypt(string source)
        {
            return Encrypt(source, _key);
        }

        /// <summary>
        /// 解密字符串，输入为Base64编码的字符串
        /// </summary>
        public string Decrypt(string source)
        {
            return Decrypt(source, _key);
        }

        /// <summary>
        /// 加密文件
        /// </summary>
        public void EncryptFile(string sourceFile, string targetFile)
        {
            EncryptFile(sourceFile, targetFile, _key);
        }

        /// <summary>
        /// 解密文件
        /// </summary>
        public void DecryptFile(string sourceFile, string targetFile)
        {
            DecryptFile(sourceFile, targetFile, _key);
        }
        #endregion

        #region 静态方法

        /// <summary>
        /// 加密字节数组
        /// </summary>
        public static byte[] Encrypt(byte[] decodeBytes, string key)
        {
            decodeBytes.CheckNotNull("decodeBytes");
            using (AesCryptoServiceProvider provider = new AesCryptoServiceProvider())
            {
                provider.Key = GetKey(key);
                provider.Mode = CipherMode.ECB;
                provider.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform transform = provider.CreateEncryptor())
                {
                    byte[] encodeBytes = transform.TransformFinalBlock(decodeBytes, 0, decodeBytes.Length);
                    provider.Clear();
                    return encodeBytes;
                }
            }
        }

        /// <summary>
        /// 解密字节数组
        /// </summary>
        public static byte[] Decrypt(byte[] encodeBytes, string key)
        {
            encodeBytes.CheckNotNull("source");
            using (AesCryptoServiceProvider provider = new AesCryptoServiceProvider())
            {
                provider.Key = GetKey(key);
                provider.Mode = CipherMode.ECB;
                provider.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform transform = provider.CreateDecryptor())
                {
                    byte[] decodeBytes = transform.TransformFinalBlock(encodeBytes, 0, encodeBytes.Length);
                    provider.Clear();
                    return decodeBytes;
                }
            }
        }

        /// <summary>
        /// 加密字符串，输出为Base64字符串
        /// </summary>
        public static string Encrypt(string source, string key)
        {
            source.CheckNotNull("source");

            byte[] decodeBytes = source.ToBytes();
            byte[] encodeBytes = Encrypt(decodeBytes, key);
            return Convert.ToBase64String(encodeBytes, 0, encodeBytes.Length);
        }

        /// <summary>
        /// 解密字符串，输入为Base64字符串
        /// </summary>
        public static string Decrypt(string source, string key)
        {
            source.CheckNotNull("source");

            byte[] encodeBytes = Convert.FromBase64String(source);
            byte[] decodeBytes = Decrypt(encodeBytes, key);
            return decodeBytes.ToString2();
        }

        /// <summary>
        /// 加密文件
        /// </summary>
        public static void EncryptFile(string sourceFile, string targetFile, string key)
        {
            sourceFile.CheckFileExists("sourceFile");
            targetFile.CheckNotNullOrEmpty("targetFile");

            using (FileStream ifs = new FileStream(sourceFile, FileMode.Open, FileAccess.Read), ofs = new FileStream(targetFile, FileMode.Create, FileAccess.Write))
            {
                long length = ifs.Length;
                byte[] decodeBytes = new byte[length];
                ifs.Read(decodeBytes, 0, decodeBytes.Length);
                byte[] encodeBytes = Encrypt(decodeBytes, key);
                ofs.Write(encodeBytes, 0, encodeBytes.Length);
            }
        }

        /// <summary>
        /// 解密文件
        /// </summary>
        public static void DecryptFile(string sourceFile, string targetFile, string key)
        {
            sourceFile.CheckFileExists("sourceFile");
            targetFile.CheckNotNullOrEmpty("targetFile");

            using (FileStream ifs = new FileStream(sourceFile, FileMode.Open, FileAccess.Read), ofs = new FileStream(targetFile, FileMode.Create, FileAccess.Write))
            {
                long length = ifs.Length;
                byte[] encodeBytes = new byte[length];
                ifs.Read(encodeBytes, 0, encodeBytes.Length);
                byte[] decodeBytes = Decrypt(encodeBytes, key);
                ofs.Write(decodeBytes, 0, decodeBytes.Length);
            }
        }

        /// <summary>
        /// 获取密钥，AES加密密钥必须是32位
        /// </summary>
        public static byte[] GetKey(string key)
        {
            key.CheckNotNullOrEmpty("key");
            if (key.Length < 32)
            {
                key = key.PadRight(32, '0');
            }
            else if (key.Length > 32)
            {
                key = key.Substring(0, 32);
            }
            return key.ToBytes();
        }

        #endregion
    }
}