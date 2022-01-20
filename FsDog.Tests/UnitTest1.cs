using System;
using System.IO;
using System.Text.RegularExpressions;
using FR.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FsDog.Tests {
    [TestClass]
    public class UnitTest1 {
        class Foo {
            public string Bar { get; set; }
        }

        [TestMethod]
        public void RegexEscape() {
            var s = Regex.Escape("\\,*,+,?,|,{,[,(,),^,$,.,# ");
        }

        [TestMethod]
        public void NameOf() {
            var name = nameof(FsApp.Instance.Config.Options.Appearance);
            Assert.AreEqual(name, "Appearance");
        }

        [TestMethod]
        public void JsonSubPart() {
            var json = "{ 'foo': { 'Bar': 'blah' }, 'other': 'prop' }";
            JObject obj = JObject.Parse(json);
            var sub = obj["foo"];
            var ser = new JsonSerializer();
            using (var r = sub.CreateReader()) {
                var foo = ser.Deserialize<Foo>(r);
                Assert.AreEqual("blah", foo.Bar);
            }
        }
    }
}
