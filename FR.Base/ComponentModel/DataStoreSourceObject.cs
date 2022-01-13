// Decompiled with JetBrains decompiler
// Type: FR.ComponentModel.DataStoreSourceObject
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.ComponentModel;
using System.Diagnostics;

namespace FR.ComponentModel
{
  public class DataStoreSourceObject : 
    MarshalByRefObject,
    IUniqueItem,
    ICustomTypeDescriptor,
    INotifyPropertyChanging,
    INotifyPropertyChanged
  {
    private static int _nextUniqueId;
    private ICustomTypeDescriptor _descriptor;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private object _baseObject;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private DataStoreSource _dataSource;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int _uniqueKey;

    internal DataStoreSourceObject(DataStoreSource dataSource, object baseObject)
    {
      this._dataSource = dataSource;
      this._baseObject = baseObject;
      this._uniqueKey = DataStoreSourceObject._nextUniqueId++;
      if (this.BaseObject is INotifyPropertyChanging baseObject1)
        baseObject1.PropertyChanging += new PropertyChangingEventHandler(this.BaseObject_PropertyChanging);
      if (!(this.BaseObject is INotifyPropertyChanged baseObject2))
        return;
      baseObject2.PropertyChanged += new PropertyChangedEventHandler(this.BaseObject_PropertyChanged);
    }

    public event PropertyChangingEventHandler PropertyChanging;

    public event PropertyChangedEventHandler PropertyChanged;

    public object BaseObject
    {
      [DebuggerNonUserCode] get => this._baseObject;
    }

    public DataStoreSource DataSource
    {
      [DebuggerNonUserCode] get => this._dataSource;
    }

    public int UniqueKey
    {
      [DebuggerNonUserCode] get => this._uniqueKey;
    }

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
        this._descriptor = TypeDescriptor.GetProvider(this.BaseObject).GetTypeDescriptor(this.BaseObject);
      return this._descriptor;
    }

    private object GetEditor(Type editorBaseType) => this.GetDescriptor().GetEditor(editorBaseType);

    private EventDescriptorCollection GetEvents(Attribute[] attrs) => this.GetDescriptor().GetEvents(attrs);

    private PropertyDescriptorCollection GetProperties(
      Attribute[] attributes)
    {
      PropertyDescriptor[] propertyDescriptorArray = new PropertyDescriptor[this.DataSource.ItemProperties.Count];
      this.DataSource.ItemProperties.Values.CopyTo(propertyDescriptorArray, 0);
      return new PropertyDescriptorCollection(propertyDescriptorArray);
    }

    private object GetPropertyOwner(PropertyDescriptor pd) => (object) this;

    private void OnPropertyChanging(string propertyName)
    {
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, new PropertyChangingEventArgs(propertyName));
    }

    private void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    private void BaseObject_PropertyChanging(object sender, PropertyChangingEventArgs e) => this.OnPropertyChanging(e.PropertyName);

    private void BaseObject_PropertyChanged(object sender, PropertyChangedEventArgs e) => this.OnPropertyChanged(e.PropertyName);

    object IUniqueItem.UniqueKey => (object) this.UniqueKey;

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

    event PropertyChangingEventHandler INotifyPropertyChanging.PropertyChanging
    {
      add => this.PropertyChanging += value;
      remove => this.PropertyChanging -= value;
    }

    event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
    {
      add => this.PropertyChanged += value;
      remove => this.PropertyChanged -= value;
    }
  }
}
