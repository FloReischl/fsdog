// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.SqlConnectionDialog
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace FR.Windows.Forms
{
  public class SqlConnectionDialog : Form
  {
    private IContainer components;
    private Label label1;
    private GroupBox groupBox1;
    private TextBox txtServer;
    private ComboBox cboDatabase;
    private Label label5;
    private ComboBox cboMode;
    private TextBox txtPassword;
    private Label label3;
    private TextBox txtUser;
    private Label label2;
    private Label label4;
    private Button btnCancel;
    private Button btnOk;
    private Button btnTest;
    private SqlConnectionStringBuilder _connectionBuilder;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.label1 = new Label();
      this.groupBox1 = new GroupBox();
      this.btnTest = new Button();
      this.cboDatabase = new ComboBox();
      this.label5 = new Label();
      this.cboMode = new ComboBox();
      this.txtPassword = new TextBox();
      this.label3 = new Label();
      this.txtUser = new TextBox();
      this.label2 = new Label();
      this.txtServer = new TextBox();
      this.label4 = new Label();
      this.btnCancel = new Button();
      this.btnOk = new Button();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(6, 22);
      this.label1.Name = "label1";
      this.label1.Size = new Size(38, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "&Server";
      this.groupBox1.Controls.Add((Control) this.btnTest);
      this.groupBox1.Controls.Add((Control) this.cboDatabase);
      this.groupBox1.Controls.Add((Control) this.label5);
      this.groupBox1.Controls.Add((Control) this.cboMode);
      this.groupBox1.Controls.Add((Control) this.txtPassword);
      this.groupBox1.Controls.Add((Control) this.label3);
      this.groupBox1.Controls.Add((Control) this.txtUser);
      this.groupBox1.Controls.Add((Control) this.label2);
      this.groupBox1.Controls.Add((Control) this.txtServer);
      this.groupBox1.Controls.Add((Control) this.label4);
      this.groupBox1.Controls.Add((Control) this.label1);
      this.groupBox1.Location = new Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(243, 183);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Connection Information";
      this.btnTest.Location = new Point(162, 151);
      this.btnTest.Name = "btnTest";
      this.btnTest.Size = new Size(75, 23);
      this.btnTest.TabIndex = 10;
      this.btnTest.Text = "&Test";
      this.btnTest.UseVisualStyleBackColor = true;
      this.btnTest.Click += new EventHandler(this.btnTest_Click);
      this.cboDatabase.FormattingEnabled = true;
      this.cboDatabase.Location = new Point(71, 124);
      this.cboDatabase.Name = "cboDatabase";
      this.cboDatabase.Size = new Size(166, 21);
      this.cboDatabase.Sorted = true;
      this.cboDatabase.TabIndex = 9;
      this.cboDatabase.DropDown += new EventHandler(this.cboDatabase_DropDown);
      this.label5.AutoSize = true;
      this.label5.Location = new Point(6, (int) sbyte.MaxValue);
      this.label5.Name = "label5";
      this.label5.Size = new Size(53, 13);
      this.label5.TabIndex = 8;
      this.label5.Text = "&Database";
      this.cboMode.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboMode.FormattingEnabled = true;
      this.cboMode.Location = new Point(71, 45);
      this.cboMode.Name = "cboMode";
      this.cboMode.Size = new Size(166, 21);
      this.cboMode.TabIndex = 3;
      this.cboMode.SelectedIndexChanged += new EventHandler(this.cboMode_SelectedIndexChanged);
      this.txtPassword.Location = new Point(71, 98);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.Size = new Size(166, 20);
      this.txtPassword.TabIndex = 7;
      this.txtPassword.UseSystemPasswordChar = true;
      this.label3.AutoSize = true;
      this.label3.Location = new Point(6, 101);
      this.label3.Name = "label3";
      this.label3.Size = new Size(53, 13);
      this.label3.TabIndex = 6;
      this.label3.Text = "&Password";
      this.txtUser.Location = new Point(71, 72);
      this.txtUser.Name = "txtUser";
      this.txtUser.Size = new Size(166, 20);
      this.txtUser.TabIndex = 5;
      this.label2.AutoSize = true;
      this.label2.Location = new Point(6, 75);
      this.label2.Name = "label2";
      this.label2.Size = new Size(29, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "&User";
      this.txtServer.Location = new Point(71, 19);
      this.txtServer.Name = "txtServer";
      this.txtServer.Size = new Size(166, 20);
      this.txtServer.TabIndex = 1;
      this.txtServer.TextChanged += new EventHandler(this.txtServer_TextChanged);
      this.label4.AutoSize = true;
      this.label4.Location = new Point(6, 48);
      this.label4.Name = "label4";
      this.label4.Size = new Size(34, 13);
      this.label4.TabIndex = 2;
      this.label4.Text = "&Mode";
      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.Location = new Point(180, 201);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.btnOk.Location = new Point(99, 201);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new Size(75, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "&Ok";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new EventHandler(this.btnOk_Click);
      this.AcceptButton = (IButtonControl) this.btnOk;
      this.CancelButton = (IButtonControl) this.btnCancel;
      this.ClientSize = new Size(267, 231);
      this.Controls.Add((Control) this.btnOk);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.groupBox1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "CSqlConnectionDialog";
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Connect SQL Server";
      this.Load += new EventHandler(this.CFormSqlConnect_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
    }

    public SqlConnectionStringBuilder ConnectionStringBuilder
    {
      get => this._connectionBuilder;
      set => this._connectionBuilder = value;
    }

    public SqlConnectionDialog() => this.InitializeComponent();

    private void CFormSqlConnect_Load(object sender, EventArgs e)
    {
      this.cboMode.Items.Add((object) new SqlConnectionDialog.ModeItem(SqlConnectionDialog.SqlConnectionMode.Sql));
      this.cboMode.Items.Add((object) new SqlConnectionDialog.ModeItem(SqlConnectionDialog.SqlConnectionMode.Windows));
      this.cboMode.SelectedItem = (object) SqlConnectionDialog.SqlConnectionMode.Sql;
    }

    private void txtServer_TextChanged(object sender, EventArgs e)
    {
      string text = this.cboDatabase.Text;
      this.cboDatabase.Items.Clear();
      this.cboDatabase.Text = text;
    }

    private void cboMode_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.cboMode.SelectedIndex == -1)
        return;
      bool flag = false;
      if (this.cboMode.SelectedItem.Equals((object) SqlConnectionDialog.SqlConnectionMode.Sql))
        flag = true;
      this.txtUser.Enabled = flag;
      this.txtPassword.Enabled = flag;
    }

    private void cboDatabase_DropDown(object sender, EventArgs e)
    {
      SqlConnectionStringBuilder connectionBuilder = this.GetConnectionBuilder();
      try
      {
        SqlConnection sqlConnection = new SqlConnection(connectionBuilder.ConnectionString);
        sqlConnection.Open();
        foreach (DataRow row in (InternalDataCollectionBase) sqlConnection.GetSchema("DATABASES").Rows)
          this.cboDatabase.Items.Add(row["DATABASE_NAME"]);
      }
      catch
      {
      }
    }

    private void btnTest_Click(object sender, EventArgs e)
    {
      this.GetConnectionBuilder();
      try
      {
        SqlConnection sqlConnection = new SqlConnection(this.ConnectionStringBuilder.ConnectionString);
        sqlConnection.Open();
        sqlConnection.Close();
        sqlConnection.Dispose();
        int num = (int) MessageBox.Show((IWin32Window) this, "Connection test succeeded", "Connection", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show((IWin32Window) this, string.Format("Connection test failed:\r\n\r\n{0}", (object) ex.Message), "Connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.ConnectionStringBuilder = (SqlConnectionStringBuilder) null;
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      this.GetConnectionBuilder();
      try
      {
        SqlConnection sqlConnection = new SqlConnection(this.ConnectionStringBuilder.ConnectionString);
        sqlConnection.Open();
        sqlConnection.Close();
        sqlConnection.Dispose();
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
      catch
      {
        int num = (int) MessageBox.Show((IWin32Window) this, "Connection failed", "Connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private SqlConnectionStringBuilder GetConnectionBuilder()
    {
      this.ConnectionStringBuilder = new SqlConnectionStringBuilder();
      this.ConnectionStringBuilder.DataSource = this.txtServer.Text;
      this.ConnectionStringBuilder.InitialCatalog = this.cboDatabase.Text;
      if (this.cboMode.SelectedItem.Equals((object) SqlConnectionDialog.SqlConnectionMode.Windows))
      {
        this.ConnectionStringBuilder.IntegratedSecurity = true;
      }
      else
      {
        this.ConnectionStringBuilder.IntegratedSecurity = false;
        this.ConnectionStringBuilder.UserID = this.txtUser.Text;
        this.ConnectionStringBuilder.Password = this.txtPassword.Text;
      }
      return this.ConnectionStringBuilder;
    }

    private enum SqlConnectionMode
    {
      Sql,
      Windows,
    }

    private class ModeItem
    {
      private SqlConnectionDialog.SqlConnectionMode _mode;

      public ModeItem(SqlConnectionDialog.SqlConnectionMode mode) => this._mode = mode;

      public override int GetHashCode() => this._mode.GetHashCode();

      public override bool Equals(object obj)
      {
        switch (obj)
        {
          case SqlConnectionDialog.ModeItem _:
            return ((SqlConnectionDialog.ModeItem) obj).Equals((object) this);
          case SqlConnectionDialog.SqlConnectionMode sqlConnectionMode:
            return sqlConnectionMode.Equals((object) this._mode);
          default:
            return false;
        }
      }

      public override string ToString()
      {
        if (this._mode == SqlConnectionDialog.SqlConnectionMode.Sql)
          return "SQL Server Authentication";
        return this._mode == SqlConnectionDialog.SqlConnectionMode.Windows ? "Windows Authentication" : "Unknown";
      }
    }
  }
}
