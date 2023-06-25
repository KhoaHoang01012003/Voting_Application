using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
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
    public partial class CreateVote13 : Form
    {
        FirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        private Index indexForm;
        public CAMPAIGN Data { get; set; }

        public CreateVote13(Index indexForm)
        {
            InitializeComponent();
            this.indexForm = indexForm;
        }
        private void CreateVote13_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            label2.Text = "Bầu " + Data.CampaignName;
            label4.Text = "Bầu " + Data.CampaignName;
            label6.Text = Data.StartTime.ToString();
            label9.Text = Data.EndTime.ToString();
            label11.Text = Data.Category;
            label13.Text = Data.Candidate_ID.Count().ToString();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            indexForm.Show();
            this.Close();
        }

        private async void addButton_Click(object sender, EventArgs e)
        {
            PushResponse response = await client.PushTaskAsync("Campaigns/", Data);
            MessageBox.Show("Tạo cuộc bỏ phiếu thành công!");
            var openForm = new adminElectionActivities(indexForm);
            openForm.Show();
            this.Close();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionActivities(indexForm);
            openForm.Show();
            this.Close();
        }
    }
}
