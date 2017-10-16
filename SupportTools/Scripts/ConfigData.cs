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
            config_Create = new Config_Create();
            config_Add = new Config_Add();
            config_Code = new Config_Code();
        }
        [XmlElement("Config_Create")]
        public Config_Create config_Create;
        [XmlElement("Config_Add")]
        public Config_Add config_Add;
        [XmlElement("Config_Code")]
        public Config_Code config_Code;
    }
    /// <summary>
    /// 配置文件界面的配置文件
    /// </summary>
    [XmlRoot(ElementName = "Config_Create")]
    public class Config_Create
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
    /// <summary>
    /// 新增  界面的配置文件 
    /// </summary>
    [XmlRoot(ElementName = "Config_Add")]
    public class Config_Add
    {
        [XmlElement("ConfigPath")]
        public string configPath = @"E:\FTPDownload\WenDingVersion-C\Config\Version-C.config";
        [XmlElement("Backup")]
        public bool backup = true;
    }
    /// <summary>
    /// 加密  解密
    /// </summary>
    [XmlRoot(ElementName = "Config_Code")]
    public class Config_Code
    {
        [XmlElement("DESKey")]
        public string desKey = "ЁЃ؁ࠆ";
        [XmlElement("DESVI")]
        public string desVI= "܊Č̄ଂ";
        [XmlElement("CharKey")]
        public string charKey = "z";
    }
}
