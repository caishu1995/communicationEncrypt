using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace connectSocket
{
    /// <summary>
    /// 根据RSA对指定内容加密解密
    /// </summary>
    class RSAChange
    {
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="publickey">公钥xml内容</param>
        /// <param name="content">需加密的内容</param>
        /// <returns>加密后结果</returns>
        public string RSAEncrypt(string publickey, string content)
        {
            byte[] cipherbytes;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publickey);//从xml中导入公钥
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);//对content内容加密

            return Convert.ToBase64String(cipherbytes);
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="privatekey">私钥xml内容</param>
        /// <param name="content">需解密的内容</param>
        /// <returns>解密后结果</returns>
        public string RSADecrypt(string privatekey, string content)
        {
            byte[] cipherbytes;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privatekey); //从xml中导入私钥
            cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);// 对content内容解密

            return Encoding.UTF8.GetString(cipherbytes);
        }
    }
}
