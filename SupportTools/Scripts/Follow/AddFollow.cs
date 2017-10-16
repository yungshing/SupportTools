using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
namespace SupportTools.Follow
{
    public class AddFollow : BaseFollow
    {
        public TextBox p2con_tbx;
        public TextBox p2ftpadd_tbx;

        public CheckBox ftp_cbx;
        public CheckBox p2base_cbx;
        public CheckBox p2other_cbx;
        public CheckBox p2backup_cbx;

        public Button p2sel_btn;
        public Button p2start_btn;

        public RichTextBox p2infor_rtb;

        public TableLayoutPanel p2change_tlp;

        public Form1 form;

        VersionXML versionXML;
        bool isLoad = false;
        List<string> changeChildFolders;
        bool haveFTPIP = false, haveBase = false, haveOther = false;
        /// <summary>
        /// 如果一个Checkbox也没有选且Change文件夹的子文件夹也一个没有选，则为false;
        /// </summary>
        bool editorAnything = false;
        /// <summary>
        /// 文件夹根目录
        /// eg. E:\WenDing-C
        /// </summary>
        string root = "";
        public override void OnLoad()
        {
            if (isLoad)
            {
                return;
            }
            isLoad = true;
            ftp_cbx.Checked = haveFTPIP;
            p2base_cbx.Checked = haveBase;
            p2other_cbx.Checked = haveOther;
            p2backup_cbx.Checked = GlobalData.config.config_Add.backup;

            changeChildFolders = new List<string>();
            P2con_tbx_Drop();
            P2sel_btn_Click();
            P2start_btn_Click();
            CheckBoxsEvent();
            SetDoShowInfo();
        }
        void P2con_tbx_Drop()
        {
            p2con_tbx.DragEnter += (d, z) =>
              {
                  if (z.Data.GetDataPresent(DataFormats.FileDrop))
                  {
                      var n = ((System.Array)z.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                      if (n.EndsWith(".config"))
                      {
                          z.Effect = DragDropEffects.Link;
                      }
                  }
                  else
                  {
                      z.Effect = DragDropEffects.None;
                  }
              };
            p2con_tbx.DragDrop += (d, z) =>
              {
                  var n = ((System.Array)z.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                  if (n.EndsWith(".config"))
                  {
                      p2con_tbx.Text = n;
                      GetConfigInfo();
                  }
              };
        }
        void P2sel_btn_Click()
        {
            p2sel_btn.Click += (d, z) =>
              {
                  OpenFileDialog o = new OpenFileDialog();
                  o.Filter = "配置文件|*.config";
                  if (o.ShowDialog() == DialogResult.OK)
                  {
                      p2con_tbx.Text = o.FileName;
                      GetConfigInfo();
                  }
              };
        }

        void P2start_btn_Click()
        {
            p2start_btn.Click += (d, z) =>
              {
                  if (!Ready())
                  {
                      return;
                  }
                  SaveConfig();
                  CheckEditor();
                  Thread t = new Thread(() =>
                  {
                      CalHash();
                      SaveFile();
                  });
                  t.Start();
              };
        }
        void CheckEditor()
        {
            editorAnything = (haveBase || haveFTPIP || haveOther || changeChildFolders.Count > 0);
        }
        void CheckBoxsEvent()
        {
            ftp_cbx.CheckedChanged += (d, z) =>
              {
                  haveFTPIP = ftp_cbx.Checked;
              };
            p2base_cbx.CheckedChanged += (d, z) =>
            {
                haveBase = p2base_cbx.Checked;
            };
            p2other_cbx.CheckedChanged += (d, z) =>
            {
                haveOther = p2other_cbx.Checked;
            };
        }
       public override  void LoadConfig()
        {
            try
            {
                versionXML = Utility.XMLDeserialize<VersionXML>(GlobalData.config.config_Add.configPath);
                AnalysisConfigData();
            }
            catch
            {

            }
        }
        /// <summary>
        /// 获取配置文件信息
        /// </summary>
        void GetConfigInfo()
        {
            try
            {
                versionXML = Utility.Decode<VersionXML>(p2con_tbx.Text);
                GlobalData.config.config_Add.configPath = p2con_tbx.Text;
                AnalysisConfigData();
            }
            catch
            {
                p2con_tbx.Text = "";
                MessageBox.Show("所选文件不能被识别");
            }
        }

        void AnalysisConfigData()
        {
            p2con_tbx.Text = GlobalData.config.config_Add.configPath;
            p2ftpadd_tbx.Text = "";
            for (int i = 0; i < versionXML.x_FTPAddress.Length; i++)
            {
                p2ftpadd_tbx.Text += versionXML.x_FTPAddress[i] + ";";
            }
            root = GlobalData.config.config_Add.configPath.Split(new string[] { "\\Config" }, StringSplitOptions.RemoveEmptyEntries)[0];
            ShowChangeChildFolder(Path.Combine(root, "Change"));
        }

        /// <summary>
        /// 显示Change文件夹下的子文件夹
        /// </summary>
        /// <param name="changeFolder"></param>
        void ShowChangeChildFolder(string changeFolder)
        {
            var r = new DirectoryInfo(changeFolder);
            var checkBoxs = new List<CheckBox>();
            var selectAll = new CheckBox();
            selectAll.Text = "全选";
            selectAll.Click += (d, z) =>
              {
                  changeChildFolders.Clear();
                  foreach (var item in checkBoxs)
                  {
                      item.Checked = selectAll.Checked;
                      if (selectAll.Checked)
                      {
                          changeChildFolders.Add(item.Text);
                      }
                  }
              };
            p2change_tlp.RowCount = 1;
            p2change_tlp.Controls.Add(selectAll);
            p2change_tlp.SetRow(selectAll,0);
            foreach (var item in r.GetDirectories())
            {
                var c = new CheckBox();
                c.Text = item.Name;
                c.CheckedChanged += (d, z) =>
                  {
                      if (c.Checked)
                      {
                          if (!changeChildFolders.Contains(c.Text))
                          {
                              changeChildFolders.Add(c.Text);
                          }
                      }
                      else
                      {
                          if (changeChildFolders.Contains(c.Text))
                          {
                              changeChildFolders.Remove(c.Text);
                          }
                          selectAll.CheckState = CheckState.Unchecked;
                      }
                  };
                p2change_tlp.Controls.Add(c);
                p2change_tlp.RowCount++;
                p2change_tlp.SetRow(c, p2change_tlp.RowCount - 1);
                checkBoxs.Add(c);
            }
        }

        /// <summary>
        /// 计算文件Hash值
        /// </summary>
        void CalHash()
        {
            if (haveFTPIP)
            {
                RunDoShowInfo("更新FTPIP");
                versionXML.x_FTPAddress = p2ftpadd_tbx.Text.Split(';');
                RunDoShowInfo("更新FTPIP......完成!");
            }
            if (haveBase)
            {
                RunDoShowInfo("检测Base文件夹");
                string folder = "Base";
                var d = new DirectoryInfo(Path.Combine(root, folder));
                versionXML.x_FileList.x_base.Files.Clear();
                GetFiles(versionXML.x_FileList.x_base,d, folder);
                RunDoShowInfo("更新Base文件夹......完成!");
            }
            if (haveOther)
            {
                RunDoShowInfo("检测Ohter文件夹");
                string folder = "Ohter";
                var d = new DirectoryInfo(Path.Combine(root, folder));
                versionXML.x_FileList.x_other.Files.Clear();
                GetFiles(versionXML.x_FileList.x_base, d, folder);
                RunDoShowInfo("更新Ohter文件夹......完成!");
            }
            if (changeChildFolders.Count > 0)
            {
                RunDoShowInfo("检测Change文件夹");
                foreach (var item in changeChildFolders)
                {
                    bool flag = false;///是否在x_change中找到
                    var d = new DirectoryInfo(Path.Combine(root, "Change\\" + item));
                    foreach (var item2 in versionXML.x_FileList .x_change)
                    {
                        if (item2.Folder == item)
                        {
                            flag = true;
                            item2.Files.Clear();
                            GetFiles(item2, d, "\\Change\\" + item);
                        }
                    }
                    if (!flag)///向x_change中添加元素
                    {
                        var v = new XMLFileList();
                        GetFiles(v, d, "\\Change\\" + item);
                        v.Folder = item;
                        versionXML.x_FileList.x_change.Add(v);
                    }
                }
            }
        }
        private void GetFiles(XMLFileList xml, DirectoryInfo dir, string replaceFolder)
        {
            var rootFolder = new DirectoryInfo(root).Name;
            foreach (var item in dir.GetFiles())
            {
                RunDoShowInfo("检测文件:" + item.Name + "\n");
                var fileinfo = new XMLFileInfo();
                var file = new FileInfo(item.FullName);
                fileinfo.Name = item.Name;
                RunDoShowInfo("计算Hash值..\n");
                fileinfo.Hash = Utility.GetMD5Value(item.FullName);
                RunDoShowInfo("Hash值：" + fileinfo.Hash + "\n");
                var oP = rootFolder + item.FullName.Split(new string[] { rootFolder }, StringSplitOptions.RemoveEmptyEntries)[1];
                fileinfo.Address = oP;
                fileinfo.InstallPath = oP.Replace(replaceFolder, "");
                RunDoShowInfo("当前文件检测完毕！\n");
                if (versionXML.x_ProcessInfo.path == item.FullName)
                {
                    versionXML.x_ProcessInfo.path = fileinfo.InstallPath;
                }
                xml.Files.Add(fileinfo);
            }
            foreach (var item in dir.GetDirectories())
            {
                RunDoShowInfo("进入目录:" + item.FullName + "\n");
                GetFiles(xml, item, replaceFolder);
            }
        }

        void SaveFile()
        {
            if (!editorAnything)
            {
                RunDoShowInfo("未做任何更改！！");
                return;
            }
            var tmp = GlobalData.config.config_Add.configPath + ".bak";
            var tmp2 = tmp;
            while (File.Exists(tmp2))
            {
                File.Move(tmp2, tmp2 + ".bak");
                tmp2 = tmp2 + ".bak.bak";
            }

            var xmlTmp = GlobalData.config.config_Add.configPath + ".xml";
            var xmlTmp2 = xmlTmp;
            while (File.Exists(xmlTmp2))
            {
                File.Move(xmlTmp2, xmlTmp2 + ".bak");
                xmlTmp2 = xmlTmp2 + ".bak.bak";
            }

            File.Move(GlobalData.config.config_Add.configPath,tmp);
            Utility.Encryption(versionXML, GlobalData.config.config_Add.configPath);
            Utility.XMLSerialize(versionXML, GlobalData.config.config_Add.configPath + ".xml");
            Utility.ShowExplore(GlobalData.config.config_Add.configPath);
        }

        bool Ready()
        {
            if (!File.Exists(p2con_tbx.Text))
            {
                MessageBox.Show("未发现配置文件!!");
                return false;
            }
            return true;
        }

        public override void SaveConfig()
        {
            GlobalData.config.config_Add.configPath = p2con_tbx.Text;
            GlobalData.config.config_Add.backup = p2backup_cbx.Checked;
            Utility.SaveOrOpenConfig(false);
        }
        public override void Dispose()
        {
            throw new NotImplementedException();
        }
        #region UI Event
        private event Action<string> doShowInfo;
        private void SetDoShowInfo()
        {
            doShowInfo = ShowInfoInvoke;
        }
        private void RunDoShowInfo(string s)
        {
            doShowInfo?.Invoke(s);
        }
        private void ShowInfoInvoke(string s)
        {
            if (p2infor_rtb.InvokeRequired)
            {
                p2infor_rtb.Invoke(new Action<string>(ShowInfoInvoke),s);
            }
            else
            {
                p2infor_rtb.AppendText(s+"\n");
                p2infor_rtb.Select(p2infor_rtb.TextLength, 0);
                p2infor_rtb.ScrollToCaret();
            }
        }
        #endregion
    }
}
