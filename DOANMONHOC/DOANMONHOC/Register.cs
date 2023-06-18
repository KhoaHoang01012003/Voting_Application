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

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/",
        };

        IFirebaseClient client;
        private Index indexForm;

        public Register(Index indexForm)
        {
            InitializeComponent();
            this.indexForm = indexForm;
        }

        private void Register_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
        }

        private bool IsValidEmail(string email)
        {
            // Regular expression for a valid email
            string emailPattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)*\.[A-Za-z]{2,}$";

            // Check if the email matches the pattern and ends with "edu.vn"
            return Regex.IsMatch(email, emailPattern) && email.EndsWith("uit.edu.vn");
        }

        private bool IsStrongPassword(string password)
        {
            // Minimum length requirement
            int minLength = 8;

            // Regular expressions for uppercase letters, lowercase letters, digits, and special characters
            string upperCasePattern = @"[A-Z]+";
            string lowerCasePattern = @"[a-z]+";
            string digitPattern = @"[0-9]+";
            string specialCharPattern = @"[^a-zA-Z0-9]+";

            // Check if the password meets the criteria
            return password.Length >= minLength
                && Regex.IsMatch(password, upperCasePattern)
                && Regex.IsMatch(password, lowerCasePattern)
                && Regex.IsMatch(password, digitPattern)
                && Regex.IsMatch(password, specialCharPattern);
        }

        public static string HashPassword(string password)
        {
            // Generate a salt with a work factor of 12 (the default)
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            // Hash the password using the salt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            if (IsValidEmail(guna2TextBox1.Text) && IsStrongPassword(guna2TextBox2.Text))
            {
                FirebaseResponse emailCheckResponse = await client.GetTaskAsync("Users/");
                var emailCheckData = emailCheckResponse.ResultAs<Dictionary<string, USER>>();

                bool emailExists = emailCheckData.Values.Any(u => u.Email == guna2TextBox1.Text);

                if (emailExists)
                {
                    label4.Text = "Email đã được đăng ký. Vui lòng sử dụng email khác.";
                    return;
                }

                var data = new USER
                {
                    Email = guna2TextBox1.Text,
                    Password = HashPassword(guna2TextBox2.Text),
                    Fullname = "",
                    Student_ID = guna2TextBox1.Text.Substring(0,8),
                    Faculty_ID = "",
                    Class_ID = "",
                    UserName = guna2TextBox1.Text
                };


                PushResponse response = await client.PushTaskAsync("Users/", data);
                USER result = response.ResultAs<USER>();

                MessageBox.Show("DK THANH CONG " + result.Email);
                label4.Text = "";
            }
            else if (!IsValidEmail(guna2TextBox1.Text))
            {
                label4.Text = "Email không đúng! Hãy dùng email trường cung cấp";
            }
            else
            {
                label4.Text += "\nMật khẩu yếu!";
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            indexForm.Show();
            this.Close();
        }
    }
}
