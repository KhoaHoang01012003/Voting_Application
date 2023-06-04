using FireSharp.Config;
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
            username.Text = "School email addess";
            username.ForeColor = SystemColors.GrayText;
            username.Enter += username_Enter;
            username.Leave += username_Leave;
            //tên textbox pass
            password.Text = "Password";
            password.ForeColor = SystemColors.GrayText;
            password.Enter += password_Enter;
            password.Leave += password_Leave;
        }

        private async Task<int> id_index()
        {
            for (int i = 1; ; i++)
            {
                FirebaseResponse response = await client.GetTaskAsync("Users/" + i.ToString());
                if (response.Body == "null") return i;
            }
        }

        private void Sign_in_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
            if (client != null)
            {
                MessageBox.Show("Connection is established");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void username_Enter(object sender, EventArgs e)
        {
            if (username.Text == "School email addess")
            {
                username.Clear();
                username.ForeColor = SystemColors.ControlText;
            }
        }

        private void username_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(username.Text))
            {
                username.Text = "School email addess";
                username.ForeColor = SystemColors.GrayText;
            }
        }

        private void password_Enter(object sender, EventArgs e)
        {
            if (password.Text == "Password")
            {
                password.Clear();
                password.UseSystemPasswordChar = true;
                password.ForeColor = SystemColors.ControlText;
            }
        }

        private void password_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(password.Text))
            {
                password.Text = "Password";
                password.UseSystemPasswordChar = false;
                password.ForeColor = SystemColors.GrayText;
            }
        }

        private void Sign_in_as_Admin_button_Click(object sender, EventArgs e)
        {
            Form AdminForm = new Sign_in_as_Admin();
            AdminForm.Show();
            this.Close();
        }
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        private async void sign_in_button_Click(object sender, EventArgs e)
        {
            if (username.Text.Contains("@admin")) MessageBox.Show("Please sign in as Admin!");
            else
            {
                Task<int> task = id_index();
                int limit = await task;
                for (int i = 1; i < limit; i++)
                {
                    FirebaseResponse response = await client.GetTaskAsync("Users/" + i.ToString());
                    Data sel = response.ResultAs<Data>();
                    if (sel.Email == username.Text && VerifyPassword(password.Text, sel.Pw))
                    {
                        MessageBox.Show("Success");
                        break;
                    }
                    else if (i == (limit - 1) && sel.Email != username.Text && sel.Pw != password.Text) MessageBox.Show("Email hoặc mật khẩu không đúng, vui lòng nhập lại.");

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}