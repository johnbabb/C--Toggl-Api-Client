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
        public void Get_Tasks()
        {
            var t = new TaskService();

            var tasks = t.GetTasks();

            Assert.AreEqual(tasks.Count(),0 );
        }
    }
}
