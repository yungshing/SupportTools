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
            Create();
            Add();
            Code();
            Other();
        }
        /// <summary>
        /// 加载配置文件
        /// </summary>
        void LoadConfig()
        {
            Utility.SaveOrOpenConfig(true);
        }

        /// <summary>
        /// tabControl 
        /// page1 配置文件
        /// </summary>
        void Create()
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
        /// <summary>
        /// tabControl 
        /// page2 新增
        /// </summary>
        void Add()
        {
            var follow = new AddFollow();
            follow.p2con_tbx = p2con_tbx;
            follow.p2ftpadd_tbx = p2ftpadd_tbx;

            follow.p2sel_btn = p2sel_btn;
            follow.p2start_btn = p2start_btn;

            follow.ftp_cbx = ftp_cbx;
            follow.p2base_cbx = p2base_cbx;
            follow.p2other_cbx = p2other_cbx;
            follow.p2backup_cbx = p2backup_cbx;

            follow.p2change_tlp = p2change_tlp;
            follow.p2infor_rtb = p2infor_rtb;

            follow.OnLoad();
        }
        /// <summary>
        /// tabControl 
        /// page4 解密 加密
        /// </summary>
        void Code()
        {
            CodeFollow follow = new CodeFollow();

            follow.code_source_tbx = code_source_tbx;
            follow.code_deskey_tbx = code_deskey_tbx;
            follow.code_desVI_tbx = code_desVI_tbx;
            follow.code_charKey_tbx = code_charKey_tbx;

            follow.code_scan_btn = code_scan_btn;
            follow.code_defaultdes_btn = code_defaultdes_btn;
            follow.code_confirm_btn = code_confirm_btn;

            follow.code_sel_cbx = code_sel_cbx;

            follow.OnLoad();
        }
        /// <summary>
        /// tabControl 
        /// page5 其它
        /// </summary>
        void Other()
        {
            OtherFollow follow = new OtherFollow();
            follow.o_filepath_tbx = o_filepath_tbx;
            follow.o_showhash_tbx = o_showhash_tbx;
            follow.o_xmlpath_tbx = o_xmlpath_tbx;

            follow.o_checkhash_btn = o_checkhash_btn;
            follow.o_clear_btn = o_clear_btn;
            follow.o_scan_btn = o_scan_btn;

            follow.o_showinfo_rtb = o_showinfo_rtb;

            follow.OnLoad();
        }
    }
}
