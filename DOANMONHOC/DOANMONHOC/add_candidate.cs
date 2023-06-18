using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Guna.UI2.WinForms;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOANMONHOC
{
    public partial class add_candidate : Form
    {
        public add_candidate()
        {
            InitializeComponent();
        }
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient candidate;
        private async Task<int> id_index()
        {
            for (int i = 1; ; i++)
            {
                FirebaseResponse response = await candidate.GetTaskAsync("Candidates/" + "Candidates " + i.ToString());
                if (response.Body == "null") return i;
            }
        }
        private async Task<int> id_class()
        {
            for (int i = 1; ; i++)
            {
                FirebaseResponse response = await candidate.GetTaskAsync("Classes/" + i.ToString());
                if (response.Body == "null") return i;
            }
        }
        public class ClassAndFaculty
        {
            public string ClassId { get; set; }
            public string FacultyId { get; set; }
        }
        private async Task<ClassAndFaculty> SearchClass(string className)
        {
            int index = await id_class();
            for (int i = 1; i < index; i++)
            {
                FirebaseResponse response = await candidate.GetTaskAsync("Classes/" + i.ToString());
                if (response.Body != "null")
                {
                    JObject user = JObject.Parse(response.Body);
                    if (user["ClassName"].ToString() == className)
                    {
                        ClassAndFaculty result = new ClassAndFaculty();
                        result.ClassId = user["Class_ID"].ToString();
                        result.FacultyId = user["Faculty_ID"].ToString();
                        return result;
                    }
                }
            }
            return null;
        }

        private void add_candidate_Load(object sender, EventArgs e)
        {
            candidate = new FireSharp.FirebaseClient(config);
            if (candidate != null)
            {
                //MessageBox.Show("Connection is established");
            }
        }

        private async void guna2Button4_Click(object sender, EventArgs e)
        {
            ClassAndFaculty tmp_class = await SearchClass(guna2TextBox5.Text);

            if (guna2TextBox1.Text == "")
            {
                MessageBox.Show("Please enter a value for Candidate Name.");
                guna2TextBox1.Focus();
            }
            else if (guna2TextBox2.Text == "")
            {
                MessageBox.Show("Please enter a value for Birthday.");
                guna2TextBox2.Focus();
            }

            else if (guna2TextBox4.Text == "")
            {
                MessageBox.Show("Please enter a value for Promise.");
                guna2TextBox3.Focus();
            }
            // Kiểm tra tên lớp có tồn tại trong cơ sở dữ liệu hay không
            else if (tmp_class == null)
            {
                MessageBox.Show("Class name not found. Please enter a valid class name.");
                guna2TextBox5.Focus();
            }
            else
            {
                Task<int> id = id_index();
                int tmp = await id;

                var data = new CANDIDATE
                {
                    Candidate_ID = tmp.ToString(),
                    CandidateName = guna2TextBox1.Text,
                    Birthday = guna2TextBox2.Text,
                    Description = guna2TextBox3.Text,
                    Faculty_ID = tmp_class.FacultyId,
                    Class_ID = tmp_class.ClassId,
                    Promise = guna2TextBox4.Text,
                };


                SetResponse response = await candidate.SetTaskAsync("Candidates/" + "Candidates " + tmp.ToString(), data);
                USER result = response.ResultAs<USER>();

                MessageBox.Show("Data... inserted " + result.Email);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }
    }
}