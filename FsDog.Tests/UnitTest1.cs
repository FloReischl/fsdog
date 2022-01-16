using System;
using System.IO;
using System.Text.RegularExpressions;
using FR.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FsDog.Tests {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void MyTestMethod() {
            var s = Regex.Escape("\\,*,+,?,|,{,[,(,),^,$,.,# ");
        }
    }
}
