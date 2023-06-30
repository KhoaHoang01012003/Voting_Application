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
        private readonly FirebaseConfig _config = new()
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        private IFirebaseClient _client;
        private readonly Form _indexForm;
        private bool _isBackButtonPressed;
        public CAMPAIGN Data { get; set; }

        public adminElectionDetail_Setting(Form parentForm)
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

        private async void adminElectionDetail_Setting_Load(object sender, EventArgs e)
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

            FirebaseResponse classResponse = await _client.GetTaskAsync("Classes").ConfigureAwait(false);
            Dictionary<string, CLASS> classes = classResponse.ResultAs<Dictionary<string, CLASS>>();

            if (IsHandleCreated)
            {
                Invoke((MethodInvoker)delegate
                {
                    UpdateUIWithClassData(classes);
                });
            }
        }

        private void UpdateUIWithClassData(Dictionary<string, CLASS> classes)
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
            {
                Invoke((MethodInvoker)delegate
                {
                    UpdateUIWithClassData(classes);
                });
            }
        }

        private void UpdateUIWithClassData(Dictionary<string, CLASS> classes)
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
                    if (classID != classObj.Class_ID) continue;
                    selectClass.Text += classObj.ClassName + ", ";
                    break;
                }
            }
        }

        private bool ValidateDateTimePickers(Guna2DateTimePicker startTime, Guna2DateTimePicker endTime)
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

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionDetail_Overview(_indexForm) { Data = Data };
            OpenFormAndCloseCurrent(openForm);
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionDetail_Result(_indexForm) { Data = Data };
            OpenFormAndCloseCurrent(openForm);
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            OpenFormAndCloseCurrent(_indexForm);
        }

        private async void updateCampaign_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs() || !ValidateDateTimePickers(startTime, endTime))
            {
                return;
            }

            FirebaseResponse titleCheckResponse = await _client.GetTaskAsync("Campaigns/");
            JObject campaignsJson = JObject.Parse(titleCheckResponse.Body);
            var campaigns = campaignsJson.ToObject<Dictionary<string, CAMPAIGN>>();

            bool titleExists = campaigns.Values.Any(u => u.CampaignName == campaignNameEdited.Text);
            if (titleExists)
            {
                MessageBox.Show("Cuộc bỏ phiếu này đã được đăng ký!");
                return;
            }

            Data.CampaignName = campaignName.Text;
            Data.Description = description.Text;
            Data.StartTime = startTime.Value;
            Data.EndTime = endTime.Value;
            Data.Category = category.Text;

            await _client.PushTaskAsync("Campaigns/", Data);
            MessageBox.Show("Đã lưu!");

            var openForm = new adminElectionDetail_Setting(_indexForm) { Data = Data };
            OpenFormAndCloseCurrent(openForm);
        }

        private bool ValidateInputs()
        {
            if (!string.IsNullOrWhiteSpace(description.Text) && !string.IsNullOrWhiteSpace(campaignName.Text) && !string.IsNullOrWhiteSpace(category.Text))
                return true;

            MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
            return false;
        }

        private async void deleteCampaign_Click(object sender, EventArgs e)
        {
            FirebaseResponse titleCheckResponse = await _client.GetTaskAsync("Campaigns/");
            JObject campaignsJson = JObject.Parse(titleCheckResponse.Body);
            var campaigns = campaignsJson.ToObject<Dictionary<string, CAMPAIGN>>();

            string keyToDelete = campaigns.FirstOrDefault(c => c.Value.CampaignName == campaignName.Text).Key;
            await _client.DeleteTaskAsync($"Campaigns/{keyToDelete}");
            MessageBox.Show("Xoá thành công");

            var openForm = new adminElectionActivities(_indexForm);
            OpenFormAndCloseCurrent(openForm);
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionDetail_Candidate(_indexForm) { Data = Data };
            OpenFormAndCloseCurrent(openForm);
        }
    }
}
