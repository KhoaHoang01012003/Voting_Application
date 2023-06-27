using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Guna.UI2.WinForms;
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
    public partial class adminElectionDetail_Result : Form
    {
        FirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        private Index indexForm = new Index();
        public CAMPAIGN Data { get; set; }

        public adminElectionDetail_Result()
        {
            InitializeComponent();
        }

        private async void CreateVote3_Load(object sender, EventArgs e)
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

            int cntUser = 0;
            HashSet<int> classIds = new HashSet<int>(Data.Class_ID);

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
                if (vote.Value.Campaign_ID == Data.Campaint_ID.ToString())
                {
                    cntVote++;
                }
            }

            this.Invoke((MethodInvoker)delegate
            {
                campaignName.Text = Data.CampaignName;
                totalCandidate.Text = Data.Candidate_ID.Length.ToString();
                totalUser.Text = cntUser.ToString();
                totalVote.Text = cntVote.ToString();
            });

            int xOffset = sampleRow.Location.X;
            int yOffset = sampleRow.Location.Y;

            // Tạo danh sách các ứng viên cùng với số phiếu mỗi người nhận được
            List<Tuple<CANDIDATE, CLASS, int>> candidateVotes = new List<Tuple<CANDIDATE, CLASS, int>>();
            foreach (var candidate in candidates.Values)
            {
                int votePerCandidate = votes.Values.Count(vote => vote.Candidate_ID == candidate.Candidate_ID.ToString() && vote.Campaign_ID == Data.Campaint_ID.ToString());
                CLASS candidateClass = classes.Values.FirstOrDefault(ObjClass => ObjClass.Class_ID == candidate.Class_ID);
                candidateVotes.Add(Tuple.Create(candidate, candidateClass, votePerCandidate));
            }

            // Sắp xếp danh sách theo số phiếu giảm dần
            candidateVotes.Sort((a, b) => b.Item3.CompareTo(a.Item3));

            int tmp = 0;
            foreach (var item in candidateVotes)
            {
                for (int i = 0; i < Data.Candidate_ID.Length; i++) 
                {
                    if (Data.Candidate_ID[i] == item.Item1.Candidate_ID)
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
                if (tmp == Data.Candidate_ID.Length)
                {
                    break;
                }
            }
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var openForm = new adminDashboard();
            openForm.Show();
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionActivities();
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

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionDetail_Overview();
            openForm.Data = Data;
            openForm.Show();
            this.Close();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionDetail_Setting();
            openForm.Data = Data;
            openForm.Show();
            this.Close();
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionDetail_Candidate();
            openForm.Data = Data;
            openForm.Show();
            this.Close();
        }
    }
}
