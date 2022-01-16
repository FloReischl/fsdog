using FR.IO;
using FsDog.Detail;
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

namespace FsDog.Search {
    public partial class FormSearch : Form {
        private DirectoryInfo _root;
        //private string _fileName;
        private Regex _fileName;
        private string _contained;
        private HashSet<string> _extensions;
        private DetailTable _table;

        public FormSearch() {
            InitializeComponent();
        }

        public DirectoryInfo Directory { get; internal set; }

        private void FormSearch_Load(object sender, EventArgs e) {
            if (this.DesignMode) {
                return;
            }

            cboDirectory.Text = Directory?.FullName;

            DataGridViewColumn createColumn(string name, int width) {
                var col = GridColumnFactory.CreateGridColumn(_table.Columns[name]);
                col.Width = width;
                return col;
            }

            _table = new DetailTable();
            gridResults.DataSource = _table;
            gridResults.Columns.Clear();
            gridResults.Columns.Add(createColumn("Image", 24));
            gridResults.Columns.Add(createColumn("Name", 300));
            gridResults.Columns.Add(createColumn("Size", 150));
            gridResults.Columns.Add(createColumn("TypeName", 150));
            gridResults.Columns.Add(createColumn("DateModified", 150));
        }

        private void btnSearch_Click(object sender, EventArgs e) {
            if (!TryPrepareInput()) {
                return;
            }

            _table.Rows.Clear();

            SearchAsync(_root);
        }

        private bool TryPrepareInput() {
            if (string.IsNullOrEmpty(cboDirectory.Text)) {
                return false;
            }

            try {
                _root = new DirectoryInfo(cboDirectory.Text);
            }
            catch {
                MessageBox.Show("Invalid directory format.", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!_root.Exists) {
                MessageBox.Show("Directory does not exist.", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            var fn = cboFileName.Text;
            fn = Regex.Escape(fn).Replace("\\*", ".*");
            //if (!fn.EndsWith(".*")) fn += ".*";
            //if (!fn.StartsWith(".*")) fn = ".*" + fn;
            _fileName = new Regex(fn, RegexOptions.Compiled);
            _contained = cboContains.Text;
            _contained = _contained.Replace("\\r", "\r").Replace("\\n", "\n").Replace("\\t", "\t");
            var extS = cboExtensions.Text ?? string.Empty;
            _extensions = new HashSet<string>(extS.Split(new[] { ",", " ", ";" }, StringSplitOptions.RemoveEmptyEntries).Distinct(), StringComparer.InvariantCultureIgnoreCase);

            return true;
        }

        private void SearchAsync(DirectoryInfo dir) {
            foreach (var item in dir.GetFiles()) {
                if (!IsValidExtension(item)) {
                    continue;
                }

                if (!IsValidFileName(item)) {
                    continue;
                }

                if (!IsValidContent(item)) {
                    continue;
                }

                _table.Add(item);
            }

            foreach (var item in dir.GetDirectories()) {
                if (IsValidFileName(item)) {
                    _table.Add(item);
                }

                SearchAsync(item);
            }
        }

        private bool IsValidExtension(FileInfo file) {
            return _extensions.Count == 0 || _extensions.Contains(file.Extension);
        }

        private bool IsValidFileName(FileSystemInfo fsi) {
            return _fileName.IsMatch(fsi.Name);
        }

        private bool IsValidContent(FileInfo file) {
            return string.IsNullOrEmpty(_contained)
                || (TextFile.CouldBeTextFile(file.FullName) && TextFile.Contains(file.FullName, _contained, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
