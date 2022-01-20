using System;
using FsDog.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FsDog.Tests {
    [TestClass]
    public class FsDogConfigTests {
        [TestMethod]
        public void Load() {
            var config = FsDogConfig.Load();
        }
    }
}
