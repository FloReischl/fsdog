// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoDataTypeConverter
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System;
using System.ComponentModel;
using System.Globalization;

namespace FR.Data.SqlClient.Smo
{
  public class SmoDataTypeConverter : TypeConverter
  {
    private SmoDataType _dataType;

    public SmoDataTypeConverter(SmoDataType dataType) => this._dataType = dataType;

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => base.CanConvertFrom(context, sourceType);

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => base.CanConvertTo(context, destinationType);

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      return base.ConvertFrom(context, culture, value);
    }

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      return base.ConvertTo(context, culture, value, destinationType);
    }

    public override bool IsValid(ITypeDescriptorContext context, object value) => base.IsValid(context, value);
  }
}
