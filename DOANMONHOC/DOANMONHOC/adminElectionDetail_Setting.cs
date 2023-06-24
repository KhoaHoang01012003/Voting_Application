﻿using FireSharp.Config;
using FireSharp.Interfaces;
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
    public partial class adminElectionDetail_Setting : Form
    {
        FirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        private Index indexForm;
        public CAMPAIGN Data { get; set; }

        public adminElectionDetail_Setting(Index indexForm)
        {
            InitializeComponent();
            this.indexForm = indexForm;
        }

        private void adminElectionDetail_Setting_Load(object sender, EventArgs e)
        {
            title.Text = Data.CampaignName;
            description.Text = Data.Description;
            startTime.Value = Data.StartTime;
            endTime.Value = Data.EndTime;
            category.Text = Data.Category;
            facultyList.SelectedValue = Data.
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var openForm = new adminDashboard(indexForm);
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

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionDetail_Overview(indexForm);
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

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            indexForm.Show();
            this.Close();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {

        }

        
    }
}
