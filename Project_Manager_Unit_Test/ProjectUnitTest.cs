using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Manager_API.Models;
using Project_Manager_API.Controllers;
using System.Collections.Generic;
using System.Web.Http.Results;

namespace Project_Manager_Unit_Test
{
    [TestClass]
    public class ProjectUnitTest
    {
        [TestMethod]
        public void GetAllProjects()
        {
            var context = new TestContext();
            context.Projects.Add(new Project
            {
                Project1="Project 1",
                Project_ID=1,
                Priority=10,
                Start_Date = Convert.ToDateTime("2019/01/01"),
                End_Date = Convert.ToDateTime("2019/11/01"),
            });
            context.Projects.Add(new Project
            {
                Project1 = "Project 2",
                Project_ID = 2,
                Priority = 3,
                Start_Date = Convert.ToDateTime("2019/01/02"),
                End_Date = Convert.ToDateTime("2019/11/02"),
            });
            context.Projects.Add(new Project
            {
                Project1 = "Project 3",
                Project_ID = 3,
                Priority = 5,
                Start_Date = Convert.ToDateTime("2019/01/03"),
                End_Date = Convert.ToDateTime("2019/11/03"),
            });
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
                Project_ID = 2,
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
            var controller = new ProjectsController(context);
            var contentResult = controller.GetProjects() as OkNegotiatedContentResult<List<projectView>>;
            Assert.AreEqual(contentResult.Content[0].TaskCount, 2);
        }
        [TestMethod]
        public void DeleteProject_ShouldReturnOK()
        {
            var context = new TestContext();
            var project = GetDemoProject();
            context.Projects.Add(project);

            var controller = new ProjectsController(context);
            var result = controller.DeleteProject(3) as OkNegotiatedContentResult<Project>;

            Assert.IsNotNull(result);
            Assert.AreEqual(project.Project_ID, result.Content.Project_ID);
        }
        [TestMethod]
        public void PostProject_ShouldReturnSameItem()
        {
            var controller = new ProjectsController(new TestContext());

            var project = GetDemoProject();

            var result = controller.PostProject(project) as CreatedAtRouteNegotiatedContentResult<Project>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Project_ID);
            Assert.AreEqual(result.Content.Project1, project.Project1);
        }
        [TestMethod]
        public void GetProject_ShouldReturnItemWithSameID()
        {
            var context = new TestContext();
            context.Projects.Add(GetDemoProject());
            context.Users.Add(GetDemoUser());
            var controller = new ProjectsController(context);
            var result = controller.GetProject(3) as OkNegotiatedContentResult<List<FilteredProject>>;
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Content[0].Project_ID);
        }
        [TestMethod]
        public void PutProject_ShouldFail_WhenDifferentID()
        {
            var controller = new ProjectsController(new TestContext());

            var project = GetDemoProject();
            var result = controller.PutProject(1000, project);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
        Project GetDemoProject()
        {
            return new Project
            {
                Project1 = "Project 3",
                Project_ID = 3,
                Priority = 5,
                Start_Date = Convert.ToDateTime("2019/01/03"),
                End_Date = Convert.ToDateTime("2019/11/03"),
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
                Project_ID = 3
            };
        }
    }
}
