// Decompiled with JetBrains decompiler
// Type: FR.ApplicationInformation
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Diagnostics;
using System.Reflection;

namespace FR
{
  public class ApplicationInformation
  {
    private FileVersionInfo _fileVersionInfo;
    private string _description;
    private string _title;
    private Version _version;

    internal ApplicationInformation() => this.Assembly = ApplicationBase.Instance.GetType().Assembly;

    public Assembly Assembly { get; private set; }

    public string CompanyName => this.getFileVersionInfo().CompanyName;

    public string FileDescription => this.getFileVersionInfo().FileDescription;

    public string Description
    {
      get
      {
        if (this._description == null)
        {
          object[] customAttributes = this.Assembly.GetCustomAttributes(typeof (AssemblyDescriptionAttribute), false);
          if (customAttributes.Length != 0)
            this._description = ((AssemblyDescriptionAttribute) customAttributes[0]).Description;
        }
        return this._description;
      }
    }

    public string Language => this.getFileVersionInfo().Language;

    public string LegalCopyright => this.getFileVersionInfo().LegalCopyright;

    public string LegalTrademarks => this.getFileVersionInfo().LegalTrademarks;

    public string ProductName => this.getFileVersionInfo().ProductName;

    public string Title
    {
      get
      {
        if (this._title == null)
        {
          object[] customAttributes = this.Assembly.GetCustomAttributes(typeof (AssemblyTitleAttribute), false);
          this._title = customAttributes.Length == 0 ? string.Empty : ((AssemblyTitleAttribute) customAttributes[0]).Title;
        }
        return this._title;
      }
    }

    public Version Version
    {
      get
      {
        if (this._version == (Version) null)
          this._version = this.Assembly.GetName().Version;
        return this._version;
      }
    }

    private FileVersionInfo getFileVersionInfo()
    {
      if (this._fileVersionInfo == null)
        this._fileVersionInfo = FileVersionInfo.GetVersionInfo(this.Assembly.Location);
      return this._fileVersionInfo;
    }
  }
}
