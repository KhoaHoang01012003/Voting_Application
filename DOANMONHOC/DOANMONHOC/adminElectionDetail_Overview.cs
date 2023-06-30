using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
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

namespace DOANMONHOC
{
    public partial class adminElectionDetail_Overview : Form
    {
        private readonly FirebaseConfig _config = new()
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        private IFirebaseClient _client;
        private readonly Form _indexForm;
        private bool _isBackButtonPressed;
        public CAMPAIGN Data { get; set; }

        public adminElectionDetail_Overview(Form parentForm)
        {
            InitializeComponent();
            FormClosed += FormClosed_Exit;
            _indexForm = parentForm;
            _isBackButtonPressed = false;
        }

        private void FormClosed_Exit(object sender, FormClosedEventArgs e)
        {
            if (!_isBackButtonPressed)
            {
                Application.ExitThread();
            }
        }

        private async void CreateVote2_Load(object sender, EventArgs e)
        {
            _client = new FireSharp.FirebaseClient(_config);
            byte[] originalBytesAvt = Convert.FromBase64String(Properties.Settings.Default.avt.ToString());

            // Tạo một đối tượng Image từ chuỗi byte gốc
            Image imageAvt;
            using (MemoryStream ms = new MemoryStream(originalBytesAvt))
            {
                imageAvt = Image.FromStream(ms);
            }

            avatar.Image = imageAvt.GetThumbnailImage(40, 40, null, IntPtr.Zero);
            FullName.Text = Properties.Settings.Default.Name.ToString();

            campaignName.Text = Data.CampaignName;
            startTime.Text = Data.StartTime.ToString();
            endTime.Text = Data.EndTime.ToString();
            cntCandidates.Text = Data.Candidate_ID.Count().ToString();
            category.Text = Data.Category;
        }

        private void OpenFormAndCloseCurrent(Form openForm)
        {
            openForm.Show();
            _isBackButtonPressed = true;
            Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var openForm = new adminDashboard(_indexForm);
            OpenFormAndCloseCurrent(openForm);
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            _indexForm.Show();
            _isBackButtonPressed = true;
            Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionActivities(_indexForm);
            OpenFormAndCloseCurrent(openForm);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            var openForm = new list_candidate(_indexForm);
            OpenFormAndCloseCurrent(openForm);
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionDetail_Setting(_indexForm) { Data = Data };
            OpenFormAndCloseCurrent(openForm);
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionDetail_Result(_indexForm) { Data = Data };
            OpenFormAndCloseCurrent(openForm);
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionDetail_Candidate(_indexForm) { Data = Data };
            OpenFormAndCloseCurrent(openForm);
        }
    }

}
