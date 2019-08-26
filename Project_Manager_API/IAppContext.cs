using Project_Manager_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_Manager_API
{
    public interface IAppContext : IDisposable
    {
        DbSet<Task> Tasks { get; }
        DbSet<Parent_Task> Parent_Task { get; }
        DbSet<User> Users { get; }
        DbSet<Project> Projects { get; }
        int SaveChanges();
    }
}