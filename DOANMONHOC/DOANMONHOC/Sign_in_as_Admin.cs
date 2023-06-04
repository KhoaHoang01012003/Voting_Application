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
            username_admin.ForeColor = SystemColors.GrayText;
            username_admin.Enter += username_admin_Enter;
            username_admin.Leave += username_admin_Leave;
            //tên textbox pass
            password_admin.Text = "Password";
            password_admin.ForeColor = SystemColors.GrayText;
            password_admin.Enter += password_admin_Enter;
            password_admin.Leave += password_admin_Leave;
        }


        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (username.Text == "School email addess")
            {
                username.Clear();
                username.ForeColor = SystemColors.ControlText;
            }

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(username.Text))
            {
                username.Text = "School email addess";
                username.ForeColor = SystemColors.GrayText;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Password")
            {
                textBox2.Clear();
                textBox2.UseSystemPasswordChar = true;
                textBox2.ForeColor = SystemColors.ControlText;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = "Password";
                textBox2.UseSystemPasswordChar = false;
                textBox2.ForeColor = SystemColors.GrayText;
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
            if (!username.Text.Contains("@admin")) MessageBox.Show("You are not Admin!");
            else
            {
                Task<int> task = id_index();
                int limit = await task;
                for (int i = 1; i < limit; i++)
                {
                    FirebaseResponse response = await client.GetTaskAsync("Users/" + i.ToString());
                    Data sel = response.ResultAs<Data>();
                    if (sel.Email == username.Text && sel.Pw == textBox2.Text && sel.Is_Admin == "1")
                    {
                        MessageBox.Show("Success");
                        break;
                    }
                    else if (i == (limit - 1) && sel.Email != username.Text && sel.Pw != textBox2.Text) MessageBox.Show("Email hoặc mật khẩu không đúng, vui lòng nhập lại.");
                    else if (sel.Email == username.Text && sel.Pw == textBox2.Text && sel.Is_Admin != "1") MessageBox.Show("You are not Admin!");

                }
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

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Sign_in_as_Admin_Load_1(object sender, EventArgs e)
        {

        }

        private void username_admin_Enter(object sender, EventArgs e)
        {

        }

        private void username_admin_Leave(object sender, EventArgs e)
        {

        }

        private void password_admin_Enter(object sender, EventArgs e)
        {

        }

        private void password_admin_Leave(object sender, EventArgs e)
        {

        }
    }
}
