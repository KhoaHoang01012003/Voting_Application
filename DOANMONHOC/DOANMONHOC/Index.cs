﻿using System;
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

        private void button2_Click(object sender, EventArgs e)
        {
            var LoginForm = new Sign_in();
            LoginForm.Show();
            //this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var RegisterForm = new Register();
            RegisterForm.Show();
            //this.Hide();
        }
    }
}
