using FR.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsDog.FileSystem {
    public class DogItemList : ObservableCollection<DogItem>, ITypedList, IListSource {
        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors) {
            var pis = typeof(DogItem).GetProperties();
            var pds = pis.Select(pi => new ReflectionPropertyDescriptor(pi))
                        .Where(pd => listAccessors == null || listAccessors.Any(x => pd.Name.Equals(x.Name)))
                        .ToArray();

            return new PropertyDescriptorCollection(pds, true);
        }

        public string Name { get; set; }

        public bool ContainsListCollection => false;

        public string GetListName(PropertyDescriptor[] listAccessors) => Name;

        public void SetFilter(string filter) {
            System.Data.DataView dt;
        }

        public IList GetList() {
            return new DogItemListView(this);
        }
    }
}
