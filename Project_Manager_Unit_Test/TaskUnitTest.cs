using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Manager_API.Models;
using Project_Manager_API.Controllers;
using System.Collections.Generic;
using System.Web.Http.Results;

namespace Project_Manager_Unit_Test
{
    [TestClass]
    public class TaskUnitTest
    {
        [TestMethod]
        public void GetAllTasks()
        {
            var context = new TestContext();
            context.Tasks.Add(new Task
            {
                Task_ID = 100,
                Project_ID = 1,
                Parent_ID = 1,
                Priority = 10,
                Start_Date = Convert.ToDateTime("2019/01/01"),
                End_Date = Convert.ToDateTime("2019/11/01"),
                Status = "",
                Task1 = "Task1"
            });
            context.Tasks.Add(new Task
            {
                Task_ID = 101,
                Project_ID = 1,
                Parent_ID = 2,
                Priority = 10,
                Start_Date = Convert.ToDateTime("2019/01/02"),
                End_Date = Convert.ToDateTime("2019/11/02"),
                Status = "",
                Task1 = "Task2"
            });
            context.Tasks.Add(new Task
            {
                Task_ID = 102,
                Project_ID = 1,
                Parent_ID = 3,
                Priority = 12,
                Start_Date = Convert.ToDateTime("2019/01/03"),
                End_Date = Convert.ToDateTime("2019/11/03"),
                Status = "Completed",
                Task1 = "Task3"
            });
            context.Parent_Task.Add(new Parent_Task
            {
                Parent_ID = 1,
                Parent_Task1 = "Parent_Task1"
            });
            context.Parent_Task.Add(new Parent_Task
            {
                Parent_ID = 2,
                Parent_Task1 = "Parent_Task1"
            });
            context.Parent_Task.Add(new Parent_Task
            {
                Parent_ID = 3,
                Parent_Task1 = "Parent_Task3"
            });
            var controller = new TasksController(context);
            var contentResult = controller.GetTasks() as List<TaskView>;
            Assert.AreEqual(contentResult.Count, 3);
        }
        [TestMethod]
        public void DeleteTask_ShouldReturnOK()
        {
            var context = new TestContext();
            var task = GetDemoTask();
            context.Tasks.Add(task);

            var controller = new TasksController(context);
            var result = controller.DeleteTask(102) as OkNegotiatedContentResult<Task>;

            Assert.IsNotNull(result);
            Assert.AreEqual(task.Task_ID, result.Content.Task_ID);
        }
        [TestMethod]
        public void PostTask_ShouldReturnSameItem()
        {
            var controller = new TasksController(new TestContext());

            var task = GetDemoTask();
           
            var result = controller.PostTask(task) as CreatedAtRouteNegotiatedContentResult<Task>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Task_ID);
            Assert.AreEqual(result.Content.Task1, task.Task1);
        }
        [TestMethod]
        public void GetTask_ShouldReturnItemWithSameID()
        {
            var context = new TestContext();
            context.Tasks.Add(GetDemoTask());
            context.Projects.Add(GetDemoProject());
            context.Parent_Task.Add(GetDemoParentTask());
            context.Users.Add(GetDemoUser());
            var controller = new TasksController(context);
            var result = controller.GetTask(102) as OkNegotiatedContentResult<List<FilteredTask>>;
            Assert.IsNotNull(result);
            Assert.AreEqual(102, result.Content[0].Id);
        }
        [TestMethod]
        public void PutTask_ShouldFail_WhenDifferentID()
        {
            var controller = new TasksController(new TestContext());

            var task = GetDemoTask();
            var result = controller.PutTask(1000,task);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
        Task GetDemoTask()
        {
            return new Task
            {
                Task_ID = 102,
                Project_ID = 1,
                Parent_ID = 3,
                Priority = 12,
                Start_Date = Convert.ToDateTime("2019/01/03"),
                End_Date = Convert.ToDateTime("2019/11/03"),
                Status = "Completed",
                Task1 = "Task3"
            };
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
        Project GetDemoProject()
        {
            return new Project
            {
                Project_ID = 1,
                Project1 = "Project 1",
                Priority = 10,
                Start_Date = Convert.ToDateTime("2019/01/03"),
                End_Date = Convert.ToDateTime("2019/11/03"),
                IsCompleted = "Yes"
            };
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
