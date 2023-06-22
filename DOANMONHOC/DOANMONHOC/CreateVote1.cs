using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Guna.UI2.WinForms;
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

namespace DOANMONHOC
{
    public partial class CreateVote1 : Form
    {
        FirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        private Index indexForm;

        public CreateVote1(Index indexForm)
        {
            InitializeComponent();
            this.indexForm = indexForm;
        }

        private async Task<int> id_index(string table)
        {
            for (int i = 1; ; i++)
            {
                FirebaseResponse response = await client.GetTaskAsync(table + "/" + i.ToString());
                if (response.Body == "null") return i;
            }
        }

        private async void Dashboard_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            int cntAdmin = await id_index("Admins").ConfigureAwait(false);
            StringBuilder adminPathBuilder = new StringBuilder("Admins/");
            for (int i = 1; i < cntAdmin; i++)
            {
                adminPathBuilder.Clear().Append("Admins/").Append(i);
                FirebaseResponse response = await client.GetTaskAsync(adminPathBuilder.ToString()).ConfigureAwait(false);
                ADMIN admin = response.ResultAs<ADMIN>();
                if (admin.UserName == Properties.Settings.Default.Username.ToString())
                {
                    this.Invoke((MethodInvoker)delegate {
                        label7.Text = admin.AdminName;
                    });
                    break;
                }
            }

            this.Invoke((MethodInvoker)delegate {
                facultyList.Items.Clear();
            });

            FirebaseResponse facultyResponse = await client.GetTaskAsync("Faculties").ConfigureAwait(false);
            Dictionary<string, FACULTY> faculties = facultyResponse.ResultAs<Dictionary<string, FACULTY>>();
            foreach (FACULTY faculty in faculties.Values)
            {
                this.Invoke((MethodInvoker)delegate {
                    facultyList.Items.Add(faculty.FacultyName);
                });
            }

            facultyList.SelectedIndexChanged += async (sender, e) =>
            {
                int selectedFacultyIndex = facultyList.SelectedIndex;
                if (selectedFacultyIndex != -1)
                {
                    string selectedFacultyName = facultyList.SelectedItem.ToString();

                    FACULTY selectedFaculty = faculties.Values.ElementAt(selectedFacultyIndex);

                    int selectedFacultyID = selectedFaculty.Faculty_ID;

                    this.Invoke((MethodInvoker)delegate {
                        classList.Items.Clear();
                    });
                    FirebaseResponse classResponse = await client.GetTaskAsync("Classes").ConfigureAwait(false);
                    Dictionary<string, CLASS> classes = classResponse.ResultAs<Dictionary<string, CLASS>>();
                    foreach (CLASS classObj in classes.Values)
                    {
                        if (selectedFacultyID == classObj.Faculty_ID)
                            this.Invoke((MethodInvoker)delegate {
                                classList.Items.Add(classObj.ClassName);
                            });
                    }
                }
            };
        }


        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private async void guna2Button4_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(startTime.Value, endTime.Value) < 0)
            {
                FirebaseResponse titleCheckResponse = await client.GetTaskAsync("Campaigns/");
                var campaigns = titleCheckResponse.ResultAs<Dictionary<string, CAMPAIGN>>();

                bool titleExists = campaigns.Values.Any(u => u.CampaignName == campaignName.Text);

                if (titleExists)
                {
                    MessageBox.Show("Sự kiện này đã được đăng ký!");
                    return;
                }

                var data = new CAMPAIGN
                {
                    Campaint_ID = campaigns.Count(),
                    Description = description.Text,
                    CampaignName = campaignName.Text,
                    StartTime = startTime.Value,
                    EndTime = endTime.Value,
                    Candidate_ID = new int[] {},
                    Category = category.Text,
                    Status = 1,
                };

                var openForm = new CreateVote12(indexForm);
                openForm.Data = data;
                openForm.Show();
                this.Close();

                //PushResponse response = await client.PushTaskAsync("Campaigns/", data);
                var open = new CreateVote2(indexForm);
            }
            else
            {
                MessageBox.Show("Khoảng thời gian không hợp lệ!");
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            indexForm.Show();
            this.Close();
        }
    }
}
