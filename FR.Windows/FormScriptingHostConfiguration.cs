// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.FormScriptingHostConfiguration
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FR.Windows.Forms
{
  public class FormScriptingHostConfiguration : Form
  {
    private const string HostDirectory = "Host Directory";
    private const string ScriptDirectory = "ScriptDirectory";
    private IContainer components;
    private ListView lvwHosts;
    private GroupBox groupBox1;
    private Button btnCancel;
    private Button btnOk;
    private LinkLabel btnDown;
    private LinkLabel btnUp;
    private Label label2;
    private TextBox txtLocation;
    private Label label1;
    private TextBox txtName;
    private Button btnDelete;
    private Button btnUpdate;
    private ComboBox cboExecuteAt;
    private Label label3;
    private Button btnLocation;
    private Button btnAdd;
    private Label label4;
    private TextBox txtArguments;
    private ToolTip tipArguments;

    public FormScriptingHostConfiguration()
    {
      this.InitializeComponent();
      this.Hosts = new List<ScriptingHostConfiguration>();
      this.FormClosing += new FormClosingEventHandler(this.FromScriptingHostConfiguration_FormClosing);
      this.Load += new EventHandler(this.FromScriptingHostConfiguration_Load);
      this.lvwHosts.SelectedIndexChanged += new EventHandler(this.lvwHosts_SelectedIndexChanged);
      this.btnUp.Click += new EventHandler(this.btnUp_Click);
      this.btnDown.Click += new EventHandler(this.btnDown_Click);
      this.btnLocation.Click += new EventHandler(this.btnLocation_Click);
      this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
      this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
      this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
    }

    public List<ScriptingHostConfiguration> Hosts { get; private set; }

    private ListViewItem CreateListViewItem() => new ListViewItem("")
    {
      SubItems = {
        "Location",
        "Execute At",
        "Arguments"
      }
    };

    private void HostToItem(ScriptingHostConfiguration host, ListViewItem item)
    {
      item.Text = host.Name;
      item.SubItems[1].Text = host.Location;
      item.SubItems[2].Text = host.ExecutionLocation != ScriptExecutionLocation.HostDirectory ? "ScriptDirectory" : "Host Directory";
      item.SubItems[3].Text = this.txtArguments.Text;
      item.Tag = (object) host;
    }

    private bool UpdateHost(ScriptingHostConfiguration host)
    {
      if (string.IsNullOrEmpty(this.txtName.Text))
      {
        int num = (int) MessageBox.Show((IWin32Window) this, "Name cannot be empty", "Not saved", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        return false;
      }
      if (string.IsNullOrEmpty(this.txtLocation.Text))
      {
        int num = (int) MessageBox.Show((IWin32Window) this, "Script host location cannot be empty", "Not saved", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        return false;
      }
      if (this.cboExecuteAt.SelectedIndex == -1)
      {
        int num = (int) MessageBox.Show((IWin32Window) this, "No script execution location was specified.", "Not saved", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        return false;
      }
      host.Name = this.txtName.Text;
      host.Location = this.txtLocation.Text;
      host.Arguments = this.txtArguments.Text;
      host.ExecutionLocation = !(this.cboExecuteAt.SelectedItem.ToString() == "Host Directory") ? ScriptExecutionLocation.ScriptDirectory : ScriptExecutionLocation.HostDirectory;
      return true;
    }

    private void FromScriptingHostConfiguration_Load(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      this.cboExecuteAt.Items.Add((object) "ScriptDirectory");
      this.cboExecuteAt.Items.Add((object) "Host Directory");
      this.lvwHosts.Columns.Add("Name", 80);
      this.lvwHosts.Columns.Add("Location", 270);
      this.lvwHosts.Columns.Add("Execute At", 90);
      this.lvwHosts.Columns.Add("Arguments", 70);
      foreach (ScriptingHostConfiguration host1 in this.Hosts)
      {
        ScriptingHostConfiguration host2 = new ScriptingHostConfiguration(host1);
        ListViewItem listViewItem = this.CreateListViewItem();
        this.HostToItem(host2, listViewItem);
        this.lvwHosts.Items.Add(listViewItem);
      }
    }

    private void FromScriptingHostConfiguration_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.DialogResult != DialogResult.OK)
        return;
      this.Hosts.Clear();
      foreach (ListViewItem listViewItem in this.lvwHosts.Items)
        this.Hosts.Add((ScriptingHostConfiguration) listViewItem.Tag);
    }

    private void lvwHosts_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.lvwHosts.SelectedItems.Count == 0)
      {
        this.btnUp.Enabled = false;
        this.btnDown.Enabled = false;
        this.btnDelete.Enabled = false;
        this.btnUpdate.Enabled = false;
        this.cboExecuteAt.SelectedIndex = -1;
        this.cboExecuteAt.Enabled = false;
        this.txtName.Text = string.Empty;
        this.txtLocation.Text = string.Empty;
        this.txtArguments.Text = string.Empty;
      }
      else
      {
        this.btnUp.Enabled = true;
        this.btnDown.Enabled = true;
        this.btnDelete.Enabled = true;
        this.btnUpdate.Enabled = true;
        this.cboExecuteAt.Enabled = true;
        ScriptingHostConfiguration tag = (ScriptingHostConfiguration) this.lvwHosts.SelectedItems[0].Tag;
        this.txtName.Text = tag.Name;
        this.txtLocation.Text = tag.Location;
        this.txtArguments.Text = tag.Arguments;
        if (tag.ExecutionLocation == ScriptExecutionLocation.HostDirectory)
          this.cboExecuteAt.SelectedItem = (object) "Host Directory";
        else
          this.cboExecuteAt.SelectedItem = (object) "ScriptDirectory";
      }
    }

    private void btnUp_Click(object sender, EventArgs e)
    {
      this.lvwHosts.BeginUpdate();
      for (int index1 = 0; index1 < this.lvwHosts.SelectedItems.Count; ++index1)
      {
        ListViewItem selectedItem = this.lvwHosts.SelectedItems[index1];
        int index2 = selectedItem.Index;
        if (index2 != 0)
        {
          this.lvwHosts.Items.RemoveAt(index2);
          this.lvwHosts.Items.Insert(index2 - 1, selectedItem);
          selectedItem.Selected = true;
        }
        else
          break;
      }
      this.lvwHosts.EndUpdate();
    }

    private void btnDown_Click(object sender, EventArgs e)
    {
      this.lvwHosts.BeginUpdate();
      for (int index1 = this.lvwHosts.SelectedItems.Count - 1; index1 >= 0; --index1)
      {
        ListViewItem selectedItem = this.lvwHosts.SelectedItems[index1];
        int index2 = selectedItem.Index;
        if (index2 != this.lvwHosts.Items.Count - 1)
        {
          this.lvwHosts.Items.RemoveAt(index2);
          this.lvwHosts.Items.Insert(index2 + 1, selectedItem);
          selectedItem.Selected = true;
        }
        else
          break;
      }
      this.lvwHosts.EndUpdate();
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      foreach (ListViewItem listViewItem in this.lvwHosts.Items)
      {
        if (listViewItem.Text == this.txtName.Text)
        {
          int num = (int) MessageBox.Show((IWin32Window) this, "The name of a scripting host has to be unique", "Not saved", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          return;
        }
      }
      ScriptingHostConfiguration host = new ScriptingHostConfiguration();
      if (!this.UpdateHost(host))
        return;
      ListViewItem listViewItem1 = this.CreateListViewItem();
      this.HostToItem(host, listViewItem1);
      this.lvwHosts.Items.Add(listViewItem1);
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
      if (this.lvwHosts.SelectedItems.Count == 0)
        return;
      ListViewItem selectedItem = this.lvwHosts.SelectedItems[0];
      foreach (ListViewItem listViewItem in this.lvwHosts.Items)
      {
        if (listViewItem.Text == this.txtName.Text && selectedItem != listViewItem)
        {
          int num = (int) MessageBox.Show((IWin32Window) this, "The name of a scripting host has to be unique", "Not saved", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          return;
        }
      }
      ScriptingHostConfiguration tag = (ScriptingHostConfiguration) selectedItem.Tag;
      if (!this.UpdateHost(tag))
        return;
      this.HostToItem(tag, selectedItem);
    }

    private void btnLocation_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "Applications (*.exe)|*.exe|All Files (*.*)|*.*";
      openFileDialog.CheckFileExists = true;
      openFileDialog.Multiselect = false;
      openFileDialog.Title = "Select a new command file to be executed";
      if (openFileDialog.ShowDialog((IWin32Window) WindowsApplication.Instance.MainForm) != DialogResult.OK)
        return;
      this.txtLocation.Text = openFileDialog.FileName;
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      if (this.lvwHosts.SelectedItems.Count == 0)
        return;
      this.lvwHosts.SelectedItems[0].Remove();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.lvwHosts = new ListView();
      this.groupBox1 = new GroupBox();
      this.label4 = new Label();
      this.txtArguments = new TextBox();
      this.btnAdd = new Button();
      this.btnDelete = new Button();
      this.btnUpdate = new Button();
      this.cboExecuteAt = new ComboBox();
      this.label3 = new Label();
      this.btnLocation = new Button();
      this.label2 = new Label();
      this.txtLocation = new TextBox();
      this.label1 = new Label();
      this.txtName = new TextBox();
      this.btnCancel = new Button();
      this.btnOk = new Button();
      this.btnDown = new LinkLabel();
      this.btnUp = new LinkLabel();
      this.tipArguments = new ToolTip(this.components);
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      this.lvwHosts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.lvwHosts.FullRowSelect = true;
      this.lvwHosts.GridLines = true;
      this.lvwHosts.HideSelection = false;
      this.lvwHosts.Location = new Point(12, 12);
      this.lvwHosts.MultiSelect = false;
      this.lvwHosts.Name = "lvwHosts";
      this.lvwHosts.Size = new Size(541, 220);
      this.lvwHosts.TabIndex = 0;
      this.lvwHosts.UseCompatibleStateImageBehavior = false;
      this.lvwHosts.View = View.Details;
      this.groupBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.groupBox1.Controls.Add((Control) this.label4);
      this.groupBox1.Controls.Add((Control) this.txtArguments);
      this.groupBox1.Controls.Add((Control) this.btnAdd);
      this.groupBox1.Controls.Add((Control) this.btnDelete);
      this.groupBox1.Controls.Add((Control) this.btnUpdate);
      this.groupBox1.Controls.Add((Control) this.cboExecuteAt);
      this.groupBox1.Controls.Add((Control) this.label3);
      this.groupBox1.Controls.Add((Control) this.btnLocation);
      this.groupBox1.Controls.Add((Control) this.label2);
      this.groupBox1.Controls.Add((Control) this.txtLocation);
      this.groupBox1.Controls.Add((Control) this.label1);
      this.groupBox1.Controls.Add((Control) this.txtName);
      this.groupBox1.Location = new Point(12, 251);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Padding = new Padding(5, 5, 8, 5);
      this.groupBox1.Size = new Size(541, 124);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Configuration";
      this.label4.AutoSize = true;
      this.label4.Location = new Point(8, 74);
      this.label4.Name = "label4";
      this.label4.Size = new Size(57, 13);
      this.label4.TabIndex = 11;
      this.label4.Text = "Arguments";
      this.txtArguments.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtArguments.Location = new Point(71, 71);
      this.txtArguments.Name = "txtArguments";
      this.txtArguments.Size = new Size(150, 20);
      this.txtArguments.TabIndex = 10;
      this.tipArguments.SetToolTip((Control) this.txtArguments, "Specify the startup arguments for teh scripting host.\r\n\r\nSpecify [f] for the position of the script file.");
      this.btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btnAdd.Location = new Point(293, 92);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new Size(75, 23);
      this.btnAdd.TabIndex = 9;
      this.btnAdd.Text = "&Add";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btnDelete.Location = new Point(455, 92);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new Size(75, 23);
      this.btnDelete.TabIndex = 8;
      this.btnDelete.Text = "&Delete";
      this.btnDelete.UseVisualStyleBackColor = true;
      this.btnUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btnUpdate.Location = new Point(374, 92);
      this.btnUpdate.Name = "btnUpdate";
      this.btnUpdate.Size = new Size(75, 23);
      this.btnUpdate.TabIndex = 7;
      this.btnUpdate.Text = "&Update";
      this.btnUpdate.UseVisualStyleBackColor = true;
      this.cboExecuteAt.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.cboExecuteAt.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboExecuteAt.FormattingEnabled = true;
      this.cboExecuteAt.Location = new Point(380, 20);
      this.cboExecuteAt.Name = "cboExecuteAt";
      this.cboExecuteAt.Size = new Size(150, 21);
      this.cboExecuteAt.TabIndex = 6;
      this.label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.label3.AutoSize = true;
      this.label3.Location = new Point(315, 24);
      this.label3.Name = "label3";
      this.label3.Size = new Size(59, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "Execute At";
      this.btnLocation.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btnLocation.Location = new Point(495, 44);
      this.btnLocation.Name = "btnLocation";
      this.btnLocation.Size = new Size(35, 20);
      this.btnLocation.TabIndex = 4;
      this.btnLocation.Text = "...";
      this.btnLocation.UseVisualStyleBackColor = true;
      this.label2.AutoSize = true;
      this.label2.Location = new Point(8, 50);
      this.label2.Name = "label2";
      this.label2.Size = new Size(48, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Location";
      this.txtLocation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtLocation.Location = new Point(71, 45);
      this.txtLocation.Name = "txtLocation";
      this.txtLocation.ReadOnly = true;
      this.txtLocation.Size = new Size(418, 20);
      this.txtLocation.TabIndex = 2;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(8, 24);
      this.label1.Name = "label1";
      this.label1.Size = new Size(35, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Name";
      this.txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtName.Location = new Point(71, 21);
      this.txtName.Name = "txtName";
      this.txtName.Size = new Size(150, 20);
      this.txtName.TabIndex = 0;
      this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.Location = new Point(478, 381);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnOk.DialogResult = DialogResult.OK;
      this.btnOk.Location = new Point(397, 381);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new Size(75, 23);
      this.btnOk.TabIndex = 3;
      this.btnOk.Text = "&Ok";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnDown.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnDown.AutoSize = true;
      this.btnDown.Location = new Point(518, 235);
      this.btnDown.Name = "btnDown";
      this.btnDown.Size = new Size(35, 13);
      this.btnDown.TabIndex = 4;
      this.btnDown.TabStop = true;
      this.btnDown.Text = "Down";
      this.btnUp.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnUp.AutoSize = true;
      this.btnUp.Location = new Point(491, 235);
      this.btnUp.Name = "btnUp";
      this.btnUp.Size = new Size(21, 13);
      this.btnUp.TabIndex = 5;
      this.btnUp.TabStop = true;
      this.btnUp.Text = "Up";
      this.AcceptButton = (IButtonControl) this.btnOk;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnCancel;
      this.ClientSize = new Size(565, 414);
      this.Controls.Add((Control) this.btnUp);
      this.Controls.Add((Control) this.btnDown);
      this.Controls.Add((Control) this.btnOk);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.lvwHosts);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new Size(504, 295);
      this.Name = "FormScriptingHostConfiguration";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Scripting Host Configuraton";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
