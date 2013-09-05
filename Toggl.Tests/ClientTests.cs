using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Toggl.Services;

namespace Toggl.Tests
{
    [TestFixture]
    public class ClientTests
    {
        
        [Test]
        public void List()
        {
            var srv = new ClientService();

            var obj = srv.List();

            Assert.GreaterOrEqual(obj.Count(), 0);
        }

        [Test]
        public void Add()
        {
            var workSpace = new WorkspaceService().List().FirstOrDefault();
            var srv = new ClientService();

            var obj = new Client()
            {
                Name = "New Client" + DateTime.Now.Ticks,
                HourlyRate = new Random(13).NextDouble(),
                Currency = "USD",
                WorkspaceId =  workSpace.Id
            };
            var act = srv.Add(obj);

            Assert.Greater(act.Id, 0);
        }
        [Test]
        public void Edit()
        {

            var srv = new ClientService();
            var obj = srv.List().ToList().LastOrDefault();
            if (obj == null)
            {
                this.Add();
                obj = srv.List().ToList().LastOrDefault();
            }
            obj.Name = "Edit Test - " + DateTime.Now.Ticks;
            var act = srv.Edit(obj);
            Assert.True(obj.Name==act.Name);


        }

        [Test]
        public void Delete()
        {
            
            var srv = new ClientService();
            var obj = srv.List().ToList().LastOrDefault();
            if (obj == null)
            {
                this.Add();
                obj = srv.List().ToList().LastOrDefault();
            }
            var act = srv.Delete(obj.Id.Value);

            Assert.True(act==true);
        }

    }
}
