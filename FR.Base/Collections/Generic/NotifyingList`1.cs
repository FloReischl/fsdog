// Decompiled with JetBrains decompiler
// Type: FR.Collections.Generic.NotifyingList`1
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace FR.Collections.Generic
{
  [DebuggerDisplay("Type: {GetType()} | Count: {Count}")]
  public class NotifyingList<TItem> : 
    IList<TItem>,
    ICollection<TItem>,
    IEnumerable<TItem>,
    IBindingList,
    IList,
    ICollection,
    IEnumerable,
    ISupportInitializeNotification,
    ISupportInitialize
  {
    private List<TItem> _list;
    private Dictionary<PropertyDescriptor, Dictionary<object, List<TItem>>> _indexes;
    private Dictionary<string, PropertyDescriptor> _itemPropertiesByName;
    private bool _changedWhileSuspended;
    private bool _suspendListChangedNotification;
    private Dictionary<PropertyDescriptor, IComparer> _itemPropertyComparer;
    private System.Collections.Generic.Comparer<TItem> _comparer;
    private bool _isInitialized;
    private bool _isSorted;
    private ListSortDirection _sortDirection;
    private PropertyDescriptor _sortProperty;

    public NotifyingList()
    {
      this._list = new List<TItem>();
      this._isInitialized = true;
    }

    public event ListChangedEventHandler ListChanged;

    public event EventHandler Initialized;

    public TItem this[int index]
    {
      get => this._list[index];
      set => this.Insert(index, value);
    }

    public System.Collections.Generic.Comparer<TItem> Comparer
    {
      [DebuggerNonUserCode] get => this._comparer;
      [DebuggerNonUserCode] set => this._comparer = value;
    }

    public int Count
    {
      [DebuggerNonUserCode] get => this._list.Count;
    }

    public bool IsInitialized
    {
      [DebuggerNonUserCode] get => this._isInitialized;
    }

    public bool IsSorted
    {
      [DebuggerNonUserCode] get => this._isSorted;
    }

    public ListSortDirection SortDirection
    {
      [DebuggerNonUserCode] get => this._sortDirection;
    }

    public PropertyDescriptor SortProperty
    {
      [DebuggerNonUserCode] get => this._sortProperty;
    }

    protected virtual bool AllowEdit
    {
      [DebuggerNonUserCode] get => true;
    }

    protected virtual bool AllowNew
    {
      [DebuggerNonUserCode] get => false;
    }

    protected virtual bool AllowRemove
    {
      [DebuggerNonUserCode] get => true;
    }

    public virtual int Add(TItem item) => this.Insert(this.Count, item);

    public virtual void AddIndex(string propertyName) => this.AddIndex(this.GetItemProperty(propertyName));

    public virtual void AddIndex(PropertyDescriptor property)
    {
      if (this._indexes == null)
        this._indexes = new Dictionary<PropertyDescriptor, Dictionary<object, List<TItem>>>();
      Dictionary<object, List<TItem>> index = new Dictionary<object, List<TItem>>();
      this.ResetIndex(index, property);
      this._indexes.Add(property, index);
    }

    public virtual void AddPropertyComparer(string propertyName, IComparer comparer) => this.AddPropertyComparer(this.GetItemProperty(propertyName), comparer);

    public virtual void AddPropertyComparer(PropertyDescriptor property, IComparer comparer)
    {
      if (this._itemPropertyComparer == null)
        this._itemPropertyComparer = new Dictionary<PropertyDescriptor, IComparer>();
      if (this._itemPropertyComparer.ContainsKey(property))
        this._itemPropertyComparer.Remove(property);
      this._itemPropertyComparer.Add(property, comparer);
    }

    public virtual void AddRange(IEnumerable<TItem> collection)
    {
      bool changedNotification = this._suspendListChangedNotification;
      this._suspendListChangedNotification = true;
      foreach (TItem obj in collection)
        this.Insert(this.Count, obj);
      this._suspendListChangedNotification = changedNotification;
      this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    }

    public virtual void ApplySort(PropertyDescriptor property, ListSortDirection direction)
    {
      SortedList<object, TItem> sortedList = new SortedList<object, TItem>();
      sortedList.AllowDuplicates = true;
      List<TItem> objList = new List<TItem>(this.Count);
      objList.AddRange((IEnumerable<TItem>) this);
      foreach (TItem component in objList)
        sortedList.Add(property.GetValue((object) component), component);
      this._list.Clear();
      this._list.AddRange((IEnumerable<TItem>) sortedList.GetValueList());
      if (direction == ListSortDirection.Descending)
        this._list.Reverse();
      this._isSorted = true;
      this._sortDirection = direction;
      this._sortProperty = property;
      this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    }

    public virtual void ApplySort(string propertyName, ListSortDirection direction) => this.ApplySort(TypeDescriptor.GetProperties(this.GetType().GetGenericArguments()[0])[propertyName], direction);

    public virtual void BeginInit()
    {
      this._isInitialized = false;
      this._suspendListChangedNotification = true;
    }

    public virtual void Clear()
    {
      this._list.Clear();
      this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    }

    public virtual bool Contains(TItem item) => this._list.Contains(item);

    public virtual void CopyTo(TItem[] array, int arrayIndex) => this._list.CopyTo(array, arrayIndex);

    public virtual void EndInit()
    {
      this._isInitialized = true;
      this._suspendListChangedNotification = false;
      this.OnInitialized();
      if (!this._changedWhileSuspended)
        return;
      this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
      this._changedWhileSuspended = false;
    }

    public virtual TItem Find(string propertyName, object key) => this.Find(this.GetItemProperty(propertyName), key);

    public virtual TItem Find(PropertyDescriptor property, object key)
    {
      TItem[] all = this.FindAll(property, key);
      return all.Length != 0 ? all[0] : default(TItem);
    }

    public virtual TItem[] FindAll(string propertyName, object key) => this.FindAll(this.GetItemProperty(propertyName), key);

    public virtual TItem[] FindAll(PropertyDescriptor property, object key)
    {
      List<TItem> objList = (List<TItem>) null;
      Dictionary<object, List<TItem>> dictionary = (Dictionary<object, List<TItem>>) null;
      if (this._indexes != null && this._indexes.ContainsKey(property))
        dictionary = this._indexes[property];
      if (dictionary != null)
      {
        dictionary.TryGetValue(key, out objList);
      }
      else
      {
        IComparer propertyComparer = this.GetItemPropertyComparer(property);
        objList = new List<TItem>();
        foreach (TItem component in this)
        {
          object y = property.GetValue((object) component);
          if (propertyComparer.Compare(key, y) == 0)
            objList.Add(component);
        }
      }
      if (objList == null)
        objList = new List<TItem>();
      return objList.ToArray();
    }

    public virtual int FindPosition(string propertyName, object key) => this.FindPosition(this.GetItemProperty(propertyName), key);

    public virtual int FindPosition(PropertyDescriptor property, object key)
    {
      int position = -1;
      IComparer propertyComparer = this.GetItemPropertyComparer(property);
      for (int index = 0; index < this.Count; ++index)
      {
        TItem component = this[index];
        object y = property.GetValue((object) component);
        if (propertyComparer.Compare(key, y) == 0)
        {
          position = index;
          break;
        }
      }
      return position;
    }

    public IEnumerator<TItem> GetEnumerator() => (IEnumerator<TItem>) this._list.GetEnumerator();

    public int IndexOf(TItem item) => this._list.IndexOf(item);

    public virtual int Insert(int index, TItem item)
    {
      if (!this._isSorted)
      {
        this._list.Insert(index, item);
      }
      else
      {
        IComparer comparer = (IComparer) this.Comparer ?? (IComparer) System.Collections.Generic.Comparer<TItem>.Default;
        if (this.SortDirection == ListSortDirection.Ascending)
        {
          index = this.Count;
          for (int index1 = 0; index1 < this._list.Count; ++index1)
          {
            if (comparer.Compare((object) this._list[index1], (object) item) < 0)
            {
              index = index1;
              break;
            }
          }
        }
        else
        {
          index = 0;
          for (int index2 = this._list.Count - 1; index2 >= 0; --index2)
          {
            if (comparer.Compare((object) this._list[index2], (object) item) > 0)
            {
              index = index2;
              break;
            }
          }
        }
        this._list.Insert(index, item);
      }
      if (item is INotifyPropertyChanged notifyPropertyChanged)
        notifyPropertyChanged.PropertyChanged += new PropertyChangedEventHandler(this.NotifyingItem_PropertyChanged);
      this.UpdateIndexes(ListChangedType.ItemAdded, item);
      this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
      return index;
    }

    public virtual bool Remove(TItem item)
    {
      int index = this.IndexOf(item);
      if (index == -1)
        return false;
      this.RemoveAt(index);
      return true;
    }

    public virtual void RemoveAt(int index)
    {
      TItem obj = this[index];
      this._list.RemoveAt(index);
      if (obj is INotifyPropertyChanged notifyPropertyChanged)
        notifyPropertyChanged.PropertyChanged -= new PropertyChangedEventHandler(this.NotifyingItem_PropertyChanged);
      this.UpdateIndexes(ListChangedType.ItemDeleted, obj);
      this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
    }

    public virtual void RemoveIndex(PropertyDescriptor property)
    {
      if (this._indexes == null || !this._indexes.ContainsKey(property))
        return;
      this._indexes.Remove(property);
    }

    public virtual void RemoveSort()
    {
      this._isSorted = false;
      this._sortProperty = (PropertyDescriptor) null;
    }

    public virtual void Reverse() => this._list.Reverse();

    protected virtual object AddNew() => throw new NotImplementedException("The method or operation is not implemented.");

    protected virtual PropertyDescriptor GetItemProperty(string propertyName)
    {
      if (this._itemPropertiesByName == null)
        this._itemPropertiesByName = new Dictionary<string, PropertyDescriptor>();
      PropertyDescriptor property;
      if (!this._itemPropertiesByName.ContainsKey(propertyName))
      {
        property = TypeDescriptor.GetProperties(this.GetType().GetGenericArguments()[0])[propertyName];
        this._itemPropertiesByName.Add(propertyName, property);
      }
      else
        property = this._itemPropertiesByName[propertyName];
      return property;
    }

    protected virtual IComparer GetItemPropertyComparer(PropertyDescriptor property)
    {
      if (this._itemPropertyComparer == null)
        this._itemPropertyComparer = new Dictionary<PropertyDescriptor, IComparer>();
      IComparer comparer;
      if (!this._itemPropertyComparer.TryGetValue(property, out comparer))
      {
        comparer = (IComparer) Type.GetType(string.Format("{0}.{1}[[{2}.{3},{4}]]", (object) typeof (System.Collections.Generic.Comparer<>).Namespace, (object) typeof (System.Collections.Generic.Comparer<>).Name, (object) property.PropertyType.Namespace, (object) property.PropertyType.Name, (object) property.PropertyType.Assembly.GetName().Name)).GetProperty("Default", BindingFlags.Static | BindingFlags.Public).GetValue((object) null, (object[]) null);
        this.AddPropertyComparer(property, comparer);
      }
      return comparer;
    }

    protected virtual void OnInitialized()
    {
      if (this.Initialized == null)
        return;
      this.Initialized((object) this, EventArgs.Empty);
    }

    protected virtual void OnListChanged(ListChangedEventArgs e)
    {
      if (!this._suspendListChangedNotification)
      {
        if (this.ListChanged == null)
          return;
        this.ListChanged((object) this, e);
      }
      else
        this._changedWhileSuspended = true;
    }

    protected virtual void ResetIndex(
      Dictionary<object, List<TItem>> index,
      PropertyDescriptor property)
    {
      index.Clear();
      foreach (TItem component in this)
      {
        object key = property.GetValue((object) component);
        List<TItem> objList;
        if (index.ContainsKey(key))
        {
          objList = index[key];
        }
        else
        {
          objList = new List<TItem>();
          index.Add(key, objList);
        }
        objList.Add(component);
      }
    }

    protected virtual void ResetIndexes()
    {
      if (this._indexes == null)
        return;
      foreach (KeyValuePair<PropertyDescriptor, Dictionary<object, List<TItem>>> index in this._indexes)
        this.ResetIndex(index.Value, index.Key);
    }

    protected virtual void UpdateIndex(
      ListChangedType changeType,
      Dictionary<object, List<TItem>> index,
      PropertyDescriptor property,
      TItem item)
    {
      switch (changeType)
      {
        case ListChangedType.Reset:
          this.ResetIndex(index, property);
          break;
        case ListChangedType.ItemAdded:
          object key1 = property.GetValue((object) item);
          List<TItem> objList1;
          if (index.ContainsKey(key1))
          {
            objList1 = index[key1];
          }
          else
          {
            objList1 = new List<TItem>();
            index.Add(key1, objList1);
          }
          objList1.Add(item);
          break;
        case ListChangedType.ItemDeleted:
          object key2 = property.GetValue((object) item);
          List<TItem> objList2 = index[key2];
          bool flag = false;
          for (int index1 = 0; index1 < objList2.Count; ++index1)
          {
            TItem objB = objList2[index1];
            if (object.ReferenceEquals((object) item, (object) objB))
            {
              objList2.RemoveAt(index1);
              flag = true;
              break;
            }
          }
          if (flag)
            break;
          objList2.RemoveAt(0);
          break;
      }
    }

    protected virtual void UpdateIndexes(ListChangedType changeType, TItem item)
    {
      if (this._indexes == null)
        return;
      foreach (KeyValuePair<PropertyDescriptor, Dictionary<object, List<TItem>>> index in this._indexes)
        this.UpdateIndex(changeType, index.Value, index.Key, item);
    }

    [DebuggerNonUserCode]
    int IList<TItem>.IndexOf(TItem item) => this.IndexOf(item);

    [DebuggerNonUserCode]
    void IList<TItem>.Insert(int index, TItem item) => this.Insert(index, item);

    [DebuggerNonUserCode]
    void IList<TItem>.RemoveAt(int index) => this.RemoveAt(index);

    TItem IList<TItem>.this[int index]
    {
      [DebuggerNonUserCode] get => this[index];
      [DebuggerNonUserCode] set => this[index] = value;
    }

    [DebuggerNonUserCode]
    void ICollection<TItem>.Add(TItem item) => this.Add(item);

    [DebuggerNonUserCode]
    void ICollection<TItem>.Clear() => this.Clear();

    [DebuggerNonUserCode]
    bool ICollection<TItem>.Contains(TItem item) => this.Contains(item);

    [DebuggerNonUserCode]
    void ICollection<TItem>.CopyTo(TItem[] array, int arrayIndex) => this.CopyTo(array, arrayIndex);

    int ICollection<TItem>.Count
    {
      [DebuggerNonUserCode] get => this.Count;
    }

    bool ICollection<TItem>.IsReadOnly
    {
      [DebuggerNonUserCode] get => true;
    }

    [DebuggerNonUserCode]
    bool ICollection<TItem>.Remove(TItem item) => this.Remove(item);

    [DebuggerNonUserCode]
    IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator() => this.GetEnumerator();

    [DebuggerNonUserCode]
    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    [DebuggerNonUserCode]
    int IList.Add(object value)
    {
      this.Add((TItem) value);
      return this.Count - 1;
    }

    [DebuggerNonUserCode]
    void IList.Clear() => this.Clear();

    [DebuggerNonUserCode]
    bool IList.Contains(object value) => this.Contains((TItem) value);

    [DebuggerNonUserCode]
    int IList.IndexOf(object value) => this.IndexOf((TItem) value);

    [DebuggerNonUserCode]
    void IList.Insert(int index, object value) => this.Insert(index, (TItem) value);

    bool IList.IsFixedSize
    {
      [DebuggerNonUserCode] get => false;
    }

    bool IList.IsReadOnly
    {
      [DebuggerNonUserCode] get => false;
    }

    [DebuggerNonUserCode]
    void IList.Remove(object value) => this.Remove((TItem) value);

    [DebuggerNonUserCode]
    void IList.RemoveAt(int index) => this.RemoveAt(index);

    object IList.this[int index]
    {
      [DebuggerNonUserCode] get => (object) this[index];
      [DebuggerNonUserCode] set => this[index] = (TItem) value;
    }

    [DebuggerNonUserCode]
    void ICollection.CopyTo(Array array, int index) => this.CopyTo((TItem[]) array, index);

    int ICollection.Count
    {
      [DebuggerNonUserCode] get => this.Count;
    }

    bool ICollection.IsSynchronized
    {
      [DebuggerNonUserCode] get => false;
    }

    object ICollection.SyncRoot
    {
      [DebuggerNonUserCode] get => (object) null;
    }

    [DebuggerNonUserCode]
    void IBindingList.AddIndex(PropertyDescriptor property) => this.AddIndex(property);

    [DebuggerNonUserCode]
    object IBindingList.AddNew() => this.AddNew();

    bool IBindingList.AllowEdit
    {
      [DebuggerNonUserCode] get => this.AllowEdit;
    }

    bool IBindingList.AllowNew
    {
      [DebuggerNonUserCode] get => this.AllowNew;
    }

    bool IBindingList.AllowRemove
    {
      [DebuggerNonUserCode] get => this.AllowRemove;
    }

    [DebuggerNonUserCode]
    void IBindingList.ApplySort(
      PropertyDescriptor property,
      ListSortDirection direction)
    {
      this.ApplySort(property, direction);
    }

    [DebuggerNonUserCode]
    int IBindingList.Find(PropertyDescriptor property, object key) => this.FindPosition(property, key);

    bool IBindingList.IsSorted
    {
      [DebuggerNonUserCode] get => this._isSorted;
    }

    event ListChangedEventHandler IBindingList.ListChanged
    {
      [DebuggerNonUserCode] add => this.ListChanged += value;
      [DebuggerNonUserCode] remove => this.ListChanged -= value;
    }

    [DebuggerNonUserCode]
    void IBindingList.RemoveIndex(PropertyDescriptor property) => this.RemoveIndex(property);

    [DebuggerNonUserCode]
    void IBindingList.RemoveSort() => this.RemoveSort();

    ListSortDirection IBindingList.SortDirection
    {
      [DebuggerNonUserCode] get => this.SortDirection;
    }

    PropertyDescriptor IBindingList.SortProperty
    {
      [DebuggerNonUserCode] get => this.SortProperty;
    }

    bool IBindingList.SupportsChangeNotification
    {
      [DebuggerNonUserCode] get => true;
    }

    bool IBindingList.SupportsSearching
    {
      [DebuggerNonUserCode] get => true;
    }

    bool IBindingList.SupportsSorting
    {
      [DebuggerNonUserCode] get => true;
    }

    [DebuggerNonUserCode]
    void ISupportInitialize.BeginInit() => this.BeginInit();

    [DebuggerNonUserCode]
    void ISupportInitialize.EndInit() => this.EndInit();

    event EventHandler ISupportInitializeNotification.Initialized
    {
      [DebuggerNonUserCode] add => this.Initialized += value;
      [DebuggerNonUserCode] remove => this.Initialized -= value;
    }

    bool ISupportInitializeNotification.IsInitialized
    {
      [DebuggerNonUserCode] get => this.IsInitialized;
    }

    private void NotifyingItem_PropertyChanged(object sender, PropertyChangedEventArgs e) => this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, this.IndexOf((TItem) sender)));
  }
}
