using FR.Collections.Generic;
using FsDog.Detail;
using FsDog.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FsDog {
    public partial class Form1 : Form {
        private DogItemList _list;
        private DogItemListView _view;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            CreateFields();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = _view;
            var columns = new[] { "Image", "Name", "Size", "TypeName", "DateModified" };
            columns.ForEach((col) => dataGridView1.Columns.Add(GridColumnFactory.CreateGridColumn(col)));
            this.DoubleBuffered = true;
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

            var dir = new DirectoryInfo("C:\\Temp\\testdir");
            foreach (var fi in dir.GetFiles()) {
                list.Add(new DogFile(fi));
            }

            //for (int i = 0; i < 50; i++) {
            //    if (i % 5 == 0) {
            //        list.Add(new DogDirectory($"C:\\temp\\dir{i}"));
            //    }
            //    else {
            //        list.Add(new DogFile($"C:\\temp\\test{i}.txt"));
            //    }
            //}

            return list;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            try {
                var input = textBox1.Text;
                var intermediate = input.Replace('*', '\x0011');
                intermediate = Regex.Escape(intermediate);
                intermediate = intermediate.Replace("\x0011", ".*");
                var filter = new ViewFilter { Expression = ExpressionType.Match, PropertyName = "Name", Value = intermediate };
                _view.Filter = filter;
            }
            catch (Exception ex) {
                label1.Text = ex.Message;
            }
        }
    }
}
