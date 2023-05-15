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
            textBox3.Text = "School email addess";
            textBox3.ForeColor = SystemColors.GrayText;
            textBox3.Enter += textBox3_Enter;
            textBox3.Leave += textBox3_Leave;
            //tên textbox pass
            textBox4.Text = "Password";
            textBox4.ForeColor = SystemColors.GrayText;
            textBox4.Enter += textBox4_Enter;
            textBox4.Leave += textBox4_Leave;
        }


        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "School email addess")
            {
                textBox3.Clear();
                textBox3.ForeColor = SystemColors.ControlText;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                textBox3.Text = "School email addess";
                textBox3.ForeColor = SystemColors.GrayText;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                textBox4.Text = "Password";
                textBox4.UseSystemPasswordChar = false;
                textBox4.ForeColor = SystemColors.GrayText;
            }

        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Password")
            {
                textBox4.Clear();
                textBox4.UseSystemPasswordChar = true;
                textBox4.ForeColor = SystemColors.ControlText;
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
            if (client != null)
            {
                MessageBox.Show("Connection is established");
            }
        }
        private async Task<int> id_index()
        {
            for (int i = 1; ; i++)
            {
                FirebaseResponse response = await client.GetTaskAsync("Users/" + i.ToString());
                if (response.Body == "null") return i;
            }
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Contains("@admin")) MessageBox.Show("Please sign in as Admin!");
            else
            {
                Task<int> task = id_index();
                int limit = await task;
                for (int i = 1; i < limit; i++)
                {
                    FirebaseResponse response = await client.GetTaskAsync("Users/" + i.ToString());
                    Data sel = response.ResultAs<Data>();
                    if (sel.Email == textBox3.Text && sel.Pw == textBox4.Text)
                    {
                        MessageBox.Show("Success");
                        break;
                    }
                    else if (i == (limit - 1) && sel.Email != textBox3.Text && sel.Pw != textBox4.Text) MessageBox.Show("Email hoặc mật khẩu không đúng, vui lòng nhập lại.");

                }
            }
           
        }

        private void Sign_in_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form AdminForm = new Sign_in_as_Admin();
            AdminForm.Show();
            this.Close();
        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }
}