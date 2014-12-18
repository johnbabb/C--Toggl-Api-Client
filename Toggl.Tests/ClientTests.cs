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
        public void GetById()
        {
            var srv = new ClientService();

            var obj = GetClientMock();

            obj = srv.Add(obj);

            var expId = obj.Id;           

            var actObj = srv.Get(expId.Value);
            
            Assert.IsNotNull(actObj);

            Assert.IsTrue(actObj.Id.HasValue);

            Assert.GreaterOrEqual(actObj.Id, 1);
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
                WorkspaceId = workSpace.Id
            };
            var act = srv.Add(obj);

            Assert.Greater(act.Id, 0);
        }

        [Test]
        public void Edit()
        {

            var workSpace = new WorkspaceService().List().FirstOrDefault();
            var srv = new ClientService();

            var obj = new Client()
            {
                Name = "New Client" + DateTime.Now.Ticks,
                HourlyRate = new Random(13).NextDouble(),
                Currency = "USD",
                WorkspaceId = workSpace.Id
            };
            var exp = srv.Add(obj);

            Assert.Greater(exp.Id, 0);

            exp.Name = "Edit Test - " + DateTime.Now.Ticks;
            var act = srv.Edit(exp);
            Assert.True(act.Name == exp.Name);
            Assert.True(act.Name != obj.Name);


        }

        [Test]
        public void Delete()
        {
            
            var srv = new ClientService();

            var obj = GetClientMock();
            
            obj = srv.Add(obj);
            
            var expId = obj.Id;

            var act = srv.Delete(obj.Id.Value);
            
            Assert.True(act == true);

            var actObj = srv.Get(expId.Value);

            Assert.IsFalse(actObj.Id.HasValue);

        }
        private Client GetClientMock()
        {
            return new Client()
            {
                Name = "New Client" + DateTime.Now.Ticks,
                HourlyRate = new Random(13).NextDouble(),
                Currency = "USD",
                WorkspaceId = Constants.DefaultWorkspaceId
            };
        }

    }
}
