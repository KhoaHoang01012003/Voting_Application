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
    public partial class list_candidate : Form
    {
        private Index indexForm;
        public list_candidate()
        {
            InitializeComponent();
        }
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient candidate;

        private async void list_candidate_Load(object sender, EventArgs e)
        {
            candidate = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await candidate.GetTaskAsync("Candidates/");
            Dictionary<string, CANDIDATE> candidates = response.ResultAs<Dictionary<string, CANDIDATE>>();

            response = await candidate.GetTaskAsync("Classes/");
            Dictionary<string, CLASS> classes = response.ResultAs<Dictionary<string, CLASS>>();

            int totalCandidates = candidates.Count;
            int candidatesPerPage = 6;
            int totalPages = (int)Math.Ceiling((double)totalCandidates / candidatesPerPage);

            int currentPage = 1;
            int startIndex = (currentPage - 1) * candidatesPerPage;

            int xOffset = 312;
            int yOffset = 213;
            int itemWidth = 315;
            int itemHeight = 334;
            int spacing = 52;

            for (int i = startIndex; i < totalCandidates; i++)
            {
                CANDIDATE candidate = candidates.ElementAt(i).Value;

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
                view.Text = "Chỉnh sửa";
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
                    var openForm = new add_candidate(candidate);
                    openForm.ShowDialog();
                    this.Close();
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
                    xOffset = 312;
                    yOffset += itemHeight + 79;
                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            var openForm = new add_candidate();
            openForm.ShowDialog();
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
            list_candidate_Load(sender, e);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var openForm = new adminDashboard();
            openForm.Show();
            this.Close();
        }
    }
}
