﻿using FireSharp.Config;
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

        private void VerifyEmail_Admin_Load(object sender, EventArgs e)
        {
            string from, to, pass, content;
            Random random = new Random();
            otp = random.Next(1000, 9999);
            from = "doannt106.n21.antt@gmail.com";
            to = admin.Email;
            pass = "ttwfdbubwvnyxbtz";
            content = "Số xác nhận của bạn là: " + otp.ToString();
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
        int otp;
        ADMIN admin = new ADMIN();
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

            FirebaseResponse response = await client.GetTaskAsync("Admins/");
            Dictionary<string, ADMIN> users = response.ResultAs<Dictionary<string, ADMIN>>();
            foreach (var user in users)
            {
                if (user.Value.Email == this.admin.Email)
                {
                    user.Value.Password = HashPassword(passwordBuilder.ToString());
                    var updateResponse = await client.SetTaskAsync("Admins/" + user.Key, user.Value);
                    break;
                }
            }

            string from, to, pass, content;
            from = "doannt106.n21.antt@gmail.com";
            to = admin.Email;
            pass = "ttwfdbubwvnyxbtz";
            content = "Mật khẩu mới của bạn là của bạn là: " + passwordBuilder.ToString() + "\nVui lòng đổi mật khẩu sau khi đã đăng nhập để bảo mật tài khoản";
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
            smtp.Credentials = new NetworkCredential(from, pass); //from: gmail of sender; pass: password gmail of sender
            try
            {
                smtp.Send(mail);
                MessageBox.Show("Bạn đã xác nhận thành công!\nĐã gửi mật khẩu mới vào email của bạn");
                var form = new Sign_in_as_Admin(indexForm);
                form.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            }
            else
            {
                MessageBox.Show("Mã OTP không khớp!\nVui lòng nhập lại");
                otp_enter.Focus();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            VerifyEmail_Admin_Load(sender, e);
        }
    }
}