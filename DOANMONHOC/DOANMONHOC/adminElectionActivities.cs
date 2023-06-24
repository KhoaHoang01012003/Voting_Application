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
    public partial class adminElectionActivities : Form
    {
        FirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        private Index indexForm;

        public adminElectionActivities(Index indexForm)
        {
            InitializeComponent();
            this.indexForm = indexForm;
        }

        private async void adminElectionActivities_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            FirebaseResponse titleCheckResponse = await client.GetTaskAsync("Campaigns/");
            JObject campaignsJson = JObject.Parse(titleCheckResponse.Body);
            var campaigns = campaignsJson.ToObject<Dictionary<string, CAMPAIGN>>();

            int xOffset = sampleRow.Location.X;
            int yOffset = sampleRow.Location.Y;

            for (int i = 0; i < campaigns.Count; i++)
            {
                CAMPAIGN campaign = campaigns.ElementAt(i).Value;

                // Tạo Panel
                Guna2Panel row = new Guna2Panel();
                row.Size = sampleRow.Size;
                row.CustomizableEdges = sampleRow.CustomizableEdges;
                row.Location = new Point(xOffset, yOffset);
                row.ShadowDecoration.CustomizableEdges = sampleRow.ShadowDecoration.CustomizableEdges;
                row.BorderColor = sampleRow.BorderColor;
                row.CustomBorderThickness = sampleRow.CustomBorderThickness;
                row.CustomBorderColor = sampleRow.CustomBorderColor;

                // Tạo label name
                Label nameLabel = new Label();
                nameLabel.Text = campaign.CampaignName;
                nameLabel.Location = sampleNameLabel.Location;
                nameLabel.Size = sampleNameLabel.Size;

                // Tạo label start time
                Label startTime = new Label();
                startTime.Text = campaign.StartTime.ToString();
                startTime.Location = sampleStartTimeLabel.Location;
                startTime.Size = sampleStartTimeLabel.Size;

                // Tạo label end time
                Label endTime = new Label();
                endTime.Text = campaign.EndTime.ToString();
                endTime.Location = sampleEndTimeLabel.Location;
                endTime.Size = sampleEndTimeLabel.Size;

                // Tạo status color
                Guna2CirclePictureBox sampleStatus = new Guna2CirclePictureBox();
                sampleStatus.FillColor = sampleStatusPicture.FillColor;
                sampleStatus.ImageRotate = sampleStatusPicture.ImageRotate;
                sampleStatus.Location = sampleStatusPicture.Location;
                sampleStatus.ShadowDecoration.CustomizableEdges = sampleStatusPicture.ShadowDecoration.CustomizableEdges;
                sampleStatus.ShadowDecoration.Mode = sampleStatusPicture.ShadowDecoration.Mode;
                sampleStatus.Size = sampleStatusPicture.Size;

                // Tạo label view details
                Label viewDetail = new Label();
                viewDetail.ForeColor = sampleViewDetailLabel.ForeColor;
                viewDetail.Text = sampleViewDetailLabel.Text;
                viewDetail.Location = sampleViewDetailLabel.Location;
                viewDetail.Size = sampleViewDetailLabel.Size;
                viewDetail.Cursor = sampleViewDetailLabel.Cursor;
                viewDetail.Click += add_Click;

                void add_Click(object sender, EventArgs e)
                {
                    var openForm = new adminElectionDetail_Overview(indexForm);
                    openForm.Data = campaign;
                    openForm.Show();
                    this.Close();
                }

                row.Controls.Add(nameLabel);
                row.Controls.Add(startTime);
                row.Controls.Add(endTime);
                row.Controls.Add(sampleStatus); 
                row.Controls.Add(viewDetail);

                sampleRow.Parent.Controls.Add(row);
                row.BringToFront();

                yOffset += sampleRow.Height;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var openForm = new CreateVote1(indexForm);
            openForm.Show();
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            indexForm.Show();
            this.Close();
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}
