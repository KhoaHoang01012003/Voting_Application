﻿using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.IO;

namespace DOANMONHOC
{
    public partial class Sign_in : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        public Sign_in()
        {
            InitializeComponent();
            //tên textbox email
            username.Text = "Username";
            username.ForeColor = Color.FromArgb(37, 83, 140);
            username.Enter += username_Enter;
            username.Leave += username_Leave;
            //tên textbox pass
            password.Text = "Password";
            password.ForeColor = Color.FromArgb(37, 83, 140);
            password.Enter += password_Enter;
            password.Leave += password_Leave;
        }

        private async Task<int> id_index()
        {
            for (int i = 1; ; i++)
            {
                FirebaseResponse response = await client.GetTaskAsync("Users/" + "User " + i.ToString());
                if (response.Body == "null") return i;
            }
        }

        private void Sign_in_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
            if (client != null)
            {
                //MessageBox.Show("Connection is established");
            }
        }

        private void username_Enter(object sender, EventArgs e)
        {
            if (username.Text == "Username")
            {
                username.Clear();
                username.ForeColor = Color.FromArgb(37, 83, 140);
            }
        }

        private void username_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(username.Text))
            {
                username.Text = "Username";
                username.ForeColor = Color.FromArgb(37, 83, 140);
            }
        }

        private void password_Enter(object sender, EventArgs e)
        {
            if (password.Text == "Password")
            {
                password.Clear();
                password.UseSystemPasswordChar = true;
                password.ForeColor = Color.FromArgb(37, 83, 140);
            }
        }

        private void password_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(password.Text))
            {
                password.Text = "Password";
                password.UseSystemPasswordChar = false;
                password.ForeColor = Color.FromArgb(37, 83, 140);
            }
        }

        private void Sign_in_as_Admin_button_Click(object sender, EventArgs e)
        {
            Form AdminForm = new Sign_in_as_Admin();
            AdminForm.Show();
        }
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        private async void sign_in_button_Click(object sender, EventArgs e)
        {
            Task<int> task = id_index();
            int limit = await task;
            for (int i = 1; i < limit; i++)
            {
                FirebaseResponse response = await client.GetTaskAsync("Users/" + "User " + i.ToString());
                USER sel = response.ResultAs<USER>();
                if (sel.UserName == username.Text && VerifyPassword(password.Text, sel.Password))
                {
                    MessageBox.Show("Success");
                    break;
                }
                else if (i == (limit - 1) && sel.UserName != username.Text && !VerifyPassword(password.Text, sel.Password)) MessageBox.Show("Username hoặc mật khẩu không đúng, vui lòng nhập lại.");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}