using System;
using Project_Manager_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Project_Manager_Unit_Test
{
    class TestProjectDbSet : TestDbSet<Project>
    {
        public override Project Find(params object[] keyValues)
        {
            return this.SingleOrDefault(item => item.Project_ID == (int)keyValues.Single());
        }

       
    }
}
