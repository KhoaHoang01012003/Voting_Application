using FireSharp.Config;
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
    public partial class Admin_Dashboard : Form
    {
        FirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        private Index indexForm;

        public Admin_Dashboard(Index indexForm)
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

        private async void Admin_Dashboard_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            Task<int> task = id_index();
            int limit = await task;
            for (int i = 1; i < limit; i++)
            {
                FirebaseResponse response = await client.GetTaskAsync("Admins/" + i.ToString());
                ADMIN admin = response.ResultAs<ADMIN>();
                if (admin.UserName == Properties.Settings.Default.Username.ToString())
                {
                    label_user_name.Text = admin.AdminName;
                    label7.Text = admin.AdminName;
                    break;
                }
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            indexForm.Show();
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionActivities(indexForm);
            openForm.Show();
            this.Close();
        }
    }
}
