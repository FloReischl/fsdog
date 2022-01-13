// Decompiled with JetBrains decompiler
// Type: FR.ComponentModel.ExpandablePropertyDescriptor
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System.ComponentModel;
using System.Reflection;

namespace FR.ComponentModel
{
  public class ExpandablePropertyDescriptor : ReflectionPropertyDescriptor
  {
    private TypeConverter _converter;

    public override TypeConverter Converter
    {
      get
      {
        if (this._converter == null)
          this._converter = base.Converter.GetType() == typeof (TypeConverter) ? (TypeConverter) new ExpandableObjectConverter() : base.Converter;
        return this._converter;
      }
    }

    public ExpandablePropertyDescriptor(PropertyInfo propertyInfo)
      : base(propertyInfo)
    {
    }
  }
}
