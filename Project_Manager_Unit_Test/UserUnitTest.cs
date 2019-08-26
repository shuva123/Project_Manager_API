using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Manager_API.Controllers;
using Project_Manager_API.Models;

namespace Project_Manager_Unit_Test
{
    [TestClass]
    public class UserUnitTest
    {
        [TestMethod]
        public void GetAllUsers()
        {            
            var context = new TestContext();
            context.Users.Add(
                new User
               {
                   User_ID = 1,
                   First_Name = "Soudipta",
                   Last_Name = "Swar",
                   Employee_ID = 379983,
                   Task_ID = 102,
                   Project_ID = 1
               });
            context.Users.Add(
               new User
               {
                   User_ID = 2,
                   First_Name = "Shuvajit",
                   Last_Name = "Chakraobrty",
                   Employee_ID = 379982,
                   Task_ID = 101,
                   Project_ID = 1
               });
            context.Users.Add(
               new User
               {
                   User_ID = 1,
                   First_Name = "Rupesh",
                   Last_Name = "Kumar",
                   Employee_ID = 542316,
                   Task_ID = 103,
                   Project_ID = 1
               });
            var controller = new UsersController(context);
            var contentResult = controller.GetUsers() as OkNegotiatedContentResult<List<User>>;
            Assert.AreEqual(contentResult.Content.Count, 3);

        }
        [TestMethod]
        public void DeleteUser_ShouldReturnOK()
        {
            var context = new TestContext();
            var user = GetDemoUser();
            context.Users.Add(user);

            var controller = new UsersController(context);
            var result = controller.DeleteUser(1) as OkNegotiatedContentResult<User>;

            Assert.IsNotNull(result);
            Assert.AreEqual(user.User_ID, result.Content.User_ID);
        }
        [TestMethod]
        public void PostUser_ShouldReturnSameItem()
        {
            var controller = new UsersController(new TestContext());

            var user = GetDemoUser();

            var result = controller.PostUser(user) as CreatedAtRouteNegotiatedContentResult<User>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.User_ID);
            Assert.AreEqual(result.Content.First_Name, user.First_Name);
        }
        [TestMethod]
        public void GetUser_ShouldReturnItemWithSameID()
        {
            var context = new TestContext();
            context.Users.Add(GetDemoUser());
            var controller = new UsersController(context);
            var result = controller.GetUser(1) as OkNegotiatedContentResult<User>;
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.User_ID);
        }
        [TestMethod]
        public void PutUser_ShouldFail_WhenDifferentID()
        {
            var controller = new UsersController(new TestContext());

            var user = GetDemoUser();
            var result = controller.PutUser(1000, user);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
        User GetDemoUser()
        {
            return new User
            {
                User_ID = 1,
                First_Name = "Soudipta",
                Last_Name = "Swar",
                Employee_ID = 379983,
                Task_ID = 102,
                Project_ID = 1
            };
        }
    }
}
