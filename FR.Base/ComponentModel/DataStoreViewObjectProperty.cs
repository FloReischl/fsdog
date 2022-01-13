// Decompiled with JetBrains decompiler
// Type: FR.ComponentModel.DataStoreViewObjectProperty
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.ComponentModel;

namespace FR.ComponentModel
{
  internal sealed class DataStoreViewObjectProperty : PropertyDescriptor
  {
    private PropertyDescriptor _baseProperty;

    internal DataStoreViewObjectProperty(PropertyDescriptor baseProperty)
      : base(baseProperty.Name, (Attribute[]) null)
    {
      this._baseProperty = baseProperty;
    }

    public override AttributeCollection Attributes => this._baseProperty.Attributes;

    public override Type ComponentType => typeof (DataStoreViewObject);

    public override bool IsReadOnly => this._baseProperty.IsReadOnly;

    public override Type PropertyType => this._baseProperty.PropertyType;

    public override bool CanResetValue(object component) => this._baseProperty.CanResetValue(((DataStoreViewObject) component).DataObject.BaseObject);

    public override object GetValue(object component) => this._baseProperty.GetValue(((DataStoreViewObject) component).DataObject.BaseObject);

    public override void ResetValue(object component)
    {
      DataStoreViewObject dataStoreViewObject = (DataStoreViewObject) component;
      dataStoreViewObject.OnPropertyChanging(this.Name);
      dataStoreViewObject.View.OnObjectChanging(dataStoreViewObject);
      this._baseProperty.ResetValue(dataStoreViewObject.DataObject.BaseObject);
      dataStoreViewObject.OnPropertyChanged(this.Name);
      dataStoreViewObject.View.OnObjectChanged(dataStoreViewObject);
    }

    public override void SetValue(object component, object value)
    {
      DataStoreViewObject dataStoreViewObject = (DataStoreViewObject) component;
      dataStoreViewObject.OnPropertyChanging(this.Name);
      dataStoreViewObject.View.OnObjectChanging(dataStoreViewObject);
      this._baseProperty.SetValue(dataStoreViewObject.DataObject.BaseObject, value);
      dataStoreViewObject.OnPropertyChanged(this.Name);
      dataStoreViewObject.View.OnObjectChanged(dataStoreViewObject);
    }

    public override bool ShouldSerializeValue(object component) => this._baseProperty.ShouldSerializeValue(((DataStoreViewObject) component).DataObject.BaseObject);
  }
}
