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
        public init_user_info()
        {
            InitializeComponent();
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
            foreach (var user in classes)
            {
                if (user.Value.ClassName == className)
                {
                    ClassAndFaculty result = new ClassAndFaculty();
                    result.ClassId = user.Value.Class_ID;
                    result.FacultyId = user.Value.Faculty_ID;
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
            Properties.Settings.Default.Username = "21521955@gm.uit.edu.vn";
            Properties.Settings.Default.Save();
        }

        private async void guna2Button7_Click(object sender, EventArgs e)
        {
            if (nameBox.Text != "" && facultyList.Text != "" && classList.Text != "")
            {
                FirebaseResponse response = await client.GetTaskAsync("Users/");
                Dictionary<string, USER> users = response.ResultAs<Dictionary<string, USER>>();
                ClassAndFaculty tmp_class = await SearchClassID(classList.Text.ToUpper());
                foreach (var user in users)
                {
                    if (user.Value.UserName == Properties.Settings.Default.Username.ToString())
                    {
                        user.Value.Fullname = nameBox.Text;
                        user.Value.Faculty_ID = tmp_class.FacultyId;
                        user.Value.Class_ID = tmp_class.ClassId;
                        var updateResponse = await client.SetTaskAsync("Users/" + user.Key, user.Value);
                        MessageBox.Show("Update!");
                        break;
                    }
                }
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
