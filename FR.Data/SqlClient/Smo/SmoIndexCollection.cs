// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoIndexCollection
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

namespace FR.Data.SqlClient.Smo {
    public class SmoIndexCollection : SmoCollection<SmoIndex> {
        internal SmoIndexCollection(SmoTable table)
          : base((SmoObject)table) {
        }

        internal SmoIndexCollection(SmoView view)
          : base((SmoObject)view) {
        }

        public new int Add(SmoIndex index) {
            this.Add(index);
            return this.Count;
        }

        public new void Remove(SmoIndex index) {
            foreach (SmoIndex smoIndex in (SmoCollection<SmoIndex>)this) {
                if (smoIndex.Equals((object)index))
                    smoIndex.SetState(SmoObjectState.Deleted);
            }
        }

        public override SmoIndex FindByName(string name, SmoScriptOptions options) {
            SmoIndex byName = base.FindByName(name, options);
            return byName != null && byName.State != SmoObjectState.Deleted ? byName : (SmoIndex)null;
        }
    }
}
