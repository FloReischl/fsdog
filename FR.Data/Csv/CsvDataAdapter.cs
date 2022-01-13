// Decompiled with JetBrains decompiler
// Type: FR.Data.Csv.CsvDataAdapter
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;

namespace FR.Data.Csv
{
  public class CsvDataAdapter : IDataAdapter
  {
    private const string SINGLEQUOTE = "\"";
    private const string DOUBLEQUOTE = "\"\"";
    private const string CRLF = "\r\n";
    private string m_sFilename;
    private Stream m_OuputStream;
    private string m_sTablename;
    private bool m_bHasHeader;
    private string[] m_saHeaders;
    private char m_cSeperator = ',';
    private bool m_bTrim;
    private int m_i4MaxCols;
    private string m_sCustomHeader = "";
    private string m_sCustomDateFormat = "";
    private string m_sCustomDoubleFormat = "#0.#";
    private Encoding m_FileEncoding = Encoding.UTF8;
    private CultureInfo m_CultureInfo;

    public CsvDataAdapter(string filename, bool hasHeader, char seperator, bool trim)
    {
      this.m_sFilename = filename;
      FileInfo fileInfo = new FileInfo(this.m_sFilename);
      this.m_sTablename = fileInfo.Name.Replace("." + fileInfo.Extension, "");
      this.m_bHasHeader = hasHeader;
      this.m_cSeperator = seperator;
      this.m_bTrim = trim;
      this.m_CultureInfo = new CultureInfo("en-US");
    }

    public CsvDataAdapter(string sFilename, bool hasHeader)
      : this(sFilename, hasHeader, ',', false)
    {
    }

    public CsvDataAdapter(string sFilename)
      : this(sFilename, false)
    {
    }

    public CsvDataAdapter(Stream OutputStream, bool hasHeader, char seperator, bool trim)
    {
      this.m_OuputStream = OutputStream;
      this.Trim = trim;
      this.Seperator = seperator;
      this.HasHeaderRow = hasHeader;
    }

    public CsvDataAdapter(Stream OutputStream, bool hasHeader)
      : this(OutputStream, hasHeader, ',', false)
    {
    }

    public CsvDataAdapter(Stream OutputStream)
      : this(OutputStream, true)
    {
    }

    public string FileName
    {
      get => this.m_sFilename;
      set => this.m_sFilename = value;
    }

    public char Seperator
    {
      get => this.m_cSeperator;
      set => this.m_cSeperator = value;
    }

    public bool Trim
    {
      get => this.m_bTrim;
      set => this.m_bTrim = value;
    }

    public int MaxColumns
    {
      get => this.m_i4MaxCols;
      set => this.m_i4MaxCols = value;
    }

    public bool HasHeaderRow
    {
      get => this.m_bHasHeader;
      set => this.m_bHasHeader = value;
    }

    public string CustomHeader
    {
      get => this.m_sCustomHeader;
      set => this.m_sCustomHeader = value;
    }

    public string CustomDateFormat
    {
      get
      {
        if (this.m_sCustomDateFormat == "")
          this.CustomDateFormat = string.Format("{0} {1}", (object) this.CultureInfo.DateTimeFormat.ShortDatePattern, (object) this.CultureInfo.DateTimeFormat.ShortTimePattern);
        return this.m_sCustomDateFormat;
      }
      set => this.m_sCustomDateFormat = value;
    }

    public string CustomDoubleFormat
    {
      get => this.m_sCustomDoubleFormat;
      set => this.m_sCustomDoubleFormat = value;
    }

    public Encoding TextEncoding
    {
      get => this.m_FileEncoding;
      set => this.m_FileEncoding = value;
    }

    public string StreamString
    {
      get
      {
        this.m_OuputStream.Position = 0L;
        byte[] numArray = new byte[this.m_OuputStream.Length];
        this.m_OuputStream.Read(numArray, 0, numArray.Length);
        return this.TextEncoding.GetString(numArray);
      }
    }

    public string CultureInfoName
    {
      get => this.CultureInfo.Name;
      set => this.CultureInfo = new CultureInfo(value);
    }

    public CultureInfo CultureInfo
    {
      get => this.m_CultureInfo;
      set => this.m_CultureInfo = value;
    }

    public int Fill(DataSet dataSet) => this.Fill(dataSet, this.m_sTablename);

    public int Fill(DataSet dataSet, string tableName)
    {
      DataTable dataTable = dataSet.Tables.Add(tableName);
      int num = 0;
      StreamReader streamReader = new StreamReader(this.m_sFilename);
      string[] values;
      try
      {
        if (this.m_bHasHeader)
        {
          this.m_saHeaders = this.SplitRow(streamReader.ReadLine());
          foreach (string saHeader in this.m_saHeaders)
          {
            DataColumn dataColumn = dataTable.Columns.Add(saHeader);
            dataColumn.DataType = typeof (string);
            dataColumn.AllowDBNull = true;
          }
          values = this.SplitRow(streamReader.ReadLine());
        }
        else
        {
          values = this.SplitRow(streamReader.ReadLine());
          for (int index = 0; index < values.Length; ++index)
          {
            DataColumn dataColumn = dataTable.Columns.Add("Column " + index.ToString());
            dataColumn.DataType = typeof (string);
            dataColumn.AllowDBNull = true;
          }
        }
      }
      catch (Exception ex)
      {
        streamReader.Close();
        throw new Exception("Error while reading column information.", ex);
      }
      try
      {
        while (streamReader.Peek() > 0)
        {
          ++num;
          if (this.m_bTrim)
            dataTable.Rows.Add((object[]) this.TrimRow(values));
          else
            dataTable.Rows.Add((object[]) values);
          values = this.SplitRow(streamReader.ReadLine());
        }
        ++num;
        if (this.Trim)
          dataTable.Rows.Add((object[]) this.TrimRow(values));
        else
          dataTable.Rows.Add((object[]) values);
      }
      catch (Exception ex)
      {
        throw new Exception(string.Format("Error while reading row {0}", (object) num), ex);
      }
      finally
      {
        streamReader.Close();
      }
      dataSet.AcceptChanges();
      return num;
    }

    ITableMappingCollection IDataAdapter.TableMappings => (ITableMappingCollection) null;

    MissingSchemaAction IDataAdapter.MissingSchemaAction
    {
      get => MissingSchemaAction.Add;
      set
      {
      }
    }

    MissingMappingAction IDataAdapter.MissingMappingAction
    {
      get => MissingMappingAction.Passthrough;
      set
      {
      }
    }

    IDataParameter[] IDataAdapter.GetFillParameters() => (IDataParameter[]) null;

    DataTable[] IDataAdapter.FillSchema(DataSet dataSet, SchemaType schemaType) => (DataTable[]) null;

    public int Update(DataSet dataSet) => this.Update(dataSet, this.m_sTablename);

    public int Update(DataTable table)
    {
      if (table == null)
        return 0;
      byte[] bytes1 = this.TextEncoding.GetBytes("\r\n");
      int num = 0;
      Stream stream = this.m_OuputStream != null ? this.m_OuputStream : (Stream) File.OpenWrite(this.m_sFilename);
      if (this.CustomHeader != "")
      {
        byte[] bytes2 = this.TextEncoding.GetBytes(this.CustomHeader);
        stream.Write(bytes2, 0, bytes2.Length);
        stream.Write(bytes1, 0, bytes1.Length);
      }
      if (this.m_bHasHeader)
      {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (DataColumn column in (InternalDataCollectionBase) table.Columns)
        {
          stringBuilder.Append(this.QuoteString((object) column.Caption));
          stringBuilder.Append(this.m_cSeperator);
        }
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        byte[] bytes3 = this.TextEncoding.GetBytes(stringBuilder.ToString());
        stream.Write(bytes3, 0, bytes3.Length);
        stream.Write(bytes1, 0, bytes1.Length);
      }
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
      {
        if (row.RowState != DataRowState.Deleted)
        {
          byte[] bytes4 = this.TextEncoding.GetBytes(this.BuildRow(row.ItemArray).ToString());
          stream.Write(bytes4, 0, bytes4.Length);
          stream.Write(bytes1, 0, bytes1.Length);
          ++num;
        }
      }
      if (this.m_OuputStream == null)
        stream.Close();
      return num;
    }

    public int Update(DataSet dataSet, string srcTable)
    {
      if (!dataSet.HasChanges())
        return 0;
      DataTable table;
      try
      {
        table = dataSet.Tables[srcTable];
      }
      catch
      {
        throw new ArgumentException("srcTable does not exist in specified dataSet");
      }
      int num = this.Update(table);
      dataSet.AcceptChanges();
      return num;
    }

    private string QuoteString(object inString)
    {
      string str = (string) inString;
      if (str.IndexOf(' ') > -1 || str.IndexOf(this.m_cSeperator) > -1 || str.IndexOf("\"") > -1)
        str = "\"" + str.Replace("\"", "\"\"") + "\"";
      return str;
    }

    private StringBuilder BuildRow(object[] values)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (object obj in values)
      {
        if (obj != null)
        {
          string inString = this.ValueToString(obj);
          stringBuilder.Append(this.QuoteString((object) inString));
        }
        stringBuilder.Append(this.m_cSeperator);
      }
      stringBuilder.Remove(stringBuilder.Length - 1, 1);
      return stringBuilder;
    }

    private string ValueToString(object obj)
    {
      string str;
      switch (obj)
      {
        case DateTime dt:
          str = this.DateToString(dt);
          break;
        case double num1:
          str = num1.ToString(this.CustomDoubleFormat, (IFormatProvider) this.CultureInfo.NumberFormat);
          break;
        case float num2:
          str = num2.ToString(this.CustomDoubleFormat, (IFormatProvider) this.CultureInfo.NumberFormat);
          break;
        default:
          str = obj.ToString();
          break;
      }
      return this.Trim ? str.Trim() : str;
    }

    private string DateToString(DateTime dt)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.CustomDateFormat);
      stringBuilder.Replace("yyyy", dt.ToString("yyyy ").TrimEnd(' '));
      stringBuilder.Replace("yy", dt.ToString("yy ").TrimEnd(' '));
      stringBuilder.Replace("MM", dt.ToString("MM ").TrimEnd(' '));
      stringBuilder.Replace("M", dt.ToString("M ").TrimEnd(' '));
      stringBuilder.Replace("dd", dt.ToString("dd ").TrimEnd(' '));
      stringBuilder.Replace("d", dt.ToString("d ").TrimEnd(' '));
      stringBuilder.Replace("hh", dt.ToString("hh ").TrimEnd(' '));
      stringBuilder.Replace("h", dt.ToString("h ").TrimEnd(' '));
      stringBuilder.Replace("HH", dt.ToString("HH ").TrimEnd(' '));
      stringBuilder.Replace("H", dt.ToString("H ").TrimEnd(' '));
      stringBuilder.Replace("mm", dt.ToString("mm ").TrimEnd(' '));
      stringBuilder.Replace("m", dt.ToString("m ").TrimEnd(' '));
      stringBuilder.Replace("ss", dt.ToString("ss ").TrimEnd(' '));
      stringBuilder.Replace("s", dt.ToString("s ").TrimEnd(' '));
      stringBuilder.Replace("fff", dt.ToString("fff ").TrimEnd(' '));
      stringBuilder.Replace("ff", dt.ToString("ff ").TrimEnd(' '));
      stringBuilder.Replace("f", dt.ToString("f ").TrimEnd(' '));
      return stringBuilder.ToString();
    }

    private string[] TrimRow(string[] values)
    {
      string[] strArray = new string[values.Length];
      int num = 0;
      foreach (string str in values)
        strArray[num++] = str.Trim();
      return strArray;
    }

    private string[] SplitRow(string row)
    {
      string[] strArray = row.Split(this.m_cSeperator);
      ArrayList arrayList = new ArrayList();
      bool flag = false;
      string str = "";
      for (int index = 0; index < strArray.Length && (this.m_i4MaxCols == 0 || this.m_i4MaxCols > index); ++index)
      {
        if (strArray[index].StartsWith("\""))
        {
          if (strArray[index].EndsWith("\""))
          {
            arrayList.Add((object) strArray[index].Trim('"'));
          }
          else
          {
            str = strArray[index].TrimStart('"');
            flag = true;
          }
        }
        else if (flag)
        {
          if (strArray[index].EndsWith("\""))
          {
            arrayList.Add((object) (str + "," + strArray[index].TrimEnd('"')));
            flag = false;
          }
          else
            str = str + "," + strArray[index];
        }
        else
          arrayList.Add((object) strArray[index]);
      }
      return (string[]) arrayList.ToArray(typeof (string));
    }
  }
}
