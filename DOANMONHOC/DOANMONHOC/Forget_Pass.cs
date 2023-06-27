﻿using FireSharp.Config;
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
        public Forget_Pass()
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);
        }
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/",
        };

        IFirebaseClient client;
        private bool IsValidEmail(string email)
        {
            // Regular expression for a valid email
            string emailPattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)*\.[A-Za-z]{2,}$";

            // Check if the email matches the pattern and ends with "edu.vn"
            return Regex.IsMatch(email, emailPattern) && email.EndsWith("uit.edu.vn");
        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            FirebaseResponse emailCheckResponse = await client.GetTaskAsync("Users/");
            var emailCheckData = emailCheckResponse.ResultAs<Dictionary<string, USER>>();
            bool emailExists = emailCheckData.Values.Any(u => u.UserName == username.Text);
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

                var form = new VerifyEmail(data, "Forget");
                form.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Email không hợp lệ!");
                guna2Button1.Focus();
            }
        }
           
    }
}