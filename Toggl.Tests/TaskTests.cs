using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Toggl.Services;

namespace Toggl.Tests
{
    [TestFixture]
    public class TaskTests
    {
        
        [Test]
        public void GetTasks()
        {
            var srv = new TaskService();

            var obj = srv.List();

            Assert.GreaterOrEqual(obj.Count(), 0);
        }
        /// <summary>
        /// 
        /// 
        /// </summary>
        [Test]
        public void AddTask()
        {
            var t = new TaskService();

            var act = new Task
                          {
                              IsActive = true,
                              User = new User { Id = Constants.DefaultUserId },
                              Name = "test123",
                              EstimatedSeconds = 3600,
                              Project = new Project { Id = Constants.DefaultProjectId }
                          };

            var exp = t.Add(act);
            
            Assert.AreEqual(exp, act);
        }
    }
}
