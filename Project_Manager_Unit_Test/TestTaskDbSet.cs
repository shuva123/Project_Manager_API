using Project_Manager_API.Models;
using System.Linq;
using System;
using System.Data.Entity;

namespace Project_Manager_Unit_Test
{
    internal class TestTaskDbSet : TestDbSet<Task>
    {
        public override Task Find(params object[] keyValues)
        {
            return this.SingleOrDefault(item => item.Task_ID == (int)keyValues.Single());
        }
    }
}
