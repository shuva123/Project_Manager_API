using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Manager_API.Models
{
    public class ProjectView
    {
        public int Project_ID { get; set; }
        public string ProjectName { get; set; }
        public int TaskCount { get; set; }
        public int CompletedTaskCount { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public Nullable<int> Priority { get; set; }
        public string IsCompleted { get; set; }
    }
}