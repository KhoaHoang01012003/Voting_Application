namespace DOANMONHOC
{
    public class USER
    {
        public string Student_ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Faculty_ID { get; set; }
        public string Class_ID { get; set; }

    }
    public class ADMIN
    {
        public string Admin_ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string AdminName { get; set; }
    }
    public class CANDIDATE
    {
        public int Candidate_ID { get; set; }
        public string CandidateName { get; set; }
        public string Birthday { get; set; }
        public string Description { get; set; }
        public int Faculty_ID { get; set; }
        public int Class_ID { get; set; }
        public string Promise { get; set; }
    }
    public class CAMPAIGN
    {
        public int Campaint_ID { get; set; }
        public string CampaignName { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Category { get; set; }
        public int[] Candidate_ID { get; set; }
        public int[] Class_ID { get; set; }
    }

    public class VOTE
    {
        public string Student_ID { get; set; }
        public string Campaign_ID { get; set; }
        public string Candidate_ID { get; set; }
        public string TimeVoted { get; set; }
    }
    internal class FACULTY
    {
        public int Faculty_ID { get; set; }
        public string FacultyName { get; set; }
    }
    public class CLASS
    {
        public int Class_ID { get; set; }
        public int Faculty_ID { get; set; }
        public string ClassName { get; set; }
    }
}