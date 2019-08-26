using System;

namespace Project_Manager_API.Controllers
{
    public class projectView
    {
        public int Project_ID { get; set; }
        public string ProjectName { get; set; }
        public int TaskCount { get; set; }
        public int CompletedTaskCount { get; set; }
        public Nullable<DateTime> Start_Date { get; set; }
        public Nullable<DateTime> End_Date { get; set; }
        public Nullable<int> Priority { get; set; }
        public string IsCompleted { get; set; }
    }
}