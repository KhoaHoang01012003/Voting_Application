﻿using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.VisualBasic.ApplicationServices;
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
    public partial class adminElectionDetail_Overview : Form
    {
        FirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        private Form indexForm;
        private bool isBackButtonPressed;
        public CAMPAIGN Data { get; set; }

        public adminElectionDetail_Overview(Form parentForm)
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(FormClosed_Exit);
            indexForm = parentForm;
            isBackButtonPressed = false;
        }

        void FormClosed_Exit(object sender, FormClosedEventArgs e)
        {
            if (!isBackButtonPressed)
            {
                Application.ExitThread();
            }
        }

        private async void CreateVote2_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            campaignName.Text = Data.CampaignName;
            startTime.Text = Data.StartTime.ToString();
            endTime.Text = Data.EndTime.ToString();
            cntCandidates.Text = Data.Candidate_ID.Count().ToString();
            category.Text = Data.Category;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            var openForm = new adminDashboard(indexForm);
            openForm.Show();
            isBackButtonPressed = true;
            this.Close();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            indexForm.Show();
            isBackButtonPressed = true;
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionActivities(indexForm);
            openForm.Show();
            isBackButtonPressed = true;
            this.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            var openForm = new list_candidate(indexForm);
            openForm.Show();
            isBackButtonPressed = true;
            this.Close();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionDetail_Setting(indexForm);
            openForm.Data = Data;
            openForm.Show();
            isBackButtonPressed = true;
            this.Close();
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            var open = new adminElectionDetail_Result(indexForm);
            open.Data = Data;
            open.Show();
            isBackButtonPressed = true;
            this.Close();
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            var open = new adminElectionDetail_Candidate(indexForm);
            open.Data = Data;
            open.Show();
            isBackButtonPressed = true;
            this.Close();
        }
    }
}
