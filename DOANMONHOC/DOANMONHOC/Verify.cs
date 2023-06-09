﻿using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
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
    public partial class Verify : Form
    {
        private Form indexForm;
        private bool isBackButtonPressed;
        VOTE tempvote;
        string Candidate_name;
        FirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        public Verify(Form parentForm, string Candidate_name, VOTE vt = null)
        {
            InitializeComponent();
            tempvote = vt;
            client = new FireSharp.FirebaseClient(config);
            this.Candidate_name = Candidate_name;
            indexForm = parentForm;
            isBackButtonPressed = false;
        }

   
        private async void verify_button_Click(object sender, EventArgs e)
        {
            PushResponse response = await client.PushTaskAsync("Votes/", tempvote);
            
            Form success = new Success(this, Candidate_name);
            success.ShowDialog();
            isBackButtonPressed = true;
        }

        private void Verify_Load(object sender, EventArgs e)
        {
            label3.Text = Candidate_name.ToString();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            isBackButtonPressed = true;
            this.Close();
        }
    }
}
