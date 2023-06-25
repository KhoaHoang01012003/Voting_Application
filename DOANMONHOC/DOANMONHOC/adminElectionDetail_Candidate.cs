using FireSharp.Config;
using FireSharp.Interfaces;
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
    public partial class adminElectionDetail_Candidate : Form
    {
        FirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        private Index indexForm;
        public CAMPAIGN Data { get; set; }

        public adminElectionDetail_Candidate(Index indexForm)
        {
            InitializeComponent();
            this.indexForm = indexForm;
        }

        private void adminElectionDetail_Candidate_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            this.Invoke((MethodInvoker)delegate
            {
                campaignName.Text = Data.CampaignName;
            });
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

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionDetail_Overview(indexForm);
            openForm.Data = Data;
            openForm.Show();
            this.Close();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionDetail_Setting(indexForm);
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
    }
}
