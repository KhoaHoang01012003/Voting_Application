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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


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
        private Index indexForm;

        public Sign_in_as_Admin(Index indexForm)
        {
            InitializeComponent();
            this.indexForm = indexForm;
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
                FirebaseResponse response = await client.GetTaskAsync("Admins/" + i.ToString());
                if (response.Body == "null") return i;
            }
        }


        private void Sign_in_as_Admin_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
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
            return password == hashedPassword;
            //return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        private async void admin_sign_in_Click(object sender, EventArgs e)
        {
            Task<int> task = id_index();
            int limit = await task;
            bool check = false;
            for (int i = 1; i < limit; i++)
            {
                FirebaseResponse response = await client.GetTaskAsync("Admins/" + i.ToString());
                ADMIN admin = response.ResultAs<ADMIN>();
                if (admin.UserName == username_admin.Text && VerifyPassword(password_admin.Text, admin.Password))
                {
                    Properties.Settings.Default.Username = username_admin.Text;
                    Properties.Settings.Default.Save();

                    var openForm = new adminDashboard(indexForm);
                    openForm.Show();
                    this.Close();
                    check = true;
                    break;
                }
            }
            if (!check)
            {
                MessageBox.Show("Username hoặc mật khẩu không đúng, vui lòng nhập lại.");
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            indexForm.Show();
            this.Close();
        }
    }
}
