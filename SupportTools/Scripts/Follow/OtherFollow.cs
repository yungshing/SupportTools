using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace SupportTools.Follow
{
    public class OtherFollow : BaseFollow
    {
        public TextBox o_filepath_tbx;
        public TextBox o_showhash_tbx;
        public TextBox o_xmlpath_tbx;

        public Button o_checkhash_btn;
        public Button o_clear_btn;
        public Button o_scan_btn;

        public RichTextBox o_showinfo_rtb;

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override void LoadConfig()
        {
            throw new NotImplementedException();
        }

        public override void OnLoad()
        {
            O_filepath_tbx_Event();
            O_xmlpath_tbx_Event();
            O_checkhash_btn_Click();
            O_clear_btn_Click();
            O_scan_btn_Click();

            SetDoShowCheckInfo(DoShowCheckInfo);
        }

        public override void SaveConfig()
        {
            throw new NotImplementedException();
        }

        private void O_filepath_tbx_Event()
        {
            o_filepath_tbx.DragEnter += (d, z) =>
              {
                  var n = ((System.Array)z.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                  if (File.Exists(n))
                  {
                      z.Effect = DragDropEffects.Link;
                  }
              };
            o_filepath_tbx.DragDrop += (d, z) =>
            {
                var n = ((System.Array)z.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                if (File.Exists(n))
                {
                    o_filepath_tbx.Text = n;
                }
            };
        }
        private void O_xmlpath_tbx_Event()
        {
            o_xmlpath_tbx.DragEnter += (d, z) =>
            {
                var n = ((System.Array)z.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                if (Directory.Exists(n))
                {
                    z.Effect = DragDropEffects.Link;
                }
            };
            o_xmlpath_tbx.DragDrop += (d, z) =>
            {
                var n = ((System.Array)z.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                if (Directory.Exists(n))
                {
                    o_xmlpath_tbx.Text = n;
                }
            };
        }

        private void O_checkhash_btn_Click()
        {
            o_checkhash_btn.Click += (d, z) =>
              {
                  if (File.Exists(o_filepath_tbx.Text))
                  {
                      o_showhash_tbx.Text = Utility.GetMD5Value(o_filepath_tbx.Text);
                  }
              };
        }

        private void O_clear_btn_Click()
        {
            o_clear_btn.Click += (d, z) =>
              {
                  if (Directory.Exists(o_xmlpath_tbx.Text))
                  {
                      Thread t = new Thread(()=>
                      {
                          var dir = new DirectoryInfo(o_xmlpath_tbx.Text);
                          int count = 0;
                          ClearIFrame(dir, ref count);
                          RunDoShowCheckInfo("修改文件完成\n");
                          RunDoShowCheckInfo("共计修改" + count.ToString() + "个文件\n");
                      });
                      t.Start();
                  }
              };
        }

        private void O_scan_btn_Click()
        {
            o_scan_btn.Click += (d, z) =>
              {
                  FolderBrowserDialog f = new FolderBrowserDialog();
                  if (f.ShowDialog()== DialogResult.OK)
                  {
                      o_xmlpath_tbx.Text = f.SelectedPath;
                  }
              };
        }
        /// <summary>
        /// 清除xml文件中追加的IFrame节点
        /// </summary>
        void ClearIFrame(DirectoryInfo d,ref int count)
        {
            foreach (var item in d.GetFiles())
            {
                if (item.FullName.ToLower().EndsWith(".xml"))
                {
                    var list = new List<string>();
                    bool needEditor = false;
                    using (var sr = new StreamReader(item.FullName))
                    {
                        var txt = sr.ReadLine();
                        while(txt != null)
                        {
                            if (txt.Contains("Photo.scr"))
                            {
                                needEditor = true;
                                count++;
                                break;
                            }
                            else
                            {
                                list.Add(txt);
                            }
                            txt = sr.ReadLine();
                        }
                    }
                    if (needEditor)
                    {
                        RunDoShowCheckInfo("修改文件：" + item.Name);
                        File.Delete(item .FullName);
                        using (var sw = new StreamWriter(item.FullName))
                        {
                            foreach (var item1 in list)
                            {
                                sw.WriteLine(item1);
                            }
                        }
                    }
                }
            }

            foreach (var item in d.GetDirectories())
            {
                ClearIFrame(item,ref count);
            }
        }

        #region UI Event
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
            if (o_showinfo_rtb.InvokeRequired)
            {
                o_showinfo_rtb.Invoke(new Action<string>(DoShowCheckInfo), s);
            }
            else
            {
                o_showinfo_rtb.AppendText(s);
                o_showinfo_rtb.Select(o_showinfo_rtb.TextLength, 0);
                o_showinfo_rtb.ScrollToCaret();
            }
        }
        #endregion
    }
}
