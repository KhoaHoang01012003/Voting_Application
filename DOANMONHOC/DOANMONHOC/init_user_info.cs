using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Guna.UI2.WinForms;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DOANMONHOC
{
    public partial class init_user_info : Form
    {
        USER data = new USER();
        string avt = "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";
        private readonly Form indexForm;
        private bool isBackButtonPressed;
        public init_user_info(Form parentForm, USER user)
        {
            InitializeComponent();
            this.data = user;
            this.indexForm = parentForm;
        }
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        public class ClassAndFaculty
        {
            public int ClassId { get; set; }
            public int FacultyId { get; set; }
        }
        private async Task<ClassAndFaculty> SearchClassID(string className)
        {
            FirebaseResponse response1 = await client.GetTaskAsync("Classes/");
            Dictionary<string, CLASS> classes = response1.ResultAs<Dictionary<string, CLASS>>();
            int index = classes.Count() + 1;
            foreach (var class_d in classes)
            {
                if (class_d.Value.ClassName == className)
                {
                    ClassAndFaculty result = new ClassAndFaculty();
                    result.ClassId = class_d.Value.Class_ID;
                    result.FacultyId = class_d.Value.Faculty_ID;
                    return result;
                }
            }
            return null;
        }

        private async void init_user_info_Load(object sender, EventArgs e)
        {
            guna2Button6_Click(sender, e);
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse facultyResponse = await client.GetTaskAsync("Faculties").ConfigureAwait(false);
            Dictionary<string, FACULTY> faculties = facultyResponse.ResultAs<Dictionary<string, FACULTY>>();
            foreach (FACULTY faculty in faculties.Values)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    facultyList.Items.Add(faculty.FacultyName);
                });
            }

            facultyList.SelectedIndexChanged += async (sender, e) =>
            {
                int selectedFacultyIndex = facultyList.SelectedIndex;
                if (selectedFacultyIndex != -1)
                {
                    string selectedFacultyName = facultyList.SelectedItem.ToString();

                    FACULTY selectedFaculty = faculties.Values.ElementAt(selectedFacultyIndex);

                    int selectedFacultyID = selectedFaculty.Faculty_ID;

                    this.Invoke((MethodInvoker)delegate
                    {
                        classList.Items.Clear();
                    });
                    FirebaseResponse classResponse = await client.GetTaskAsync("Classes").ConfigureAwait(false);
                    Dictionary<string, CLASS> classes = classResponse.ResultAs<Dictionary<string, CLASS>>();
                    foreach (CLASS classObj in classes.Values)
                    {
                        if (selectedFacultyID == classObj.Faculty_ID)
                            this.Invoke((MethodInvoker)delegate
                            {
                                classList.Items.Add(classObj.ClassName);
                            });
                    }
                }
            };
        }

        private async void guna2Button7_Click(object sender, EventArgs e)
        {
            if (nameBox.Text != "" && facultyList.Text != "" && classList.Text != "")
            {
                ClassAndFaculty tmp_class = await SearchClassID(classList.Text.ToUpper());
                data.AvtUser = avt;
               
                data.Fullname = nameBox.Text;
                data.Faculty_ID = tmp_class.FacultyId;
                data.Class_ID = tmp_class.ClassId;

                PushResponse response = await client.PushTaskAsync("Users/", data);
                USER result = response.ResultAs<USER>();

                MessageBox.Show("DK THANH CONG " + result.Email);
                indexForm.Show();
                this.Close();
            }
            else if (nameBox.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên");
                nameBox.Focus();
            }
            else if (facultyList.Text == "")
            {
                MessageBox.Show("Vui lòng chọn khoa");
                facultyList.Focus();
            }
            else if (classList.Text == "")
            {
                MessageBox.Show("Vui lòng chọn lớp");
                classList.Focus();
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Xử lý file ảnh được chọn ở đây
                string filePath = openFileDialog.FileName;
                byte[] imageBytes = File.ReadAllBytes(filePath);

                // Mã hóa chuỗi byte thành định dạng Base64
                string base64String = Convert.ToBase64String(imageBytes);
                this.avt = base64String;

                // Tạo một đối tượng Image từ chuỗi byte gốc
                Image image;
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    image = Image.FromStream(ms);
                }
                // Tạo bản thu nhỏ của ảnh
                Image thumbnailImage = image.GetThumbnailImage(80, 80, null, IntPtr.Zero);

                // Hiển thị ảnh trong một PictureBox
                Picture.Image = thumbnailImage;
            }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            avt = "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";
            string base64String = "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";
            byte[] originalBytes = Convert.FromBase64String(base64String);

            // Tạo một đối tượng Image từ chuỗi byte gốc
            Image image;
            using (MemoryStream ms = new MemoryStream(originalBytes))
            {
                image = Image.FromStream(ms);
            }
            // Tạo bản thu nhỏ của ảnh
            Image thumbnailImage = image.GetThumbnailImage(80, 80, null, IntPtr.Zero);

            // Hiển thị ảnh trong một PictureBox
            Picture.Image = thumbnailImage;
        }
    }
}
