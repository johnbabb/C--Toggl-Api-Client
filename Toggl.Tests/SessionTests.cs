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
        public void Get_Session_By_Api_Token(string api_token)
        {
            var t = new TogglService();
            var lstKv = new List<KeyValuePair<string, string>>();
            lstKv.Add(new KeyValuePair<string, string>("api_token", api_token));
            
            var s = t.GetSession(lstKv);
            Assert.AreEqual(s.api_token, Constants.ApiToken);
        }
    }
}
