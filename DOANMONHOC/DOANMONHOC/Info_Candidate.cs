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
using static DOANMONHOC.add_candidate;

namespace DOANMONHOC
{
    public partial class Info_Candidate : Form
    {
        CANDIDATE CANDIDATE;
        public Info_Candidate(CANDIDATE u)
        {
            InitializeComponent();
            CANDIDATE = u;
        }
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient candidate;
        private async Task<string> SearchClass(int ID)
        {
            candidate = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await candidate.GetTaskAsync("Classes/");
            Dictionary<string, CLASS> classes = response.ResultAs<Dictionary<string, CLASS>>();
            int index = classes.Count() + 1;
            foreach (var user in classes)
            {
                if (user.Value.Class_ID == ID)
                {
                    return user.Value.ClassName;
                }
            }
            return null;
        }
        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private async void Info_Candidate_Load(object sender, EventArgs e)
        {
            Label_name.Text = CANDIDATE.CandidateName;
            Label_Name_Cadidate.Text = CANDIDATE.CandidateName;
            Label_Age.Text = CANDIDATE.Birthday.ToString();
            Label_Slogan.Text = "\" " + CANDIDATE.Promise + " \"";
            textBox1.Text = CANDIDATE.Description;
            string temp = await SearchClass(CANDIDATE.Class_ID);
            Label_Class.Text = temp;

            byte[] originalBytes = Convert.FromBase64String(CANDIDATE.AvtCandidate);

            // Tạo một đối tượng Image từ chuỗi byte gốc
            Image image;
            using (MemoryStream ms = new MemoryStream(originalBytes))
            {
                image = Image.FromStream(ms);
            }
            // Tạo bản thu nhỏ của ảnh
            Image thumbnailImage = image.GetThumbnailImage(250, 250, null, IntPtr.Zero);

            // Hiển thị ảnh trong một PictureBox
            Picture.Image = thumbnailImage;

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
