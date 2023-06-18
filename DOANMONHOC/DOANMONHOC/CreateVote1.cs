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

        private async Task<int> id_index()
        {
            for (int i = 1; ; i++)
            {
                FirebaseResponse response = await client.GetTaskAsync("Admins/" + i.ToString());
                if (response.Body == "null") return i;
            }
        }

        private async void Dashboard_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            Task<int> task = id_index();
            int limit = await task;
            for (int i = 1; i < limit; i++)
            {
                FirebaseResponse response = await client.GetTaskAsync("Admins/" + i.ToString());
                ADMIN admin = response.ResultAs<ADMIN>();
                if (admin.UserName == Properties.Settings.Default.Username.ToString())
                {
                    label7.Text = admin.AdminName;
                    break;
                }
            }
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
                var titleCheck = titleCheckResponse.ResultAs<Dictionary<string, CAMPAIGN>>();

                bool titleExists = titleCheck.Values.Any(u => u.Title == campaignName.Text);

                if (titleExists)
                {
                    MessageBox.Show("Sự kiện này đã được đăng ký!");
                    return;
                }

                var data = new CAMPAIGN
                {
                    Title = campaignName.Text,
                    Description = "",
                    CampaignName = campaignName.Text,
                    StartTime = startTime.Value,
                    EndTime = endTime.Value,
                    Status = 0,
                    Category = category.Text
                };


                PushResponse response = await client.PushTaskAsync("Campaigns/", data);
                USER result = response.ResultAs<USER>();
                var open = new CreateVote2(indexForm);
                open.Show();
                this.Close();
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
