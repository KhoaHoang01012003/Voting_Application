using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Guna.UI2.WinForms;
using Newtonsoft.Json.Linq;
using System.Text;

namespace DOANMONHOC
{
    public partial class CreateVote1 : Form
    {
        FirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FoBk4yXguU4VoMkIe5M7M2ylsGymwUsld8cS2Td1",
            BasePath = "https://votingapplication-2097e-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        private Form indexForm;
        private bool isBackButtonPressed;
        int[] classIdArray = new int[] { };
        bool[] ok;

        public CreateVote1(Form parentForm)
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(FormClosed_Exit);
            indexForm = parentForm;
            isBackButtonPressed = false;
        }

        void FormClosed_Exit(object sender, FormClosedEventArgs e)
        {
            if (!isBackButtonPressed)
            {
                Application.ExitThread();
            }
        }

        private async void Dashboard_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            byte[] originalBytesAvt = Convert.FromBase64String(Properties.Settings.Default.avt.ToString());

            // Tạo một đối tượng Image từ chuỗi byte gốc
            Image imageAvt;
            using (MemoryStream ms = new MemoryStream(originalBytesAvt))
            {
                imageAvt = Image.FromStream(ms);
            }

            avatar.Image = imageAvt.GetThumbnailImage(40, 40, null, IntPtr.Zero);
            FullName.Text = Properties.Settings.Default.Name.ToString();

            this.Invoke((MethodInvoker)delegate
            {
                facultyList.Items.Clear();
            });

            FirebaseResponse facultyResponse = await client.GetTaskAsync("Faculties").ConfigureAwait(false);
            Dictionary<string, FACULTY> faculties = facultyResponse.ResultAs<Dictionary<string, FACULTY>>();

            FirebaseResponse classResponse = await client.GetTaskAsync("Classes").ConfigureAwait(false);
            Dictionary<string, CLASS> classes = classResponse.ResultAs<Dictionary<string, CLASS>>();

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

            ok = Enumerable.Repeat(true, classes.Count).ToArray();

            classList.SelectedIndexChanged += (sender, e) =>
            {
                string selectedClassItem = classList.SelectedItem as string;
                List<CLASS> classesList = classes.Values.ToList();
                for (int i = 0; i < classesList.Count; i++)
                {
                    CLASS classObj = classesList[i];
                    if (classObj.ClassName == selectedClassItem && ok[i])
                    {
                        int[] tmp = new int[classIdArray.Length + 1];
                        Array.Copy(classIdArray, tmp, classIdArray.Length);
                        tmp[tmp.Length - 1] = classObj.Class_ID;
                        classIdArray = tmp;
                        selectClass.Text += classObj.ClassName + ", ";
                        ok[i] = false;
                        break;
                    }
                }
            };
        }


        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var openForm = new adminDashboard(indexForm);
            openForm.Show();
            isBackButtonPressed = true;
            this.Close();
        }

        public bool ValidateDateTimePickers(Guna2DateTimePicker startTime, Guna2DateTimePicker endTime)
        {
            DateTime now = DateTime.Now;

            if (startTime.Value < now || endTime.Value < now)
            {
                MessageBox.Show("Thời gian bắt đầu hoặc kết thúc không được nhỏ hơn thời gian hiện tại.");
                return false;
            }

            if (startTime.Value >= endTime.Value)
            {
                MessageBox.Show("Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc.");
                return false;
            }

            return true;
        }

        private async void guna2Button4_Click(object sender, EventArgs e)
        {
            FirebaseResponse titleCheckResponse = await client.GetTaskAsync("Campaigns/");
            JObject campaignsJson = JObject.Parse(titleCheckResponse.Body);
            var campaigns = campaignsJson.ToObject<Dictionary<string, CAMPAIGN>>();

            bool titleExists = campaigns.Values.Any(u => u.CampaignName == campaignName.Text);
            if (titleExists)
            {
                MessageBox.Show("Cuộc bỏ phiếu này đã được đăng ký!");
                return;
            }

            bool checkValidTime = ValidateDateTimePickers(startTime, endTime);
            if (!checkValidTime)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(description.Text) || string.IsNullOrWhiteSpace(campaignName.Text) || string.IsNullOrWhiteSpace(category.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (classIdArray.Length == 0)
            {
                MessageBox.Show("Vui lòng chọn lớp!");
                return;
            }

            var data = new CAMPAIGN
            {
                Campaint_ID = campaigns.Count(),
                Description = description.Text,
                CampaignName = campaignName.Text,
                StartTime = startTime.Value,
                EndTime = endTime.Value,
                Candidate_ID = new int[] { },
                Class_ID = classIdArray,
                Category = category.Text,
            };

            var openForm = new CreateVote12(indexForm);
            openForm.Data = data;
            openForm.Show();
            isBackButtonPressed = true;
            this.Close();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            indexForm.Show();
            isBackButtonPressed = true;
            this.Close();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ok.Length; i++)
            {
                ok[i] = true;
            }

            selectClass.Text = "Các lớp đã chọn: ";
            classIdArray = new int[] { };
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var openForm = new adminElectionActivities(indexForm);
            openForm.Show();
            isBackButtonPressed = true;
            this.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            var openForm = new list_candidate(indexForm);
            openForm.Show();
            isBackButtonPressed = true;
            this.Close();
        }
    }
}
