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
using System.Reflection.Metadata;
using FireSharp.Response;
using FireSharp.Config;
using FireSharp.Interfaces;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DOANMONHOC
{
    public partial class VerifyEmail : Form
    {
        string flag;
        public VerifyEmail(USER user, string flag = "")
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);
            this.user = user;
            this.flag = flag;
            otp_enter.Text = "Nhập mã OTP";
            otp_enter.ForeColor = Color.FromArgb(37, 83, 140);
            otp_enter.Enter += OTP_Enter;
            otp_enter.Leave += OTP_Leave;
        }
        int otp;
        USER user = new USER();
        private Index indexForm = new Index();
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;


        private void OTP_Enter(object sender, EventArgs e)
        {
            if (otp_enter.Text == "Nhập mã OTP")
            {
                otp_enter.Clear();
                otp_enter.ForeColor = Color.FromArgb(37, 83, 140);
            }
        }

        private void OTP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(otp_enter.Text))
            {
                otp_enter.Text = "Nhập mã OTP";
                otp_enter.ForeColor = Color.FromArgb(37, 83, 140);
            }
        }

        public static string HashPassword(string password)
        {
            // Generate a salt with a work factor of 12 (the default)
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            // Hash the password using the salt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            string otp_str = otp_enter.Text.Trim();
            if (otp_str == otp.ToString())
            {
                if (flag == "")
                {
                    MessageBox.Show("Bạn đã xác nhận thành công!\nVui lòng cập nhật thông tin ở form sau");
                    var form = new init_user_info(user);
                    form.Show();
                    this.Close();
                }
                else if (flag == "Forget")
                {
                    // Define the character sets
                    string uppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    string lowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
                    string digits = "0123456789";
                    string specialChars = "!@#$%^&*()_+";

                    // Combine the character sets into a single string
                    string allChars = uppercaseLetters + lowercaseLetters + digits + specialChars;

                    // Use a StringBuilder to build the password
                    StringBuilder passwordBuilder = new StringBuilder();
                    Random random = new Random();

                    // Add 8 random characters to the password
                    int index = random.Next(0, uppercaseLetters.Length);
                    passwordBuilder.Append(uppercaseLetters[index]);
                    index = random.Next(0, lowercaseLetters.Length);
                    passwordBuilder.Append(lowercaseLetters[index]);
                    index = random.Next(0, digits.Length);
                    passwordBuilder.Append(digits[index]);
                    index = random.Next(0, specialChars.Length);
                    passwordBuilder.Append(specialChars[index]);

                    for (int i = 0; i < 8; i++)
                    {
                        index = random.Next(0, allChars.Length);
                        passwordBuilder.Append(allChars[index]);
                    }

                    FirebaseResponse response = await client.GetTaskAsync("Users/");
                    Dictionary<string, USER> users = response.ResultAs<Dictionary<string, USER>>();
                    foreach (var user in users)
                    {
                        if (user.Value.UserName == this.user.UserName)
                        {
                            user.Value.Password = HashPassword(passwordBuilder.ToString());
                            var updateResponse = await client.SetTaskAsync("Users/" + user.Key, user.Value);
                            break;
                        }
                    }

                    string from, to, pass, content;
                    from = "doannt106.n21.antt@gmail.com";
                    to = user.Email;
                    pass = "ttwfdbubwvnyxbtz";
                    content = "Mật khẩu mới của bạn là của bạn là: " + passwordBuilder.ToString() + "\nVui lòng đổi mật khẩu sau khi đã đăng nhập để bảo mật tài khoản";
                    content += "\r\n------------------------------------------------\r\nĐây là email tự động của hệ thống mail, vui lòng không trả lời email này.";
                    MailMessage mail = new MailMessage();
                    mail.To.Add(to);
                    mail.From = new MailAddress(from);
                    mail.Subject = "[VOTING APPLICATION] MẬT KHẨU MỚI";
                    mail.Body = content;

                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    smtp.EnableSsl = true;
                    smtp.Port = 587;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(from, pass); //from: gmail of sender; pass: password gmail of sender
                    try
                    {
                        smtp.Send(mail);
                        MessageBox.Show("Bạn đã xác nhận thành công!\nĐã gửi mật khẩu mới vào email của bạn");
                        var form = new Sign_in(indexForm);
                        form.Show();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }

            }
            else
            {
                MessageBox.Show("Mã OTP không khớp!\nVui lòng nhập lại");
                otp_enter.Focus();
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
            content += "\r\n------------------------------------------------\r\nĐây là email tự động của hệ thống mail, vui lòng không trả lời email này.";
            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress(from);
            mail.Subject = "[VOTING APPLICATION] MÃ OTP XÁC THỰC";
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
