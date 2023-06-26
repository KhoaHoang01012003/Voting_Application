namespace DOANMONHOC
{
    partial class VerifyEmail
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pictureBox1 = new PictureBox();
            textBox1 = new TextBox();
            label1 = new Label();
            Box4 = new RichTextBox();
            Box3 = new RichTextBox();
            Box2 = new RichTextBox();
            Box1 = new RichTextBox();
            guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.Verify_Email2;
            pictureBox1.Location = new Point(100, 195);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(713, 671);
            pictureBox1.TabIndex = 19;
            pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.ControlLightLight;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 21F, FontStyle.Regular, GraphicsUnit.Pixel);
            textBox1.ForeColor = Color.FromArgb(37, 83, 140);
            textBox1.Location = new Point(850, 385);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(387, 69);
            textBox1.TabIndex = 13;
            textBox1.Text = "Nhập mã đã được gửi cho bạn ở địa chỉ email trên";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 55F, FontStyle.Bold, GraphicsUnit.Pixel);
            label1.ForeColor = Color.FromArgb(37, 83, 140);
            label1.Location = new Point(835, 308);
            label1.Name = "label1";
            label1.Size = new Size(452, 74);
            label1.TabIndex = 12;
            label1.Text = "Đã gửi tin nhắn!";
            // 
            // Box4
            // 
            Box4.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            Box4.Location = new Point(1147, 503);
            Box4.Multiline = false;
            Box4.Name = "Box4";
            Box4.Size = new Size(72, 72);
            Box4.TabIndex = 18;
            Box4.Text = "";
            Box4.KeyPress += richTextBox4_KeyPress;
            // 
            // Box3
            // 
            Box3.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            Box3.Location = new Point(1056, 503);
            Box3.Multiline = false;
            Box3.Name = "Box3";
            Box3.Size = new Size(72, 72);
            Box3.TabIndex = 16;
            Box3.Text = "";
            Box3.KeyPress += richTextBox3_KeyPress;
            // 
            // Box2
            // 
            Box2.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            Box2.Location = new Point(957, 503);
            Box2.Multiline = false;
            Box2.Name = "Box2";
            Box2.Size = new Size(72, 72);
            Box2.TabIndex = 15;
            Box2.Text = "";
            Box2.KeyPress += richTextBox2_KeyPress;
            // 
            // Box1
            // 
            Box1.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            Box1.Location = new Point(856, 503);
            Box1.Multiline = false;
            Box1.Name = "Box1";
            Box1.Size = new Size(72, 72);
            Box1.TabIndex = 14;
            Box1.Text = "";
            Box1.KeyPress += richTextBox1_KeyPress;
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
            guna2Button1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            guna2Button1.ForeColor = Color.White;
            guna2Button1.Location = new Point(835, 636);
            guna2Button1.Margin = new Padding(2);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2Button1.Size = new Size(422, 64);
            guna2Button1.TabIndex = 20;
            guna2Button1.Text = "Đăng ký";
            guna2Button1.Click += guna2Button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(850, 737);
            label2.Name = "label2";
            label2.Size = new Size(251, 35);
            label2.TabIndex = 21;
            label2.Text = "Chưa nhận được mã?";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 20F, FontStyle.Underline, GraphicsUnit.Point);
            label3.ForeColor = Color.FromArgb(37, 83, 140);
            label3.Location = new Point(1107, 726);
            label3.Name = "label3";
            label3.Size = new Size(194, 46);
            label3.TabIndex = 22;
            label3.Text = "Gửi lại ngay";
            label3.Click += label3_Click;
            // 
            // VerifyEmail
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1422, 977);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(guna2Button1);
            Controls.Add(pictureBox1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(Box4);
            Controls.Add(Box3);
            Controls.Add(Box2);
            Controls.Add(Box1);
            Name = "VerifyEmail";
            Text = "VerifyEmail";
            Load += VerifyEmail_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private TextBox textBox1;
        private Label label1;
        private RichTextBox Box4;
        private RichTextBox Box3;
        private RichTextBox Box2;
        private RichTextBox Box1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Label label2;
        private Label label3;
    }
}