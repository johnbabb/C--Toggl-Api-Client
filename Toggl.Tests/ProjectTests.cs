using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Toggl.Services;

namespace Toggl.Tests
{
    [TestFixture]
    public class ProjectTests
    {
        
        

        [Test]
        public void Add()
        {
            var srv = new ProjectService();
            
            

            var obj = new Project
            {
                IsBillable = true,
                WorkspaceId = 303523 ,
                Name = "New Project" + DateTime.UtcNow,
                IsAutoEstimates = false
            };

            var act = srv.Add(obj);

            Assert.NotNull(act,"response back from api does not have at project object");
            Assert.GreaterOrEqual(act.Id, 0, "response back from object does not have a project id greater than zero");
        }

        [Test]
        public void Get()
        {
                       
            var srv = new ProjectService();

            var obj = srv.List();

            Assert.GreaterOrEqual(obj.Count(), 0);
        }
        
    }
}
