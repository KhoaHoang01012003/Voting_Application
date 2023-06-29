using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using BCrypt.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DOANMONHOC
{
    public partial class Register : Form
    {

        private readonly IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        private IFirebaseClient client;
        private readonly Index indexForm;
        private bool isBackButtonPressed;
        private Dictionary<string, USER> users;

        public Register(Index parentForm)
        {
            InitializeComponent();
            FormClosed += FormClosed_Exit;
            indexForm = parentForm;
        }

        private void FormClosed_Exit(object sender, FormClosedEventArgs e)
        {
            if (!isBackButtonPressed)
            {
                Application.ExitThread();
            }
        }

        private async void Register_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
            password.UseSystemPasswordChar = true;
            await FetchUsers();
        }

        private async Task FetchUsers()
        {
            FirebaseResponse response = await client.GetTaskAsync("Users/");
            users = response.ResultAs<Dictionary<string, USER>>();
        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)*\.[A-Za-z]{2,}$";
            return Regex.IsMatch(email, emailPattern) && email.EndsWith("uit.edu.vn");
        }

        private bool IsStrongPassword(string password)
        {
            int minLength = 8;
            string upperCasePattern = @"[A-Z]+";
            string lowerCasePattern = @"[a-z]+";
            string digitPattern = @"[0-9]+";
            string specialCharPattern = @"[^a-zA-Z0-9]+";

            return password.Length >= minLength
                    && Regex.IsMatch(password, upperCasePattern)
                    && Regex.IsMatch(password, lowerCasePattern)
                    && Regex.IsMatch(password, digitPattern)
                    && Regex.IsMatch(password, specialCharPattern);
        }

        public static string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hashedPassword;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            bool emailExists = users.Values.Any(u => u.Email == guna2TextBox1.Text);

            if (IsValidEmail(guna2TextBox1.Text) && IsStrongPassword(password.Text) && !emailExists)
            {
                var data = new USER
                {
                    Email = guna2TextBox1.Text,
                    Password = HashPassword(password.Text),
                    Fullname = "",
                    Student_ID = guna2TextBox1.Text.Substring(0, 8),
                    Faculty_ID = 0,
                    Class_ID = 0,
                    UserName = guna2TextBox1.Text
                };

                var form = new VerifyEmail(indexForm,data);
                form.Show();
                isBackButtonPressed = true;
                Close();
            }
            else
            {
                HandleInvalidRegistration(emailExists);
            }
        }

        private void HandleInvalidRegistration(bool emailExists)
        {
            if (emailExists)
            {
                MessageBox.Show("Email đã được đăng ký. Vui lòng sử dụng email khác.");
                guna2TextBox1.Focus();
            }
            else if (!IsValidEmail(guna2TextBox1.Text))
            {
                MessageBox.Show("Email không đúng! Hãy dùng email trường cung cấp");
                guna2TextBox1.Focus();
            }
            else
            {
                MessageBox.Show("Mật khẩu yếu!");
                password.Focus();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            indexForm.Show();
            isBackButtonPressed = true;
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            password.UseSystemPasswordChar = !checkBox1.Checked;
        }
    }
}
