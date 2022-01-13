// Decompiled with JetBrains decompiler
// Type: FR.ComponentModel.DataStoreViewObject
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.ComponentModel;
using System.Diagnostics;

namespace FR.ComponentModel
{
  public class DataStoreViewObject : 
    ICustomTypeDescriptor,
    IEditableObject,
    INotifyPropertyChanged,
    INotifyPropertyChanging,
    IUniqueItem
  {
    private ICustomTypeDescriptor _descriptor;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private DataStoreSourceObject _dataObject;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int _ordinalPosition;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private DataStoreView _view;

    internal DataStoreViewObject(DataStoreView view, DataStoreSourceObject dataObject)
    {
      this._view = view;
      this._dataObject = dataObject;
    }

    public event PropertyChangingEventHandler PropertyChanging;

    public event PropertyChangedEventHandler PropertyChanged;

    public object this[int index]
    {
      get => this.View.ItemProperties[index].GetValue(this.DataObject.BaseObject);
      set => this.View.ItemProperties[index].SetValue(this.DataObject.BaseObject, value);
    }

    public object this[string name]
    {
      get => this.View.ItemProperties[name].GetValue((object) this);
      set => this.View.ItemProperties[name].SetValue((object) this, value);
    }

    public DataStoreSourceObject DataObject
    {
      [DebuggerNonUserCode] get => this._dataObject;
    }

    public int OrdinalPosition
    {
      [DebuggerNonUserCode] get => this._ordinalPosition;
    }

    public int UniqueKey
    {
      [DebuggerNonUserCode] get => this.DataObject.UniqueKey;
    }

    public DataStoreView View
    {
      [DebuggerNonUserCode] get => this._view;
    }

    internal void OnPropertyChanging(string propertyName)
    {
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, new PropertyChangingEventArgs(propertyName));
    }

    internal void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    internal void SetOrdinalPosition(int position) => this._ordinalPosition = position;

    private AttributeCollection GetAttributes() => this.GetDescriptor().GetAttributes();

    private string GetClassName() => string.Format("{0}.{1}", (object) this.GetType().Namespace, (object) this.GetType().Name);

    private string GetComponentName() => this.GetDescriptor().GetComponentName();

    private TypeConverter GetConverter() => this.GetDescriptor().GetConverter();

    private EventDescriptor GetDefaultEvent() => this.GetDescriptor().GetDefaultEvent();

    private PropertyDescriptor GetDefaultProperty()
    {
      PropertyDescriptor defaultProperty = this.GetDescriptor().GetDefaultProperty();
      return this.GetProperties((Attribute[]) null)[defaultProperty.Name];
    }

    private ICustomTypeDescriptor GetDescriptor()
    {
      if (this._descriptor == null)
        this._descriptor = TypeDescriptor.GetProvider(this.DataObject.BaseObject).GetTypeDescriptor(this.DataObject.BaseObject);
      return this._descriptor;
    }

    private object GetEditor(Type editorBaseType) => this.GetDescriptor().GetEditor(editorBaseType);

    private EventDescriptorCollection GetEvents(Attribute[] attrs) => this.GetDescriptor().GetEvents(attrs);

    private PropertyDescriptorCollection GetProperties(
      Attribute[] attributes)
    {
      return this.View.ItemProperties;
    }

    private object GetPropertyOwner(PropertyDescriptor pd) => (object) this;

    AttributeCollection ICustomTypeDescriptor.GetAttributes() => this.GetAttributes();

    string ICustomTypeDescriptor.GetClassName() => this.GetClassName();

    string ICustomTypeDescriptor.GetComponentName() => this.GetComponentName();

    TypeConverter ICustomTypeDescriptor.GetConverter() => this.GetConverter();

    EventDescriptor ICustomTypeDescriptor.GetDefaultEvent() => this.GetDefaultEvent();

    PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty() => this.GetDefaultProperty();

    object ICustomTypeDescriptor.GetEditor(Type editorBaseType) => throw new Exception("The method or operation is not implemented.");

    EventDescriptorCollection ICustomTypeDescriptor.GetEvents(
      Attribute[] attributes)
    {
      return this.GetEvents(attributes);
    }

    EventDescriptorCollection ICustomTypeDescriptor.GetEvents() => this.GetEvents((Attribute[]) null);

    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(
      Attribute[] attributes)
    {
      return this.GetProperties(attributes);
    }

    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties() => this.GetProperties((Attribute[]) null);

    object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd) => this.GetPropertyOwner(pd);

    void IEditableObject.BeginEdit()
    {
    }

    void IEditableObject.CancelEdit()
    {
    }

    void IEditableObject.EndEdit()
    {
    }

    event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
    {
      add => this.PropertyChanged += value;
      remove => this.PropertyChanged -= value;
    }

    event PropertyChangingEventHandler INotifyPropertyChanging.PropertyChanging
    {
      add => this.PropertyChanging += value;
      remove => this.PropertyChanging -= value;
    }

    object IUniqueItem.UniqueKey => (object) this.UniqueKey;
  }
}
