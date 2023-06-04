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

        public Register()
        {
            InitializeComponent();
        }   

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
            
            /*if (client != null)
            {
                MessageBox.Show("Connection is established");
            }*/
        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

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

        private async Task<int> id_index()
        {
            for (int i = 1; ; i++)
            {
                FirebaseResponse response = await client.GetTaskAsync("Users/" + i.ToString());
                if (response.Body == "null") return i;
            }
        }

        private void button_register_Click(object sender, EventArgs e)
        {
            
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private async void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if (IsValidEmail(guna2TextBox1.Text) && IsStrongPassword(guna2TextBox2.Text))
            {
                Task<int> id = id_index();
                int tmp = await id;
                tmp++;

                var data = new Data
                {
                    ID = tmp.ToString(),
                    Email = guna2TextBox1.Text,
                    Pw = HashPassword(guna2TextBox2.Text),
                    User_role = "student",
                    Fullname = "",
                    Avt_ID = "",
                    Is_Admin = "",
                    Student_ID = guna2TextBox1.Text.Substring(0, 8),
                    Faculty_ID = "",
                    Class_ID = ""
                };


                SetResponse response = await client.SetTaskAsync("User/" + tmp.ToString(), data);
                Data result = response.ResultAs<Data>();

                MessageBox.Show("Data... inserted " + result.Email);
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
    }
}
