// Decompiled with JetBrains decompiler
// Type: FR.ComponentModel.DataStoreSource
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
using System.Runtime.Serialization;

namespace FR.ComponentModel
{
  [Serializable]
  public class DataStoreSource : DataStore<DataStoreSourceObject>
  {
    private Dictionary<string, PropertyDescriptor> _itemPropertiesInternal;
    private ReadOnlyDictionary<string, PropertyDescriptor> _itemProperties;
    private object _originalDataSource;

    public DataStoreSource(object originalDataSource)
      : base(PropertyDescriptorHelper.GetDynamicProperty(PropertyDescriptorHelper.GetProperty(typeof (DataStoreSourceObject), "UniqueKey"), true), (IPrimaryKey) new PrimaryKeyDictionary<int, object>((IEqualityComparer<int>) new Int32Comparer()))
    {
      this.Refresh(originalDataSource);
    }

    public DataStoreSource(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    public object OriginalDataSource
    {
      [DebuggerNonUserCode] get => this._originalDataSource;
      [DebuggerNonUserCode] set => this.Refresh(value);
    }

    public virtual DataStoreView FindAll(
      string propertyName,
      IndexFindOption option,
      object key,
      params object[] keys)
    {
      return new DataStoreView(this, (IEnumerable<DataStoreSourceObject>) base.FindAll(propertyName, option, key, keys));
    }

    public virtual DataStoreView FindAll(
      PropertyDescriptor property,
      IndexFindOption option,
      object key,
      params object[] keys)
    {
      return new DataStoreView(this, (IEnumerable<DataStoreSourceObject>) base.FindAll(property, option, key, keys));
    }

    public void Refresh(object newDataSource)
    {
      this.BeginInit();
      if (this.OriginalDataSource != null)
      {
        object obj = this.OriginalDataSource;
        if (obj is IListSource listSource)
          obj = (object) listSource.GetList();
        if (obj is IBindingList bindingList)
          bindingList.ListChanged -= new ListChangedEventHandler(this.SourceList_ListChanged);
      }
      this.Clear();
      this._originalDataSource = newDataSource != null ? newDataSource : throw IndexExceptionHelper.GetOriginalDataSourceNull();
      IEnumerable enumerable = !(this.OriginalDataSource is IListSource originalDataSource) ? (IEnumerable) this.OriginalDataSource : (IEnumerable) originalDataSource.GetList();
      foreach (object baseObject in enumerable)
        this.Add(new DataStoreSourceObject(this, baseObject));
      if (enumerable is IBindingList bindingList1)
      {
        if (bindingList1.IsSorted)
          this.ApplySort(this.ItemProperties[bindingList1.SortProperty.Name], bindingList1.SortDirection);
        if (bindingList1.SupportsChangeNotification)
          bindingList1.ListChanged += new ListChangedEventHandler(this.SourceList_ListChanged);
      }
      if (enumerable is ITypedList typedList)
      {
        this.Name = typedList.GetListName((PropertyDescriptor[]) null);
        PropertyDescriptorCollection itemProperties = typedList.GetItemProperties((PropertyDescriptor[]) null);
        this._itemPropertiesInternal = new Dictionary<string, PropertyDescriptor>(itemProperties.Count);
        this._itemProperties = new ReadOnlyDictionary<string, PropertyDescriptor>((IDictionary<string, PropertyDescriptor>) this._itemPropertiesInternal);
        foreach (PropertyDescriptor propertyDescriptor in itemProperties)
          this._itemPropertiesInternal.Add(propertyDescriptor.Name, propertyDescriptor);
      }
      this.EndInit();
    }

    protected override ReadOnlyDictionary<string, PropertyDescriptor> GetItemProperties()
    {
      if (this._itemProperties == null)
      {
        DataStoreSourceObject storeSourceObject = (DataStoreSourceObject) null;
        for (int index = 0; index < this.Count; ++index)
        {
          storeSourceObject = this[index];
          if (storeSourceObject != null && storeSourceObject.BaseObject != null)
            break;
        }
        this._itemPropertiesInternal = new Dictionary<string, PropertyDescriptor>();
        this._itemProperties = new ReadOnlyDictionary<string, PropertyDescriptor>((IDictionary<string, PropertyDescriptor>) this._itemPropertiesInternal);
        if (storeSourceObject == null || storeSourceObject.BaseObject == null)
          return new ReadOnlyDictionary<string, PropertyDescriptor>((IDictionary<string, PropertyDescriptor>) this._itemPropertiesInternal);
        foreach (PropertyDescriptor template in !(storeSourceObject.BaseObject is ICustomTypeDescriptor) ? TypeDescriptor.GetProvider(storeSourceObject.BaseObject).GetTypeDescriptor(storeSourceObject.BaseObject.GetType(), storeSourceObject.BaseObject).GetProperties() : ((ICustomTypeDescriptor) storeSourceObject.BaseObject).GetProperties())
        {
          PropertyDescriptor dynamicProperty = PropertyDescriptorHelper.GetDynamicProperty(template, true);
          this._itemPropertiesInternal.Add(template.Name, (PropertyDescriptor) new DataStoreSourceObjectProperty(dynamicProperty));
        }
      }
      return this._itemProperties;
    }

    private void SourceList_ListChanged(object sender, ListChangedEventArgs e)
    {
      IList list = !(this.OriginalDataSource is IListSource) ? (IList) this.OriginalDataSource : ((IListSource) this.OriginalDataSource).GetList();
      if (e.ListChangedType == ListChangedType.ItemAdded)
      {
        int newIndex = e.NewIndex;
        DataStoreSourceObject storeSourceObject = new DataStoreSourceObject(this, list[newIndex]);
        this.Insert(newIndex, storeSourceObject);
      }
      else if (e.ListChangedType == ListChangedType.ItemChanged)
      {
        object obj = list[e.NewIndex];
        foreach (DataStoreSourceObject storeSourceObject in (DataStore<DataStoreSourceObject>) this)
        {
          if (storeSourceObject.BaseObject == obj)
          {
            this.UpdateIndexes(ListChangedType.ItemChanged, storeSourceObject);
            break;
          }
        }
      }
      else if (e.ListChangedType == ListChangedType.ItemDeleted)
        this.RemoveAt(e.OldIndex);
      else if (e.ListChangedType == ListChangedType.Reset)
        this.Refresh(this.OriginalDataSource);
      else if (e.ListChangedType != ListChangedType.PropertyDescriptorAdded && e.ListChangedType != ListChangedType.PropertyDescriptorChanged && e.ListChangedType != ListChangedType.PropertyDescriptorDeleted)
      {
        int listChangedType = (int) e.ListChangedType;
      }
      if (e.PropertyDescriptor != null)
        this.OnListChanged(new ListChangedEventArgs(e.ListChangedType, e.NewIndex, e.PropertyDescriptor));
      else
        this.OnListChanged(new ListChangedEventArgs(e.ListChangedType, e.NewIndex, e.OldIndex));
    }
  }
}
