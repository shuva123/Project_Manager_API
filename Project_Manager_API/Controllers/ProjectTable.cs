using System;

namespace Project_Manager_API.Controllers
{
    public class ProjectTable
    {
        public int? Project_ID { get; set; }
        public string ProjectName { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public string Priority { get; set; }
        public string IsCompleted { get; set; }
        public string Empolyee_Name { get; set; }
        public int? Employee_ID { get; set; }
    }
}