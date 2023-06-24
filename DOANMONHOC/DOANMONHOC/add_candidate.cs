using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Guna.UI2.WinForms.Suite;
using Microsoft.VisualBasic.ApplicationServices;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DOANMONHOC
{
    public partial class add_candidate : Form
    {
        private Index indexForm;

        CANDIDATE tempCandidate;
        bool flag = false;
        public add_candidate(CANDIDATE u = null)
        {
            InitializeComponent();
            tempCandidate = u;
        }
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient candidate;

        public class ClassAndFaculty
        {
            public int ClassId { get; set; }
            public int FacultyId { get; set; }
        }
        private async Task<ClassAndFaculty> SearchClassID(string className)
        {
            FirebaseResponse response1 = await candidate.GetTaskAsync("Classes/");
            Dictionary<string, CLASS> classes = response1.ResultAs<Dictionary<string, CLASS>>();
            int index = classes.Count() + 1;
            foreach (var user in classes)
            {
                if (user.Value.ClassName == className)
                {
                    ClassAndFaculty result = new ClassAndFaculty();
                    result.ClassId = user.Value.Class_ID;
                    result.FacultyId = user.Value.Faculty_ID;
                    return result;
                }
            }
            return null;
        }

        private async void add_candidate_Load(object sender, EventArgs e)
        {
            candidate = new FireSharp.FirebaseClient(config);
            if (candidate != null)
            {
                //MessageBox.Show("Connection is established");
            }
            if (tempCandidate != null)
            {

                guna2TextBox1.Text = tempCandidate.CandidateName;
                guna2TextBox2.Text = tempCandidate.Birthday;
                guna2TextBox3.Text = tempCandidate.Description;
                guna2TextBox4.Text = tempCandidate.Promise;
                FirebaseResponse response1 = await candidate.GetTaskAsync("Classes/");
                Dictionary<string, CLASS> classes = response1.ResultAs<Dictionary<string, CLASS>>();
                int index = classes.Count() + 1;
                foreach (var user in classes)
                {
                    if (user.Value.Class_ID == tempCandidate.Class_ID)
                    {
                        guna2TextBox5.Text = user.Value.ClassName;
                        break;
                    }
                }
                flag = true;
            }
        }

        private async void guna2Button4_Click(object sender, EventArgs e)
        {
            ClassAndFaculty tmp_class = await SearchClassID(guna2TextBox5.Text.ToUpper());

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
            else if (guna2TextBox3.Text == "")
            {
                MessageBox.Show("Please enter a value for Description.");
                guna2TextBox3.Focus();
            }
            else if (guna2TextBox4.Text == "")
            {
                MessageBox.Show("Please enter a value for Promise.");
                guna2TextBox4.Focus();
            }
            // Kiểm tra tên lớp có tồn tại trong cơ sở dữ liệu hay không
            else if (tmp_class == null)
            {
                MessageBox.Show("Class name not found. Please enter a valid class name.");
                guna2TextBox5.Focus();
            }
            else
            {
                if (!flag)
                {
                    int cnt;
                    try
                    {
                        FirebaseResponse response = await candidate.GetTaskAsync("Candidates/");
                        Dictionary<string, CANDIDATE> users = response.ResultAs<Dictionary<string, CANDIDATE>>();
                        cnt = users.Count() + 1;
                    }
                    catch
                    {
                        cnt = 1;
                    }


                    var data = new CANDIDATE
                    {
                        Candidate_ID = cnt,
                        CandidateName = guna2TextBox1.Text,
                        Birthday = guna2TextBox2.Text,
                        Description = guna2TextBox3.Text,
                        Faculty_ID = tmp_class.FacultyId,
                        Class_ID = tmp_class.ClassId,
                        Promise = guna2TextBox4.Text,
                    };

                    PushResponse response_result = await candidate.PushTaskAsync("Candidates/", data);
                    USER result = response_result.ResultAs<USER>();

                    MessageBox.Show("Data... inserted " + result.Email);
                }
                else
                {
                    FirebaseResponse response = await candidate.GetTaskAsync("Candidates/");
                    Dictionary<string, CANDIDATE> candidates = response.ResultAs<Dictionary<string, CANDIDATE>>();
                    foreach (var user in candidates)
                    {
                        if (user.Value.Candidate_ID == tempCandidate.Candidate_ID)
                        {
                            user.Value.CandidateName = guna2TextBox1.Text;
                            user.Value.Birthday = guna2TextBox2.Text;
                            user.Value.Description = guna2TextBox3.Text;
                            user.Value.Faculty_ID = tmp_class.FacultyId;
                            user.Value.Class_ID = tmp_class.ClassId;
                            user.Value.Promise = guna2TextBox4.Text;
                            var updateResponse = await candidate.SetTaskAsync("Candidates/" + user.Key, user.Value);
                            MessageBox.Show("Update!");
                            break;
                        }
                    }
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            var openForm = new list_candidate();
            openForm.Show();
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionActivities(indexForm);
            openForm.Show();
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var openForm = new adminDashboard(indexForm);
            openForm.Show();
            this.Close();
        }
    }
}
