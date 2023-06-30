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
        private readonly FirebaseConfig _config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        private IFirebaseClient _client;
        private readonly Form _indexForm;
        private bool _isBackButtonPressed;

        public adminElectionActivities(Form parentForm)
        {
            InitializeComponent();
            this.FormClosed += FormClosed_Exit;
            _indexForm = parentForm;
            _isBackButtonPressed = false;
        }

        void FormClosed_Exit(object sender, FormClosedEventArgs e)
        {
            if (!_isBackButtonPressed)
            {
                Application.ExitThread();
            }
        }

        private async void adminElectionActivities_Load(object sender, EventArgs e)
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

            FirebaseResponse titleCheckResponse = await _client.GetTaskAsync("Campaigns/");
            JObject campaignsJson = JObject.Parse(titleCheckResponse.Body);
            var campaigns = campaignsJson.ToObject<Dictionary<string, CAMPAIGN>>();

            // Đưa các tác vụ phức tạp vào một task để thực hiện song song
            var loadRowsTask = Task.Run(() =>
            {
                DateTime currentTime = DateTime.Now.ToUniversalTime();

                var sortedCampaigns = campaigns
                    .OrderByDescending(c => c.Value.StartTime < currentTime && c.Value.EndTime > currentTime)
                    .ThenBy(c => c.Value.StartTime < currentTime && c.Value.EndTime < currentTime)
                    .ThenBy(c => c.Value.StartTime > currentTime && c.Value.EndTime > currentTime)
                    .ThenBy(c => c.Value.EndTime)
                    .ToDictionary(c => c.Key, c => c.Value);

                int yOffset = sampleRow.Location.Y;

                List<Guna2Panel> rows = new List<Guna2Panel>();

                foreach (var campaignEntry in sortedCampaigns)
                {
                    CAMPAIGN campaign = campaignEntry.Value;

                    // Tạo một row mới và cập nhật thông tin của nó
                    Guna2Panel row = CreateNewRow(campaign, currentTime, yOffset);
                    rows.Add(row);

                    yOffset += sampleRow.Height;
                }

                return rows;
            });

            // Chờ cho tất cả các row được tạo xong
            var rows = await loadRowsTask;

            // Cập nhật giao diện một lần sau khi đã tạo xong tất cả các row
            if (this.IsHandleCreated)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    foreach (var row in rows)
                    {
                        sampleRow.Parent.Controls.Add(row);
                        row.BringToFront();
                    }
                }));
            }
        }

        private Guna2Panel CreateNewRow(CAMPAIGN campaign, DateTime currentTime, int yOffset)
        {
            int xOffset = sampleRow.Location.X;

            Guna2Panel row = new Guna2Panel
            {
                Size = sampleRow.Size,
                CustomizableEdges = sampleRow.CustomizableEdges,
                Location = new Point(xOffset, yOffset),
                ShadowDecoration =
        {
            CustomizableEdges = sampleRow.ShadowDecoration.CustomizableEdges,
            Mode = sampleRow.ShadowDecoration.Mode
        },
                BorderColor = sampleRow.BorderColor,
                CustomBorderThickness = sampleRow.CustomBorderThickness,
                CustomBorderColor = sampleRow.CustomBorderColor
            };

            Label nameLabel = new Label
            {
                Text = campaign.CampaignName,
                Location = sampleNameLabel.Location,
                Size = sampleNameLabel.Size
            };

            var tmp = campaign.StartTime;
            Label startTimeLabel = new Label
            {
                Text = tmp.ToLocalTime().ToString(),
                Location = sampleStartTimeLabel.Location,
                Size = sampleStartTimeLabel.Size
            };

            tmp = campaign.EndTime;
            Label endTimeLabel = new Label
            {
                Text = tmp.ToLocalTime().ToString(),
                Location = sampleEndTimeLabel.Location,
                Size = sampleEndTimeLabel.Size
            };

            Guna2CirclePictureBox statusPicture = new Guna2CirclePictureBox
            {
                FillColor = currentTime >= campaign.StartTime && currentTime <= campaign.EndTime ? Color.Green :
                    currentTime < campaign.StartTime ? Color.Yellow : Color.Red,
                ImageRotate = sampleStatusPicture.ImageRotate,
                Location = sampleStatusPicture.Location,
                ShadowDecoration =
        {
            CustomizableEdges = sampleStatusPicture.ShadowDecoration.CustomizableEdges,
            Mode = sampleStatusPicture.ShadowDecoration.Mode
        },
                Size = sampleStatusPicture.Size
            };
            Label viewDetailLabel = new Label
            {
                ForeColor = sampleViewDetailLabel.ForeColor,
                Text = sampleViewDetailLabel.Text,
                Location = sampleViewDetailLabel.Location,
                Size = sampleViewDetailLabel.Size,
                Cursor = sampleViewDetailLabel.Cursor
            };

            void add_Click(object sender, EventArgs e)
            {
                var openForm = new adminElectionDetail_Overview(_indexForm)
                {
                    Data = campaign
                };
                _isBackButtonPressed = true;
                openForm.Show();
                this.Close();
            }

            viewDetailLabel.Click += add_Click;

            row.Controls.Add(nameLabel);
            row.Controls.Add(startTimeLabel);
            row.Controls.Add(endTimeLabel);
            row.Controls.Add(statusPicture);
            row.Controls.Add(viewDetailLabel);

            return row;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var openForm = new CreateVote1(_indexForm);
            openForm.Show();
            _isBackButtonPressed = true;
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var openForm = new adminDashboard(_indexForm);
            openForm.Show();
            _isBackButtonPressed = true;
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionActivities(_indexForm);
            openForm.Show();
            _isBackButtonPressed = true;
            this.Close();
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            var openForm = new list_candidate(_indexForm);
            openForm.Show();
            _isBackButtonPressed = true;
            this.Close();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            _indexForm.Show();
            _isBackButtonPressed = true;
            this.Close();
        }

        private void avatar_Click(object sender, EventArgs e)
        {
            var form = new changeInfo_Admin(_indexForm);
            form.Show();
            _isBackButtonPressed = true;
            this.Close();
        }
    }
}
