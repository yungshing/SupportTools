using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SupportTools
{
    [XmlRoot(ElementName ="Config")]
    public class ConfigData
    {
        public ConfigData()
        {
            p1_Config = new SupportTools.P1_Config();
        }
        [XmlElement("P1_Config")]
        public P1_Config p1_Config;
    }
    /// <summary>
    /// 配置文件界面的配置文件
    /// </summary>
    [XmlRoot(ElementName = "P1_Config")]
    public class P1_Config
    {
        [XmlArray("FTPAddress")]
        [XmlArrayItem("f")]
        public string[] ftpAddress = new string[] { "ftp://120.76.28.224/" };
        [XmlElement("FTPUsername")]
        public string ftpUsername = "ftpa";
        [XmlElement("FTPPassword")]
        public string ftpPassword = "123456";
        [XmlElement("RootDirectory")]
        public string rootDirectory = @"E:\FTPDownload\WenDingVersion-C";
        [XmlElement("Client")]
        public string runClient = @"E:\FTPDownload\WenDingVersion-C\Base\UI2.0.exe";
        [XmlElement("ConfigDirectory")]
        public string configDirectory = @"E:\FTPDownload\WenDingVersion-C\Config\Version-C.config";
        [XmlElement("Encrypt")]
        public bool isEncrypt = true; ///是否加密 。保存为config文件
        [XmlArray("Kills")]
        [XmlArrayItem("k")]
        public string[] Kills = new string[] { "UI2.0.exe", "DrivingSchool.exe", "RuiHang-2.0.exe" };

    }
}
