// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoIndexColumnCollection
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using FR.Logging;
using System;

namespace FR.Data.SqlClient.Smo {
    public class SmoIndexColumnCollection : SmoCollection<SmoIndexColumn> {
        internal SmoIndexColumnCollection(SmoIndex index)
          : base((SmoObject)index) {
        }

        private ILogger Log { get; } = LoggingProvider.CreateLogger();

        public new int Add(SmoIndexColumn column) {
            this.Add(column);
            this._parent.SetState(SmoObjectState.Changed);
            return this.Count;
        }

        public SmoIndexColumn Add(string name) {
            SmoIndex parent = (SmoIndex)this._parent;
            if (parent.ParentObject is SmoTable) {
                SmoTableColumn byName = parent.Table.Columns.FindByName(name);
                if (byName == null) {
                    Exception invalidOperation = (Exception)SmoExceptionHelper.GetInvalidOperation("The table column with name '{0}' cannot be found", (object)name);
                    this.Log.Ex(invalidOperation);
                    throw invalidOperation;
                }
                return this.Add(byName);
            }
            SmoViewColumn byName1 = parent.View.Columns.FindByName(name);
            if (byName1 == null) {
                Exception invalidOperation = (Exception)SmoExceptionHelper.GetInvalidOperation("The view column with name '{0}' cannot be found", (object)name);
                this.Log.Ex(invalidOperation);
                throw invalidOperation;
            }
            return this.Add(byName1);
        }

        public SmoIndexColumn Add(SmoTableColumn tableColumn) {
            if (tableColumn == null)
                throw SmoExceptionHelper.GetArgumentNull(nameof(tableColumn));
            if (((SmoIndex)this._parent).Table != tableColumn.Table) {
                Exception invalidOperation = (Exception)SmoExceptionHelper.GetInvalidOperation("The table of the index and the new column have to be equal");
                this.Log.Ex(invalidOperation);
                throw invalidOperation;
            }
            SmoIndexColumn column = new SmoIndexColumn((SmoIndex)this._parent, ref tableColumn);
            this.Add(column);
            return column;
        }

        public SmoIndexColumn Add(SmoViewColumn viewColumn) {
            if (viewColumn == null)
                throw SmoExceptionHelper.GetArgumentNull(nameof(viewColumn));
            if (((SmoIndex)this._parent).View != viewColumn.View) {
                Exception invalidOperation = (Exception)SmoExceptionHelper.GetInvalidOperation("The view of the index and the new column have to be equal");
                this.Log.Ex(invalidOperation);
                throw invalidOperation;
            }
            SmoIndexColumn column = new SmoIndexColumn((SmoIndex)this._parent, ref viewColumn);
            this.Add(column);
            return column;
        }

        public new void Remove(SmoIndexColumn column) {
            foreach (SmoIndexColumn smoIndexColumn in (SmoCollection<SmoIndexColumn>)this) {
                if (smoIndexColumn.Equals((object)column))
                    smoIndexColumn.SetState(SmoObjectState.Deleted);
            }
        }

        public override SmoIndexColumn FindByName(string name, SmoScriptOptions options) {
            SmoIndexColumn byName = base.FindByName(name, options);
            return byName != null && byName.State != SmoObjectState.Deleted ? byName : (SmoIndexColumn)null;
        }
    }
}
