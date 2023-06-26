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
    public partial class Success : Form
    {
        private Index indexForm;
        string Candidate_name;
        public Success(String Candidate_name)
        {
            InitializeComponent();
            this.indexForm = indexForm;
            this.Candidate_name = Candidate_name;
        }


        private void Success_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            Form vote = new vote_view_candidate_details(indexForm);
            vote.Show();
            this.Close();
        }

        private void Success_Load(object sender, EventArgs e)
        {
            label3.Text = Candidate_name.ToString();
        }
    }
}
