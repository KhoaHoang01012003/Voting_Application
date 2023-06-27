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
        private Index indexForm = new Index();

        public Sign_in_as_Admin()
        {
            InitializeComponent();
            //this.FormClosed += new FormClosedEventHandler(FormClosed_Exit);
        }

        /*void FormClosed_Exit(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
        }*/

        private void Sign_in_as_Admin_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
            password_admin.UseSystemPasswordChar = true;
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private async void admin_sign_in_Click(object sender, EventArgs e)
        {
            FirebaseResponse responseGet = await client.GetTaskAsync("Admins/");
            Dictionary<string, ADMIN> admins = responseGet.ResultAs<Dictionary<string, ADMIN>>();
            bool check = false;

            foreach (var admin in admins.Values)
            {
                if (admin.UserName == username_admin.Text && VerifyPassword(password_admin.Text, admin.Password))
                {
                    Properties.Settings.Default.Username = username_admin.Text;
                    Properties.Settings.Default.Save();

                    var openForm = new adminDashboard();
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                password_admin.UseSystemPasswordChar = false;
            }
            else
            {
                password_admin.UseSystemPasswordChar = true;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            indexForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new Forget_Pass(true);
            form.Show();
            this.Close();
        }
    }
}
