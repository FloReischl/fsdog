// Decompiled with JetBrains decompiler
// Type: FR.ComponentModel.DataStoreSourceObjectProperty
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.ComponentModel;

namespace FR.ComponentModel
{
  internal sealed class DataStoreSourceObjectProperty : PropertyDescriptor
  {
    private PropertyDescriptor _baseProperty;

    internal DataStoreSourceObjectProperty(PropertyDescriptor baseProperty)
      : base(baseProperty.Name, (Attribute[]) null)
    {
      this._baseProperty = baseProperty;
    }

    public override Type ComponentType => typeof (DataStoreSourceObject);

    public override bool IsReadOnly => this._baseProperty.IsReadOnly;

    public override Type PropertyType => this._baseProperty.PropertyType;

    public override bool CanResetValue(object component) => this._baseProperty.CanResetValue(((DataStoreSourceObject) component).BaseObject);

    public override object GetValue(object component) => this._baseProperty.GetValue(((DataStoreSourceObject) component).BaseObject);

    public override void ResetValue(object component) => this._baseProperty.ResetValue(((DataStoreSourceObject) component).BaseObject);

    public override void SetValue(object component, object value) => this._baseProperty.SetValue(((DataStoreSourceObject) component).BaseObject, value);

    public override bool ShouldSerializeValue(object component) => this._baseProperty.ShouldSerializeValue(((DataStoreSourceObject) component).BaseObject);
  }
}
