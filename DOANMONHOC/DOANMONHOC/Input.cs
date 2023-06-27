﻿using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOANMONHOC
{
    public partial class Input : Form
    {

        FirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        public Input()
        {
            InitializeComponent();
        }

        private void Input_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            var data = new CLASS
            {
                ClassName = guna2TextBox1.Text,
                Class_ID = int.Parse(guna2TextBox2.Text),
                Faculty_ID = int.Parse(guna2TextBox3.Text)
            };


            PushResponse response = await client.PushTaskAsync("Classes/", data);
            USER result = response.ResultAs<USER>();

            MessageBox.Show("ok" + result.Email);
        }

        private async void guna2Button2_Click(object sender, EventArgs e)
        {
            var data = new FACULTY
            {
                FacultyName = guna2TextBox6.Text,
                Faculty_ID = int.Parse(guna2TextBox5.Text)
            };


            PushResponse response = await client.PushTaskAsync("Faculties/", data);
            USER result = response.ResultAs<USER>();

            MessageBox.Show("ok" + result.Email);
        }

        public static string HashPassword(string password)
        {
            // Generate a salt with a work factor of 12 (the default)
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            // Hash the password using the salt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }

        private async void guna2Button3_Click(object sender, EventArgs e)
        {
            int cnt;
            try
            {
                FirebaseResponse responseGet = await client.GetTaskAsync("Admins/");
                Dictionary<string, ADMIN> admins = responseGet.ResultAs<Dictionary<string, ADMIN>>();
                cnt = admins.Count() + 1;
            }
            catch
            {
                cnt = 1;
            }

            var data = new ADMIN
            {
                AdminName = adminName.Text,
                Admin_ID = cnt,
                Email = email.Text,
                Password= HashPassword("Default12345_"),
                UserName = username.Text,
            };

            PushResponse response = await client.PushTaskAsync("Admins/", data);

            MessageBox.Show("Admins added!");
        }
    }
}
