// Decompiled with JetBrains decompiler
// Type: FR.ComponentModel.DataStoreView
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using FR.Collections;
using FR.Collections.Generic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace FR.ComponentModel
{
  [Serializable]
  public class DataStoreView : 
    IBindingList,
    IList,
    ICollection,
    IEnumerable,
    ISupportInitializeNotification,
    ISupportInitialize,
    ITypedList
  {
    private DataStore<DataStoreViewObject> _store;
    private DataStoreSource _source;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private DataStoreSource _dataSource;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private bool _isSorted;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private PropertyDescriptorCollection _itemProperties;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private ListSortDirection _sortDirection;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private PropertyDescriptor _sortProperty;

    public DataStoreView(DataStoreSource dataSource, IEnumerable<DataStoreSourceObject> items)
    {
      if (dataSource == null)
        throw ExceptionHelper.GetArgumentNull(nameof (dataSource));
      if (items == null)
        throw ExceptionHelper.GetArgumentNull(nameof (items));
      this._dataSource = dataSource;
      PrimaryKeyDictionary<int, object> primaryKeyDictionary = new PrimaryKeyDictionary<int, object>((IEqualityComparer<int>) new Int32Comparer());
      this._store = new DataStore<DataStoreViewObject>(PropertyDescriptorHelper.GetDynamicProperty(PropertyDescriptorHelper.GetProperty(typeof (DataStoreViewObject), "UniqueKey"), false), (IPrimaryKey) primaryKeyDictionary);
      PrimaryKey<int, object> ordinalPositionIndex = DataStoreView.GetOrdinalPositionIndex();
      PropertyDescriptor positionProperty = DataStoreView.GetOrdinalPositionProperty();
      this._store.AddIndex(positionProperty, (IIndex) ordinalPositionIndex);
      this.BeginInit();
      int num = 0;
      foreach (DataStoreSourceObject storeSourceObject in items)
      {
        object key = dataSource.PrimaryKeyProperty.GetValue((object) storeSourceObject);
        if (dataSource.PrimaryKey.FindValue(key) == null)
        {
          this.Clear();
          throw IndexExceptionHelper.GetViewObjectNotInDataSource((object) storeSourceObject);
        }
        DataStoreViewObject dataStoreViewObject = new DataStoreViewObject(this, storeSourceObject);
        dataStoreViewObject.SetOrdinalPosition(num++);
        this._store.Add(dataStoreViewObject);
      }
      this._store.ApplySort(positionProperty, ListSortDirection.Ascending);
      this.EndInit();
      this._store.Initialized += new EventHandler(this.Store_Initialized);
      this._store.ListChanged += new ListChangedEventHandler(this.Store_ListChanged);
      this._source = dataSource;
      this._source.ListChanged += new ListChangedEventHandler(this.DataSource_ListChanged);
    }

    public event EventHandler Initialized;

    public event ListChangedEventHandler ListChanged;

    public event DataStoreViewObjectHandler ObjectAdded;

    public event DataStoreViewObjectHandler ObjectAdding;

    public event DataStoreViewObjectHandler ObjectChanged;

    public event DataStoreViewObjectHandler ObjectChanging;

    public event DataStoreViewObjectHandler ObjectRemoved;

    public event DataStoreViewObjectHandler ObjectRemoving;

    public event EventHandler SortingChanged;

    public DataStoreViewObject this[int index]
    {
      get => this._store[index];
      set => this.Insert(index, value);
    }

    public bool AllowEdit
    {
      [DebuggerNonUserCode] get => this._dataSource.AllowEdit;
    }

    public bool AllowNew
    {
      [DebuggerNonUserCode] get => this._dataSource.AllowNew;
    }

    public bool AllowRemove
    {
      [DebuggerNonUserCode] get => this._dataSource.AllowRemove;
    }

    public int Count
    {
      [DebuggerNonUserCode] get => this._store.Count;
    }

    public DataStoreSource DataSource
    {
      [DebuggerNonUserCode] get => this._dataSource;
    }

    public bool IsFixedSize
    {
      [DebuggerNonUserCode] get => this._dataSource.IsFixedSize;
    }

    public bool IsReadOnly
    {
      [DebuggerNonUserCode] get => this._dataSource.IsReadOnly;
    }

    public bool IsInitialized
    {
      [DebuggerNonUserCode] get => this._store.IsInitialized;
    }

    public bool IsSorted
    {
      [DebuggerNonUserCode] get => this._isSorted;
    }

    public bool IsSynchronized
    {
      [DebuggerNonUserCode] get => false;
    }

    public virtual PropertyDescriptorCollection ItemProperties
    {
      [DebuggerNonUserCode] get
      {
        if (this._itemProperties == null)
          this._itemProperties = this.GetItemProperties();
        return this._itemProperties;
      }
    }

    public string Name
    {
      [DebuggerNonUserCode] get => this._store.Name;
      [DebuggerNonUserCode] set => this._store.Name = value;
    }

    public ListSortDirection SortDirection
    {
      [DebuggerNonUserCode] get => this._sortDirection;
    }

    public PropertyDescriptor SortProperty
    {
      [DebuggerNonUserCode] get => this._sortProperty;
    }

    public bool SupportsChangeNotification
    {
      [DebuggerNonUserCode] get => this._store.SupportsChangeNotification;
    }

    public bool SupportsSearching
    {
      [DebuggerNonUserCode] get => true;
    }

    public bool SupportsSorting
    {
      [DebuggerNonUserCode] get => true;
    }

    public object SyncRoot
    {
      [DebuggerNonUserCode] get => (object) null;
    }

    public int Add(DataStoreViewObject item)
    {
      this.Insert(this.Count, item);
      return this.Count - 1;
    }

    public void AddIndex(PropertyDescriptor property)
    {
      if (this._dataSource.FindIndex(property, ListSortDirection.Ascending) != null)
        return;
      this._dataSource.AddIndex(property, ListSortDirection.Ascending);
    }

    public DataStoreViewObject AddNew()
    {
      DataStoreViewObject dataStoreViewObject = new DataStoreViewObject(this, this._dataSource.AddNew());
      this._store.Add(dataStoreViewObject);
      return dataStoreViewObject;
    }

    public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
    {
      this._store.RemoveIndex(this._store.SortProperty, this._store.SortDirection);
      this._dataSource.ApplySort(this._dataSource.ItemProperties[property.Name], direction);
      int num = 0;
      foreach (DataStoreSourceObject storeSourceObject in (DataStore<DataStoreSourceObject>) this._dataSource)
        this._store.Find(this._store.PrimaryKeyProperty, (object) storeSourceObject.UniqueKey)?.SetOrdinalPosition(num++);
      this._isSorted = true;
      this._sortDirection = direction;
      this._sortProperty = property;
      IIndex ordinalPositionIndex = (IIndex) DataStoreView.GetOrdinalPositionIndex();
      PropertyDescriptor positionProperty = DataStoreView.GetOrdinalPositionProperty();
      this._store.AddIndex(positionProperty, ordinalPositionIndex);
      this._store.ApplySort(positionProperty, ListSortDirection.Ascending);
      this.OnSortingChanged();
    }

    public void BeginInit() => this._store.BeginInit();

    public void Clear()
    {
      this.BeginInit();
      foreach (DataStoreViewObject dataStoreViewObject in this)
        this.Remove(dataStoreViewObject);
      this.EndInit();
    }

    public bool Contains(DataStoreViewObject item) => this._store.Contains(item);

    public void CopyTo(DataStoreViewObject[] array, int index) => this._store.CopyTo(array, index);

    public void EndInit() => this._store.EndInit();

    public IEnumerator GetEnumerator() => (IEnumerator) this._store.GetEnumerator();

    public int IndexOf(PropertyDescriptor property, object key) => this._store.IndexOf(property, key);

    public int IndexOf(DataStoreViewObject item) => this._store.IndexOf(item);

    public void Insert(int index, DataStoreViewObject item)
    {
      this.OnObjectAdding(item);
      this._dataSource.Add(item.DataObject);
      this._store.Insert(index, item);
      this.OnObjectAdded(item);
    }

    public void Remove(DataStoreViewObject item)
    {
      this.OnObjectRemoving(item);
      this._dataSource.Remove(item.DataObject);
      this._store.Remove(item);
      this.OnObjectRemoved(item);
    }

    public void RemoveAt(int index) => this.Remove(this[index]);

    public void RemoveSort() => this._store.RemoveSort();

    protected PropertyDescriptorCollection GetItemProperties()
    {
      DataStoreViewObject dataStoreViewObject = (DataStoreViewObject) null;
      for (int index = 0; index < this.Count; ++index)
      {
        dataStoreViewObject = this[index];
        if (dataStoreViewObject != null && dataStoreViewObject.DataObject.BaseObject != null)
          break;
      }
      if (dataStoreViewObject == null || dataStoreViewObject.DataObject.BaseObject == null)
        return new PropertyDescriptorCollection(new PropertyDescriptor[0]);
      PropertyDescriptorCollection dynamicProperties = PropertyDescriptorHelper.GetDynamicProperties(PropertyDescriptorHelper.GetProperties(dataStoreViewObject.DataObject.BaseObject));
      List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
      foreach (PropertyDescriptor baseProperty in dynamicProperties)
        propertyDescriptorList.Add((PropertyDescriptor) new DataStoreViewObjectProperty(baseProperty));
      return new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
    }

    protected virtual void OnObjectAdded(DataStoreViewObject obj)
    {
      if (this.ObjectAdded == null)
        return;
      this.ObjectAdded((object) this, new DataStoreViewObjectArgs(obj));
    }

    protected virtual void OnObjectAdding(DataStoreViewObject obj)
    {
      if (this.ObjectAdding == null)
        return;
      this.ObjectAdding((object) this, new DataStoreViewObjectArgs(obj));
    }

    protected internal virtual void OnObjectChanged(DataStoreViewObject obj)
    {
      if (this.ObjectChanged == null)
        return;
      this.ObjectChanged((object) this, new DataStoreViewObjectArgs(obj));
    }

    protected internal virtual void OnObjectChanging(DataStoreViewObject obj)
    {
      if (this.ObjectChanging == null)
        return;
      this.ObjectChanging((object) this, new DataStoreViewObjectArgs(obj));
    }

    protected virtual void OnObjectRemoved(DataStoreViewObject obj)
    {
      if (this.ObjectRemoved == null)
        return;
      this.ObjectRemoved((object) this, new DataStoreViewObjectArgs(obj));
    }

    protected virtual void OnObjectRemoving(DataStoreViewObject obj)
    {
      if (this.ObjectRemoving == null)
        return;
      this.ObjectRemoving((object) this, new DataStoreViewObjectArgs(obj));
    }

    protected virtual void OnInitialized()
    {
      if (this.Initialized == null)
        return;
      this.Initialized((object) this, EventArgs.Empty);
    }

    protected virtual void OnListChanged(ListChangedEventArgs e)
    {
      if (this.ListChanged == null)
        return;
      this.ListChanged((object) this, e);
    }

    protected virtual void OnSortingChanged()
    {
      if (this.SortingChanged == null)
        return;
      this.SortingChanged((object) this, EventArgs.Empty);
    }

    private static PrimaryKey<int, object> GetOrdinalPositionIndex() => new PrimaryKey<int, object>((IComparer<int>) new Int32Comparer(), ListSortDirection.Ascending);

    private static PropertyDescriptor GetOrdinalPositionProperty() => TypeDescriptor.GetProperties(typeof (DataStoreViewObject))["OrdinalPosition"];

    private void Store_Initialized(object sender, EventArgs e) => this.OnInitialized();

    private void Store_ListChanged(object sender, ListChangedEventArgs e) => this.OnListChanged(e);

    private void DataSource_ListChanged(object sender, ListChangedEventArgs e)
    {
      if (e.ListChangedType == ListChangedType.ItemAdded)
        return;
      if (e.ListChangedType == ListChangedType.ItemChanged)
      {
        if (e.PropertyDescriptor == null)
          return;
        DataStoreSourceObject storeSourceObject = this._source[e.NewIndex];
        this._store.Find(this._store.PrimaryKeyProperty, IndexFindOption.Equal, (object) storeSourceObject.UniqueKey)?.OnPropertyChanged(e.PropertyDescriptor.Name);
        int newIndex = this._store.IndexOf(this._store.PrimaryKeyProperty, (object) storeSourceObject.UniqueKey);
        if (newIndex == -1)
          return;
        PropertyDescriptor itemProperty = this.ItemProperties[e.PropertyDescriptor.Name];
        this.OnListChanged(new ListChangedEventArgs(e.ListChangedType, newIndex, itemProperty));
      }
      else
      {
        if (e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemMoved || e.ListChangedType == ListChangedType.PropertyDescriptorAdded || e.ListChangedType == ListChangedType.PropertyDescriptorChanged || e.ListChangedType == ListChangedType.PropertyDescriptorDeleted)
          return;
        int listChangedType = (int) e.ListChangedType;
      }
    }

    void IBindingList.AddIndex(PropertyDescriptor property) => this.AddIndex(property);

    object IBindingList.AddNew() => (object) this.AddNew();

    bool IBindingList.AllowEdit => this.AllowEdit;

    bool IBindingList.AllowNew => this.AllowNew;

    bool IBindingList.AllowRemove => this.AllowRemove;

    void IBindingList.ApplySort(
      PropertyDescriptor property,
      ListSortDirection direction)
    {
      this.ApplySort(property, direction);
    }

    int IBindingList.Find(PropertyDescriptor property, object key) => this.IndexOf(property, key);

    bool IBindingList.IsSorted => this.IsSorted;

    event ListChangedEventHandler IBindingList.ListChanged
    {
      add => this.ListChanged += value;
      remove => this.ListChanged -= value;
    }

    void IBindingList.RemoveIndex(PropertyDescriptor property)
    {
    }

    void IBindingList.RemoveSort() => this.RemoveSort();

    ListSortDirection IBindingList.SortDirection => this.SortDirection;

    PropertyDescriptor IBindingList.SortProperty => this.SortProperty;

    bool IBindingList.SupportsChangeNotification => this.SupportsChangeNotification;

    bool IBindingList.SupportsSearching => this.SupportsSearching;

    bool IBindingList.SupportsSorting => this.SupportsSorting;

    int IList.Add(object value) => this.Add((DataStoreViewObject) value);

    void IList.Clear() => this.Clear();

    bool IList.Contains(object value) => this.Contains((DataStoreViewObject) value);

    int IList.IndexOf(object value) => this.IndexOf((DataStoreViewObject) value);

    void IList.Insert(int index, object value) => this.Insert(index, (DataStoreViewObject) value);

    bool IList.IsFixedSize => this.IsFixedSize;

    bool IList.IsReadOnly => this.IsReadOnly;

    void IList.Remove(object value) => this.Remove((DataStoreViewObject) value);

    void IList.RemoveAt(int index) => this.RemoveAt(index);

    object IList.this[int index]
    {
      get => (object) this[index];
      set => this[index] = (DataStoreViewObject) value;
    }

    void ICollection.CopyTo(Array array, int index) => this.CopyTo((DataStoreViewObject[]) array, index);

    int ICollection.Count => this.Count;

    bool ICollection.IsSynchronized => this.IsSynchronized;

    object ICollection.SyncRoot => this.SyncRoot;

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    void ISupportInitialize.BeginInit() => this.BeginInit();

    void ISupportInitialize.EndInit() => this.EndInit();

    event EventHandler ISupportInitializeNotification.Initialized
    {
      add => this.Initialized += value;
      remove => this.Initialized -= value;
    }

    bool ISupportInitializeNotification.IsInitialized => this.IsInitialized;

    PropertyDescriptorCollection ITypedList.GetItemProperties(
      PropertyDescriptor[] listAccessors)
    {
      return this.ItemProperties;
    }

    string ITypedList.GetListName(PropertyDescriptor[] listAccessors) => this.Name;
  }
}
