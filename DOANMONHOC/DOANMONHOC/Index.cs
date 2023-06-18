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
    public partial class Index : Form
    {
        public Index()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var RegisterForm = new Register(this);
            RegisterForm.Show(this);
            this.Hide();

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var LoginForm = new Sign_in(this);
            LoginForm.Show();
            this.Hide();
        }

        private void Index_Load(object sender, EventArgs e)
        {

        }
    }
}
