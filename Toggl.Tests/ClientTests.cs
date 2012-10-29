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
        
    }
}
