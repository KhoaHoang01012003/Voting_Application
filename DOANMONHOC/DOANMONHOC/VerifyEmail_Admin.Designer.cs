﻿namespace DOANMONHOC
{
    partial class VerifyEmail_Admin
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerifyEmail_Admin));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            otp_enter = new Guna.UI2.WinForms.Guna2TextBox();
            pictureBox2 = new PictureBox();
            label3 = new Label();
            label2 = new Label();
            guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            pictureBox1 = new PictureBox();
            textBox1 = new TextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // otp_enter
            // 
            otp_enter.BorderColor = Color.FromArgb(37, 83, 140);
            otp_enter.BorderRadius = 30;
            otp_enter.CustomizableEdges = customizableEdges1;
            otp_enter.DefaultText = "";
            otp_enter.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            otp_enter.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            otp_enter.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            otp_enter.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            otp_enter.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            otp_enter.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            otp_enter.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            otp_enter.Location = new Point(868, 542);
            otp_enter.Name = "otp_enter";
            otp_enter.PasswordChar = '\0';
            otp_enter.PlaceholderText = "";
            otp_enter.SelectedText = "";
            otp_enter.ShadowDecoration.CustomizableEdges = customizableEdges2;
            otp_enter.Size = new Size(422, 64);
            otp_enter.TabIndex = 32;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(88, 71);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(129, 154);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 31;
            pictureBox2.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 20F, FontStyle.Underline, GraphicsUnit.Point);
            label3.ForeColor = Color.FromArgb(37, 83, 140);
            label3.Location = new Point(1140, 765);
            label3.Name = "label3";
            label3.Size = new Size(194, 46);
            label3.TabIndex = 30;
            label3.Text = "Gửi lại ngay";
            label3.Click += label3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(883, 776);
            label2.Name = "label2";
            label2.Size = new Size(251, 35);
            label2.TabIndex = 29;
            label2.Text = "Chưa nhận được mã?";
            // 
            // guna2Button1
            // 
            guna2Button1.BorderRadius = 30;
            guna2Button1.CustomizableEdges = customizableEdges3;
            guna2Button1.DisabledState.BorderColor = Color.DarkGray;
            guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button1.FillColor = Color.FromArgb(37, 83, 140);
            guna2Button1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            guna2Button1.ForeColor = Color.White;
            guna2Button1.Location = new Point(868, 675);
            guna2Button1.Margin = new Padding(2);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2Button1.Size = new Size(422, 64);
            guna2Button1.TabIndex = 28;
            guna2Button1.Text = "Đăng ký";
            guna2Button1.Click += guna2Button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.Verify_Email2;
            pictureBox1.Location = new Point(133, 234);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(713, 671);
            pictureBox1.TabIndex = 27;
            pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.ControlLightLight;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 21F, FontStyle.Regular, GraphicsUnit.Pixel);
            textBox1.ForeColor = Color.FromArgb(37, 83, 140);
            textBox1.Location = new Point(883, 424);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(387, 69);
            textBox1.TabIndex = 26;
            textBox1.Text = "Nhập mã đã được gửi cho bạn ở địa chỉ email trên";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 55F, FontStyle.Bold, GraphicsUnit.Pixel);
            label1.ForeColor = Color.FromArgb(37, 83, 140);
            label1.Location = new Point(868, 347);
            label1.Name = "label1";
            label1.Size = new Size(452, 74);
            label1.TabIndex = 25;
            label1.Text = "Đã gửi tin nhắn!";
            // 
            // VerifyEmail_Admin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1422, 977);
            Controls.Add(otp_enter);
            Controls.Add(pictureBox2);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(guna2Button1);
            Controls.Add(pictureBox1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "VerifyEmail_Admin";
            StartPosition = FormStartPosition.CenterParent;
            Text = "VerifyEmail_Admin";
            Load += VerifyEmail_Admin_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox otp_enter;
        private PictureBox pictureBox2;
        private Label label3;
        private Label label2;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private PictureBox pictureBox1;
        private TextBox textBox1;
        private Label label1;
    }
}