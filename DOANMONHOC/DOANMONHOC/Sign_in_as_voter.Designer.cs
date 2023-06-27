namespace DOANMONHOC
{
    partial class Sign_in
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sign_in));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            textBox2 = new TextBox();
            pictureBox2 = new PictureBox();
            checkBox1 = new CheckBox();
            button1 = new Button();
            username = new Guna.UI2.WinForms.Guna2TextBox();
            password = new Guna.UI2.WinForms.Guna2TextBox();
            sign_in_button = new Guna.UI2.WinForms.Guna2Button();
            Sign_in_as_Admin_button = new Button();
            guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(100, 208);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(660, 660);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 28F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(835, 154);
            label1.Name = "label1";
            label1.Size = new Size(441, 62);
            label1.TabIndex = 2;
            label1.Text = "Chào mừng trở lại!";
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.ControlLightLight;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBox2.ForeColor = Color.Black;
            textBox2.Location = new Point(847, 234);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(383, 57);
            textBox2.TabIndex = 3;
            textBox2.Text = "Đăng nhập và thực hiện bỏ phiếu trên hệ thống cho ứng cử viên mà bạn muốn.";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(55, 32);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(129, 154);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(867, 564);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(147, 24);
            checkBox1.TabIndex = 7;
            checkBox1.Text = "Ghi nhớ mật khẩu";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.FlatAppearance.BorderColor = SystemColors.Control;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = SystemColors.ControlText;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.FromArgb(37, 83, 140);
            button1.Location = new Point(1185, 559);
            button1.Name = "button1";
            button1.Size = new Size(152, 29);
            button1.TabIndex = 8;
            button1.Text = "Quên mật khẩu ?";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // username
            // 
            username.BorderColor = Color.FromArgb(37, 83, 140);
            username.BorderRadius = 30;
            username.CustomizableEdges = customizableEdges1;
            username.DefaultText = "";
            username.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            username.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            username.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            username.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            username.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            username.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            username.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            username.Location = new Point(847, 354);
            username.Name = "username";
            username.PasswordChar = '\0';
            username.PlaceholderText = "";
            username.SelectedText = "";
            username.ShadowDecoration.CustomizableEdges = customizableEdges2;
            username.Size = new Size(505, 64);
            username.TabIndex = 11;
            // 
            // password
            // 
            password.BorderColor = Color.FromArgb(37, 83, 140);
            password.BorderRadius = 30;
            password.CustomizableEdges = customizableEdges3;
            password.DefaultText = "";
            password.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            password.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            password.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            password.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            password.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            password.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            password.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            password.Location = new Point(847, 468);
            password.Name = "password";
            password.PasswordChar = '\0';
            password.PlaceholderText = "";
            password.SelectedText = "";
            password.ShadowDecoration.CustomizableEdges = customizableEdges4;
            password.Size = new Size(505, 64);
            password.TabIndex = 12;
            // 
            // sign_in_button
            // 
            sign_in_button.BorderRadius = 30;
            sign_in_button.CustomizableEdges = customizableEdges5;
            sign_in_button.DisabledState.BorderColor = Color.DarkGray;
            sign_in_button.DisabledState.CustomBorderColor = Color.DarkGray;
            sign_in_button.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            sign_in_button.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            sign_in_button.FillColor = Color.FromArgb(37, 83, 140);
            sign_in_button.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            sign_in_button.ForeColor = Color.White;
            sign_in_button.Location = new Point(847, 689);
            sign_in_button.Name = "sign_in_button";
            sign_in_button.ShadowDecoration.CustomizableEdges = customizableEdges6;
            sign_in_button.Size = new Size(505, 60);
            sign_in_button.TabIndex = 13;
            sign_in_button.Text = "Đăng nhập";
            sign_in_button.Click += sign_in_button_Click;
            // 
            // Sign_in_as_Admin_button
            // 
            Sign_in_as_Admin_button.BackColor = Color.Transparent;
            Sign_in_as_Admin_button.FlatAppearance.BorderColor = Color.White;
            Sign_in_as_Admin_button.FlatAppearance.BorderSize = 0;
            Sign_in_as_Admin_button.FlatAppearance.MouseDownBackColor = SystemColors.ControlText;
            Sign_in_as_Admin_button.FlatStyle = FlatStyle.Flat;
            Sign_in_as_Admin_button.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            Sign_in_as_Admin_button.ForeColor = Color.FromArgb(37, 83, 140);
            Sign_in_as_Admin_button.Location = new Point(922, 755);
            Sign_in_as_Admin_button.Name = "Sign_in_as_Admin_button";
            Sign_in_as_Admin_button.Size = new Size(354, 36);
            Sign_in_as_Admin_button.TabIndex = 14;
            Sign_in_as_Admin_button.Text = "Đăng nhập với tư cách Admin";
            Sign_in_as_Admin_button.UseVisualStyleBackColor = true;
            Sign_in_as_Admin_button.Click += Sign_in_as_Admin_button_Click;
            // 
            // guna2Button1
            // 
            guna2Button1.CustomizableEdges = customizableEdges7;
            guna2Button1.DisabledState.BorderColor = Color.DarkGray;
            guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            guna2Button1.ForeColor = Color.White;
            guna2Button1.Location = new Point(1306, 32);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges8;
            guna2Button1.Size = new Size(85, 45);
            guna2Button1.TabIndex = 15;
            guna2Button1.Text = "Back";
            guna2Button1.Click += guna2Button1_Click;
            // 
            // Sign_in
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1422, 977);
            Controls.Add(guna2Button1);
            Controls.Add(Sign_in_as_Admin_button);
            Controls.Add(sign_in_button);
            Controls.Add(password);
            Controls.Add(username);
            Controls.Add(button1);
            Controls.Add(checkBox1);
            Controls.Add(pictureBox2);
            Controls.Add(textBox2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "Sign_in";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sign in";
            Load += Sign_in_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pictureBox1;
        private Label label1;
        private TextBox textBox2;
        private PictureBox pictureBox2;
        private CheckBox checkBox1;
        private Button button1;
        private Guna.UI2.WinForms.Guna2TextBox username;
        private Guna.UI2.WinForms.Guna2TextBox password;
        private Guna.UI2.WinForms.Guna2Button sign_in_button;
        private Button Sign_in_as_Admin_button;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
    }
}