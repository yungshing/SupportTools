using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        public static void CreateOrOpenConfig(bool isOpen = true)
        {
            string path = @"E:\a\b.xml";
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
    }
}
