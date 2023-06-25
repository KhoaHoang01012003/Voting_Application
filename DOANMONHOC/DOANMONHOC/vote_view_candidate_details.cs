﻿using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Suite;
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
    public partial class vote_view_candidate_details : Form
    {
        FirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        private Index indexForm;
        public vote_view_candidate_details(Index indexForm)
        {
            InitializeComponent();
            this.indexForm = indexForm;
        }

        private async void vote_view_candidate_details_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            FirebaseResponse response = await client.GetTaskAsync("Users/");
            Dictionary<string, USER> users = response.ResultAs<Dictionary<string, USER>>();
            foreach (var user in users)
            {
                if (user.Value.UserName == Properties.Settings.Default.Username.ToString())
                {
                    label7.Text = user.Value.Fullname;
                    break;
                }
            }
            //Lấy list campaign
            FirebaseResponse titleCheckResponse = await client.GetTaskAsync("Campaigns/");
            JObject campaignsJson = JObject.Parse(titleCheckResponse.Body);
            var campaigns = campaignsJson.ToObject<Dictionary<string, CAMPAIGN>>();
            List<CAMPAIGN> campaignList = campaigns.Values.ToList();
            //Lấy list candidate
            FirebaseResponse response_candidate = await client.GetTaskAsync("Candidates/");
            Dictionary<string, CANDIDATE> candidates = response_candidate.ResultAs<Dictionary<string, CANDIDATE>>();
            List<CANDIDATE> candidatesList = candidates.Values.ToList();

            FirebaseResponse cls = await client.GetTaskAsync("Classes/");
            Dictionary<string, CLASS> classes = cls.ResultAs<Dictionary<string, CLASS>>();

            int xOffset = 312;
            int yOffset = 213;
            int itemWidth = 1089;
            int itemHeight = 360;
            int spacing = 52;
            Candidate_Detail.Hide();
            info.Hide();
            /*int x_candidate_detail_panel = Candidate_Detail.Location.X;
            int y_candidate_detail_panel = Candidate_Detail.Location.Y;*/
            foreach (CAMPAIGN campaign in campaignList)
            {
                Panel panel = new Panel();
                panel.Location = new Point(xOffset, yOffset);
                panel.Size = new Size(itemWidth, itemHeight);

                // Tạo nhãn tên campaign
                Label name = new Label();
                name.AutoSize = true;
                name.AutoSize = campaign_name.AutoSize;
                name.Anchor = campaign_name.Anchor;
                name.Font = campaign_name.Font;
                name.Location = new Point(77, 12);
                name.Text = campaign.CampaignName;
                MessageBox.Show(name.Text);
                panel.Controls.Add(name);



                int x_infopanel = 1;
                int y_infopanel = 57;
                List<Panel> panelList_candidate = new List<Panel>();
                foreach (int id in campaign.Candidate_ID)
                {
                    foreach (CANDIDATE candidate in candidatesList)
                    {
                        if (id == candidate.Candidate_ID)
                        {
                            Panel panelinfo = new Panel();
                            panelinfo.Location = new Point(x_infopanel, y_infopanel);
                            panelinfo.Size = info.Size;
                            // tên candidate
                            TextBox namecdd = new TextBox();
                            namecdd.Location = candidate_name.Location;
                            namecdd.BorderStyle = candidate_name.BorderStyle;
                            namecdd.Font = candidate_name.Font;
                            namecdd.Multiline = true;
                            namecdd.Size = candidate_name.Size;
                            namecdd.Text = candidate.CandidateName;
                            namecdd.TextAlign = HorizontalAlignment.Center;
                            //class candidate
                            TextBox classcdd = new TextBox();
                            classcdd.Location = candidate_class.Location;
                            classcdd.BorderStyle = candidate_class.BorderStyle;
                            classcdd.Font = candidate_class.Font;
                            classcdd.Multiline = true;
                            classcdd.Size = candidate_class.Size;
                            foreach (var Class in classes)
                            {
                                if (Class.Value.Class_ID == candidate.Class_ID)
                                {
                                    classcdd.Text = Class.Value.ClassName;
                                    break;
                                }
                            }
                            classcdd.TextAlign = HorizontalAlignment.Center;
                            //vote
                            Guna2Button votebutton = new Guna2Button();
                            votebutton.BorderRadius = 10;
                            votebutton.DisabledState.BorderColor = Color.DarkGray;
                            votebutton.DisabledState.CustomBorderColor = Color.DarkGray;
                            votebutton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
                            votebutton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
                            votebutton.FillColor = Color.FromArgb(37, 83, 140);
                            votebutton.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
                            votebutton.ForeColor = Color.White;
                            votebutton.Location = new Point(16, 209);
                            votebutton.Size = new Size(100, 50);
                            votebutton.TabIndex = 3;
                            votebutton.Text = "Bỏ phiếu";
                            votebutton.Click += vote_Click;

                            void vote_Click(object sender, EventArgs e)
                            {
                                string studentID = "";
                                foreach (var user in users)
                                {
                                    if (user.Value.UserName == Properties.Settings.Default.Username.ToString())
                                    {
                                        studentID = user.Value.Student_ID;
                                        break;
                                    }
                                }
                                var newvote = new VOTE
                                {
                                    Student_ID = studentID,
                                    Campaign_ID = campaign.Campaint_ID.ToString(),
                                    Candidate_ID = candidate.Candidate_ID.ToString(),
                                    TimeVoted = DateTime.Now.ToString()
                                };
                                var openForm = new Verify(newvote);
                                openForm.ShowDialog();
                            }
                            //add
                            panelinfo.Controls.Add(namecdd);
                            panelinfo.Controls.Add(classcdd);
                            panelinfo.Controls.Add(votebutton);
                            panelList_candidate.Add(panelinfo);
                            x_infopanel += 250;
                            break;
                        }
                    }
                }
                foreach (Panel in4panel in panelList_candidate)
                {
                    panel.BringToFront();
                    panel.Controls.Add(in4panel);


                }

                Candidate_Detail.Parent.Controls.Add(panel);
                yOffset += 300;
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            indexForm.Show();
            this.Close();
        }

        private void Candidate_Detail_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
