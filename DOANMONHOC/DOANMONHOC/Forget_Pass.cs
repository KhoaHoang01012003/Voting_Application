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
    public partial class Forget_Pass : Form
    {
        private readonly IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/",
        };

        private readonly IFirebaseClient client;
        private readonly bool isAdmin;
        private readonly Form indexForm;
        private bool isBackButtonPressed;
        private Dictionary<string, USER> users;
        private Dictionary<string, ADMIN> admins;

        public Forget_Pass(Form parentForm, bool isAdmin = false)
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);
            this.isAdmin = isAdmin;
            FormClosed += FormClosed_Exit;
            indexForm = parentForm;

            FetchUsersAndAdmins();
        }

        private async Task FetchUsersAndAdmins()
        {
            FirebaseResponse usersResponse = await client.GetTaskAsync("Users/");
            users = usersResponse.ResultAs<Dictionary<string, USER>>();

            FirebaseResponse adminsResponse = await client.GetTaskAsync("Admins/");
            admins = adminsResponse.ResultAs<Dictionary<string, ADMIN>>();
        }

        private void FormClosed_Exit(object sender, FormClosedEventArgs e)
        {
            if (!isBackButtonPressed)
            {
                Application.ExitThread();
            }
        }

        private static bool IsValidEmail(string email)
        {
            // Regular expression for a valid email
            const string emailPattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)*\.[A-Za-z]{2,}$";

            // Check if the email matches the pattern and ends with "uit.edu.vn"
            return Regex.IsMatch(email, emailPattern) && email.EndsWith("uit.edu.vn");
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!isAdmin)
            {
                HandleUserForgetPassword();
            }
            else
            {
                HandleAdminForgetPassword();
            }
        }

        private void HandleUserForgetPassword()
        {
            bool emailExists = users.Values.Any(u => u.UserName == username.Text);
            if (IsValidEmail(username.Text) && emailExists)
            {
                var data = new USER
                {
                    Email = username.Text,
                    Password = "",
                    Fullname = "",
                    Student_ID = "",
                    Faculty_ID = 0,
                    Class_ID = 0,
                    UserName = username.Text
                };

                var form = new VerifyEmail(indexForm, data, "Forget");
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Email không hợp lệ!");
                guna2Button1.Focus();
            }
        }

        private void HandleAdminForgetPassword()
        {
            bool emailExists = admins.Values.Any(u => u.Email == username.Text);
            if (emailExists)
            {
                var data = new ADMIN
                {
                    Admin_ID = 0,
                    UserName = "",
                    Password = "",
                    Email = username.Text,
                    AdminName = ""
                };

                var form = new VerifyEmail_Admin(indexForm, data);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Email không hợp lệ!");
                guna2Button1.Focus();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var form = new Sign_in(indexForm);
            isBackButtonPressed = true;
            form.Show();
            Close();
        }
    }
}
