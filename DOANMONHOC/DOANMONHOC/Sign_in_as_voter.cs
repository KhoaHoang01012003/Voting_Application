using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.IO;

namespace DOANMONHOC
{
    public partial class Sign_in : Form
    {
        private readonly IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        private IFirebaseClient client;
        private readonly Form indexForm;
        private bool isBackButtonPressed;
        private Dictionary<string, USER> users;

        public Sign_in(Form parentForm)
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

        private async void Sign_in_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
            password.UseSystemPasswordChar = true;
            await FetchUsers();
        }

        private async Task FetchUsers()
        {
            FirebaseResponse response = await client.GetTaskAsync("Users/");
            users = response.ResultAs<Dictionary<string, USER>>();
        }

        private void Sign_in_as_Admin_button_Click(object sender, EventArgs e)
        {
            Form AdminForm = new Sign_in_as_Admin(indexForm);
            AdminForm.Show();
            isBackButtonPressed = true;
            Close();
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private void sign_in_button_Click(object sender, EventArgs e)
        {
            bool check = false;
            foreach (var user in users)
            {
                if (user.Value.UserName == username.Text && VerifyPassword(password.Text, user.Value.Password))
                {
                    Properties.Settings.Default.Username = username.Text;
                    Properties.Settings.Default.StudentID = user.Value.Student_ID;
                    Properties.Settings.Default.ClassID = user.Value.Class_ID;
                    Properties.Settings.Default.avt = user.Value.AvtUser;
                    Properties.Settings.Default.Save();

                    var openForm = new Dashboard(indexForm);
                    openForm.Show();
                    isBackButtonPressed = true;
                    Close();
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
            isBackButtonPressed = true;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new Forget_Pass(indexForm);
            form.Show();
            isBackButtonPressed = true;
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            password.UseSystemPasswordChar = !checkBox1.Checked;
        }
    }
}