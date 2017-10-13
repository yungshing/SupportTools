namespace SupportTools
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.label1 = new System.Windows.Forms.Label();
            this.propath_tbx = new System.Windows.Forms.TextBox();
            this.proname_tbx = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.uptime_tbx = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.upcon_rbx = new System.Windows.Forms.RichTextBox();
            this.sure_btn = new System.Windows.Forms.Button();
            this.downloadPath_tbx = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.userName_tbx = new System.Windows.Forms.TextBox();
            this.password_tbx = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.version_tbx = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.killNams_richtxt = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.setPath_Btn = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "程序路径:";
            // 
            // propath_tbx
            // 
            this.propath_tbx.Enabled = false;
            this.propath_tbx.Location = new System.Drawing.Point(79, 13);
            this.propath_tbx.Name = "propath_tbx";
            this.propath_tbx.Size = new System.Drawing.Size(323, 21);
            this.propath_tbx.TabIndex = 1;
            // 
            // proname_tbx
            // 
            this.proname_tbx.Enabled = false;
            this.proname_tbx.Location = new System.Drawing.Point(79, 40);
            this.proname_tbx.Name = "proname_tbx";
            this.proname_tbx.ReadOnly = true;
            this.proname_tbx.Size = new System.Drawing.Size(211, 21);
            this.proname_tbx.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "程序名字:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(292, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "秒";
            // 
            // uptime_tbx
            // 
            this.uptime_tbx.Location = new System.Drawing.Point(79, 67);
            this.uptime_tbx.Name = "uptime_tbx";
            this.uptime_tbx.Size = new System.Drawing.Size(211, 21);
            this.uptime_tbx.TabIndex = 7;
            this.uptime_tbx.Text = "7200";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "更新时间:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 191);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "更新内容";
            // 
            // upcon_rbx
            // 
            this.upcon_rbx.Location = new System.Drawing.Point(12, 212);
            this.upcon_rbx.Name = "upcon_rbx";
            this.upcon_rbx.Size = new System.Drawing.Size(229, 166);
            this.upcon_rbx.TabIndex = 10;
            this.upcon_rbx.Text = "首次更新将会下载全部文件需要一定时间，请耐心等待。";
            // 
            // sure_btn
            // 
            this.sure_btn.Location = new System.Drawing.Point(408, 43);
            this.sure_btn.Name = "sure_btn";
            this.sure_btn.Size = new System.Drawing.Size(75, 23);
            this.sure_btn.TabIndex = 11;
            this.sure_btn.Text = "确定";
            this.sure_btn.UseVisualStyleBackColor = true;
            // 
            // downloadPath_tbx
            // 
            this.downloadPath_tbx.Location = new System.Drawing.Point(120, 94);
            this.downloadPath_tbx.Name = "downloadPath_tbx";
            this.downloadPath_tbx.ReadOnly = true;
            this.downloadPath_tbx.Size = new System.Drawing.Size(306, 21);
            this.downloadPath_tbx.TabIndex = 13;
            this.downloadPath_tbx.Text = "ftp://120.76.28.224/Version-C.config";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "配置文件下载路径:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 159);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "帐号：";
            // 
            // userName_tbx
            // 
            this.userName_tbx.Location = new System.Drawing.Point(46, 156);
            this.userName_tbx.Name = "userName_tbx";
            this.userName_tbx.Size = new System.Drawing.Size(163, 21);
            this.userName_tbx.TabIndex = 15;
            this.userName_tbx.Text = "ftpa";
            // 
            // password_tbx
            // 
            this.password_tbx.Location = new System.Drawing.Point(309, 156);
            this.password_tbx.Name = "password_tbx";
            this.password_tbx.Size = new System.Drawing.Size(185, 21);
            this.password_tbx.TabIndex = 17;
            this.password_tbx.Text = "123456";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(275, 159);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 16;
            this.label10.Text = "密码：";
            // 
            // version_tbx
            // 
            this.version_tbx.Location = new System.Drawing.Point(79, 121);
            this.version_tbx.Name = "version_tbx";
            this.version_tbx.Size = new System.Drawing.Size(408, 21);
            this.version_tbx.TabIndex = 19;
            this.version_tbx.Text = "V1.0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 124);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 12);
            this.label11.TabIndex = 18;
            this.label11.Text = "版本号:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(408, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "选择";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // killNams_richtxt
            // 
            this.killNams_richtxt.Location = new System.Drawing.Point(263, 212);
            this.killNams_richtxt.Name = "killNams_richtxt";
            this.killNams_richtxt.Size = new System.Drawing.Size(231, 166);
            this.killNams_richtxt.TabIndex = 21;
            this.killNams_richtxt.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(270, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "需要结束的进程名";
            // 
            // setPath_Btn
            // 
            this.setPath_Btn.Enabled = false;
            this.setPath_Btn.Location = new System.Drawing.Point(438, 92);
            this.setPath_Btn.Name = "setPath_Btn";
            this.setPath_Btn.Size = new System.Drawing.Size(47, 23);
            this.setPath_Btn.TabIndex = 23;
            this.setPath_Btn.Text = "默认";
            this.setPath_Btn.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(373, 71);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(120, 16);
            this.checkBox1.TabIndex = 24;
            this.checkBox1.Text = "保存为序列化文件";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 390);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.setPath_Btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.killNams_richtxt);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.version_tbx);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.password_tbx);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.userName_tbx);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.downloadPath_tbx);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.sure_btn);
            this.Controls.Add(this.upcon_rbx);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.uptime_tbx);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.proname_tbx);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.propath_tbx);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配置表";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox propath_tbx;
        private System.Windows.Forms.TextBox proname_tbx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox uptime_tbx;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox upcon_rbx;
        private System.Windows.Forms.Button sure_btn;
        private System.Windows.Forms.TextBox downloadPath_tbx;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox userName_tbx;
        private System.Windows.Forms.TextBox password_tbx;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox version_tbx;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox killNams_richtxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button setPath_Btn;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}