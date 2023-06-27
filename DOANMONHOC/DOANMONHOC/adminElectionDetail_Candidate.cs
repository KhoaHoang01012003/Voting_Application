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

        private async void adminElectionDetail_Candidate_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            this.Invoke((MethodInvoker)delegate
            {
                campaignName.Text = Data.CampaignName;
            });

            var task1 = client.GetTaskAsync("Candidates/");
            var task2 = client.GetTaskAsync("Classes/");

            await Task.WhenAll(task1, task2);

            FirebaseResponse response1 = task1.Result;
            FirebaseResponse response2 = task2.Result;

            Dictionary<string, CANDIDATE> candidates = response1.ResultAs<Dictionary<string, CANDIDATE>>();
            Dictionary<string, CLASS> classes = response2.ResultAs<Dictionary<string, CLASS>>();

            int totalCandidates = candidates.Count;

            int startIndex = 0;
            int xOffset = samplePanel.Location.X;
            int yOffset = samplePanel.Location.Y;
            int itemWidth = 315;
            int itemHeight = 334;
            int spacing = 52;

            int tmp = 0;
            for (int i = startIndex; i < totalCandidates; i++)
            {
                CANDIDATE candidate = candidates.ElementAt(i).Value;
                for (int j = 0; j < Data.Candidate_ID.Length; j++)
                {
                    if (candidate.Candidate_ID == Data.Candidate_ID[j])
                    {
                        tmp++;

                        // Tạo Panel để chứa các thông tin của candidate
                        Guna2Panel panel = new Guna2Panel();
                        panel.Size = new Size(itemWidth, itemHeight);
                        panel.Location = new Point(xOffset, yOffset);
                        panel.BackColor = System.Drawing.Color.White;
                        panel.BorderRadius = 2;
                        panel.CustomizableEdges = samplePanel.CustomizableEdges;
                        panel.ShadowDecoration.CustomizableEdges = samplePanel.ShadowDecoration.CustomizableEdges;


                        // Tạo Label để hiển thị thông tin tên candidate
                        Label nameLabel = new Label();
                        nameLabel.Text = candidate.CandidateName;
                        nameLabel.AutoSize = true;
                        nameLabel.Location = candidateName.Location;
                        nameLabel.Anchor = candidateName.Anchor;
                        nameLabel.AutoSize = candidateName.AutoSize;
                        nameLabel.Location = candidateName.Location;

                        Label classLabel = new Label();
                        classLabel.Text = className.Text;
                        classLabel.AutoSize = true;
                        classLabel.Location = className.Location;
                        classLabel.Anchor = className.Anchor;
                        classLabel.AutoSize = className.AutoSize;
                        classLabel.Location = className.Location;

                        foreach (var Class in classes)
                        {
                            if (Class.Value.Class_ID == candidate.Class_ID)
                            {
                                classLabel.Text = Class.Value.ClassName;
                                break;
                            }
                        }

                        // Tạo Button "View"
                        Guna2Button view = new Guna2Button();
                        view.Text = "Xem chi tiết";
                        view.Location = viewButton.Location;
                        view.Font = viewButton.Font;
                        view.BorderRadius = viewButton.BorderRadius;
                        view.BorderThickness = viewButton.BorderThickness;
                        view.CustomizableEdges = viewButton.CustomizableEdges;
                        view.DisabledState.BorderColor = viewButton.DisabledState.BorderColor;
                        view.DisabledState.CustomBorderColor = viewButton.DisabledState.CustomBorderColor;
                        view.DisabledState.FillColor = viewButton.DisabledState.FillColor;
                        view.DisabledState.ForeColor = viewButton.DisabledState.ForeColor;
                        view.FillColor = viewButton.FillColor;
                        view.ForeColor = viewButton.ForeColor;
                        view.ShadowDecoration.CustomizableEdges = view.ShadowDecoration.CustomizableEdges;
                        view.Size = viewButton.Size;
                        view.Click += view_Click;

                        void view_Click(object sender, EventArgs e)
                        {
                            var openForm = new Info_Candidate(candidate);
                            openForm.ShowDialog();
                        }

                        Guna2CirclePictureBox avatar = new Guna2CirclePictureBox();
                        avatar.Image = sampleAvatar.Image;
                        avatar.Size = sampleAvatar.Size;
                        avatar.ImageRotate = sampleAvatar.ImageRotate;
                        avatar.Location = sampleAvatar.Location;
                        avatar.ShadowDecoration.CustomizableEdges = sampleAvatar.ShadowDecoration.CustomizableEdges;
                        avatar.ShadowDecoration.Mode = sampleAvatar.ShadowDecoration.Mode;
                        avatar.TabIndex = sampleAvatar.TabIndex;
                        avatar.TabStop = sampleAvatar.TabStop;

                        // Thêm các thành phần vào Panel
                        panel.Controls.Add(nameLabel);
                        panel.Controls.Add(classLabel);
                        panel.Controls.Add(view);
                        panel.Controls.Add(avatar);

                        // Thêm Panel vào form
                        samplePanel.Parent.Controls.Add(panel);
                        panel.BringToFront();

                        // Cập nhật vị trí của ô thông tin tiếp theo
                        xOffset += itemWidth + spacing;

                        // Nếu đã hiển thị đủ số lượng ô thông tin trên một hàng, xuống hàng tiếp theo
                        if ((i + 1) % 3 == 0)
                        {
                            yOffset += itemHeight + 79;
                        }

                        break;
                    }
                    if (tmp == Data.Candidate_ID.Length)
                    {
                        break;
                    }
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
