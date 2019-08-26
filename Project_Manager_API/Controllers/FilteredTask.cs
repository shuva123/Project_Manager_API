using System;

namespace Project_Manager_API.Controllers
{
    public class FilteredTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Parent_ID { get; set; }
        public string Parent_Task_Name { get; set; }
        public Nullable<int> Project_ID { get; set; }
        public Nullable<DateTime> Start_Date { get; set; }
        public Nullable<DateTime> End_Date { get; set; }
        public Nullable<int> Priority { get; set; }
        public string Status { get; set; }
        public Nullable<int> UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string Project_Name { get; set; }
    }
}