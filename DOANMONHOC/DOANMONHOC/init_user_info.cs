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
        private Index indexForm;
        USER data = new USER();
        public init_user_info(Index indexForm, USER user)
        {
            InitializeComponent();
            this.indexForm = indexForm;
            this.data = user;
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

    }
}
