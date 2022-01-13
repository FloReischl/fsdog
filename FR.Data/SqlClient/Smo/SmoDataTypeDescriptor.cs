// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoDataTypeDescriptor
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System;
using System.ComponentModel;

namespace FR.Data.SqlClient.Smo
{
  public class SmoDataTypeDescriptor : ICustomTypeDescriptor
  {
    private SmoDataType _dataType;

    public SmoDataTypeDescriptor(SmoDataType dataType) => this._dataType = dataType;

    AttributeCollection ICustomTypeDescriptor.GetAttributes() => TypeDescriptor.GetAttributes((object) this._dataType);

    string ICustomTypeDescriptor.GetClassName() => TypeDescriptor.GetClassName((object) this._dataType);

    string ICustomTypeDescriptor.GetComponentName() => TypeDescriptor.GetComponentName((object) this._dataType);

    TypeConverter ICustomTypeDescriptor.GetConverter() => (TypeConverter) new SmoDataTypeConverter(this._dataType);

    EventDescriptor ICustomTypeDescriptor.GetDefaultEvent() => TypeDescriptor.GetDefaultEvent((object) this._dataType);

    PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty() => TypeDescriptor.GetDefaultProperty((object) this._dataType);

    object ICustomTypeDescriptor.GetEditor(Type editorBaseType) => TypeDescriptor.GetEditor((object) this._dataType, editorBaseType);

    EventDescriptorCollection ICustomTypeDescriptor.GetEvents(
      Attribute[] attributes)
    {
      return TypeDescriptor.GetEvents((object) this._dataType, attributes);
    }

    EventDescriptorCollection ICustomTypeDescriptor.GetEvents() => TypeDescriptor.GetEvents((object) this._dataType);

    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(
      Attribute[] attributes)
    {
      return TypeDescriptor.GetProperties((object) this._dataType, attributes);
    }

    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties() => TypeDescriptor.GetProperties((object) this._dataType);

    object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd) => (object) this._dataType;
  }
}
