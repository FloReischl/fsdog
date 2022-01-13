// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoDatabaseObjectCollection`1
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System;
using System.Text.RegularExpressions;

namespace FR.Data.SqlClient.Smo
{
  public class SmoDatabaseObjectCollection<T> : SmoCollection<T> where T : class
  {
    internal SmoDatabaseObjectCollection(SmoDatabase parent)
      : base((SmoObject) parent)
    {
    }

    public override T FindByName(string name)
    {
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      if (new Regex("^(\\[?[a-zA-Z0-9\\ ]{0,256}\\]?\\.\\[?[a-zA-Z0-9\\ ]{0,256}\\]?\\.\\[?[a-zA-Z0-9\\ ]{0,256}\\]?)$").IsMatch(name))
      {
        name = name.Replace("[", "");
        name = name.Replace("]", "");
        string[] strArray = name.Split(new string[1]{ "." }, StringSplitOptions.None);
        str1 = strArray[0];
        str2 = strArray[1];
        str3 = strArray[2];
      }
      string str4;
      if (new Regex("^(\\[?[a-zA-Z0-9\\ ]{0,256}\\]?\\.\\[?[a-zA-Z0-9\\ ]{0,256}\\]?)$").IsMatch(name))
      {
        name = name.Replace("[", "");
        name = name.Replace("]", "");
        string[] strArray = name.Split(new string[1]{ "." }, StringSplitOptions.None);
        str2 = strArray[0];
        str4 = strArray[1];
      }
      else
      {
        name = name.Replace("[", "");
        name = name.Replace("]", "");
        str4 = name;
      }
      foreach (T byName in (SmoCollection<T>) this)
      {
        SmoDatabaseObject smoDatabaseObject = (SmoDatabaseObject) (object) byName;
        bool flag = true;
        if (str1 != null && str1.ToLower() != smoDatabaseObject.Database.Name.ToLower())
          flag = false;
        if (str2 != null && str2.ToLower() != smoDatabaseObject.Schema.Name.ToLower())
          flag = false;
        if (str4.ToLower() != smoDatabaseObject.Name.ToLower())
          flag = false;
        if (flag)
          return byName;
      }
      return (T) null;
    }
  }
}
