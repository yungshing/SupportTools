using SupportTools.Follow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SupportTools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadConfig();
            InitializeFollow();
        }

        void InitializeFollow()
        {
            P1Follow();
        }
        /// <summary>
        /// 加载配置文件
        /// </summary>
        void LoadConfig()
        {
            Utility.CreateOrOpenConfig();
        }
        void P1Follow()
        {
            var f = new Follow.MadeConfigFollow();

            f.form = this;

            f.selRoot_btn = selRoot_btn;
            f.selpro_btn = selpro_btn;
            f.selconfig_btn = selconfig_btn;
            f.star_btn = star_btn;
            f.p1add_btn = p1add_btn;
            f.p1def_btn = p1def_btn;
            
            f.prefix_tbx = prefix_tbx;
            f.root_tbx = root_tbx;
            f.delpro_tbx = delpro_tbx;
            f.config_tbx = config_tbx;
            f.ftpAcc_tbx = ftpAcc_tbx;
            f.p1ftpPass_tbx = p1ftpPass_tbx;

            f.killname_rt = killname_rt;
            f.showTip_ri = showTip_ri;

            f.lock_cb = lock_cb;

            f.OnLoad();
        }
    }
}
