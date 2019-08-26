using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Manager_API.Models
{
    public class TaskView
    {
        public int Task_ID { get; set; }
        public string Parent_Name { get; set; }
        public Nullable<int> Project_ID { get; set; }
        public string Task1 { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public Nullable<int> Priority { get; set; }
        public string Status { get; set; }
    }
}