using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOANMONHOC
{
    public partial class change_user_info : Form
    {
        public change_user_info()
        {
            InitializeComponent();
        }
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient User;

        private async void change_user_info_Load(object sender, EventArgs e)
        {
            User = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await User.GetTaskAsync("Users/");
            Dictionary<string, USER> users = response.ResultAs<Dictionary<string, USER>>();
            foreach (var user in users)
            {
                if (user.Value.UserName == Properties.Settings.Default.Username.ToString())
                {
                    guna2TextBox1.Text = user.Value.Fullname;
                    break;
                }
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

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
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


        private async void guna2Button7_Click(object sender, EventArgs e)
        {
            if(guna2TextBox2.Text != "" && guna2TextBox3.Text != "" && guna2TextBox4.Text != "")
            {
                if(guna2TextBox3.Text == guna2TextBox4.Text && IsStrongPassword(guna2TextBox3.Text)) {
                    FirebaseResponse response = await User.GetTaskAsync("Users/");
                    Dictionary<string, USER> users = response.ResultAs<Dictionary<string, USER>>();
                    foreach (var user in users)
                    {
                        if (user.Value.UserName == Properties.Settings.Default.Username.ToString() && VerifyPassword(guna2TextBox2.Text, user.Value.Password))
                        {
                            user.Value.Password = HashPassword(guna2TextBox3.Text);
                            user.Value.Fullname = guna2TextBox1.Text;
                            var updateResponse = await User.SetTaskAsync("Users/" + user.Key, user.Value);
                            MessageBox.Show("Update!");
                            break;
                        }
                    }
                }
                else if (!IsStrongPassword(guna2TextBox3.Text)){
                    MessageBox.Show("Mật khẩu mới yếu");
                    guna2TextBox3.Focus();
                }
                else
                {
                    MessageBox.Show("Mật khẩu mới không khớp");
                    
                }
                
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mật khẩu");
                guna2TextBox2.Focus();
            }
        }
    }
}
