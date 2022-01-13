using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace FR.Collections.Generic {
    public partial class DataStore<TItem> {

        [DebuggerNonUserCode]
        IEnumerator IEnumerable.GetEnumerator() => (IEnumerator)this.GetEnumerator();

        [DebuggerNonUserCode]
        int IList.Add(object value) => this.Add((TItem)value);

        [DebuggerNonUserCode]
        void IList.Clear() => this.Clear();

        [DebuggerNonUserCode]
        bool IList.Contains(object value) => this.Contains((TItem)value);

        [DebuggerNonUserCode]
        int IList.IndexOf(object value) => this.IndexOf((TItem)value);

        [DebuggerNonUserCode]
        void IList.Insert(int index, object value) => this.Insert(index, (TItem)value);

        bool IList.IsFixedSize {
            [DebuggerNonUserCode]
            get => this.IsFixedSize;
        }

        bool IList.IsReadOnly {
            [DebuggerNonUserCode]
            get => this.IsReadOnly;
        }

        [DebuggerNonUserCode]
        void IList.Remove(object value) => this.Remove((TItem)value);

        [DebuggerNonUserCode]
        void IList.RemoveAt(int index) => this.RemoveAt(index);

        object IList.this[int index] {
            [DebuggerNonUserCode]
            get => (object)this[index];
            [DebuggerNonUserCode]
            set => this[index] = (TItem)value;
        }

        [DebuggerNonUserCode]
        void ICollection.CopyTo(Array array, int index) => this.CopyTo((TItem[])array, index);

        int ICollection.Count {
            [DebuggerNonUserCode]
            get => this.Count;
        }

        bool ICollection.IsSynchronized {
            [DebuggerNonUserCode]
            get => false;
        }

        object ICollection.SyncRoot {
            [DebuggerNonUserCode]
            get => (object)null;
        }

        [DebuggerNonUserCode]
        void IBindingList.AddIndex(PropertyDescriptor property) => this.AddIndex(property, ListSortDirection.Ascending);

        [DebuggerNonUserCode]
        object IBindingList.AddNew() => (object)this.AddNew();

        bool IBindingList.AllowEdit {
            [DebuggerNonUserCode]
            get => this.AllowEdit;
        }

        bool IBindingList.AllowNew {
            [DebuggerNonUserCode]
            get => this.AllowNew;
        }

        bool IBindingList.AllowRemove {
            [DebuggerNonUserCode]
            get => this.AllowRemove;
        }

        [DebuggerNonUserCode]
        void IBindingList.ApplySort(
          PropertyDescriptor property,
          ListSortDirection direction) {
            this.ApplySort(property, direction);
        }

        [DebuggerNonUserCode]
        int IBindingList.Find(PropertyDescriptor property, object key) => this.IndexOf(property, key);

        bool IBindingList.IsSorted {
            [DebuggerNonUserCode]
            get => this.IsSorted;
        }

        event ListChangedEventHandler IBindingList.ListChanged {
            [DebuggerNonUserCode]
            add => this.ListChanged += value;
            [DebuggerNonUserCode]
            remove => this.ListChanged -= value;
        }

        [DebuggerNonUserCode]
        void IBindingList.RemoveIndex(PropertyDescriptor property) => this.RemoveIndex(property, ListSortDirection.Ascending);

        [DebuggerNonUserCode]
        void IBindingList.RemoveSort() => this.RemoveSort();

        ListSortDirection IBindingList.SortDirection {
            [DebuggerNonUserCode]
            get => this.SortDirection;
        }

        PropertyDescriptor IBindingList.SortProperty {
            [DebuggerNonUserCode]
            get => this.SortProperty;
        }

        bool IBindingList.SupportsChangeNotification {
            [DebuggerNonUserCode]
            get => this.SupportsChangeNotification;
        }

        bool IBindingList.SupportsSearching {
            [DebuggerNonUserCode]
            get => true;
        }

        bool IBindingList.SupportsSorting {
            [DebuggerNonUserCode]
            get => true;
        }

        [DebuggerNonUserCode]
        void ISupportInitialize.BeginInit() => this.BeginInit();

        [DebuggerNonUserCode]
        void ISupportInitialize.EndInit() => this.EndInit();

        event EventHandler ISupportInitializeNotification.Initialized {
            [DebuggerNonUserCode]
            add => this.Initialized += value;
            [DebuggerNonUserCode]
            remove => this.Initialized -= value;
        }

        bool ISupportInitializeNotification.IsInitialized {
            [DebuggerNonUserCode]
            get => this.IsInitialized;
        }

        [DebuggerNonUserCode]
        PropertyDescriptorCollection ITypedList.GetItemProperties(
          PropertyDescriptor[] listAccessors) {
            PropertyDescriptor[] propertyDescriptorArray = new PropertyDescriptor[this.ItemProperties.Count];
            this.ItemProperties.Values.CopyTo(propertyDescriptorArray, 0);
            return new PropertyDescriptorCollection(propertyDescriptorArray);
        }

        [DebuggerNonUserCode]
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors) => this.Name;

        [DebuggerNonUserCode]
        int IList<TItem>.IndexOf(TItem item) => this.IndexOf(item);

        [DebuggerNonUserCode]
        void IList<TItem>.Insert(int index, TItem item) => this.Insert(index, item);

        [DebuggerNonUserCode]
        void IList<TItem>.RemoveAt(int index) => this.RemoveAt(index);

        TItem IList<TItem>.this[int index] {
            [DebuggerNonUserCode]
            get => this[index];
            [DebuggerNonUserCode]
            set => this[index] = value;
        }

        [DebuggerNonUserCode]
        void ICollection<TItem>.Add(TItem item) => this.Add(item);

        [DebuggerNonUserCode]
        void ICollection<TItem>.Clear() => this.Clear();

        [DebuggerNonUserCode]
        bool ICollection<TItem>.Contains(TItem item) => this.Contains(item);

        [DebuggerNonUserCode]
        void ICollection<TItem>.CopyTo(TItem[] array, int arrayIndex) => this.CopyTo(array, arrayIndex);

        int ICollection<TItem>.Count {
            [DebuggerNonUserCode]
            get => this.Count;
        }

        bool ICollection<TItem>.IsReadOnly {
            [DebuggerNonUserCode]
            get => this.IsReadOnly;
        }

        [DebuggerNonUserCode]
        bool ICollection<TItem>.Remove(TItem item) => this.Remove(item);

        [DebuggerNonUserCode]
        IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator() => this.GetEnumerator();

        [DebuggerNonUserCode]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) => this.GetObjectData(info, context);
    }
}