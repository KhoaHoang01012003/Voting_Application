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
    public partial class VerifyEmail : Form
    {
        public VerifyEmail()
        {
            InitializeComponent();
        }

        private void CenterTextInRichTextBox(RichTextBox richTextBox)
        {
            // Chọn toàn bộ văn bản trong RichTextBox
            richTextBox.SelectAll();

            // Tính toán khoảng cách
            int margin = (72 - richTextBox.GetPositionFromCharIndex(richTextBox.SelectionLength).X) / 2 - 10;

            // Đặt lề trái và lề phải cho văn bản trong RichTextBox
            richTextBox.SelectionIndent = margin;
            richTextBox.SelectionRightIndent = margin;

            // Đặt lề trên và lề dưới cho văn bản trong RichTextBox
            richTextBox.SelectionHangingIndent = margin;
            richTextBox.SelectionCharOffset = margin;

        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            CenterTextInRichTextBox(richTextBox1);
            // Chỉ cho phép nhập số và phím điều khiển
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // Chỉ cho phép nhập duy nhất 1 kí tự số
            if (char.IsDigit(e.KeyChar) && richTextBox1.TextLength >= 1)
            {
                e.Handled = true;
            }

            // Chuyển focus sang richTextBox2 nếu đã nhập kí tự số đầu tiên
            if (char.IsDigit(e.KeyChar) && richTextBox1.TextLength == 0)
            {
                e.Handled = false;
                richTextBox1.SelectionStart = 1;
                richTextBox2.Focus();
            }
        }

        private void richTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            CenterTextInRichTextBox(richTextBox2);
            // Chỉ cho phép nhập số và phím điều khiển

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // Chỉ cho phép nhập duy nhất 1 kí tự số
            if (char.IsDigit(e.KeyChar) && richTextBox2.TextLength >= 1)
            {
                e.Handled = true;
            }

            // Chuyển focus sang richTextBox2 nếu đã nhập kí tự số đầu tiên
            if (char.IsDigit(e.KeyChar) && richTextBox2.TextLength == 0)
            {
                e.Handled = false;
                richTextBox2.SelectionStart = 1;
                richTextBox3.Focus();
            }
        }

        private void richTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            CenterTextInRichTextBox(richTextBox3);
            // Chỉ cho phép nhập số và phím điều khiển

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // Chỉ cho phép nhập duy nhất 1 kí tự số
            if (char.IsDigit(e.KeyChar) && richTextBox3.TextLength >= 1)
            {
                e.Handled = true;
            }

            // Chuyển focus sang richTextBox2 nếu đã nhập kí tự số đầu tiên
            if (char.IsDigit(e.KeyChar) && richTextBox3.TextLength == 0)
            {
                e.Handled = false;
                richTextBox3.SelectionStart = 1;
                richTextBox4.Focus();
            }
        }

        private void richTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            CenterTextInRichTextBox(richTextBox4);
            // Chỉ cho phép nhập số và phím điều khiển
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // Chỉ cho phép nhập duy nhất 1 kí tự số
            if (char.IsDigit(e.KeyChar) && richTextBox4.TextLength >= 1)
            {
                e.Handled = true;
            }

            // Chuyển focus sang richTextBox2 nếu đã nhập kí tự số đầu tiên
            if (char.IsDigit(e.KeyChar) && richTextBox4.TextLength == 0)
            {
                e.Handled = false;
                richTextBox4.SelectionStart = 1;
                richTextBox4.Focus();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
