﻿namespace DOANMONHOC
{
    partial class Sign_in_as_Admin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sign_in_as_Admin));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            username = new TextBox();
            textBox2 = new TextBox();
            checkBox1 = new CheckBox();
            button1 = new Button();
            button2 = new Button();
            username_admin = new Guna.UI2.WinForms.Guna2TextBox();
            password_admin = new Guna.UI2.WinForms.Guna2TextBox();
            admin_sign_in = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(55, 32);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(129, 154);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(99, 207);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(660, 660);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 28F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(835, 207);
            label1.Name = "label1";
            label1.Size = new Size(324, 62);
            label1.TabIndex = 2;
            label1.Text = "Admin Login!";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.Location = new Point(847, 287);
            label2.Name = "label2";
            label2.Size = new Size(319, 20);
            label2.TabIndex = 3;
            label2.Text = "Chào mừng bạn đến với nền tảng bỏ phiếu UIT";
            // 
            // username
            // 
            username.BorderStyle = BorderStyle.FixedSingle;
            username.Location = new Point(424, 184);
            username.Name = "username";
            username.Size = new Size(334, 27);
            username.TabIndex = 4;
            // 
            // textBox2
            // 
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Location = new Point(424, 261);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(334, 27);
            textBox2.TabIndex = 4;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(867, 614);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(147, 24);
            checkBox1.TabIndex = 8;
            checkBox1.Text = "Ghi nhớ mật khẩu";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.FlatAppearance.BorderColor = SystemColors.Control;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = SystemColors.ControlText;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.MidnightBlue;
            button1.Location = new Point(1185, 611);
            button1.Name = "button1";
            button1.Size = new Size(152, 29);
            button1.TabIndex = 9;
            button1.Text = "Quên mật khẩu ?";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.BackColor = Color.MidnightBlue;
            button2.FlatAppearance.BorderColor = SystemColors.Control;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button2.ForeColor = Color.White;
            button2.Location = new Point(424, 380);
            button2.Name = "button2";
            button2.Size = new Size(334, 31);
            button2.TabIndex = 10;
            button2.Text = "Đăng nhập";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click_1;
            // 
            // username_admin
            // 
            username_admin.BorderColor = Color.FromArgb(37, 83, 140);
            username_admin.BorderRadius = 30;
            username_admin.CustomizableEdges = customizableEdges1;
            username_admin.DefaultText = "";
            username_admin.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            username_admin.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            username_admin.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            username_admin.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            username_admin.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            username_admin.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            username_admin.ForeColor = Color.FromArgb(37, 83, 140);
            username_admin.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            username_admin.Location = new Point(847, 407);
            username_admin.Name = "username_admin";
            username_admin.PasswordChar = '\0';
            username_admin.PlaceholderForeColor = Color.FromArgb(37, 83, 140);
            username_admin.PlaceholderText = "";
            username_admin.SelectedText = "";
            username_admin.ShadowDecoration.CustomizableEdges = customizableEdges2;
            username_admin.Size = new Size(505, 64);
            username_admin.TabIndex = 11;
            username_admin.Enter += username_admin_Enter;
            username_admin.Leave += username_admin_Leave;
            // 
            // password_admin
            // 
            password_admin.BorderColor = Color.FromArgb(37, 83, 140);
            password_admin.BorderRadius = 30;
            password_admin.CustomizableEdges = customizableEdges3;
            password_admin.DefaultText = "";
            password_admin.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            password_admin.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            password_admin.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            password_admin.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            password_admin.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            password_admin.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            password_admin.ForeColor = Color.FromArgb(37, 83, 140);
            password_admin.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            password_admin.Location = new Point(847, 521);
            password_admin.Name = "password_admin";
            password_admin.PasswordChar = '\0';
            password_admin.PlaceholderText = "";
            password_admin.SelectedText = "";
            password_admin.ShadowDecoration.CustomizableEdges = customizableEdges4;
            password_admin.Size = new Size(505, 64);
            password_admin.TabIndex = 11;
            password_admin.Enter += password_admin_Enter;
            password_admin.Leave += password_admin_Leave;
            // 
            // admin_sign_in
            // 
            admin_sign_in.BorderRadius = 30;
            admin_sign_in.CustomizableEdges = customizableEdges5;
            admin_sign_in.DisabledState.BorderColor = Color.DarkGray;
            admin_sign_in.DisabledState.CustomBorderColor = Color.DarkGray;
            admin_sign_in.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            admin_sign_in.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            admin_sign_in.FillColor = Color.FromArgb(37, 83, 140);
            admin_sign_in.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            admin_sign_in.ForeColor = Color.White;
            admin_sign_in.Location = new Point(847, 741);
            admin_sign_in.Name = "admin_sign_in";
            admin_sign_in.ShadowDecoration.CustomizableEdges = customizableEdges6;
            admin_sign_in.Size = new Size(505, 60);
            admin_sign_in.TabIndex = 12;
            admin_sign_in.Text = "Đăng nhập";
            // 
            // Sign_in_as_Admin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1422, 977);
            Controls.Add(admin_sign_in);
            Controls.Add(password_admin);
            Controls.Add(username_admin);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(checkBox1);
            Controls.Add(textBox2);
            Controls.Add(username);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Name = "Sign_in_as_Admin";
            Text = "Sign_in_as_Admin";
            Load += Sign_in_as_Admin_Load_1;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label label1;
        private Label label2;
        private TextBox username;
        private TextBox textBox2;
        private CheckBox checkBox1;
        private Button button1;
        private Button button2;
        private Guna.UI2.WinForms.Guna2TextBox username_admin;
        private Guna.UI2.WinForms.Guna2TextBox password_admin;
        private Guna.UI2.WinForms.Guna2Button admin_sign_in;
    }
}