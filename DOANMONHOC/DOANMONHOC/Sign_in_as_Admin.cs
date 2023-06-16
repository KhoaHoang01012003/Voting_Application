using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.IO;


namespace DOANMONHOC
{
    public partial class Sign_in_as_Admin : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        public Sign_in_as_Admin()
        {
            InitializeComponent();
            //tên textbox email
            username_admin.Text = "School email addess";
            username_admin.ForeColor = Color.FromArgb(37, 83, 140);
            username_admin.Enter += username_admin_Enter;
            username_admin.Leave += username_admin_Leave;
            //tên textbox pass
            password_admin.Text = "Password";
            password_admin.ForeColor = Color.FromArgb(37, 83, 140);
            password_admin.Enter += password_admin_Enter;
            password_admin.Leave += password_admin_Leave;
        }

        private async Task<int> id_index()
        {
            for (int i = 1; ; i++)
            {
                FirebaseResponse response = await client.GetTaskAsync("Users/" + i.ToString());
                if (response.Body == "null") return i;
            }
        }


        private void Sign_in_as_Admin_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
            /*if (client != null)
            {
                MessageBox.Show("Connection is established");
            }*/
        }


        private void username_admin_Enter(object sender, EventArgs e)
        {
            if (username_admin.Text == "School email addess")
            {
                username_admin.Clear();
                username_admin.ForeColor = Color.FromArgb(37, 83, 140);
            }
        }

        private void username_admin_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(username_admin.Text))
            {
                username_admin.Text = "School email addess";
                username_admin.ForeColor = Color.FromArgb(37, 83, 140);
            }
        }

        private void password_admin_Enter(object sender, EventArgs e)
        {
            if (password_admin.Text == "Password")
            {
                password_admin.Clear();
                password_admin.UseSystemPasswordChar = true;
                password_admin.ForeColor = Color.FromArgb(37, 83, 140);
            }
        }

        private void password_admin_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(password_admin.Text))
            {
                password_admin.Text = "Password";
                password_admin.UseSystemPasswordChar = false;
                password_admin.ForeColor = Color.FromArgb(37, 83, 140);
            }
        }
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        private async void admin_sign_in_Click(object sender, EventArgs e)
        {
            if (!username_admin.Text.Contains("@admin")) MessageBox.Show("You are not Admin!");
            else
            {
                Task<int> task = id_index();
                int limit = await task;
                for (int i = 1; i < limit; i++)
                {
                    FirebaseResponse response = await client.GetTaskAsync("Users/" + i.ToString());
                    Users sel = response.ResultAs<Users>();
                    if (sel.Email == username_admin.Text && VerifyPassword(password_admin.Text, sel.Pw) && sel.Is_Admin == "1")
                    {
                        MessageBox.Show("Success");
                        break;
                    }
                    else if (i == (limit - 1) && sel.Email != username_admin.Text && !VerifyPassword(password_admin.Text, sel.Pw)) MessageBox.Show("Email hoặc mật khẩu không đúng, vui lòng nhập lại.");
                    else if (sel.Is_Admin != "1") MessageBox.Show("You are not Admin!");

                }
            }
        }

        private void username_admin_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
