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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace DOANMONHOC
{
    public partial class list_candidate : Form
    {
        private readonly IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        private readonly Form indexForm;
        private bool isBackButtonPressed;
        private IFirebaseClient candidate;
        private Dictionary<string, CANDIDATE> candidates;
        private Dictionary<string, CLASS> classes;
        private int currentPage;
        private int ypos;

        public list_candidate(Form parentForm)
        {
            InitializeComponent();
            this.FormClosed += FormClosed_Exit;
            indexForm = parentForm;
            isBackButtonPressed = false;
            currentPage = 0;
            ypos = samplePanel.Location.Y;
        }

        private async Task FetchCandidates()
        {
            FirebaseResponse response = await candidate.GetTaskAsync("Candidates/");
            candidates = response.ResultAs<Dictionary<string, CANDIDATE>>();
        }

        private async Task FetchClasses()
        {
            FirebaseResponse response = await candidate.GetTaskAsync("Classes/");
            classes = response.ResultAs<Dictionary<string, CLASS>>();
        }

        private async void list_candidate_Load(object sender, EventArgs e)
        {
            candidate = new FireSharp.FirebaseClient(config);
            await FetchCandidates();
            await FetchClasses();

            int totalCandidates = candidates.Count;
            int candidatesPerPage = 6;
            int currentPage = 1;
            int startIndex = (currentPage - 1) * candidatesPerPage;

            int xOffset = samplePanel.Location.X;
            int yOffset = samplePanel.Location.Y;
            int itemWidth = 315;
            int itemHeight = 334;
            int spacing = 52;

            for (int i = startIndex; i < totalCandidates; i++)
            {
                var candidateItem = candidates.ElementAt(i).Value;
                Guna2Panel panel = CreatePanel();
                panel.Location = new Point(xOffset, yOffset);
                panel.Controls.Add(CreateNameLabel(candidateItem.CandidateName));
                panel.Controls.Add(CreateClassLabel(classes, candidateItem.Class_ID.ToString()));
                panel.Controls.Add(CreateViewButton(candidateItem));
                panel.Controls.Add(CreateAvatar(candidateItem.AvtCandidate));

                if (this.IsHandleCreated)
                {
                    this.Invoke(new MethodInvoker(delegate {
                        samplePanel.Parent.Controls.Add(panel);
                        panel.BringToFront();
                    }));
                }

                UpdateNextInfoPosition(ref xOffset, ref yOffset, i, itemWidth, itemHeight, spacing);
            }
        }

        private Guna2Panel CreatePanel()
        {
            Guna2Panel panel = new Guna2Panel();
            panel.Size = new Size(315, 334);
            panel.BackColor = System.Drawing.Color.White;
            panel.BorderRadius = 2;
            panel.CustomizableEdges = samplePanel.CustomizableEdges;
            panel.ShadowDecoration.CustomizableEdges = samplePanel.ShadowDecoration.CustomizableEdges;
            return panel;
        }

        private Label CreateNameLabel(string name)
        {
            Label nameLabel = new Label();
            nameLabel.Text = name;
            nameLabel.AutoSize = true;
            nameLabel.Location = candidateName.Location;
            nameLabel.Anchor = candidateName.Anchor;
            return nameLabel;
        }

        private Label CreateClassLabel(Dictionary<string, CLASS> classes, string classId)
        {
            Label classLabel = new Label();
            classLabel.AutoSize = true;
            classLabel.Location = className.Location;
            classLabel.Anchor = className.Anchor;
            if (classes.ContainsKey(classId))
            {
                classLabel.Text = classes[classId].ClassName;
            }
            return classLabel;
        }

        private Guna2Button CreateViewButton(CANDIDATE candidate)
        {
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
            view.ShadowDecoration.CustomizableEdges = viewButton.ShadowDecoration.CustomizableEdges;
            view.Size = viewButton.Size;
            view.Click += (sender, e) =>
            {
                var openForm = new adminAddCandidate(indexForm, candidate);
                openForm.Show();
                isBackButtonPressed = true;
                Close();
            };
            return view;
        }

        private Guna2CirclePictureBox CreateAvatar(string avatarBase64)
        {
            Guna2CirclePictureBox avatar = new Guna2CirclePictureBox();
            byte[] originalBytes = Convert.FromBase64String(avatarBase64);
            Image image;
            using (MemoryStream ms = new MemoryStream(originalBytes))
            {
                image = Image.FromStream(ms);
            }
            Image thumbnailImage = image.GetThumbnailImage(80, 80, null, IntPtr.Zero);
            avatar.Image = thumbnailImage;
            avatar.Size = sampleAvatar.Size;
            avatar.ImageRotate = sampleAvatar.ImageRotate;
            avatar.Location = sampleAvatar.Location;
            avatar.ShadowDecoration.CustomizableEdges = sampleAvatar.ShadowDecoration.CustomizableEdges;
            avatar.ShadowDecoration.Mode = sampleAvatar.ShadowDecoration.Mode;
            avatar.TabIndex = sampleAvatar.TabIndex;
            avatar.TabStop = sampleAvatar.TabStop;
            return avatar;
        }

        private void UpdateNextInfoPosition(ref int xOffset, ref int yOffset, int currentIndex, int itemWidth, int itemHeight, int spacing)
        {
            xOffset += itemWidth + spacing;
            if ((currentIndex + 1) % 3 == 0)
            {
                xOffset = samplePanel.Location.X;
                yOffset += itemHeight + 79;
            }
        }

        private void OpenNewForm(Form form)
        {
            form.Show();
            isBackButtonPressed = true;
            Close();
        }

        private void guna2Button4_Click(object sender, EventArgs e) => OpenNewForm(new adminAddCandidate(indexForm));
        private void guna2Button2_Click(object sender, EventArgs e) => OpenNewForm(new adminElectionActivities(indexForm));
        private void guna2Button3_Click(object sender, EventArgs e) => list_candidate_Load(sender, e);
        private void guna2Button1_Click(object sender, EventArgs e) => OpenNewForm(new adminDashboard(indexForm));
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            indexForm.Show();
            isBackButtonPressed = true;
            Close();
        }

        private void FormClosed_Exit(object sender, FormClosedEventArgs e)
        {
            if (!isBackButtonPressed)
            {
                Application.ExitThread();
            }
        }
    }
}
