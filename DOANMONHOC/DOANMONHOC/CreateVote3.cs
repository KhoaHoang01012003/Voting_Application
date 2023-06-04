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
    public partial class CreateVote3 : Form
    {
        public CreateVote3()
        {
            InitializeComponent();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            var open = new CreateVote2();
            open.Show();
            this.Hide();
        }

        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CreateVote3_Load(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Ứng cử viên", typeof(string));
            dataTable.Columns.Add("Lớp", typeof(string));
            dataTable.Columns.Add("Số phiếu bầu", typeof(int));

            dataTable.Rows.Add("Đào Võ Hữu Hiệp", "ATTT2021", 30);
            dataTable.Rows.Add("Đào Võ Hữu Hiệp", "ATTT2021", 20);
            dataTable.Rows.Add("Đào Võ Hữu Hiệp", "ATTT2021", 50);
            dataTable.Rows.Add("Đào Võ Hữu Hiệp", "ATTT2021", 60);
            dataTable.Rows.Add("Đào Võ Hữu Hiệp", "ATTT2021", 70);
            dataTable.Rows.Add("Đào Võ Hữu Hiệp", "ATTT2021", 90);
            dataTable.Rows.Add("Đào Võ Hữu Hiệp", "ATTT2021", 100);
            dataTable.Rows.Add("Đào Võ Hữu Hiệp", "ATTT2021", 1200);
            guna2DataGridView1.DataSource = dataTable;
        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {
            var open = new CreateVote2();
            open.Show();
            this.Hide();
        }
    }
}
