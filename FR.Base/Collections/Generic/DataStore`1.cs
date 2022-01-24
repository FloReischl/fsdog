// Decompiled with JetBrains decompiler
// Type: FR.Collections.Generic.DataStore`1
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using FR.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;

namespace FR.Collections.Generic {
    [DebuggerDisplay("Count: {Count} | Type: {GetType()}")]
    [Serializable]
    public partial class DataStore<TItem> :
      IList<TItem>,
      ICollection<TItem>,
      IEnumerable<TItem>,
      IBindingList,
      IList,
      ICollection,
      IEnumerable,
      ISupportInitializeNotification,
      ISupportInitialize,
      ITypedList,
      ISerializable {
        private Dictionary<string, PropertyDescriptor> _itemPropertiesInternal;
        private Dictionary<DataStore<TItem>.IndexKey, DataStore<TItem>.IndexValue> _indexes;
        private bool _suspendChangedNotification;
        private bool _changedWhileSuspended;
        private Dictionary<PropertyDescriptor, IComparer> _itemPropertyComparer;
        private PropertyDescriptor _changingProperty;
        private object _changingValue;
        private bool _allowEdit;
        private bool _allowRemove;
        private bool _isFixedSize;
        private bool _isReadOnly;
        private ReadOnlyDictionary<string, PropertyDescriptor> _itemProperties;

        private DataStore() {
            this._allowEdit = true;
            this._allowRemove = true;
            this._isFixedSize = false;
            this._isReadOnly = false;
            this._indexes = new Dictionary<DataStore<TItem>.IndexKey, DataStore<TItem>.IndexValue>();
            this.IsInitialized = true;
        }

        public DataStore(PropertyDescriptor primaryKeyProperty, IPrimaryKey primaryKey)
          : this() {
            Check.NotNullArg(nameof(primaryKey), primaryKey);
            Check.NotNullArg(nameof(primaryKeyProperty), primaryKeyProperty);
            this.SetPrimaryKey(primaryKeyProperty, primaryKey);
            this.SortIndex = (IIndex)primaryKey;
        }

        public DataStore(
          string primaryKeyProperty,
          IPrimaryKey primaryKey,
          IEnumerable<TItem> initialData)
          : this() {
            Check.NotNullArg(nameof(primaryKey), primaryKey);
            Check.NotNullArg(nameof(primaryKeyProperty), primaryKeyProperty);
            Check.NotNullArg(nameof(initialData), initialData);

            foreach (TItem component in initialData) {
                if ((object)component != null) {
                    this.SetPrimaryKey(PropertyDescriptorHelper.GetDynamicProperty(PropertyDescriptorHelper.GetProperty((object)component, primaryKeyProperty), true), primaryKey);
                    this.SortIndex = (IIndex)primaryKey;
                    break;
                }
            }

            if (this.PrimaryKey == null) {
                throw ExceptionHelper.GetInvalidOperation("PrimaryKey property could not be evaluated. Its required to specifiy at least one initial item to evaluate the primary key property");
            }

            foreach (TItem obj in initialData) {
                this.Add(obj);
            }
        }

        public DataStore(SerializationInfo info, StreamingContext context)
          : this() {
            string propertyName1 = info != null ? info.GetString(nameof(PrimaryKeyProperty)) : throw ExceptionHelper.GetArgumentNull(nameof(info));
            IPrimaryKey primaryKey = (IPrimaryKey)info.GetValue(nameof(PrimaryKey), typeof(IPrimaryKey));
            string propertyName2 = info.GetString(nameof(SortProperty));
            ListSortDirection direction = (ListSortDirection)Enum.Parse(typeof(ListSortDirection), info.GetString(nameof(SortDirection)));
            TItem[] collection = (TItem[])info.GetValue("Items", typeof(TItem[]));
            foreach (TItem component in collection) {
                if ((object)component != null) {
                    this.SetPrimaryKey(PropertyDescriptorHelper.GetDynamicProperty(PropertyDescriptorHelper.GetProperty((object)component, propertyName1), true), primaryKey);
                    this.SortIndex = (IIndex)primaryKey;
                    break;
                }
            }
            if (this.PrimaryKey == null)
                throw ExceptionHelper.GetInvalidOperation("PrimaryKey property could not be evaluated. Its required to specifiy at least one initial item to evaluate the primary key property");
            this.AddRange((IEnumerable<TItem>)collection);
            this.ApplySort(propertyName2, direction);
        }

        public event ListChangedEventHandler ListChanged;

        public event EventHandler Initialized;

        public event EventHandler PrimaryKeyChanged;

        public TItem this[int index] {
            get => (this.SortIndex != this.PrimaryKey ? (DataStore<TItem>.PrimaryKeyEntry)this.PrimaryKey.FindValue(this.SortIndex.GetValueAt(index)) : (DataStore<TItem>.PrimaryKeyEntry)this.PrimaryKey.GetValueAt(index)).Value;
            set => this.Insert(index, value);
        }

        public virtual bool AllowEdit {
            get => !this.IsReadOnly && this._allowEdit;
            set {
                if (this.IsReadOnly)
                    throw ExceptionHelper.GetInvalidOperation("Cannot modify read-only store");
                this._allowEdit = value;
            }
        }

        public virtual bool AllowNew => !this.IsReadOnly && !this.IsFixedSize && this.NewObjectCallback != null;

        public virtual bool AllowRemove {
            get => !this.IsReadOnly && !this.IsFixedSize && this._allowRemove;
            [DebuggerNonUserCode]
            set {
                if (this.IsReadOnly || this.IsFixedSize)
                    throw ExceptionHelper.GetInvalidOperation("Cannot modify read-only or fixed size store");
                this._allowRemove = value;
            }
        }

        public int Count {
            [DebuggerNonUserCode]
            get => this.PrimaryKey.Count;
        }

        public bool IsFixedSize {
            [DebuggerNonUserCode]
            get => this._isFixedSize;
            [DebuggerNonUserCode]
            set {
                this._isFixedSize = value;
                if (!value)
                    return;
                this._allowRemove = false;
            }
        }

        public bool IsInitialized { [DebuggerNonUserCode]
            get; private set;
        }

        public bool IsReadOnly {
            [DebuggerNonUserCode]
            get => this._isReadOnly;
            set {
                this._isReadOnly = value;
                if (!value)
                    return;
                this._allowEdit = false;
                this._isFixedSize = true;
                this._allowRemove = false;
            }
        }

        public bool IsSorted { get => true; }

        public virtual ReadOnlyDictionary<string, PropertyDescriptor> ItemProperties {
            get {
                if (this._itemProperties == null)
                    this._itemProperties = this.GetItemProperties();
                return this._itemProperties;
            }
        }

        public string Name { [DebuggerNonUserCode]
            get; [DebuggerNonUserCode]
            set; }

        public DataStoreNewObjectCallback<TItem> NewObjectCallback { [DebuggerNonUserCode]
            get; [DebuggerNonUserCode]
            set; }

        public IPrimaryKey PrimaryKey { [DebuggerNonUserCode]
            get; private set;
        }

        public PropertyDescriptor PrimaryKeyProperty { [DebuggerNonUserCode]
            get; private set;
        }

        public DataStoreRemoveObjectCallback<TItem> RemoveObjectCallback { get; set; }

        public ListSortDirection SortDirection {
            [DebuggerNonUserCode]
            get => this.SortIndex.SortDirection;
        }

        public IIndex SortIndex { [DebuggerNonUserCode]
            get; private set;
        }

        public PropertyDescriptor SortProperty { [DebuggerNonUserCode]
            get; private set;
        }

        public bool SupportsChangeNotification {
            [DebuggerNonUserCode]
            get => true;
        }

        public virtual int Add(TItem item) => this.Insert(this.Count, item);

        public void AddIndex(string propertyName, ListSortDirection direction) => this.AddIndex(this.ItemProperties[propertyName], direction);

        public void AddIndex(string propertyName, IIndex index) => this.AddIndex(this.ItemProperties[propertyName], index);

        public void AddIndex(PropertyDescriptor property, ListSortDirection direction) {
            this.GetItemPropertyIndex(property, direction);
            this.FindIndexInternal(property, ListSortDirection.Ascending).IsPublic = true;
        }

        public void AddIndex(PropertyDescriptor property, IIndex index) {
            Check.NotNullArg(nameof(property), property);
            Check.NotNullArg(nameof(index), index);
            DataStore<TItem>.IndexValue indexInternal = this.FindIndexInternal(property, index.SortDirection);
            if (indexInternal != null) {
                if (indexInternal.IsPublic)
                    throw ExceptionHelper.GetInvalidOperation("Index already exists for property '{0}'", (object)property.Name);
                this.RemoveIndexInternal(property, index.SortDirection);
            }
            this._indexes.Add(new DataStore<TItem>.IndexKey(property, index.SortDirection), new DataStore<TItem>.IndexValue(index, true));
            this.ResetIndex(property, index);
        }

        public TItem AddNew() {
            if (this.NewObjectCallback == null)
                throw ExceptionHelper.GetInvalidOperation("Cannot create new objects without specified {0} callback.", (object)typeof(DataStoreNewObjectCallback<>).Name);
            TItem obj = this.NewObjectCallback();
            this.Add(obj);
            return obj;
        }

        public virtual void AddPropertyComparer(string propertyName, IComparer comparer) => this.AddPropertyComparer(this.ItemProperties[propertyName], comparer);

        public virtual void AddPropertyComparer(PropertyDescriptor property, IComparer comparer) {
            if (this._itemPropertyComparer == null)
                this._itemPropertyComparer = new Dictionary<PropertyDescriptor, IComparer>();
            if (this._itemPropertyComparer.ContainsKey(property))
                this._itemPropertyComparer.Remove(property);
            this._itemPropertyComparer.Add(property, comparer);
        }

        public virtual void AddRange(IEnumerable<TItem> collection) {
            bool changedNotification = this._suspendChangedNotification;
            this._suspendChangedNotification = true;
            foreach (TItem obj in collection)
                this.Insert(this.Count, obj);
            this._suspendChangedNotification = changedNotification;
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        public virtual void ApplySort(PropertyDescriptor property, ListSortDirection direction) {
            IIndex itemPropertyIndex = this.GetItemPropertyIndex(property, direction);
            this.SortProperty = property;
            this.SortIndex = itemPropertyIndex;
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        public virtual void ApplySort(string propertyName, ListSortDirection direction) => this.ApplySort(this.ItemProperties[propertyName], direction);

        public virtual void BeginInit() {
            this.IsInitialized = false;
            this._suspendChangedNotification = true;
        }

        public virtual void Clear() {
            if (this.Count == 0)
                return;
            bool flag = false;
            if (!this._suspendChangedNotification) {
                flag = true;
                this.BeginInit();
            }
            int count = this.Count;
            int num = 0;
            while (this.Count != 0) {
                ++num;
                this.RemoveAt(0);
            }
            if (!flag)
                return;
            this.EndInit();
        }

        public virtual bool Contains(TItem item) => this.PrimaryKey.Find(this.PrimaryKeyProperty.GetValue((object)item), IndexFindOption.Equal).Length != 0;

        public virtual void CopyTo(TItem[] array, int arrayIndex) {
            Array all = this.PrimaryKey.GetAll(this.PrimaryKey.SortDirection);
            for (int index = 0; index < all.Length; ++index) {
                DataStore<TItem>.PrimaryKeyEntry primaryKeyEntry = (DataStore<TItem>.PrimaryKeyEntry)all.GetValue(index);
                array[arrayIndex + index] = primaryKeyEntry.Value;
            }
        }

        public virtual void EndInit() {
            this.IsInitialized = true;
            this._suspendChangedNotification = false;
            this.OnInitialized();
            if (!this._changedWhileSuspended)
                return;
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            this._changedWhileSuspended = false;
        }

        public virtual TItem Find(string propertyName, object key) => this.Find(this.ItemProperties[propertyName], key);

        public virtual TItem Find(
          string propertyName,
          IndexFindOption option,
          object key,
          params object[] keys) {
            return this.Find(this.ItemProperties[propertyName], option, key, keys);
        }

        public virtual TItem Find(PropertyDescriptor property, object key) => this.Find(property, IndexFindOption.Equal, key, (object[])null);

        public virtual TItem Find(
          PropertyDescriptor property,
          IndexFindOption option,
          object key,
          params object[] keys) {
            if (this.PrimaryKeyProperty.Name == property.Name) {
                DataStore<TItem>.PrimaryKeyEntry primaryKeyEntry = (DataStore<TItem>.PrimaryKeyEntry)this.PrimaryKey.FindValue(key);
                return primaryKeyEntry != null ? primaryKeyEntry.Value : default(TItem);
            }
            Array array = this.GetItemPropertyIndex(property, this.PrimaryKey.SortDirection).Find(option, key, keys);
            return array.Length != 0 ? ((DataStore<TItem>.PrimaryKeyEntry)this.PrimaryKey.FindValue(array.GetValue(0))).Value : default(TItem);
        }

        public virtual ReadOnlyCollection<TItem> FindAll(
          string propertyName,
          IndexFindOption option,
          object key,
          params object[] keys) {
            return this.FindAll(this.ItemProperties[propertyName], option, key, keys);
        }

        public virtual ReadOnlyCollection<TItem> FindAll(
          PropertyDescriptor property,
          IndexFindOption option,
          object key,
          params object[] keys) {
            List<TItem> list;
            if (this.PrimaryKeyProperty == property) {
                Array array = this.PrimaryKey.Find(option, key, keys);
                list = new List<TItem>(array.Length);
                foreach (DataStore<TItem>.PrimaryKeyEntry primaryKeyEntry in array)
                    list.Add(primaryKeyEntry.Value);
            }
            else {
                Array array = this.GetItemPropertyIndex(property, this.PrimaryKey.SortDirection).Find(option, key, keys);
                list = new List<TItem>(array.Length);
                foreach (object key1 in array) {
                    DataStore<TItem>.PrimaryKeyEntry primaryKeyEntry = (DataStore<TItem>.PrimaryKeyEntry)this.PrimaryKey.FindValue(key1);
                    list.Add(primaryKeyEntry.Value);
                }
            }
            return new ReadOnlyCollection<TItem>((IList<TItem>)list);
        }

        public IIndex FindIndex(PropertyDescriptor property, ListSortDirection sortDirection) {
            DataStore<TItem>.IndexValue indexInternal = this.FindIndexInternal(property, sortDirection);
            return indexInternal != null && indexInternal.IsPublic ? indexInternal.Index : (IIndex)null;
        }

        public IEnumerator<TItem> GetEnumerator() {
            int pos = -1;
            while (++pos < this.Count) {
                DataStore<TItem>.PrimaryKeyEntry entry = this.SortIndex != this.PrimaryKey ? (DataStore<TItem>.PrimaryKeyEntry)this.PrimaryKey.FindValue(this.SortIndex.GetValueAt(pos)) : (DataStore<TItem>.PrimaryKeyEntry)this.SortIndex.GetValueAt(pos);
                yield return entry.Value;
            }
        }

        public int IndexOf(TItem item) => this.IndexOf(this.PrimaryKeyProperty, this.PrimaryKeyProperty.GetValue((object)item));

        public virtual int IndexOf(string propertyName, object key) => this.IndexOf(this.ItemProperties[propertyName], key);

        public virtual int IndexOf(PropertyDescriptor property, object key) {
            int num = -1;
            if (property == this.PrimaryKeyProperty) {
                num = this.PrimaryKey.IndexOf(key);
            }
            else {
                Array array = this.GetItemPropertyIndex(property, this.PrimaryKey.SortDirection).Find(key, IndexFindOption.Equal);
                if (array.Length != 0)
                    return this.PrimaryKey.IndexOf(((DataStore<TItem>.PrimaryKeyEntry)array.GetValue(0)).Key);
            }
            return num;
        }

        public virtual int Insert(int index, TItem item) {
            object key = this.PrimaryKeyProperty.GetValue(item);
            DataStore<TItem>.PrimaryKeyEntry pkEntry = new DataStore<TItem>.PrimaryKeyEntry(key, item);
            this.PrimaryKey.Add(key, pkEntry);

            if (item is INotifyPropertyChanging propertyChanging) {
                propertyChanging.PropertyChanging += new PropertyChangingEventHandler(this.NotifyingItem_PropertyChanging);
            }

            if (item is INotifyPropertyChanged notifyPropertyChanged) {
                notifyPropertyChanged.PropertyChanged += new PropertyChangedEventHandler(this.NotifyingItem_PropertyChanged);
            }

            this.UpdateIndexes(ListChangedType.ItemAdded, item, pkEntry);
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
            return index;
        }

        public virtual bool Remove(TItem item) {
            int index = this.IndexOf(item);
            if (index == -1)
                return false;
            this.RemoveAt(index);
            return true;
        }

        public virtual void RemoveAt(int index) {
            DataStore<TItem>.PrimaryKeyEntry valueAt = (DataStore<TItem>.PrimaryKeyEntry)this.PrimaryKey.GetValueAt(index);
            TItem objectToRemove = valueAt.Value;
            this.PrimaryKey.Remove(valueAt.Key);
            this.RemoveObjectCallback?.Invoke(objectToRemove);

            if (objectToRemove is INotifyPropertyChanging propertyChanging)
                propertyChanging.PropertyChanging -= new PropertyChangingEventHandler(this.NotifyingItem_PropertyChanging);

            if (objectToRemove is INotifyPropertyChanged notifyPropertyChanged)
                notifyPropertyChanged.PropertyChanged -= new PropertyChangedEventHandler(this.NotifyingItem_PropertyChanged);

            this.UpdateIndexes(ListChangedType.ItemDeleted, objectToRemove, valueAt);
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
        }

        public virtual void RemoveIndex(PropertyDescriptor property, ListSortDirection direction) {
            DataStore<TItem>.IndexValue indexInternal = this.FindIndexInternal(property, direction);
            if (indexInternal == null || !indexInternal.IsPublic)
                throw ExceptionHelper.GetInvalidOperation("Index for property {0} was not present in store", (object)property.Name);
            this.RemoveIndexInternal(property, direction);
        }

        public virtual void RemoveSort() {
            this.SortProperty = this.PrimaryKeyProperty;
            this.SortIndex = (IIndex)this.PrimaryKey;
        }

        public virtual void Reverse() => this.SortIndex = this.GetItemPropertyIndex(this.SortProperty, this.SortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending);

        public void SetPrimaryKey(PropertyDescriptor primaryKeyProperty, IPrimaryKey primaryKey) {
            primaryKey.Clear();
            if (this.PrimaryKey != null) {
                foreach (IPrimaryKeyNode primaryKeyNode in (IEnumerable<IIndexNode>)this.PrimaryKey) {
                    DataStore<TItem>.PrimaryKeyEntry primaryKeyEntry = (DataStore<TItem>.PrimaryKeyEntry)primaryKeyNode.Value;
                    object key = primaryKeyProperty.GetValue((object)primaryKeyEntry.Value);
                    primaryKeyEntry.Key = key;
                    primaryKey.Add(key, (object)primaryKeyEntry);
                }
                this.PrimaryKey.Clear();
            }
            if (this.SortProperty == this.PrimaryKeyProperty)
                this.SortProperty = primaryKeyProperty;
            this.PrimaryKey = primaryKey;
            this.PrimaryKeyProperty = primaryKeyProperty;
            this.ResetIndexes(false);
            this.OnPrimaryKeyChanged();
        }

        protected virtual ReadOnlyDictionary<string, PropertyDescriptor> GetItemProperties() {
            TItem component = default(TItem);
            for (int index = 0; index < this.Count; ++index) {
                component = this[index];
                if ((object)component != null)
                    break;
            }
            PropertyDescriptorCollection dynamicProperties = PropertyDescriptorHelper.GetDynamicProperties((object)component == null ? PropertyDescriptorHelper.GetProperties(this.GetType().GetGenericArguments()[0]) : PropertyDescriptorHelper.GetProperties((object)component));
            this._itemPropertiesInternal = new Dictionary<string, PropertyDescriptor>(dynamicProperties.Count);
            ReadOnlyDictionary<string, PropertyDescriptor> itemProperties = new ReadOnlyDictionary<string, PropertyDescriptor>((IDictionary<string, PropertyDescriptor>)this._itemPropertiesInternal);
            foreach (PropertyDescriptor propertyDescriptor in dynamicProperties)
                this._itemPropertiesInternal.Add(propertyDescriptor.Name, propertyDescriptor);
            return itemProperties;
        }

        protected virtual IIndex GetItemPropertyIndex(
          PropertyDescriptor property,
          ListSortDirection sortDirection) {
            DataStore<TItem>.IndexValue indexInternal = this.FindIndexInternal(property, sortDirection);
            if (indexInternal != null)
                return indexInternal.Index;
            IIndex index;
            if (property.PropertyType == typeof(int))
                index = (IIndex)new RedBlackTree(sortDirection);
            else if (property.PropertyType == typeof(string))
                index = (IIndex)new RedBlackTree<string, object>((IComparer<string>)StringComparer.CurrentCultureIgnoreCase, sortDirection);
            else if (property.PropertyType == typeof(DateTime)) {
                index = (IIndex)new RedBlackTree<DateTime, object>((IComparer<DateTime>)new DateTimeComparer(), sortDirection);
            }
            else {
                IComparer propertyComparer = this.GetItemPropertyComparer(property);
                Type type = typeof(IComparer<>).MakeGenericType(property.PropertyType);
                index = (IIndex)typeof(RedBlackTree<,>).MakeGenericType(property.PropertyType, typeof(object)).GetConstructor(new Type[2]
                {
          type,
          typeof (ListSortDirection)
                }).Invoke(new object[2]
                {
          (object) propertyComparer,
          (object) sortDirection
                });
            }
            this.ResetIndex(property, index);
            this._indexes.Add(new DataStore<TItem>.IndexKey(property, sortDirection), new DataStore<TItem>.IndexValue(index, false));
            return index;
        }

        protected virtual IComparer GetItemPropertyComparer(PropertyDescriptor property) {
            if (this._itemPropertyComparer == null)
                this._itemPropertyComparer = new Dictionary<PropertyDescriptor, IComparer>();
            IComparer comparer;
            if (!this._itemPropertyComparer.TryGetValue(property, out comparer)) {
                if (property.PropertyType == typeof(string))
                    comparer = (IComparer)StringComparer.CurrentCultureIgnoreCase;
                else if (property.PropertyType == typeof(int))
                    comparer = (IComparer)new Int32Comparer();
                else if (property.PropertyType == typeof(DateTime)) {
                    comparer = (IComparer)new DateTimeComparer();
                }
                else {
                    bool flag = false;
                    foreach (Type type in property.PropertyType.GetInterfaces()) {
                        if (type == typeof(IComparable))
                            flag = true;
                    }
                    if (flag)
                        comparer = (IComparer)typeof(Comparer<>).MakeGenericType(property.PropertyType).GetProperty("Default", BindingFlags.Static | BindingFlags.Public).GetValue((object)null, (object[])null);
                    else
                        comparer = (IComparer)null;
                }
                this.AddPropertyComparer(property, comparer);
            }
            return comparer;
        }

        protected virtual void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("PrimaryKeyProperty", (object)this.PrimaryKeyProperty.Name);
            info.AddValue("PrimaryKey", (object)this.PrimaryKey);
            info.AddValue("SortProperty", (object)this.SortProperty.Name);
            info.AddValue("SortDirection", (object)this.SortDirection);
            TItem[] array = new TItem[this.Count];
            this.CopyTo(array, 0);
            info.AddValue("Items", (object)array);
        }

        protected virtual void OnInitialized() {
            if (this.Initialized == null)
                return;
            this.Initialized((object)this, EventArgs.Empty);
        }

        protected virtual void OnListChanged(ListChangedEventArgs e) {
            if (!this._suspendChangedNotification) {
                if (this.ListChanged == null)
                    return;
                this.ListChanged((object)this, e);
            }
            else
                this._changedWhileSuspended = true;
        }

        protected virtual void OnPrimaryKeyChanged() {
            if (this.PrimaryKeyChanged == null)
                return;
            this.PrimaryKeyChanged((object)this, EventArgs.Empty);
        }

        protected virtual void RemoveIndexInternal(
          PropertyDescriptor property,
          ListSortDirection sortDirection) {
            DataStore<TItem>.IndexKey key = new DataStore<TItem>.IndexKey(property, sortDirection);
            DataStore<TItem>.IndexValue indexValue;
            if (this._indexes.TryGetValue(key, out indexValue)) {
                IIndex index = indexValue.Index;
                foreach (IPrimaryKeyNode primaryKeyNode in (IEnumerable<IIndexNode>)this.PrimaryKey)
                    ((DataStore<TItem>.PrimaryKeyEntry)primaryKeyNode.Value).RemoveIndexRelation(index);
                this._indexes.Remove(key);
            }
            else
                throw ExceptionHelper.GetInvalidOperation("Index for property {0} was not present in store", (object)property.Name);
        }

        protected virtual void ResetIndex(PropertyDescriptor property, IIndex index) {
            index.Clear();
            foreach (IPrimaryKeyNode primaryKeyNode in (IEnumerable<IIndexNode>)this.PrimaryKey) {
                DataStore<TItem>.PrimaryKeyEntry primaryKeyEntry = (DataStore<TItem>.PrimaryKeyEntry)primaryKeyNode.Value;
                object key = property.GetValue((object)primaryKeyEntry.Value);
                index.Add(key, primaryKeyEntry.Key);
                primaryKeyEntry.AddIndexRelation(index, key);
            }
        }

        protected virtual void ResetIndexes(bool resetPrimaryKey) {
            if (resetPrimaryKey && this.PrimaryKey != null) {
                List<DataStore<TItem>.PrimaryKeyEntry> primaryKeyEntryList = new List<DataStore<TItem>.PrimaryKeyEntry>(this.PrimaryKey.Count);
                foreach (IPrimaryKeyNode primaryKeyNode in (IEnumerable<IIndexNode>)this.PrimaryKey) {
                    DataStore<TItem>.PrimaryKeyEntry primaryKeyEntry = (DataStore<TItem>.PrimaryKeyEntry)primaryKeyNode.Value;
                    primaryKeyEntryList.Add(primaryKeyEntry);
                }
                this.PrimaryKey.Clear();
                foreach (DataStore<TItem>.PrimaryKeyEntry primaryKeyEntry in primaryKeyEntryList) {
                    object key = this.PrimaryKeyProperty.GetValue((object)primaryKeyEntry.Value);
                    primaryKeyEntry.Key = key;
                    this.PrimaryKey.Add(key, (object)primaryKeyEntry);
                }
                this.ResetIndexes(false);
            }
            else {
                List<DataStore<TItem>.IndexKey> indexKeyList = new List<DataStore<TItem>.IndexKey>();
                foreach (KeyValuePair<DataStore<TItem>.IndexKey, DataStore<TItem>.IndexValue> index1 in this._indexes) {
                    PropertyDescriptor property = index1.Key.Property;
                    IIndex index2 = index1.Value.Index;
                    if (index1.Value.IsPublic) {
                        this.ResetIndex(property, index2);
                    }
                    else {
                        index2.Clear();
                        indexKeyList.Add(index1.Key);
                    }
                }
                foreach (DataStore<TItem>.IndexKey key in indexKeyList) {
                    this.RemoveIndexInternal(key.Property, key.SortDirection);
                    this._indexes.Remove(key);
                }
            }
        }

        protected virtual void UpdateIndexes(ListChangedType changeType, TItem item) => this.UpdateIndexes(changeType, item, (DataStore<TItem>.PrimaryKeyEntry)null);

        private DataStore<TItem>.IndexValue FindIndexInternal(
          PropertyDescriptor pd,
          ListSortDirection sortDirection) {
            if (pd == this.PrimaryKeyProperty && sortDirection == this.PrimaryKey.SortDirection)
                return new DataStore<TItem>.IndexValue((IIndex)this.PrimaryKey, true);
            DataStore<TItem>.IndexValue indexInternal;
            this._indexes.TryGetValue(new DataStore<TItem>.IndexKey(pd, sortDirection), out indexInternal);
            return indexInternal;
        }

        private void UpdateIndex(
          ListChangedType changeType,
          IIndex index,
          PropertyDescriptor property,
          TItem item,
          DataStore<TItem>.PrimaryKeyEntry pkEntry) {
            if (pkEntry == null)
                pkEntry = (DataStore<TItem>.PrimaryKeyEntry)this.PrimaryKey.FindValue(this.PrimaryKeyProperty.GetValue((object)item));
            switch (changeType) {
                case ListChangedType.Reset:
                    this.ResetIndex(property, index);
                    break;
                case ListChangedType.ItemAdded:
                    object key = property.GetValue((object)item);
                    index.Add(key, pkEntry.Key);
                    pkEntry.AddIndexRelation(index, key);
                    break;
                case ListChangedType.ItemDeleted:
                    object indexRelation1 = pkEntry.GetIndexRelation(index);
                    index.Remove(indexRelation1, pkEntry.Key, true);
                    break;
                case ListChangedType.ItemChanged:
                    object obj = property.GetValue((object)pkEntry.Value);
                    object indexRelation2 = pkEntry.GetIndexRelation(index);
                    IComparer propertyComparer = this.GetItemPropertyComparer(property);
                    if (propertyComparer != null) {
                        if (propertyComparer.Compare(obj, indexRelation2) == 0)
                            break;
                        index.Remove(indexRelation2, pkEntry.Key, true);
                        index.Add(obj, pkEntry.Key);
                        break;
                    }
                    if (obj == indexRelation2)
                        break;
                    index.Remove(indexRelation2, pkEntry.Key, true);
                    index.Add(obj, pkEntry.Key);
                    break;
            }
        }

        private void UpdateIndexes(
          ListChangedType changeType,
          TItem item,
          DataStore<TItem>.PrimaryKeyEntry pkEntry) {
            if (pkEntry == null) {
                object key = this.PrimaryKeyProperty.GetValue((object)item);
                pkEntry = (DataStore<TItem>.PrimaryKeyEntry)this.PrimaryKey.FindValue(key);
                bool flag = pkEntry != null && object.Equals((object)item, (object)pkEntry.Value);
                switch (changeType) {
                    case ListChangedType.Reset:
                        throw ExceptionHelper.GetInvalidOperation("PrimaryKey cannot be resetted and update all indexes");
                    case ListChangedType.ItemAdded:
                        this.PrimaryKey.Add(key, (object)pkEntry);
                        this.UpdateIndexes(changeType, item, pkEntry);
                        break;
                    case ListChangedType.ItemDeleted:
                        if (!flag) {
                            if (this._changingProperty == this.PrimaryKeyProperty && this._changingValue != null) {
                                pkEntry = (DataStore<TItem>.PrimaryKeyEntry)this.PrimaryKey.FindValue(this._changingValue);
                                if (pkEntry != null) {
                                    this.PrimaryKey.Remove(pkEntry.Key);
                                    pkEntry.Key = key;
                                    this.PrimaryKey.Add(key, (object)pkEntry);
                                    this.UpdateIndexes(changeType, item, pkEntry);
                                }
                                else
                                    this.ResetIndexes(true);
                                this._changingProperty = (PropertyDescriptor)null;
                                this._changingValue = (object)null;
                                break;
                            }
                            this.ResetIndexes(true);
                            break;
                        }
                        this.UpdateIndexes(changeType, item, pkEntry);
                        break;
                    case ListChangedType.ItemChanged:
                        if (!flag) {
                            this.ResetIndexes(true);
                            break;
                        }
                        this.UpdateIndexes(changeType, item, pkEntry);
                        break;
                }
            }
            else {
                foreach (KeyValuePair<DataStore<TItem>.IndexKey, DataStore<TItem>.IndexValue> index1 in this._indexes) {
                    PropertyDescriptor property = index1.Key.Property;
                    IIndex index2 = index1.Value.Index;
                    this.UpdateIndex(changeType, index2, property, item, pkEntry);
                }
            }
        }

        private void NotifyingItem_PropertyChanging(object sender, PropertyChangingEventArgs e) {
            this._changingProperty = this.ItemProperties[e.PropertyName];
            if (!this._changingProperty.Equals((object)this.PrimaryKeyProperty))
                return;
            if (this._changingProperty.PropertyType.IsValueType) {
                this._changingValue = this._changingProperty.GetValue(sender);
            }
            else {
                Type type = typeof(ICloneable);
                if (this._changingProperty.PropertyType.GetInterface(string.Format("{0}.{1}", (object)type.Namespace, (object)type.Name)) == null)
                    return;
                this._changingValue = ((ICloneable)this._changingProperty.GetValue(sender)).Clone();
            }
        }

        private void NotifyingItem_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            TItem component = (TItem)sender;
            if (this.PrimaryKeyProperty.Name == e.PropertyName) {
                if (this._changingProperty != null && this._changingProperty.Name == e.PropertyName && this._changingValue != null) {
                    object key = this.PrimaryKeyProperty.GetValue((object)component);
                    if (key == this._changingValue)
                        return;
                    DataStore<TItem>.PrimaryKeyEntry primaryKeyEntry = (DataStore<TItem>.PrimaryKeyEntry)this.PrimaryKey.FindValue(this._changingValue);
                    if (this.PrimaryKey.FindValue(key) != null)
                        throw IndexExceptionHelper.GetPrimaryKeyViolation(key);
                    this.PrimaryKey.Remove(this._changingValue);
                    primaryKeyEntry.Key = key;
                    this.PrimaryKey.Add(key, (object)primaryKeyEntry);
                }
                else
                    this.UpdateIndexes(ListChangedType.ItemChanged, component, (DataStore<TItem>.PrimaryKeyEntry)null);
            }
            else {
                DataStore<TItem>.PrimaryKeyEntry pkEntry = (DataStore<TItem>.PrimaryKeyEntry)this.PrimaryKey.FindValue(this.PrimaryKeyProperty.GetValue((object)component));
                this.UpdateIndexes(ListChangedType.ItemChanged, component, pkEntry);
            }
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, this.IndexOf(component), this.ItemProperties[e.PropertyName]));
            this._changingValue = (object)null;
            this._changingProperty = (PropertyDescriptor)null;
        }



    }
}
