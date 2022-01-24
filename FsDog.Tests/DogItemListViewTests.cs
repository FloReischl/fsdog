using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using FsDog.FileSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FsDog.Tests {
    [TestClass]
    public class DogItemListViewTests {
        private DogItemList _list;
        private DogItemListView _view;

        [TestInitialize]
        public void Init() {
            CreateFields();
        }

        [TestMethod]
        public void CreateListTest() {
            var list = CreateList();
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void CreateViewTest() {
            var list = CreateList();
            var view = list.GetList();
            Assert.AreEqual(list.Count, view.Count);
            CollectionAssert.AreEquivalent(list, view);
        }

        [TestMethod]
        public void SortView() {
            //var list = CreateList();
            //var view = (DogItemListView)list.GetList();
            var props = _list.GetItemProperties(null);
            var prop = props.Cast<PropertyDescriptor>().Where(pd => pd.Name == "Name").First();
            _view.ApplySort(prop, ListSortDirection.Ascending);
            Assert.AreEqual(_view.First().Name, "dir0");
            Assert.AreEqual("Name", _view.SortProperty.Name);
            Assert.AreEqual(_view.SortDirection, ListSortDirection.Ascending);
        }

        [TestMethod]
        public void SortPerformance() {
            var list = CreateList(1000);
            var view = (DogItemListView)list.GetList();
            var props = list.GetItemProperties(null);
            var prop = props.Cast<PropertyDescriptor>().Where(pd => pd.Name == "Name").First();
            var watch = Stopwatch.StartNew();
            view.ApplySort(prop, ListSortDirection.Ascending);
            watch.Stop();
            Console.WriteLine(watch.Elapsed);
            Assert.IsTrue(watch.ElapsedMilliseconds < 200);
        }

        [TestMethod]
        public void Filter() {
            // .*test1.*
            var argument = "*test1*";
            var intermediate = argument.Replace('*', '\x0011');
            intermediate = Regex.Escape(intermediate);
            intermediate = intermediate.Replace("\x0011", ".*");
            var filter = new ViewFilter { Expression = ExpressionType.Match, PropertyName = "Name", Value = intermediate };
            _view.Filter = filter;
            Assert.IsTrue(_view.All(item => item.Name.Contains("test1")));
        }

        [TestMethod]
        public void FilterPerformance() {
            // .*test1.*
            CreateFields(1000);
            var input = "*test1*";
            var intermediate = input.Replace('*', '\x0011');
            intermediate = Regex.Escape(intermediate);
            intermediate = intermediate.Replace("\x0011", ".*");
            var filter = new ViewFilter { Expression = ExpressionType.Match, PropertyName = "Name", Value = intermediate };
            var watch = Stopwatch.StartNew();
            _view.Filter = filter;
            watch.Stop();
            Console.WriteLine(watch.Elapsed);
            Assert.IsTrue(_view.All(item => item.Name.Contains("test1")));
        }

        [TestMethod]
        [Ignore]
        public void CreateFiles() {
            var dir = new DirectoryInfo("C:\\temp\\testdir");
            if (!dir.Exists) {
                dir.Create();
            }

            for (int i = 0; i < 50; i++) {
                using (var sw = new StreamWriter(Path.Combine(dir.FullName, $"text{i}.txt"))) {
                    sw.WriteLine(Guid.NewGuid().ToString());
                }
            }
        }

        private void CreateFields(int count = 50) {
            _list = CreateList(count);
            _view = (DogItemListView)_list.GetList();
        }

        private DogItemListView CreateView(int count = 50) {
            var list = CreateList(count);
            return (DogItemListView)list.GetList();
        }

        private DogItemList CreateList(int count = 50) {
            var list = new DogItemList();

            for (int i = 0; i < 50; i++) {
                if (i % 5 == 0) {
                    list.Add(new DogDirectory($"C:\\temp\\dir{i}"));
                }
                else {
                    list.Add(new DogFile($"C:\\temp\\test{i}.txt"));
                }
            }

            return list;
        }
    }
}
