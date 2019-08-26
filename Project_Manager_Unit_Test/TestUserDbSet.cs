using System;
using Project_Manager_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;


namespace Project_Manager_Unit_Test
{
    class TestUserDbSet : TestDbSet<User>
    {
        public override User Find(params object[] keyValues)
        {
            return this.SingleOrDefault(item => item.User_ID == (int)keyValues.Single());
        }


    }
}
