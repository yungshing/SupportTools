using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SupportTools
{
    public class Utility
    {
        /// <summary>
        /// 获取文件Hash值 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMD5Value(string fileName)
        {
            string md5 = "";
            if (!File.Exists(fileName))
            {
                return md5;
            }
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                System.Security.Cryptography.MD5 cal = System.Security.Cryptography.MD5.Create();
                Byte[] buff = cal.ComputeHash(fs);
                cal.Clear();
                StringBuilder strBu = new StringBuilder();
                for (int i = 0; i < buff.Length; i++)
                {
                    strBu.Append(buff[i].ToString("x2"));
                }
                md5 = strBu.ToString();
            }
            return md5;
        }

        /// <summary>
        /// 序列化保存xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="path"></param>
        public static void XMLSerialize<T>(T t, string path)
        {
            var d = new FileInfo(path).Directory;
            if (!Directory.Exists(d.ToString()))
            {
                Directory.CreateDirectory(d.ToString());
            }
            using (var fs = new FileStream(path, FileMode.Create))
            {
                var v = new XmlSerializer(typeof(T));
                v.Serialize(fs, t);
            }
        }
        public static string XMLSerialize<T>(T t)
        {
            string s = "";
            using (var ms = new MemoryStream())
            {
                var v = new XmlSerializer(typeof(T));
                v.Serialize(ms, t);
                ms.Position = 0;
                using (var sr = new StreamReader(ms))
                {
                    s = sr.ReadToEnd();
                }
            }
            return s;
        }
        public static T XMLDeserialize<T>(string path)
        {
            using (var xr = XmlReader.Create(path))
            {
                var ser = new XmlSerializer(typeof(T));
                return (T)ser.Deserialize(xr);
            }
        }
        static public void ShowExplore(string filePath)
        {
            var p = new Process();
            p.StartInfo.FileName = "explorer";
            p.StartInfo.Arguments = @"/select," + filePath;
            p.Start();
        }
        public static void SaveOrOpenConfig(bool isOpen = true)
        {
            string path =Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.LocalApplicationData),"ZZFZ\\config.xml");
            if (isOpen)
            {
                if (!File.Exists(path))
                {
                    GlobalData.config = new ConfigData();
                    var d = new FileInfo(path);
                    if (!Directory.Exists(d.DirectoryName))
                    {
                        Directory.CreateDirectory(d.DirectoryName);
                    }
                    XMLSerialize(GlobalData.config, path);
                }
                else
                {
                    GlobalData.config = XMLDeserialize<ConfigData>(path);
                }
            }
            else
            {
                var d = new FileInfo(path);
                if (!Directory.Exists(d.DirectoryName))
                {
                    Directory.CreateDirectory(d.DirectoryName);
                }
                XMLSerialize(GlobalData.config, path);
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="path"></param>
        static public XmlDocument Decode(string path, char deKey = 'z')
        {
            XmlDocument xml = new XmlDocument();
            string s1 = "";
            using (var sr = new StreamReader(path))
            {
                s1 = sr.ReadToEnd();
            }
            var b1 = s1.ToCharArray();
            for (int i = 0; i < b1.Length; i++)
            {
                b1[i] -= deKey;
            }
            s1 = new string(b1);
            xml.LoadXml(s1);
            return xml;
        }
        public static T Decode<T>(string path,char deKey = 'z')
        {
            string s = "";
            using (var sr = new StreamReader(path, Encoding.UTF8))
            {
                s = sr.ReadToEnd();
            }
            var c = s.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                c[i] -= deKey;
            }
            s = new string(c);
            T t = default(T);
            var xmlser = new XmlSerializer(typeof(T));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(s)))
            {
                t = (T)xmlser.Deserialize(ms);
            }
            return t;
        }
        public static void Encryption<T>(T t, string path)
        {
            var xml = ClassToXML(t);
            var s = XMLToString(xml);
            EncryptionString(s, path);
        }
        /// <summary>
         /// 1. 类序列化成XML
         /// </summary>
         /// <typeparam name="T"></typeparam>
         /// <param name="t"></param>
         /// <returns></returns>
        public static XmlDocument ClassToXML<T>(T t)
        {
            XmlDocument xml = new XmlDocument();
            XmlSerializer xmlser = new XmlSerializer(typeof(T));
            using (var ms = new MemoryStream())
            {
                xmlser.Serialize(ms, t);
                ms.Position = 0;
                xml.Load(ms);
            }
            return xml;
        }
        /// <summary>
        /// 2. XML转string
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string XMLToString(XmlDocument xml)
        {
            string s = "";
            ///xml转字符串
            using (var ms = new MemoryStream())
            {
                using (var tw = new XmlTextWriter(ms, null))
                {
                    tw.Formatting = Formatting.Indented;
                    xml.Save(tw);
                    using (var sr = new StreamReader(ms, Encoding.UTF8))
                    {
                        ms.Position = 0;
                        s = sr.ReadToEnd();
                    }
                }
            }
            return s;
        }
        /// <summary>
        /// 3. 加密string并保存
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static void EncryptionString(string s, string path)
        {
            var c = s.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                c[i] += 'z';
            }
            using (var fs = new FileStream(path, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(c);
                }
            }
        }

        /// <summary>
        /// 解密Data配置文件，非下载的配置文件
        /// </summary>
        /// <returns></returns>
        public static XmlDocument DESDecode(string path, byte[] key = default(byte[]), byte[] iv = default(byte[]))
        {
            XmlDocument xml = new XmlDocument();
            if (key == default(byte[]))
            {
                key = new byte[8] { 0x01, 0x04, 0x03, 0x04, 0x01, 0x06, 0x06, 0x08 };
            }
            if (iv == default(byte[]))
            {
                iv = new byte[8] { 0x0a, 0x07, 0x0c, 0x01, 0x04, 0x03, 0x02, 0x0b };
            }
            using (var fs = new FileStream(path, FileMode.Open))
            {
                using (var des = new DESCryptoServiceProvider { Key = key, IV = iv })
                {
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            int n = 1;
                            var buff = new byte[1024];
                            while (n > 0)
                            {
                                n = fs.Read(buff, 0, buff.Length);
                                cs.Write(buff, 0, n);
                            }
                            cs.FlushFinalBlock();

                            ms.Position = 0;
                            xml.Load(ms);
                        }
                    }
                }
            }
            return xml;
        }
    }
}
