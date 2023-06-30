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
        private readonly IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        private IFirebaseClient client;
        private readonly Form indexForm;
        private bool isBackButtonPressed;
        private Dictionary<string, ADMIN> admins;

        public Sign_in_as_Admin(Form parentForm)
        {
            InitializeComponent();
            FormClosed += FormClosed_Exit;
            indexForm = parentForm;
        }

        private void FormClosed_Exit(object sender, FormClosedEventArgs e)
        {
            if (!isBackButtonPressed)
            {
                Application.ExitThread();
            }
        }

        private async void Sign_in_as_Admin_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
            password_admin.UseSystemPasswordChar = true;
            await FetchAdmins();
        }

        private async Task FetchAdmins()
        {
            FirebaseResponse response = await client.GetTaskAsync("Admins/");
            admins = response.ResultAs<Dictionary<string, ADMIN>>();
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private void admin_sign_in_Click(object sender, EventArgs e)
        {
            var admin = admins.Values.FirstOrDefault(a => a.UserName == username_admin.Text && VerifyPassword(password_admin.Text, a.Password));

            if (admin != null)
            {
                Properties.Settings.Default.Username = username_admin.Text;
                Properties.Settings.Default.avt = admin.AvtAdmin;
                Properties.Settings.Default.Name = admin.AdminName;
                Properties.Settings.Default.Save();

                var openForm = new adminDashboard(indexForm);
                openForm.Show();
                isBackButtonPressed = true;
                Close();
            }
            else
            {
                MessageBox.Show("Username hoặc mật khẩu không đúng, vui lòng nhập lại.");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            password_admin.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            indexForm.Show();
            isBackButtonPressed = true;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new Forget_Pass(indexForm, true);
            form.Show();
            isBackButtonPressed = true;
            Close();
        }
    }
}
