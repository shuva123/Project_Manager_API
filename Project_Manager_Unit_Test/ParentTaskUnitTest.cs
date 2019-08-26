using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Manager_API.Controllers;
using Project_Manager_API.Models;
using System.Data.Entity;
namespace Project_Manager_Unit_Test
{
    [TestClass]
    public class ParentTaskUnitTest
    {
        [TestMethod]
        public void GetAllParentTasks()
        {
            var context = new TestContext();
            context.Parent_Task.Add(new Parent_Task
            {
                Parent_ID = 1,
                Parent_Task1 = "Parent_Task1"
            });
            context.Parent_Task.Add(new Parent_Task
            {
                Parent_ID = 2,
                Parent_Task1 = "Parent_Task2"
            });
            context.Parent_Task.Add(new Parent_Task
            {
                Parent_ID = 3,
                Parent_Task1 = "Parent_Task3"
            });
            var controller = new Parent_TaskController(context);
            var contentResult = controller.GetParent_Task() as OkNegotiatedContentResult<List<Parent_Task>>;
            Assert.AreEqual(3, contentResult.Content.Count);
        }
        [TestMethod]
        public void DeleteParentTask_ShouldReturnOK()
        {
            var context = new TestContext();
            var parent_Task = GetDemoParentTask();
            context.Parent_Task.Add(parent_Task);

            var controller = new Parent_TaskController(context);
            var result = controller.DeleteParent_Task(3) as OkNegotiatedContentResult<Parent_Task>;

            Assert.IsNotNull(result);
            Assert.AreEqual(parent_Task.Parent_ID, result.Content.Parent_ID);
        }
        [TestMethod]
        public void PostParentTask_ShouldReturnSameItem()
        {
            var controller = new Parent_TaskController(new TestContext());

            var parent_Task = GetDemoParentTask();

            var result = controller.PostParent_Task(parent_Task) as CreatedAtRouteNegotiatedContentResult<Parent_Task>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Parent_ID);
            Assert.AreEqual(result.Content.Parent_Task1, parent_Task.Parent_Task1);
        }
        [TestMethod]
        public void GetParentTask_ShouldReturnItemWithSameID()
        {
            var context = new TestContext();
            context.Parent_Task.Add(GetDemoParentTask());
            var controller = new Parent_TaskController(context);
            var result = controller.GetParent_Task(3) as OkNegotiatedContentResult<Parent_Task>;
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Content.Parent_ID);
        }
        [TestMethod]
        public void PutParentTask_ShouldFail_WhenDifferentID()
        {
            var controller = new Parent_TaskController(new TestContext());

            var task = GetDemoParentTask();
            var result = controller.PutParent_Task(1000, task);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        Parent_Task GetDemoParentTask()
        {
            return new Parent_Task
            {
                Parent_ID = 3,
                Parent_Task1 = "Parent_Task3"
            };
        }
    }
}
