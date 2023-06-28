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
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics.Metrics;

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
            InitializeComponent();
        }

        private async void Dashboard_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
            var task1 = client.GetTaskAsync("Users/");
            var task2 = client.GetTaskAsync("Votes/");
            var task3 = client.GetTaskAsync("Candidates/");
            var task4 = client.GetTaskAsync("Classes/");

            await Task.WhenAll(task1, task2, task3, task4);

            FirebaseResponse response1 = task1.Result;
            FirebaseResponse response2 = task2.Result;
            FirebaseResponse response3 = task3.Result;
            FirebaseResponse response4 = task4.Result;

            Dictionary<string, USER> users = response1.ResultAs<Dictionary<string, USER>>();
            Dictionary<string, VOTE> votes = response2.ResultAs<Dictionary<string, VOTE>>();
            Dictionary<string, CANDIDATE> candidates = response3.ResultAs<Dictionary<string, CANDIDATE>>();
            Dictionary<string, CLASS> classes = response4.ResultAs<Dictionary<string, CLASS>>();

            foreach (var user in users)
            {
                if (user.Value.UserName == Properties.Settings.Default.Username.ToString())
                {
                    label_user_name.Text = user.Value.Fullname;
                    label8.Text = user.Value.Fullname;
                    break;
                }
            }
            //Lấy avatar user
            byte[] originalBytes_avatar = Convert.FromBase64String(Properties.Settings.Default.avt);
            Image image_avatar;
            using (MemoryStream ms = new MemoryStream(originalBytes_avatar))
            {
                image_avatar = Image.FromStream(ms);
            }
            Image avatarImage = image_avatar.GetThumbnailImage(100, 100, null, IntPtr.Zero);
            Avatar.Image = avatarImage;

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
                panel.Click += panel_Click;

                //Click vào, hiển thị chi tiết Candidate trong campaign
                void panel_Click(object sender, EventArgs e)
                {
                    name_of_action.Text = "Tên: " + campaign.CampaignName;
                    description.Text = campaign.Description;
                    int cntUser = 0;
                    HashSet<int> classIds = new HashSet<int>(campaign.Class_ID);

                    foreach (var user in users)
                    {
                        if (classIds.Contains(user.Value.Class_ID))
                        {
                            cntUser++;
                        }
                    }

                    int cntVote = 0;
                    foreach (var vote in votes)
                    {
                        if (vote.Value.Campaign_ID == campaign.Campaint_ID.ToString())
                        {
                            cntVote++;
                        }
                    }

                    int xOffset = sampleRow.Location.X;
                    int yOffset = sampleRow.Location.Y;

                    // Tạo danh sách các ứng viên cùng với số phiếu mỗi người nhận được
                    List<Tuple<CANDIDATE, CLASS, int>> candidateVotes = new List<Tuple<CANDIDATE, CLASS, int>>();
                    foreach (var candidate in candidates.Values)
                    {
                        int votePerCandidate = votes.Values.Count(vote => vote.Candidate_ID == candidate.Candidate_ID.ToString() && vote.Campaign_ID == campaign.Campaint_ID.ToString());
                        CLASS candidateClass = classes.Values.FirstOrDefault(ObjClass => ObjClass.Class_ID == candidate.Class_ID);
                        candidateVotes.Add(Tuple.Create(candidate, candidateClass, votePerCandidate));
                    }

                    // Sắp xếp danh sách theo số phiếu giảm dần
                    candidateVotes.Sort((a, b) => b.Item3.CompareTo(a.Item3));

                    int tmp = 0;
                    foreach (var item in candidateVotes)
                    {
                        for (int i = 0; i < campaign.Candidate_ID.Length; i++)
                        {
                            if (campaign.Candidate_ID[i] == item.Item1.Candidate_ID)
                            {
                                tmp++;

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
                                nameLabel.Location = sampleNameLabel.Location;
                                nameLabel.Size = sampleNameLabel.Size;
                                nameLabel.Text = item.Item1.CandidateName;
                                // Tạo label class
                                Label className = new Label();
                                className.Location = sampleClassLabel.Location;
                                className.Size = sampleClassLabel.Size;
                                className.Text = item.Item2.ClassName;

                                // Tạo label num votes
                                Label numVotes = new Label();
                                numVotes.Location = sampleNumVotesLabel.Location;
                                numVotes.Size = sampleNumVotesLabel.Size;
                                numVotes.Text = item.Item3.ToString();

                                row.Controls.Add(nameLabel);
                                row.Controls.Add(className);
                                row.Controls.Add(numVotes);

                                sampleRow.Parent.Controls.Add(row);
                                row.BringToFront();

                                yOffset += sampleRow.Height;

                                break;
                            }
                        }
                        if (tmp == campaign.Candidate_ID.Length)
                        {
                            break;
                        }
                    }

                    //chart
                    // Tổng số user
                    Chart chart_totalUser = new Chart();
                    ChartArea chartAreaUser = new ChartArea("chartAreaUser");
                    chart_totalUser.ChartAreas.Add(chartAreaUser);
                    Legend legendUser = new Legend("legendUser");
                    chart_totalUser.Legends.Add(legendUser);
                    chart_totalUser.Location = new Point(151, 83);
                    chart_totalUser.Size = new Size(200, 200);
                    Series series1 = new Series("series1");
                    series1.ChartType = SeriesChartType.Doughnut;
                    series1.Points.Add(cntUser);
                    series1.Points[0].Color = Color.FromArgb(56, 67, 168);
                    series1.Legend = "legendUser";
                    series1.ChartArea = "chartAreaUser";
                    chart_totalUser.Series.Add(series1);
                    chart_totalUser.Legends["legendUser"].Enabled = false;

                    //Tổng số phiếu bầu
                    Chart chart_totalVote = new Chart();
                    ChartArea chartAreaVote = new ChartArea("chartAreaVote");
                    chart_totalVote.ChartAreas.Add(chartAreaVote);
                    Legend legendVote = new Legend("legendVote");
                    chart_totalVote.Legends.Add(legendVote);
                    chart_totalVote.Location = new Point(489, 83);
                    chart_totalVote.Size = new Size(200, 200);
                    Series series2 = new Series("series2");
                    series2.ChartType = SeriesChartType.Doughnut;
                    double percentVote = ((double)cntVote / cntUser) * 100;
                    double percentNotVote = 100 - percentVote;
                    series2.Points.AddXY("", percentVote);
                    series2.Points.AddXY("", percentNotVote);
                    series2.Points[0].Color = Color.FromArgb(52, 147, 94);
                    series2.Points[1].Color = Color.FromArgb(199, 254, 204);
                    series2.ChartArea = "chartAreaVote";
                    series2.Legend = "legendVote";
                    chart_totalVote.Series.Add(series2);
                    chart_totalVote.Legends["legendVote"].Enabled = false;

                    //Số candidate trong campaign so với tổng candidate
                    Chart chart_cdd = new Chart();
                    ChartArea chartAreaCandidate = new ChartArea("chartAreaCandidate");
                    chart_cdd.ChartAreas.Add(chartAreaCandidate);
                    Legend legendCandidate = new Legend("legendCandidate");
                    chart_cdd.Legends.Add(legendCandidate);
                    chart_cdd.Location = new Point(818, 83);
                    chart_cdd.Size = new Size(200, 200);
                    Series series3 = new Series("series3");
                    series3.ChartType = SeriesChartType.Doughnut;
                    double percentCandidate = ((double)campaign.Candidate_ID.Length / candidates.Count) * 100;
                    double percentNotCandidate = 100 - percentCandidate;
                    series3.Points.AddXY("", percentCandidate);
                    series3.Points.AddXY("", percentNotCandidate);
                    series3.Points[0].Color = Color.FromArgb(229, 233, 65);
                    series3.Points[1].Color = Color.FromArgb(242, 252, 179);
                    series3.ChartArea = "chartAreaCandidate";
                    series3.Legend = "legendCandidate";
                    chart_cdd.Series.Add(series3);
                    chart_cdd.Legends["legendCandidate"].Enabled = false;

                    //
                    totalUser.Text = cntUser.ToString();
                    totalUser.BringToFront();
                    totalVote.Text = cntVote.ToString();
                    cdd_in_cpn.Text = campaign.Candidate_ID.Length.ToString();

                    chart1.Parent.Controls.Add(chart_totalUser);
                    chart1.Parent.Controls.Add(chart_totalVote);
                    chart1.Parent.Controls.Add(chart_cdd);
                    chart_totalUser.BringToFront();
                    chart_totalVote.BringToFront();
                    chart_cdd.BringToFront();

                }


                // Thêm Panel vào danh sách Panel
                panelList_action.Add(panel);
                y += 60;
            }
            // Thêm các Panel vào Form
            foreach (Panel panel in panelList_action)
            {
                totalAction.Controls.Add(panel);
            }

            //Hiển thị chi tiết Candidate trong campaign


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

        private void description_TextChanged(object sender, EventArgs e)
        {

        }

        private void totalAction_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
