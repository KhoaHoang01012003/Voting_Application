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
        private readonly FirebaseConfig _config = new()
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        private IFirebaseClient _client;
        private readonly Form _indexForm;
        private bool _isBackButtonPressed;
        public CAMPAIGN Data { get; set; }

        public adminElectionDetail_Result(Form parentForm)
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

        private async void CreateVote3_Load(object sender, EventArgs e)
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

            var task1 = Task.Run(() => _client.GetTaskAsync("Users/"));
            var task2 = Task.Run(() => _client.GetTaskAsync("Votes/"));
            var task3 = Task.Run(() => _client.GetTaskAsync("Candidates/"));
            var task4 = Task.Run(() => _client.GetTaskAsync("Classes/"));

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

            if (this.IsHandleCreated)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    campaignName.Text = Data.CampaignName;
                    totalCandidate.Text = Data.Candidate_ID.Length.ToString();
                    totalUser.Text = cntUser.ToString();
                    totalVote.Text = cntVote.ToString();
                });
            }

            int xOffset = sampleRow.Location.X;
            int yOffset = sampleRow.Location.Y;

            if (this.IsHandleCreated)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    List<Tuple<CANDIDATE, CLASS, int>> candidateVotes = new List<Tuple<CANDIDATE, CLASS, int>>();
                    foreach (var candidate in candidates.Values)
                    {
                        int votePerCandidate = votes.Values.Count(vote => vote.Candidate_ID == candidate.Candidate_ID.ToString() && vote.Campaign_ID == Data.Campaint_ID.ToString());
                        CLASS candidateClass = classes.Values.FirstOrDefault(ObjClass => ObjClass.Class_ID == candidate.Class_ID);
                        candidateVotes.Add(Tuple.Create(candidate, candidateClass, votePerCandidate));
                    }

                    candidateVotes.Sort((a, b) => b.Item3.CompareTo(a.Item3));

                    int tmp = 0;
                    foreach (var item in candidateVotes)
                    {
                        for (int i = 0; i < Data.Candidate_ID.Length; i++)
                        {
                            if (Data.Candidate_ID[i] == item.Item1.Candidate_ID)
                            {
                                tmp++;
                                CreatePanelRow(xOffset, yOffset, item);
                                yOffset += sampleRow.Height;
                                break;
                            }
                        }
                        if (tmp == Data.Candidate_ID.Length)
                        {
                            break;
                        }
                    }
                }));
            }
        }

        private void CreatePanelRow(int xOffset, int yOffset, Tuple<CANDIDATE, CLASS, int> item)
        {
            Guna2Panel row = new Guna2Panel
            {
                Size = sampleRow.Size,
                CustomizableEdges = sampleRow.CustomizableEdges,
                Location = new Point(xOffset, yOffset),
                ShadowDecoration = { CustomizableEdges = sampleRow.ShadowDecoration.CustomizableEdges },
                BorderColor = sampleRow.BorderColor,
                CustomBorderThickness = sampleRow.CustomBorderThickness,
                CustomBorderColor = sampleRow.CustomBorderColor
            };

            Label nameLabel = CreateLabel(sampleNameLabel, item.Item1.CandidateName);
            Label className = CreateLabel(sampleClassLabel, item.Item2.ClassName);
            Label numVotes = CreateLabel(sampleNumVotesLabel, item.Item3.ToString());

            row.Controls.AddRange(new Control[] { nameLabel, className, numVotes });

            sampleRow.Parent.Controls.Add(row);
            row.BringToFront();
        }

        private Label CreateLabel(Label sampleLabel, string text)
        {
            return new Label
            {
                Location = sampleLabel.Location,
                Size = sampleLabel.Size,
                Text = text
            };
        }

        private void OpenNewForm(Form newForm)
        {
            newForm.Show();
            _isBackButtonPressed = true;
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e) => OpenNewForm(new adminDashboard(_indexForm));
        private void guna2Button2_Click(object sender, EventArgs e) => OpenNewForm(new adminElectionActivities(_indexForm));
        private void guna2Button3_Click(object sender, EventArgs e) => OpenNewForm(new list_candidate(_indexForm));
        private void guna2Button5_Click(object sender, EventArgs e) => OpenNewForm(_indexForm);
        private void guna2Button4_Click(object sender, EventArgs e) => OpenNewForm(new adminElectionDetail_Overview(_indexForm) { Data = Data });
        private void guna2Button7_Click(object sender, EventArgs e) => OpenNewForm(new adminElectionDetail_Setting(_indexForm) { Data = Data });
        private void guna2Button9_Click(object sender, EventArgs e) => OpenNewForm(new adminElectionDetail_Candidate(_indexForm) { Data = Data });
    }

}
