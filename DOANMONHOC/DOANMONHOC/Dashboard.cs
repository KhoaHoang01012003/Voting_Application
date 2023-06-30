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
        private Form indexForm;
        private bool isBackButtonPressed;

        Guna2ShadowPanel Result = new Guna2ShadowPanel();

        public Dashboard(Form parentForm)
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(FormClosed_Exit);
            indexForm = parentForm;
            isBackButtonPressed = false;

            Result.BackColor = Color.White;
            Result.FillColor = Color.White;
            Result.Location = new Point(311, 1487);
            Result.Name = "Result";
            Result.ShadowColor = Color.Black;
            Result.Size = new Size(1089, 378);
            Result.AutoSize = true;
            Result.TabIndex = 26;
            Result.AutoScroll = true;
            Result.ResumeLayout(false);
            Result.PerformLayout();

            this.Controls.Add(Result);
            Result.Hide();
        }

        void FormClosed_Exit(object sender, FormClosedEventArgs e)
        {
            if (!isBackButtonPressed)
            {
                Application.ExitThread();
            }
        }

        private async void Dashboard_Load(object sender, EventArgs e)
        {
            action.Hide();
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
            List<CAMPAIGN> sortedCampaignList = campaignList.OrderBy(campaign =>
            {
                if (DateTime.Now.ToUniversalTime() >= campaign.StartTime && DateTime.Now.ToUniversalTime() <= campaign.EndTime)
                {
                    return 0;
                }
                else if (DateTime.Now.ToUniversalTime() < campaign.StartTime)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }).ToList();
            int count = campaignList.Count;

            List<Panel> panelList_action = new List<Panel>();
            // Vòng lặp để tạo các panel con tương ứng với mỗi cuộc bầu cử

            int x = action.Location.X;
            int y = action.Location.Y;

            await Task.Run(() =>
            {
                foreach (CAMPAIGN campaign in sortedCampaignList)
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
                    var tmp = campaign.EndTime;
                    timeBox.Text = tmp.ToLocalTime().ToString();
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
                    if (DateTime.Compare(DateTime.Now.ToUniversalTime(), campaign.StartTime) >= 0 && DateTime.Compare(DateTime.Now.ToUniversalTime(), campaign.EndTime) <= 0)
                    {
                        stt.FillColor = happening.FillColor;
                        stt.BorderColor = happening.FillColor;
                    }
                    else if (DateTime.Compare(DateTime.Now.ToUniversalTime(), campaign.StartTime) <= 0)
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
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            panel.Controls.Add(nameBox);
                            panel.Controls.Add(timeBox);
                            panel.Controls.Add(stt);
                            panel.Click += panel_Click;
                        }));
                    }

                    //Click vào, hiển thị chi tiết Candidate trong campaign
                    void panel_Click(object sender, EventArgs e)
                    {

                        // 
                        // sampleNumVotesLabel
                        // 
                        Label sampleNumVotesLabel = new Label();
                        sampleNumVotesLabel.Location = new Point(825, 29);
                        sampleNumVotesLabel.Name = "sampleNumVotesLabel";
                        sampleNumVotesLabel.Size = new Size(138, 27);
                        sampleNumVotesLabel.TabIndex = 3;
                        // 
                        // sampleClassLabel
                        // 
                        Label sampleClassLabel = new Label();
                        sampleClassLabel.Location = new Point(387, 29);
                        sampleClassLabel.Name = "sampleClassLabel";
                        sampleClassLabel.Size = new Size(187, 27);
                        sampleClassLabel.TabIndex = 2;
                        // sampleNameLabel
                        Label sampleNameLabel = new Label();
                        sampleNameLabel.Location = new Point(37, 29);
                        sampleNameLabel.Name = "sampleNameLabel";
                        sampleNameLabel.Size = new Size(273, 24);
                        sampleNameLabel.TabIndex = 1;
                        //Create sampleRow
                        Guna2Panel sampleRow = new Guna2Panel();
                        sampleRow.AutoScroll = true;
                        sampleRow.BorderColor = Color.Black;
                        sampleRow.Controls.Add(sampleNumVotesLabel);
                        sampleRow.Controls.Add(sampleClassLabel);
                        sampleRow.Controls.Add(sampleNameLabel);
                        sampleRow.CustomBorderColor = Color.FromArgb(189, 189, 189);
                        sampleRow.CustomBorderThickness = new Padding(0, 0, 0, 1);
                        sampleRow.Location = new Point(24, 92);
                        sampleRow.Name = "sampleRow";
                        sampleRow.Size = new Size(1012, 82);
                        sampleRow.TabIndex = 0;
                        // 
                        // label9
                        // 
                        Label label9 = new Label();
                        label9.AutoSize = true;
                        label9.Location = new Point(825, 28);
                        label9.Name = "label9";
                        label9.Size = new Size(96, 20);
                        label9.TabIndex = 3;
                        label9.Text = "Số phiếu bầu";
                        // 
                        // label10
                        // 
                        Label label10 = new Label();
                        label10.AutoSize = true;
                        label10.Location = new Point(387, 28);
                        label10.Name = "label10";
                        label10.Size = new Size(34, 20);
                        label10.TabIndex = 1;
                        label10.Text = "Lớp";
                        // 
                        // label11
                        // 
                        Label label11 = new Label();
                        label11.AutoSize = true;
                        label11.Location = new Point(37, 28);
                        label11.Name = "label11";
                        label11.Size = new Size(88, 20);
                        label11.TabIndex = 0;
                        label11.Text = "Ứng cử viên";

                        Guna2Panel guna2Panel2 = new Guna2Panel();
                        guna2Panel2.BackColor = Color.FromArgb(37, 83, 140);
                        guna2Panel2.Controls.Add(label9);
                        guna2Panel2.Controls.Add(label10);
                        guna2Panel2.Controls.Add(label11);
                        guna2Panel2.ForeColor = Color.White;
                        guna2Panel2.Location = new Point(24, 10);
                        guna2Panel2.Name = "guna2Panel2";
                        guna2Panel2.Size = new Size(1015, 82);
                        guna2Panel2.TabIndex = 3;

                        guna2ShadowPanel6.Controls.Clear();

                        guna2ShadowPanel6.Controls.Add(sampleRow);
                        guna2ShadowPanel6.Controls.Add(guna2Panel2);

                        name_of_action.Text = "Tên: " + campaign.CampaignName;
                        description.Text = campaign.Description;
                        int cntUser = 0;
                        HashSet<int> classIds = new HashSet<int>(campaign.Class_ID);
                        if (stt.FillColor == end.FillColor)
                        {
                            Result.Show();
                            Result.Controls.Clear();
                            Label label_Result = new Label();
                            label_Result.AutoSize = true;
                            label_Result.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Pixel);
                            label_Result.ForeColor = Color.FromArgb(37, 83, 140);
                            label_Result.Location = new Point(517, 15);
                            label_Result.Name = "label15";
                            label_Result.Size = new Size(121, 35);
                            label_Result.TabIndex = 21;
                            label_Result.Text = "KẾT QUẢ";
                            Result.Controls.Add(label_Result);
                        }
                        else
                            Result.Hide();

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
                        int maxVote = candidateVotes[0].Item3;
                        int LocationY = 0;

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
                            if (maxVote == item.Item3 && maxVote != 0)
                            {
                                //PictureBox AVT
                                Guna2CirclePictureBox Avt_cdd_re = new Guna2CirclePictureBox();
                                byte[] originalBytes = Convert.FromBase64String(item.Item1.AvtCandidate);
                                // Tạo một đối tượng Image từ chuỗi byte gốc
                                Image image;
                                using (MemoryStream ms = new MemoryStream(originalBytes))
                                {
                                    image = Image.FromStream(ms);
                                }
                                // Tạo bản thu nhỏ của ảnh
                                Image thumbnailImage = image.GetThumbnailImage(150, 150, null, IntPtr.Zero);
                                Avt_cdd_re.ImageRotate = 0F;
                                Avt_cdd_re.Location = new Point(86, (84 + LocationY * 160));
                                Avt_cdd_re.Name = "Avt_cdd_re";
                                Avt_cdd_re.Size = new Size(150, 150);
                                Avt_cdd_re.TabIndex = 22;
                                Avt_cdd_re.TabStop = false;
                                Avt_cdd_re.Image = thumbnailImage;

                                TextBox candidate_class = new TextBox();
                                candidate_class.BorderStyle = BorderStyle.None;
                                candidate_class.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point);
                                candidate_class.Location = new Point(278, (153 + LocationY * 160));
                                candidate_class.Multiline = true;
                                candidate_class.Name = "candidate_class";
                                candidate_class.Size = new Size(267, 42);
                                candidate_class.TabIndex = 23;
                                candidate_class.Text = item.Item2.ClassName;
                                candidate_class.TextAlign = HorizontalAlignment.Center;

                                TextBox candidate_name = new TextBox();
                                candidate_name.BorderStyle = BorderStyle.None;
                                candidate_name.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point);
                                candidate_name.Location = new Point(278, (84 + LocationY * 160));
                                candidate_name.Multiline = true;
                                candidate_name.Name = "candidate_name";
                                candidate_name.Size = new Size(267, 42);
                                candidate_name.TabIndex = 23;
                                candidate_name.Text = item.Item1.CandidateName;
                                candidate_name.TextAlign = HorizontalAlignment.Center;

                                Result.Controls.Add(candidate_name);
                                Result.Controls.Add(candidate_class);
                                Result.Controls.Add(Avt_cdd_re);
                                LocationY++;
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

                        if (this.IsHandleCreated)
                        {
                            this.Invoke(new Action(() =>
                            {
                                chart1.Parent.Controls.Add(chart_totalUser);
                                chart1.Parent.Controls.Add(chart_totalVote);
                                chart1.Parent.Controls.Add(chart_cdd);
                            }));
                        }
                        chart_totalUser.BringToFront();
                        chart_totalVote.BringToFront();
                        chart_cdd.BringToFront();

                    }


                    // Thêm Panel vào danh sách Panel
                    panelList_action.Add(panel);
                    y += 60;
                }
                // Thêm các Panel vào Form
                if (this.IsHandleCreated)
                {
                    foreach (Panel panel in panelList_action)
                    {
                        if (this.IsHandleCreated)
                        {
                            this.Invoke(new Action(() =>
                            {
                                totalAction.Controls.Add(panel);
                            }));
                        }
                    }
                }
            });




        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            indexForm.Show();
            isBackButtonPressed = true;
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Form vote = new vote_view_candidate_details(indexForm);
            vote.Show();
            isBackButtonPressed = true;
            this.Close();
        }

        private void Avatar_Click(object sender, EventArgs e)
        {
            Form change = new changeInfo(indexForm);
            isBackButtonPressed = true;
            change.Show();
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Dashboard_Load(sender, e);
        }

    }
}
