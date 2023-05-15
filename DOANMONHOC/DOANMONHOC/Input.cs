using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace DOANMONHOC
{
    public partial class Input : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "8L1GpfNiXCp0XrV8Klz7TJlyXGzoEdIPAoMOOF6M",
            BasePath = "https://voting-app-test-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        public Input()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Input_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
            if (client != null)
            {
                MessageBox.Show("Connection is established");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var data = new Data
            {
                ID = textBox10.Text,
                Email = textBox1.Text,
                Pw = textBox2.Text,
                User_role = textBox3.Text,
                Fullname = textBox4.Text,
                Avt_ID = textBox5.Text,
                Is_Admin = textBox6.Text,
                Student_ID = textBox7.Text,
                Faculty_ID = textBox8.Text,
                Class_ID = textBox9.Text
            };
            SetResponse response = await client.SetTaskAsync("Users/" + textBox10.Text, data);
            Data result = response.ResultAs<Data>();
            MessageBox.Show("Data inserted" + result.ID);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.GetTaskAsync("Users/"+textBox10.Text);
            Data obj = response.ResultAs<Data>();
            textBox1.Text= obj.Email;
            textBox2.Text = obj.Pw;
            MessageBox.Show("Truy xuất thành công");
        }
    }
}
