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
        USER user = new USER();
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        private string flag;
        private int otp;
        private readonly Form indexForm;
        private bool isBackButtonPressed;

        public VerifyEmail(Form parentForm, USER user, string flag = "")
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);
            indexForm = parentForm;
            FormClosed += FormClosed_Exit;
            this.user = user;
            this.flag = flag;
            otp_enter.Text = "Nhập mã OTP";
            otp_enter.ForeColor = Color.FromArgb(37, 83, 140);
            otp_enter.Enter += OTP_Enter;
            otp_enter.Leave += OTP_Leave;
        }

        private void FormClosed_Exit(object sender, FormClosedEventArgs e)
        {
            if (!isBackButtonPressed)
            {
                Application.ExitThread();
            }
        }

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
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
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
                    ConfirmSuccessAndProceedToInitUserInfo();
                }
                else if (flag == "Forget")
                {
                    Thread thread = new Thread(() =>
                    {
                        ResetPasswordAndSendEmail();
                    });
                    thread.Start();
                }
            }
            else
            {
                MessageBox.Show("Mã OTP không khớp!\nVui lòng nhập lại");
                otp_enter.Focus();
            }
        }

        private void ConfirmSuccessAndProceedToInitUserInfo()
        {
            MessageBox.Show("Bạn đã xác nhận thành công!\nVui lòng cập nhật thông tin ở form sau");
            var form = new init_user_info(user);
            form.Show();
            this.Close();
        }

        // NEED FIXED TO nhanh hơn
        private async Task ResetPasswordAndSendEmail()
        {
            string newPassword = GenerateRandomPassword();
            await UpdateUserPasswordInFirebase(newPassword);
            string emailContent = "Mật khẩu mới của bạn là của bạn là: " + newPassword + "\nVui lòng đổi mật khẩu sau khi đã đăng nhập để bảo mật tài khoản";
            await SendEmail(user.Email, "[VOTING APPLICATION] MẬT KHẨU MỚI", emailContent);
            BeginInvoke(new Action(() =>
            {
                var form = new Sign_in(indexForm);
                form.Show();
                isBackButtonPressed = true;
                this.Close();
            }));
        }

        private string GenerateRandomPassword()
        {
            string uppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
            string digits = "0123456789";
            string specialChars = "!@#$%^&*()_+";
            string allChars = uppercaseLetters + lowercaseLetters + digits + specialChars;

            StringBuilder passwordBuilder = new StringBuilder();
            Random random = new Random();

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

            return passwordBuilder.ToString();
        }

        private async Task UpdateUserPasswordInFirebase(string newPassword)
        {
            FirebaseResponse response = await client.GetTaskAsync("Users/");
            Dictionary<string, USER> users = response.ResultAs<Dictionary<string, USER>>();
            foreach (var user in users)
            {
                if (user.Value.UserName == this.user.UserName)
                {
                    user.Value.Password = HashPassword(newPassword);
                    var updateResponse = await client.SetTaskAsync("Users/" + user.Key, user.Value);
                    break;
                }
            }
        }

        private async Task SendEmail(string to, string subject, string content)
        {
            string from = "doannt106.n21.antt@gmail.com";
            string pass = "ttwfdbubwvnyxbtz";
            content += "\r\n------------------------------------------------\r\nĐây là email tự động của hệ thống mail, vui lòng không trả lời email này.";
            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress(from);
            mail.Subject = subject;
            mail.Body = content;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);

            try
            {
                await Task.Run(() => smtp.Send(mail));
            }
            catch (Exception ex)
            {
            }
        }

        private async void VerifyEmail_Load(object sender, EventArgs e)
        {
            Random random = new Random();
            otp = random.Next(1000, 9999);
            string content = "Số xác nhận của bạn là: " + otp.ToString();
            await SendEmail(user.Email, "[VOTING APPLICATION] MÃ OTP XÁC THỰC", content);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            VerifyEmail_Load(sender, e);
        }
    }
}
