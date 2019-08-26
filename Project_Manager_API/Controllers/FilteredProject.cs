using System;

namespace Project_Manager_API.Controllers
{
    public class FilteredProject
    {
        public int Project_ID { get; set; }
        public string ProjectName { get; set; }
        public Nullable<DateTime> Start_Date { get; set; }
        public Nullable<DateTime> End_Date { get; set; }
        public int? Priority { get; set; }
        public int? Employee_ID { get; set; }
        public string Employee_Name { get; set; }


    }
}