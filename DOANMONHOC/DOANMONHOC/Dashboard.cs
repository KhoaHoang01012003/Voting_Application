using FireSharp.Config;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DOANMONHOC
{
    public partial class Dashboard : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        private Index indexForm;

        public Dashboard(Index indexForm)
        {
            InitializeComponent();
            this.indexForm = indexForm;
        }

        private async void Dashboard_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            FirebaseResponse response = await client.GetTaskAsync("Users/");

            Dictionary<string, USER> users = response.ResultAs<Dictionary<string, USER>>();
            foreach (var user in users)
            {
                if (user.Value.UserName == Properties.Settings.Default.Username.ToString())
                {
                    label_user_name.Text = user.Value.Fullname;
                    label7.Text = user.Value.Fullname;
                    break;
                }
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            indexForm.Show();
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
