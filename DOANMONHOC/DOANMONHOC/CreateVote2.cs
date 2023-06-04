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
        public CreateVote2()
        {
            InitializeComponent();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

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
    }
}
