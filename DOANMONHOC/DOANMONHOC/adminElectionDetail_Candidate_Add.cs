﻿using FireSharp.Config;
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
    public partial class adminElectionDetail_Candidate_Add : Form
    {

        FirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        public CAMPAIGN Data { get; set; }
        public Dictionary<string, CANDIDATE> candidates { get; set; }
        public Dictionary<string, CLASS> classes { get; set; }

        public adminElectionDetail_Candidate_Add()
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);
        }

        private void adminElectionDetail_Candidate_Add_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            int totalCandidates = candidates.Count;

            int startIndex = 0;

            int xOffset = 25;
            int yOffset = 148;
            int itemWidth = 315;
            int itemHeight = 334;
            int spacing = 52;

            int tmp = 0;
            for (int i = startIndex; i < totalCandidates; i++)
            {
                CANDIDATE candidate = candidates.ElementAt(i).Value;
                if (!Data.Candidate_ID.Contains(candidate.Candidate_ID) && Data.Class_ID.Contains(candidate.Class_ID))
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

                    // Tạo Button "Add"
                    Guna2Button add = new Guna2Button();
                    add.BorderRadius = addButton.BorderRadius;
                    add.Text = "Thêm";
                    add.Location = addButton.Location;
                    add.CustomizableEdges = addButton.CustomizableEdges;
                    add.DisabledState.BorderColor = addButton.DisabledState.BorderColor;
                    add.DisabledState.CustomBorderColor = addButton.DisabledState.CustomBorderColor;
                    add.DisabledState.FillColor = addButton.DisabledState.FillColor;
                    add.DisabledState.ForeColor = addButton.DisabledState.ForeColor;
                    add.FillColor = addButton.FillColor;
                    add.ForeColor = addButton.ForeColor;
                    add.Location = addButton.Location;
                    add.Font = addButton.Font;
                    add.ShadowDecoration.CustomizableEdges = addButton.ShadowDecoration.CustomizableEdges;
                    add.Size = addButton.Size;
                    add.Click += add_Click;

                    void add_Click(object sender, EventArgs e)
                    {
                        int[] newCandidate_ID = new int[Data.Candidate_ID.Length + 1];
                        Array.Copy(Data.Candidate_ID, newCandidate_ID, Data.Candidate_ID.Length);
                        newCandidate_ID[newCandidate_ID.Length - 1] = candidate.Candidate_ID;
                        Data.Candidate_ID = newCandidate_ID;
                        Guna2Button clickedButton = (Guna2Button)sender;
                        clickedButton.Enabled = false;
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

                    // Hiển thị ảnh trong một PictureBox
                    Guna2CirclePictureBox avatar = new Guna2CirclePictureBox();
                    byte[] originalBytes = Convert.FromBase64String(candidate.AvtCandidate);

                    // Tạo một đối tượng Image từ chuỗi byte gốc
                    Image image;
                    using (MemoryStream ms = new MemoryStream(originalBytes))
                    {
                        image = Image.FromStream(ms);
                    }
                    // Tạo bản thu nhỏ của ảnh
                    Image thumbnailImage = image.GetThumbnailImage(80, 80, null, IntPtr.Zero);

                    avatar.Image = thumbnailImage;
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
                    panel.Controls.Add(add);
                    panel.Controls.Add(view);
                    panel.Controls.Add(avatar);

                    // Thêm Panel vào form
                    samplePanel.Parent.Controls.Add(panel);
                    panel.BringToFront();

                    // Cập nhật vị trí của ô thông tin tiếp theo
                    xOffset += itemWidth + spacing;

                    // Nếu đã hiển thị đủ số lượng ô thông tin trên một hàng, xuống hàng tiếp theo
                    if (tmp % 3 == 0)
                    {
                        xOffset = 25;
                        yOffset += itemHeight + 79;
                    }
                }
            }
        }

        private async void guna2Button4_Click(object sender, EventArgs e)
        {
            PushResponse response = await client.PushTaskAsync("Campaigns/", Data);
            MessageBox.Show("Lưu thành công!");
            this.Close();
        }
    }
}
