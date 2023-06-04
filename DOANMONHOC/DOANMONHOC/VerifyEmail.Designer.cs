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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pictureBox1 = new PictureBox();
            textBox1 = new TextBox();
            label1 = new Label();
            richTextBox4 = new RichTextBox();
            richTextBox3 = new RichTextBox();
            richTextBox2 = new RichTextBox();
            richTextBox1 = new RichTextBox();
            guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
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
            textBox1.ForeColor = Color.MidnightBlue;
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
            label1.ForeColor = Color.MidnightBlue;
            label1.Location = new Point(835, 308);
            label1.Name = "label1";
            label1.Size = new Size(452, 74);
            label1.TabIndex = 12;
            label1.Text = "Đã gửi tin nhắn!";
            // 
            // richTextBox4
            // 
            richTextBox4.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBox4.Location = new Point(1147, 503);
            richTextBox4.Name = "richTextBox4";
            richTextBox4.Size = new Size(72, 72);
            richTextBox4.TabIndex = 18;
            richTextBox4.Text = "";
            richTextBox4.KeyPress += richTextBox4_KeyPress;
            // 
            // richTextBox3
            // 
            richTextBox3.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBox3.Location = new Point(1056, 503);
            richTextBox3.Name = "richTextBox3";
            richTextBox3.Size = new Size(72, 72);
            richTextBox3.TabIndex = 16;
            richTextBox3.Text = "";
            richTextBox3.KeyPress += richTextBox3_KeyPress;
            // 
            // richTextBox2
            // 
            richTextBox2.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBox2.Location = new Point(957, 503);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(72, 72);
            richTextBox2.TabIndex = 15;
            richTextBox2.Text = "";
            richTextBox2.KeyPress += richTextBox2_KeyPress;
            // 
            // richTextBox1
            // 
            richTextBox1.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBox1.Location = new Point(856, 503);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(72, 72);
            richTextBox1.TabIndex = 14;
            richTextBox1.Text = "";
            richTextBox1.KeyPress += richTextBox1_KeyPress;
            // 
            // guna2Button1
            // 
            guna2Button1.BorderRadius = 30;
            guna2Button1.CustomizableEdges = customizableEdges1;
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
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Button1.Size = new Size(422, 64);
            guna2Button1.TabIndex = 20;
            guna2Button1.Text = "Đăng ký";
            guna2Button1.Click += guna2Button1_Click;
            // 
            // VerifyEmail
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1422, 977);
            Controls.Add(guna2Button1);
            Controls.Add(pictureBox1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(richTextBox4);
            Controls.Add(richTextBox3);
            Controls.Add(richTextBox2);
            Controls.Add(richTextBox1);
            Name = "VerifyEmail";
            Text = "VerifyEmail";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private TextBox textBox1;
        private Label label1;
        private RichTextBox richTextBox4;
        private RichTextBox richTextBox3;
        private RichTextBox richTextBox2;
        private RichTextBox richTextBox1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
    }
}