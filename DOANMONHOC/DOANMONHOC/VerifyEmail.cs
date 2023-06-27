using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOANMONHOC
{
    public partial class VerifyEmail : Form
    {
        public VerifyEmail(USER user)
        {
            InitializeComponent();
            this.user = user;
        }
        int otp;
        USER user = new USER();
        private Index indexForm = new Index();


        private void CenterTextInRichTextBox(RichTextBox richTextBox)
        {
            // Chọn toàn bộ văn bản trong RichTextBox
            richTextBox.SelectAll();

            // Tính toán khoảng cách
            int margin = (72 - richTextBox.GetPositionFromCharIndex(richTextBox.SelectionLength).X) / 2 - 10;

            // Đặt lề trái và lề phải cho văn bản trong RichTextBox
            richTextBox.SelectionIndent = margin;
            richTextBox.SelectionRightIndent = margin;

            // Đặt lề trên và lề dưới cho văn bản trong RichTextBox
            richTextBox.SelectionHangingIndent = margin;
            richTextBox.SelectionCharOffset = margin;

        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            CenterTextInRichTextBox(Box1);
            // Chỉ cho phép nhập số và phím điều khiển
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // Chỉ cho phép nhập duy nhất 1 kí tự số
            if (char.IsDigit(e.KeyChar) && Box1.TextLength >= 1)
            {
                e.Handled = true;
            }

            // Chuyển focus sang richTextBox2 nếu đã nhập kí tự số đầu tiên
            if (char.IsDigit(e.KeyChar) && Box1.TextLength == 0)
            {
                e.Handled = false;
                Box1.SelectionStart = 1;
                Box2.Focus();
            }
        }

        private void richTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            CenterTextInRichTextBox(Box2);
            // Chỉ cho phép nhập số và phím điều khiển

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // Chỉ cho phép nhập duy nhất 1 kí tự số
            if (char.IsDigit(e.KeyChar) && Box2.TextLength >= 1)
            {
                e.Handled = true;
            }

            // Chuyển focus sang richTextBox2 nếu đã nhập kí tự số đầu tiên
            if (char.IsDigit(e.KeyChar) && Box2.TextLength == 0)
            {
                e.Handled = false;
                Box2.SelectionStart = 1;
                Box3.Focus();
            }
        }

        private void richTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            CenterTextInRichTextBox(Box3);
            // Chỉ cho phép nhập số và phím điều khiển

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // Chỉ cho phép nhập duy nhất 1 kí tự số
            if (char.IsDigit(e.KeyChar) && Box3.TextLength >= 1)
            {
                e.Handled = true;
            }

            // Chuyển focus sang richTextBox2 nếu đã nhập kí tự số đầu tiên
            if (char.IsDigit(e.KeyChar) && Box3.TextLength == 0)
            {
                e.Handled = false;
                Box3.SelectionStart = 1;
                Box4.Focus();
            }
        }

        private void richTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            CenterTextInRichTextBox(Box4);
            // Chỉ cho phép nhập số và phím điều khiển
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // Chỉ cho phép nhập duy nhất 1 kí tự số
            if (char.IsDigit(e.KeyChar) && Box4.TextLength >= 1)
            {
                e.Handled = true;
            }

            // Chuyển focus sang richTextBox2 nếu đã nhập kí tự số đầu tiên
            if (char.IsDigit(e.KeyChar) && Box4.TextLength == 0)
            {
                e.Handled = false;
                Box4.SelectionStart = 1;
                Box4.Focus();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string otp_str = Box1.Text + Box2.Text + Box3.Text + Box4.Text;
            if (otp_str == otp.ToString())
            {
                MessageBox.Show("Bạn đã xác nhận thành công!\nVui lòng cập nhật thông tin ở form sau");
                var form = new init_user_info(user);
                form.ShowDialog();
                this.Close();
            }
        }

        private void VerifyEmail_Load(object sender, EventArgs e)
        {
            string from, to, pass, content;
            Random random = new Random();
            otp = random.Next(1000, 9999);
            from = "doannt106.n21.antt@gmail.com";
            to = user.Email;
            pass = "ttwfdbubwvnyxbtz";
            content = "Số xác nhận của bạn là: " + otp.ToString();
            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress(from);
            mail.Subject = "MÃ OTP XÁC THỰC";
            mail.Body = content;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass); //from: gmail of sender; pass: password gmail of sender
            try
            {
                smtp.Send(mail);
                MessageBox.Show("Đã gửi otp đến mail");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            VerifyEmail_Load(sender, e);
        }
    }
}
