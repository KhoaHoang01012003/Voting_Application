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
                if(admin.AvtAdmin != "")
                    Properties.Settings.Default.avt = admin.AvtAdmin;
                else
                    Properties.Settings.Default.avt = "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";
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
