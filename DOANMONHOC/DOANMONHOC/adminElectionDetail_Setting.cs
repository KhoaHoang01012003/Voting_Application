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
    public partial class adminElectionDetail_Setting : Form
    {
        FirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        private Index indexForm;
        public CAMPAIGN Data { get; set; }

        public adminElectionDetail_Setting(Index indexForm)
        {
            InitializeComponent();
            this.indexForm = indexForm;
        }

        private async void adminElectionDetail_Setting_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            FirebaseResponse classResponse = await client.GetTaskAsync("Classes").ConfigureAwait(false);
            Dictionary<string, CLASS> classes = classResponse.ResultAs<Dictionary<string, CLASS>>();

            this.Invoke((MethodInvoker)delegate
            {
                campaignName.Text = Data.CampaignName;
                campaignNameEdited.Text = Data.CampaignName;
                description.Text = Data.Description;
                startTime.Value = Data.StartTime;
                endTime.Value = Data.EndTime;
                category.Text = Data.Category;
                foreach (var classID in Data.Class_ID)
                {
                    foreach (CLASS classObj in classes.Values)
                    {
                        if (classID == classObj.Class_ID)
                        {
                            selectClass.Text += classObj.ClassName + ", ";
                            break;
                        }
                    }
                }
            });
        }

        public bool ValidateDateTimePickers(Guna2DateTimePicker startTime, Guna2DateTimePicker endTime)
        {
            DateTime now = DateTime.Now;

            if (startTime.Value < now || endTime.Value < now)
            {
                MessageBox.Show("Thời gian bắt đầu hoặc kết thúc không được nhỏ hơn thời gian hiện tại.");
                return false;
            }

            if (startTime.Value >= endTime.Value)
            {
                MessageBox.Show("Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc.");
                return false;
            }

            return true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var openForm = new adminDashboard(indexForm);
            openForm.Show();
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionActivities(indexForm);
            openForm.Show();
            this.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            var openForm = new list_candidate();
            openForm.Show();
            this.Close();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionDetail_Overview(indexForm);
            openForm.Data = Data;
            openForm.Show();
            this.Close();
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionDetail_Result(indexForm);
            openForm.Data = Data;
            openForm.Show();
            this.Close();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            indexForm.Show();
            this.Close();
        }

        private async void updateCampaign_Click(object sender, EventArgs e)
        {
            FirebaseResponse titleCheckResponse = await client.GetTaskAsync("Campaigns/");
            JObject campaignsJson = JObject.Parse(titleCheckResponse.Body);
            var campaigns = campaignsJson.ToObject<Dictionary<string, CAMPAIGN>>();

            bool titleExists = campaigns.Values.Any(u => u.CampaignName == campaignNameEdited.Text);
            if (titleExists)
            {
                MessageBox.Show("Cuộc bỏ phiếu này đã được đăng ký!");
                return;
            }

            bool checkValidTime = ValidateDateTimePickers(startTime, endTime);
            if (!checkValidTime)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(description.Text) || string.IsNullOrWhiteSpace(campaignName.Text) || string.IsNullOrWhiteSpace(category.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            Data.CampaignName = campaignName.Text;
            Data.Description= description.Text;
            Data.StartTime = startTime.Value;
            Data.EndTime = endTime.Value;
            Data.Category= category.Text;

            PushResponse response = await client.PushTaskAsync("Campaigns/", Data);
            MessageBox.Show("Đã lưu!");

            var openForm = new adminElectionDetail_Setting(indexForm);
            openForm.Data = Data;
            openForm.Show();
            this.Close();
        }

        private async void deleteCampaign_Click(object sender, EventArgs e)
        {
            FirebaseResponse titleCheckResponse = await client.GetTaskAsync("Campaigns/");
            JObject campaignsJson = JObject.Parse(titleCheckResponse.Body);
            var campaigns = campaignsJson.ToObject<Dictionary<string, CAMPAIGN>>();

            string keyToDelete = campaigns.FirstOrDefault(c => c.Value.CampaignName == campaignName.Text).Key;
            await client.DeleteTaskAsync($"Campaigns/{keyToDelete}");
            MessageBox.Show("Xoá thành công");

            var openForm = new adminElectionActivities(indexForm);
            openForm.Show();
            this.Close();
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionDetail_Candidate(indexForm);
            openForm.Data = Data;
            openForm.Show();
            this.Close();
        }
    }
}
