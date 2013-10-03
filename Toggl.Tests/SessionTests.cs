using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Toggl.Services;

namespace Toggl.Tests
{
    [TestFixture]
    public class SessionTests
    {
       
        [Test]
        [TestCase(Constants.ApiToken)]
        public void GetSessionByApiToken(string apiToken)
        {
            var t = new ApiService(apiToken);
            var s = t.GetSession();
            Assert.AreEqual(s.ApiToken, Constants.ApiToken);
        }
    }
}
