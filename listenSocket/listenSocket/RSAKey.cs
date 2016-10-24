using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace listenSocket
{
    /// <summary>
    /// 公私钥制作和获取
    /// </summary>
    class RSAKey
    {
        /// <summary>
        /// 生成公私钥到指定文件
        /// </summary>
        /// <param name="PrivateKeyPath"></param>
        /// <param name="PublicKeyPath"></param>
        public void setRSAKey(string PrivateKeyPath, string PublicKeyPath)
        {
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                this.CreatePrivateKeyXML(PrivateKeyPath, provider.ToXmlString(true));
                this.CreatePublicKeyXML(PublicKeyPath, provider.ToXmlString(false));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 创建公钥文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="publickey"></param>
        private void CreatePublicKeyXML(string path, string publickey)
        {
            try
            {
                FileStream publicKeyXml = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(publicKeyXml);
                sw.WriteLine(publickey);
                sw.Close();
                publicKeyXml.Close();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 创建私钥文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="privatekey"></param>
        private void CreatePrivateKeyXML(string path, string privatekey)
        {
            try
            {
                FileStream privateKeyXml = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(privateKeyXml);
                sw.WriteLine(privatekey);
                sw.Close();
                privateKeyXml.Close();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获得公私钥
        /// </summary>
        /// <param name="xmlPath">xml文件路径</param>
        /// <returns>公私钥</returns>
        public string getRSAKey(string xmlPath)
        {
            try
            {
                FileStream keyXml = new FileStream(xmlPath, FileMode.OpenOrCreate);
                StreamReader sr = new StreamReader(keyXml);
                string key = sr.ReadToEnd();
                sr.Close();
                keyXml.Close();

                return key;
            }
            catch
            {
                throw;
            }
        }
    }
}
