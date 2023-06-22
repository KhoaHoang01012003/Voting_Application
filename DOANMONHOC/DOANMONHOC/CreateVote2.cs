﻿using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.VisualBasic.ApplicationServices;
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
    public partial class CreateVote2 : Form
    {
        FirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        private Index indexForm;

        public CreateVote2(Index indexForm)
        {
            InitializeComponent();
            this.indexForm = indexForm;
        }
        private async Task<int> id_index()
        {
            for (int i = 1; ; i++)
            {
                FirebaseResponse response = await client.GetTaskAsync("Admins/" + i.ToString());
                if (response.Body == "null") return i;
            }
        }

        private async void CreateVote2_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            Task<int> task = id_index();
            int limit = await task;
            for (int i = 1; i < limit; i++)
            {
                FirebaseResponse res = await client.GetTaskAsync("Admins/" + i.ToString());
                ADMIN admin = res.ResultAs<ADMIN>();
                if (admin.UserName == Properties.Settings.Default.Username.ToString())
                {
                    label10.Text = admin.AdminName;
                    break;
                }
            }

            FirebaseResponse response = await client.GetTaskAsync("Campaigns/");

            Dictionary<string, CAMPAIGN> campaigns = response.ResultAs<Dictionary<string, CAMPAIGN>>();
            if (campaigns.Last().Value.Status == 0)
            {
                campaignName.Text = campaigns.Last().Value.CampaignName;
                startTime.Text = campaigns.Last().Value.StartTime.ToString();
                endTime.Text = campaigns.Last().Value.EndTime.ToString();
                //CHƯA CÓ FORM CHỌN CANDIDATE
                cntCandidates.Text = "0";
                category.Text = campaigns.Last().Value.Category;
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            var open = new CreateVote3();
            open.Show();
            this.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.GetTaskAsync("Campaigns/");

            Dictionary<string, CAMPAIGN> campaigns = response.ResultAs<Dictionary<string, CAMPAIGN>>();
            if (campaigns.Last().Value.Status != 0)
            {
                var openForm = new Admin_Dashboard(indexForm);
                openForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Chưa hoàn tất thiết lập cho cuộc bỏ phiếu");
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            indexForm.Show();
            this.Close();
        }

        private void endTime_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }
    }
}
