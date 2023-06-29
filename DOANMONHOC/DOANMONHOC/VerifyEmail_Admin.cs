using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Guna.UI2.AnimatorNS;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOANMONHOC
{
    public partial class VerifyEmail_Admin : Form
    {
        private Form indexForm;
        private bool isBackButtonPressed;
        private ADMIN admin = new ADMIN();
        private IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        private IFirebaseClient client;
        private int otp;

        public VerifyEmail_Admin(Form parentForm, ADMIN admin)
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);

            this.FormClosed += new FormClosedEventHandler(FormClosed_Exit);
            indexForm = parentForm;
            isBackButtonPressed = false;
            this.admin = admin;
            otp_enter.Text = "Nhập mã OTP";
            otp_enter.ForeColor = Color.FromArgb(37, 83, 140);
            otp_enter.Enter += OTP_Enter;
            otp_enter.Leave += OTP_Leave;
        }

        void FormClosed_Exit(object sender, FormClosedEventArgs e)
        {
            if (!isBackButtonPressed)
            {
                Application.ExitThread();
            }
        }

        private async void VerifyEmail_Admin_Load(object sender, EventArgs e)
        {
            await GenerateAndSendOTP();
        }

        private async Task GenerateAndSendOTP()
        {
            Random random = new Random();
            otp = random.Next(1000, 9999);
            string from = "doannt106.n21.antt@gmail.com";
            string to = admin.Email;
            string pass = "ttwfdbubwvnyxbtz";
            string content = "Số xác nhận của bạn là: " + otp.ToString();
            content += "\r\n------------------------------------------------\r\nĐây là email tự động của hệ thống mail, vui lòng không trả lời email này.";
            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress(from);
            mail.Subject = "[VOTING APPLICATION] MÃ OTP XÁC THỰC CHO ADMIN";
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

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            string otpStr = otp_enter.Text.Trim();
            if (otpStr == otp.ToString())
            {
                string newPassword = GenerateRandomPassword();
                Thread thread = new Thread(() =>
                {
                    UpdateAdminPasswordInFirebase(newPassword).Wait();
                    SendNewPasswordToAdminEmail(newPassword);

                    BeginInvoke(new Action(() =>
                    {
                        var form = new Sign_in(indexForm);
                        form.Show();
                        isBackButtonPressed = true;
                        this.Close();
                    }));
                });
                thread.Start();
            }
            else
            {
                MessageBox.Show("Mã OTP không khớp!\nVui lòng nhập lại");
                otp_enter.Focus();
            }
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

        private async Task UpdateAdminPasswordInFirebase(string newPassword)
        {
            FirebaseResponse response = await client.GetTaskAsync("Admins/");
            Dictionary<string, ADMIN> admins = response.ResultAs<Dictionary<string, ADMIN>>();

            foreach (var admin in admins)
            {
                if (admin.Value.Email == this.admin.Email)
                {
                    admin.Value.Password = HashPassword(newPassword);
                    var updateResponse = await client.SetTaskAsync("Admins/" + admin.Key, admin.Value);
                    break;
                }
            }
        }

        private async Task SendNewPasswordToAdminEmail(string newPassword)
        {
            string from = "doannt106.n21.antt@gmail.com";
            string to = admin.Email;
            string pass = "ttwfdbubwvnyxbtz";
            string content = "Mật khẩu mới của bạn là: " + newPassword;
            content += "\r\n------------------------------------------------\r\nĐây là email tự động của hệ thống mail, vui lòng không trả lời email này.";
            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress(from);
            mail.Subject = "[VOTING APPLICATION] MẬT KHẨU MỚI CHO ADMIN";
            mail.Body = content;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);

            try
            {
                await smtp.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hashedPassword;
        }

        private async void label3_Click(object sender, EventArgs e)
        {
            await GenerateAndSendOTP();
        }
    }
}
