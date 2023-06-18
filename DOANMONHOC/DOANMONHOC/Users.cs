namespace DOANMONHOC
{
    internal class USER
    {
        public string Student_ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Faculty_ID { get; set; }
        public string Class_ID { get; set; }

    }
    internal class ADMIN
    {
        public string Adim_ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string AdminName { get; set; }
    }
    internal class CANDIDATE
    {
        public string Candidate_ID { get; set; }
        public string CandidateName { get; set; }
        public string Birthday { get; set; }
        public string Description { get; set; }
        public string Faculty_ID { get; set; }
        public string Class_ID { get; set; }
        public string Promise { get; set; }
    }
    internal class CAMPAIGN
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CampaignName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Category { get; set; }
        public int Status { get; set; }
    }
    internal class CAMPAIGN_CLASS
    {
        public string Campaign_ID { get; set; }
        public string Class_ID { get; set; }
    }
    internal class CAMPAIGN_CANDIDATE
    {
        public string Campaign_ID { get; set; }
        public string Candidate_ID { get; set; }
    }
    internal class VOTE
    {
        public string Student_ID { get; set; }
        public string Campaign_ID { get; set; }
        public string Candidate_ID { get; set; }
        public string TimeVoted { get; set; }
    }
    internal class FACULTY
    {
        public string Faculty_ID { get; set; }
        public string FacultyName { get; set; }
    }
    internal class CLASS
    {
        public string Class_ID { get; set; }
        public string Faculty_ID { get; set; }
        public string ClassName { get; set; }
    }
}