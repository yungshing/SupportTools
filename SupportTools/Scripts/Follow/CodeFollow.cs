using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
namespace SupportTools.Follow
{
    public class CodeFollow : BaseFollow
    {
        public TextBox code_source_tbx;
        public TextBox code_deskey_tbx;
        public TextBox code_desVI_tbx;
        public TextBox code_charKey_tbx;

        public Button code_scan_btn;
        public Button code_defaultdes_btn;
        public Button code_confirm_btn;

        public ComboBox code_sel_cbx;

        ChooseType chooseType = ChooseType.Null;

        public override void OnLoad()
        {
            LoadConfig();
            Code_source_tbx_Event();
            Code_scan_btn_Click();
            Code_defaultdes_btn_Click();
            Code_confirm_btn_Click();
            Code_sel_cbx_Event();
            code_sel_cbx.SelectedIndex = 1;
        }

        void Code_source_tbx_Event()
        {
            code_source_tbx.DragEnter += (d, z) =>
            {
                var n = ((System.Array)z.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                if (n.EndsWith(".config") || n.EndsWith(".xml"))
                {
                    z.Effect = DragDropEffects.Link;
                }
            };
            code_source_tbx.DragDrop += (d, z) =>
            {
                var n = ((System.Array)z.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                if (n.EndsWith(".config") || n.EndsWith(".xml"))
                {
                    code_source_tbx.Text = n;
                }
            };
        }

        void Code_scan_btn_Click()
        {
            code_scan_btn.Click += (d, z) =>
              {
                  OpenFileDialog o = new OpenFileDialog();
                  o.Filter = "配置文件|*.config";
                  if (o.ShowDialog()== DialogResult.OK)
                  {
                      code_source_tbx.Text = o.FileName;
                  }
              };
        }

        void Code_defaultdes_btn_Click()
        {
            code_defaultdes_btn.Click += (d, z) =>
              {
                  code_deskey_tbx.Text = "ЁЃ؁ࠆ";
                  code_desVI_tbx.Text = "܊Č̄ଂ";
                  code_charKey_tbx.Text = "z";
              };
        }

        void Code_confirm_btn_Click()
        {
            code_confirm_btn.Click += (d, z) =>
              {
                  if (!Ready())
                  {
                      return;
                  }
                  if (!code())
                  {
                      MessageBox.Show("解密失败");
                  }
              };
        }

        void Code_sel_cbx_Event()
        {
            code_sel_cbx.SelectedIndexChanged += (d, z) =>
              {
                  chooseType = (ChooseType)(code_sel_cbx.SelectedIndex + 1);
              };
        }

        bool Ready()
        {
            if (!File.Exists(code_source_tbx.Text))
            {
                MessageBox.Show("未发现文件");
                return false;
            }
            return true;
        }

        bool code()
        {
            switch(chooseType)
            {
                case ChooseType.DESDE:return DESDecode();
                case ChooseType.DESEN:return DESEncryption();
                case ChooseType.SIMPLEDE:return SimpleDecode();
                case ChooseType.SIMPLEEN:return SimpleEncryption();
            }
            return false;
        }
        /// <summary>
        /// des解密
        /// </summary>
        /// <returns></returns>
        bool DESDecode()
        {
            try
            {
                var key = Encoding.Unicode.GetBytes(code_deskey_tbx.Text);
                var iv = Encoding.Unicode.GetBytes(code_desVI_tbx.Text);
                var sour = Utility.DESDecode(code_source_tbx.Text,key,iv);
                var p = SelectSavePath();
                if (p != null)
                {
                    sour.Save(p);
                    Utility.ShowExplore(p);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        bool DESEncryption()
        {
            try
            {
                string s = "";
                using (var sr = new StreamReader(code_source_tbx.Text))
                {
                    s = sr.ReadToEnd();
                }
                var p = SelectSavePath("config文件(*.config)|*.config|(所有文件(*.*)|*.*)", "new file");
                if (p != null)
                {
                    var key = Encoding.Unicode.GetBytes(code_deskey_tbx.Text);
                    var iv = Encoding.Unicode.GetBytes(code_desVI_tbx.Text);
                    using (var des = new DESCryptoServiceProvider { Key = key, IV = iv })
                    {
                        using (var fs = new FileStream(p, FileMode.Create))
                        {
                            using (var cs = new CryptoStream(fs, des.CreateEncryptor(), CryptoStreamMode.Write))
                            {
                                var byt = Encoding.UTF8.GetBytes(s);
                                cs.Write(byt, 0, byt.Length);
                                cs.FlushFinalBlock();
                            }
                        }
                    }
                    Utility.ShowExplore(p);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        bool SimpleDecode()
        {
            try
            {
                var v = Utility.Decode(code_source_tbx.Text, 'z');
                var p = SelectSavePath();
                if (p != null)
                {
                    v.Save(p);
                    Utility.ShowExplore(p);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        bool SimpleEncryption()
        {
            try
            {
                string s = "";
                using (var sr = new StreamReader(code_source_tbx.Text))
                {
                    s = sr.ReadToEnd();
                }
                var c = s.ToCharArray();
                for (int i = 0; i < c.Length; i++)
                {
                    c[i] += 'z';
                }
                var path = SelectSavePath("config文件(*.config)|*.config|(所有文件(*.*)|*.*)", "new file");
                if (path != null)
                {
                    using (var fs = new FileStream(path, FileMode.Create))
                    {
                        using (var sw = new StreamWriter(fs))
                        {
                            sw.Write(c);
                        }
                    }
                    Utility.ShowExplore(path);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override void LoadConfig()
        {
            code_deskey_tbx.Text = GlobalData.config.config_Code.desKey;
            code_desVI_tbx.Text = GlobalData.config.config_Code.desVI;
            code_charKey_tbx.Text = GlobalData.config.config_Code.charKey;
        }

        public override void SaveConfig()
        {

            GlobalData.config.config_Code.desKey = code_deskey_tbx.Text;
            GlobalData.config.config_Code.desVI = code_desVI_tbx.Text;
            GlobalData.config.config_Code.charKey = code_charKey_tbx.Text;

            Utility.SaveOrOpenConfig(false);
        }

        string SelectSavePath(string filter = "XML文件(*.xml)|*.xml", string fileName = "new file.xml")
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = filter;
            sfd.FileName = fileName;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                return sfd.FileName;
            }
            return null;
        }
        public enum ChooseType
        {
            Null=0,
            DESEN,
            DESDE,
            SIMPLEEN,
            SIMPLEDE,
        }
    }
}
