using System;
using Project_Manager_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Project_Manager_Unit_Test
{
    class TestParentTaskDbSet : TestDbSet<Parent_Task>
    {
        public override Parent_Task Find(params object[] keyValues)
        {
            return this.SingleOrDefault(item => item.Parent_ID == (int)keyValues.Single());
        }
    }
}