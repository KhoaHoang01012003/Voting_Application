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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Suite;
using Newtonsoft.Json.Linq;

namespace DOANMONHOC
{

    public partial class Dashboard : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        private Index indexForm = new Index();

        public Dashboard()
        {
            InitializeComponent();        }

        private async void Dashboard_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            FirebaseResponse response = await client.GetTaskAsync("Users/");
            Dictionary<string, USER> users = response.ResultAs<Dictionary<string, USER>>();
            foreach (var user in users)
            {
                if (user.Value.UserName == Properties.Settings.Default.Username.ToString())
                {
                    label_user_name.Text = user.Value.Fullname;
                    label8.Text = user.Value.Fullname;
                    break;
                }
            }

            // Lấy dữ liệu từ table Campaign
            FirebaseResponse titleCheckResponse = await client.GetTaskAsync("Campaigns/");
            JObject campaignsJson = JObject.Parse(titleCheckResponse.Body);
            var campaigns = campaignsJson.ToObject<Dictionary<string, CAMPAIGN>>();
            List<CAMPAIGN> campaignList = campaigns.Values.ToList();
            int count = campaignList.Count;

            List<Panel> panelList_action = new List<Panel>();
            // Vòng lặp để tạo các panel con tương ứng với mỗi cuộc bầu cử
            action.Hide();
            int x = action.Location.X;
            int y = action.Location.Y;
            foreach (CAMPAIGN campaign in campaignList)
            {
                Guna2Panel panel = new Guna2Panel();
                panel.Location = new Point(x, y);
                panel.Size = action.Size;
                panel.BorderStyle = action.BorderStyle;
                panel.BorderThickness = 1;
                panel.BorderRadius = 20;
                panel.BorderColor = Color.FromArgb(37, 83, 140);

                // Tạo một TextBox để hiển thị tên của chiến dịch
                TextBox nameBox = new TextBox();
                nameBox.Name = "Campaign_name " + count.ToString();
                nameBox.Location = action_name.Location;
                nameBox.Size = action_name.Size;
                nameBox.ReadOnly = true;
                nameBox.Text = campaign.CampaignName;
                nameBox.BorderStyle = BorderStyle.None;
                nameBox.BackColor = action_name.BackColor;
                nameBox.Font = action_name.Font;
                // Tạo một TextBox để hiển thị thời gian của chiến dịch
                TextBox timeBox = new TextBox();
                timeBox.Location = action_starttime.Location;
                timeBox.Size = action_starttime.Size;
                timeBox.ReadOnly = true;
                timeBox.Text = campaign.StartTime.ToString();
                timeBox.BorderStyle = BorderStyle.None;
                timeBox.BackColor = action_starttime.BackColor;
                timeBox.Font = action_starttime.Font;
                // Tạo status
                Guna2Shapes stt = new Guna2Shapes();
                stt.Location = status.Location;
                stt.PolygonSkip = 1;
                stt.Rotate = 0F;
                stt.Shape = Guna.UI2.WinForms.Enums.ShapeType.Ellipse;
                stt.Size = status.Size;
                stt.RoundedRadius = 20;
                if (DateTime.Compare(DateTime.Now, campaign.StartTime) >= 0 && DateTime.Compare(DateTime.Now, campaign.EndTime) <= 0)
                {
                    stt.FillColor = happening.FillColor;
                    stt.BorderColor = happening.FillColor;
                }
                else if (DateTime.Compare(DateTime.Now, campaign.StartTime) <= 0)
                {
                    stt.FillColor = waiting.FillColor;
                    stt.BorderColor = waiting.FillColor;
                }
                else
                {
                    stt.FillColor = end.FillColor;
                    stt.BorderColor = end.FillColor;
                }
                // Thêm TextBox vào Panel
                panel.Controls.Add(nameBox);
                panel.Controls.Add(timeBox);
                panel.Controls.Add(stt);
                // Thêm Panel vào danh sách Panel
                panelList_action.Add(panel);
                y += 60;
            }
            // Thêm các Panel vào Form
            foreach (Panel panel in panelList_action)
            {
                totalAction.Controls.Add(panel);
                panel.Click += new EventHandler(panel_action_Click);
            }
        }
        private async void panel_action_Click(object sender, EventArgs e)
        {
            // Lấy panel được click
            Panel clickedPanel = sender as Panel;

            // Hiển thị chi tiết trong panel Chi tiết hoạt động
            foreach (Control control in clickedPanel.Controls)
            {
                if (control.Name.Contains("Campaign_name"))
                {
                    FirebaseResponse titleCheckResponse = await client.GetTaskAsync("Campaigns/");
                    JObject campaignsJson = JObject.Parse(titleCheckResponse.Body);
                    var campaigns = campaignsJson.ToObject<Dictionary<string, CAMPAIGN>>();
                    List<CAMPAIGN> campaignList = campaigns.Values.ToList();
                    foreach (CAMPAIGN campaign in campaignList)
                    {
                        if (control.Text == campaign.CampaignName)
                        {
                            name_of_action.Text = "Tên: " + campaign.CampaignName;
                            description.Text = campaign.Description;
                        }
                    }

                }
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            indexForm.Show();
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Form vote = new vote_view_candidate_details();
            vote.Show();
            this.Close();
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            Form change = new change_user_info();
            change.ShowDialog();
        }
    }
}
