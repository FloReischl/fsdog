using System;
using System.IO;
using FR.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FR.Base.Tests {
    [TestClass]
    //[DeploymentItem("Data/TextFileContains1.txt")]
    public class TextFileTests {
        private const string TestDataDir = @"C:\Users\flori\source\repos\FsDog\FR.Base.Tests\Data";
        [TestMethod]
        public void FindTextFiles() {
            var dir = new DirectoryInfo(@"C:\Program Files\IrfanView");
            FindTextFilesInDir(dir);
        }

        private void FindTextFilesInDir(DirectoryInfo dir) {
            foreach (var file in dir.GetFiles()) {
                try {
                    var content = TextFile.CouldBeTextFile(file.FullName) ? "text" : "binary";
                    Console.WriteLine($"{content}: {file.Name} ({file.FullName})");
                }
                catch (IOException ex) {
                    Console.WriteLine($"EX: {ex.Message}");
                }
            }

            foreach (var sub in dir.GetDirectories()) {
                FindTextFilesInDir(sub);
            }
        }

        [TestMethod]
        public void Contains() {
            var find = "search text";
            string fullPath(string name) => Path.Combine(TestDataDir, name);

            Assert.IsTrue(TextFile.Contains(fullPath("TextFileContains1.txt"), find));
            Assert.IsFalse(TextFile.Contains(fullPath("TextFileContains2.txt"), find));
            Assert.IsTrue(TextFile.Contains(fullPath("TextFileContains3.txt"), find));
        }
    }
}
