using FR;
using FR.Collections;
using FR.Collections.Generic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsDog.FileSystem {
    public class DogItemListView : ObservableCollection<DogItem>, IBindingListView {
        private DogItemList _all;
        private ViewFilter _filter;

        public DogItemListView(DogItemList all) : base(all) {
            _all = all;
        }

        public ViewFilter Filter {
            get => _filter;
            set {
                _filter = value;
                if (_filter == null) {
                    RemoveFilter();
                }
                else {
                    ApplyFilter();
                }
            }
        }

        public ListSortDescriptionCollection SortDescriptions =>
            SortProperty == null ? new ListSortDescriptionCollection() : new ListSortDescriptionCollection(new[] { new ListSortDescription(SortProperty, SortDirection) });

        public bool SupportsAdvancedSorting => true;

        public bool SupportsFiltering => true;

        public bool AllowNew => false;

        public bool AllowEdit => true;

        public bool AllowRemove => true;

        public bool SupportsChangeNotification => true;

        public bool SupportsSearching => true;

        public bool SupportsSorting => true;

        public bool IsSorted { get => SortProperty != null; }

        public PropertyDescriptor SortProperty { get; private set; }

        public ListSortDirection SortDirection { get; private set; }

        public event ListChangedEventHandler ListChanged;

        public void AddIndex(PropertyDescriptor property) {
            //throw new NotImplementedException();
        }

        public object AddNew() {
            throw new NotImplementedException();
        }

        public void ApplySort(ListSortDescriptionCollection sorts) {
            foreach (var lsd in sorts.Cast<ListSortDescription>()) {
                ApplySort(lsd.PropertyDescriptor, lsd.SortDirection);
                break;
            }
        }

        public void ApplySort(PropertyDescriptor property, ListSortDirection direction) {
            var sorted = ApplySortList(Items, property, direction);
            Items.Clear();
            Items.AddRange(sorted);
            SortProperty = property;
            SortDirection = direction;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public int Find(PropertyDescriptor property, object key) {
            for (int i = 0; i < Count; i++) {
                var pkey = property.GetValue(Items[i]);
                if (pkey == key) {
                    return i;
                }
            }
            return -1;
        }

        public void RemoveFilter() {
            _filter = null;
            IList<DogItem> items = _all;

            if (IsSorted) {
                items = ApplySortList(_all, SortProperty, SortDirection);
            }

            Items.Clear();
            Items.AddRange(items);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void RemoveIndex(PropertyDescriptor property) {
        }

        public void RemoveSort() {
            SortProperty = null;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
            base.OnCollectionChanged(e);
            ListChanged?.Invoke(this, GetListChangedEventArgs(e));
        }

        private List<DogItem> ApplySortList(IList<DogItem> input, PropertyDescriptor property, ListSortDirection direction) {
            IDictionary dict;
            var ptype = property.PropertyType;
            if (BaseHelper.InList(ptype, typeof(long), typeof(long?))) {
                dict = new SortedList<long?, List<DogItem>>();
            }
            else if (ptype == typeof(string)) {
                dict = new SortedList<string, List<DogItem>>();
            }
            else if (BaseHelper.InList(ptype, typeof(DateTime), typeof(DateTime?))) {
                dict = new SortedList<DateTime?, List<DogItem>>();
            }
            else {
                throw new NotSupportedException();
            }
            
            foreach (var item in input) {
                var key = property.GetValue(item);
                var items = (List<DogItem>)dict.GetOrAdd(key, k => new List<DogItem>());
                items.Add(item);
            }

            List<DogItem> result = new List<DogItem>();
            foreach (var item in dict.Values) {
                var list = (List<DogItem>)item;
                result.AddRange(list);
            }

            if (direction == ListSortDirection.Descending) {
                result.Reverse();
            }

            return result;
        }

        private ListChangedEventArgs GetListChangedEventArgs(NotifyCollectionChangedEventArgs e) {
            int count = Math.Max(e.NewItems.NotNull().Count, e.OldItems.NotNull().Count);
            if (count > 1) {
                return new ListChangedEventArgs(ListChangedType.Reset, -1);
            }

            var ni = e.NewItems.NotNull().Cast<DogItem>().FirstOrDefault();
            var oi = e.OldItems.NotNull().Cast<DogItem>().FirstOrDefault();

            switch (e.Action) {
                case NotifyCollectionChangedAction.Add:
                    return new ListChangedEventArgs(ListChangedType.ItemAdded, e.NewStartingIndex);
                case NotifyCollectionChangedAction.Remove:
                    return new ListChangedEventArgs(ListChangedType.ItemDeleted, e.OldStartingIndex);
                case NotifyCollectionChangedAction.Replace:
                    return new ListChangedEventArgs(ListChangedType.ItemChanged, e.NewStartingIndex);
                case NotifyCollectionChangedAction.Move:
                    return new ListChangedEventArgs(ListChangedType.ItemMoved, e.NewStartingIndex, e.OldStartingIndex);
                case NotifyCollectionChangedAction.Reset:
                default:
                    return new ListChangedEventArgs(ListChangedType.Reset, -1);
            }
        }

        private void ApplyFilter() {
            _filter.Prepare();

            var items = _all.Where(item => _filter.IsValid(item)).ToList();
            if (IsSorted) {
                items = ApplySortList(items, SortProperty, SortDirection);
            }

            Items.Clear();
            Items.AddRange(items);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        string IBindingListView.Filter {
            get => _filter?.ToJsonString();
            set => _filter = ViewFilter.FromJsonString(value);
        }
    }
}
