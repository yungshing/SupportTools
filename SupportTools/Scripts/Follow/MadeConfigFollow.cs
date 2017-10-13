using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace SupportTools.Follow
{
    public class MadeConfigFollow : BaseFollow
    {
        public Form form;
        /// <summary>
        /// FTP 地址TextBox
        /// </summary>
        public TextBox prefix_tbx;
        /// <summary>
        /// 程序根目录TextBox
        /// </summary>
        public TextBox root_tbx;
        /// <summary>
        /// 选择程序根目录按钮
        /// </summary>
        public Button selRoot_btn;
        /// <summary>
        /// 选择执行程序按钮 
        /// </summary>
        public Button selpro_btn;
        /// <summary>
        /// 执行程序目录显示
        /// </summary>
        public TextBox delpro_tbx;
        /// <summary>
        /// 选择配置文件按钮
        /// </summary>
        public Button selconfig_btn;
        /// <summary>
        /// 选择配置文件目录 显示
        /// </summary>
        public TextBox config_tbx;
        /// <summary>
        /// 开始按钮
        /// </summary>
        public Button star_btn;
        /// <summary>
        /// 新增 按钮
        /// </summary>
        public Button p1add_btn;
        /// <summary>
        /// 默认按钮
        /// </summary>
        public Button p1def_btn;
        /// <summary>
        /// 显示检测文件时信息的RichTextBox
        /// </summary>
        public RichTextBox showTip_ri;
        /// <summary>
        /// 显示需要结束的进程的名字
        /// </summary>
        public RichTextBox killname_rt;
        /// <summary>
        /// 加密保存 选择控件
        /// </summary>
        public CheckBox lock_cb;
        /// <summary>
        /// FTP 帐户 TextBox
        /// </summary>
        public TextBox ftpAcc_tbx;
        /// <summary>
        /// FTP 密码 TextBox
        /// </summary>
        public TextBox p1ftpPass_tbx;

        /// <summary>
        /// 是否正在检测文件
        /// 防止在检测文件的同时，用户更新其他信息
        /// </summary>
        private bool isChecking = false;
        private VersionXML versionXML;
        private bool isOnLoad = false;

        public MadeConfigFollow()
        {
            isOnLoad = false;
        }
        /// <summary>
        /// 执行一次，不能多次调用 
        /// </summary>
        public void OnLoad()
        {
            if (isOnLoad)
            {
                return;
            }
            isOnLoad = true;
            versionXML = new VersionXML();
            OnClickAddBtn();
            OnClickDefaultBtn();
            OnClickSelectConfigBtn();
            OnClickSelectProgramBtn();
            OnClickSelectRootDirectory();
            OnClickStart();
            SetTextBoxValue();
            SetDoShowCheckInfo(DoShowCheckInfo);
        }
        /// <summary>
        /// 点击开始
        /// </summary>
        /// <param name="b"></param>
        private void OnClickStart()
        {
            star_btn.Click += (z, d) =>
            {
                if (isChecking) return;
                GetTextBoxValue();
                if (!Ready())
                {
                    return;
                }
                Utility.CreateOrOpenConfig(false);
                Thread t = new Thread(()=>
                {
                    Check(versionXML, GlobalData.config.p1_Config.rootDirectory);
                    SaveFiles();
                });
                t.Start();
            };
        }
        /// <summary>
        /// 保存文件到指定位置
        /// </summary>
        private void SaveFiles()
        {
            if (form.InvokeRequired)
            {
                form.Invoke(new System.Action(SaveFiles));
            }
            else
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = GlobalData.config.p1_Config.rootDirectory + "/Config";
                if (!GlobalData.config.p1_Config.isEncrypt)
                {
                    sfd.Filter = "XML文件(*.xml)|*.xml";
                    sfd.FileName = "Version-C.xml";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        Utility.XMLSerialize(versionXML, sfd.FileName);
                        Utility.ShowExplore(sfd.FileName);
                    }
                }
                else
                {
                    sfd.Filter = "密文文件(*.config)|*.config";
                    sfd.FileName = "Version-C.config";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        Utility.Encryption(versionXML, sfd.FileName);
                        Utility.ShowExplore(sfd.FileName);
                    }
                }
            }
        }

        /// <summary>
        /// 所有设置完成且正确
        /// </summary>
        /// <returns></returns>
        private bool Ready()
        {
            ///设置的根目录不存在
            if (!Directory.Exists(root_tbx.Text))
            {
                MessageBox.Show("程序根目录不存在!!");
                return false;
            }
            if (!File.Exists(delpro_tbx.Text))
            {
                MessageBox.Show("执行程序路径错误");
                return false;
            }
            if (!File.Exists(config_tbx.Text))
            {
                MessageBox.Show("配置文件路径错误");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 把文本框的值赋给变量
        /// </summary>
        private void GetTextBoxValue()
        {
            var v = prefix_tbx.Text.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            if (v.Length == 0)
            {
                GlobalData.config.p1_Config.ftpAddress = versionXML.x_FTPAddress = new string[1] { prefix_tbx.Text };
            }
            else
            {
                GlobalData.config.p1_Config.ftpAddress = versionXML.x_FTPAddress = v;
            }

            GlobalData.config.p1_Config.rootDirectory = root_tbx.Text;
            GlobalData.config.p1_Config.runClient = delpro_tbx.Text;
            GlobalData.config.p1_Config.configDirectory = config_tbx.Text;

            GlobalData.config.p1_Config.ftpUsername = versionXML.x_FtpAccount.Username = ftpAcc_tbx.Text;
            GlobalData.config.p1_Config.ftpPassword = versionXML.x_FtpAccount.Password = p1ftpPass_tbx.Text;

            GlobalData.config.p1_Config.Kills = versionXML.x_KillProcessNames = killname_rt.Text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            GlobalData.config.p1_Config.isEncrypt = lock_cb.Checked;
        }
        /// <summary>
        /// 打开软件时，把以前的值直接赋给TextBox
        /// </summary>
        private void SetTextBoxValue()
        {
            prefix_tbx.Text = "";
            foreach (var item in GlobalData.config.p1_Config.ftpAddress)
            {
                prefix_tbx.Text += (item + ";");
            }
            root_tbx.Text = GlobalData.config.p1_Config.rootDirectory;
            delpro_tbx.Text = GlobalData.config.p1_Config.runClient;
            config_tbx.Text = GlobalData.config.p1_Config.configDirectory;
            ftpAcc_tbx.Text = GlobalData.config.p1_Config.ftpUsername;
            p1ftpPass_tbx.Text = GlobalData.config.p1_Config.ftpPassword;

            killname_rt.Clear();
            foreach (var item in GlobalData.config.p1_Config.Kills)
            {
                killname_rt.AppendText(item + "\n");
            }
            lock_cb.Checked = GlobalData.config.p1_Config.isEncrypt;
        }
        /// <summary>
        /// 点击选择程序根目录按钮
        /// </summary>
        private void OnClickSelectRootDirectory()
        {
            selRoot_btn.MouseClick += (z, d) =>
            {
                FolderBrowserDialog FBD = new FolderBrowserDialog();
                if (FBD.ShowDialog() == DialogResult.OK)
                {
                    root_tbx.Text = GlobalData.config.p1_Config.rootDirectory = FBD.SelectedPath;
                    delpro_tbx.Text = Path.Combine(FBD.SelectedPath,"Base\\UI2.0.exe");
                    config_tbx.Text = Path.Combine(FBD.SelectedPath, "Config\\Version-C.config");
                }
            };
        }
        /// <summary>
        /// 点击选择执行程序按钮
        /// </summary>
        private void OnClickSelectProgramBtn()
        {
            selpro_btn.MouseClick += (z, d) =>
            {
                var FBD = new OpenFileDialog();
                FBD.InitialDirectory = root_tbx.Text;
                if (FBD.ShowDialog() == DialogResult.OK)
                {
                    delpro_tbx.Text = GlobalData.config.p1_Config.rootDirectory = FBD.FileName;
                }
            };
        }
        /// <summary>
        /// 点击选择配置文件 按钮 
        /// </summary>
        private void OnClickSelectConfigBtn()
        {
            selconfig_btn.MouseClick += (z, d) =>
            {
                var FBD = new OpenFileDialog();
                FBD.InitialDirectory = root_tbx.Text;
                if (FBD.ShowDialog() == DialogResult.OK)
                {
                    config_tbx.Text = GlobalData.config.p1_Config.rootDirectory;
                }
            };
        }
        /// <summary>
        /// 点击默认按钮 
        /// </summary>
        private void OnClickDefaultBtn()
        {
            p1def_btn.Click += (d, z) =>
              {
                  lock_cb.Checked = true;
                  ftpAcc_tbx.Text = "ftpa";
                  p1ftpPass_tbx.Text = "123456";
              };
        }
        /// <summary>
        /// 点击新增按钮 
        /// </summary>
        private void OnClickAddBtn()
        {
            p1add_btn.Click += (d, z) =>
              {

              };
        }
        #region 检测文件事件
        private void Check(VersionXML xml,string rootPath)
        {
            var D = new DirectoryInfo(rootPath);
            foreach (var item in D.GetDirectories())
            {
                RunDoShowCheckInfo("进入目录:" + item.FullName + "\n");
                if (item.Name == "Change")
                {
                    foreach (var item1 in item.GetDirectories())
                    {
                        var v = new XMLFileList();
                        GetFiles(v, item1, "\\Change\\" + item1.Name);
                        v.Folder = item1.Name;
                        xml.x_FileList.x_change.Add(v);
                    }
                }
                else if (item.Name == "Base")
                {
                    GetFiles(xml.x_FileList.x_base, item, item.Name + "\\");
                    xml.x_FileList.x_base.Folder = "Base";
                }
                else if (item.Name == "Other")
                {
                    GetFiles(xml.x_FileList.x_other, item, item.Name + "\\");
                    xml.x_FileList.x_base.Folder = "Other";
                }
            }
        }

        private void GetFiles(XMLFileList xml, DirectoryInfo dir, string replaceFolder)
        {
            var rootFolder = new DirectoryInfo(GlobalData.config.p1_Config.rootDirectory).Name;
            foreach (var item in dir.GetFiles())
            {
                RunDoShowCheckInfo("检测文件:" + item.Name + "\n");
                var fileinfo = new XMLFileInfo();
                var file = new FileInfo(item.FullName);
                fileinfo.Name = item.Name;
                RunDoShowCheckInfo("计算Hash值..\n");
                fileinfo.Hash = Utility.GetMD5Value(item.FullName);
                var oP = rootFolder + item.FullName.Split(new string[] { rootFolder }, StringSplitOptions.RemoveEmptyEntries)[1];
                fileinfo.Address = oP;
                fileinfo.InstallPath = oP.Replace(replaceFolder, "");
                RunDoShowCheckInfo("当前文件检测完毕！\n");
                if (versionXML.x_ProcessInfo.path == item.FullName)
                {
                    versionXML.x_ProcessInfo.path = fileinfo.InstallPath;
                }
                xml.Files.Add(fileinfo);
            }
            foreach (var item in dir.GetDirectories())
            {
                RunDoShowCheckInfo("进入目录:" + item.FullName + "\n");
                GetFiles(xml, item, replaceFolder);
            }
        }
        #endregion

        #region UI事件
        private Action<string> doShowCheckInfo;

        private void RunDoShowCheckInfo(string s)
        {
            doShowCheckInfo?.Invoke(s);
        }
        private void SetDoShowCheckInfo(Action<string> a)
        {
            doShowCheckInfo = a;
        }
        private void DoShowCheckInfo(string s)
        {
            if (showTip_ri.InvokeRequired)
            {
                showTip_ri.Invoke(new Action<string>(DoShowCheckInfo),s);
            }
            else
            {
                showTip_ri.AppendText(s);
                showTip_ri.Select(showTip_ri.TextLength, 0);
                showTip_ri.ScrollToCaret();
            }
        }

        #endregion
    }
}
