using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

              };
        }

        private void O_scan_btn_Click()
        {
            o_scan_btn.Click += (d, z) =>
              {

              };
        }
    }
}
