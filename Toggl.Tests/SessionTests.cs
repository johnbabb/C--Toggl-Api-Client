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
        public void GetSessionByApiToken()
        {
            var t = new ApiService(Constants.ApiToken);
            var s = t.GetSession();
            Assert.AreEqual(Constants.ApiToken, s.ApiToken);
        }
    }
}
