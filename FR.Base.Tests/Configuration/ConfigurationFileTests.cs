using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace FR.Base.Tests.Configuration {
    [TestClass]
    public class ConfigurationFileTests {
        [TestMethod]
        public void JPathNoFound() {
            JObject obj = new JObject();
            var found = obj.SelectToken("foo");
            Assert.IsNull(found);

            Assert.IsFalse(obj.Remove("bar"));
        }
    }
}
