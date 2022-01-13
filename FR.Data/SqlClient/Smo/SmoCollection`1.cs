// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoCollection`1
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using FR.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace FR.Data.SqlClient.Smo
{
  public class SmoCollection<T> : 
    LoggingProvider,
    IEnumerable<T>,
    IEnumerable,
    IEnumerator<T>,
    IEnumerator,
    IDisposable
        where T : class
  {
    private List<T>.Enumerator _enumerator;
    internal List<T> _list;
    internal SmoObject _parent;
    private bool _readOnly;

    public int Count => this._list.Count;

    public T this[string name] => this.FindByName(name);

    public T this[int index] => this._list[index];

    public bool ReadOnly => this._readOnly;

    internal SmoCollection(SmoObject parent)
    {
      if (!typeof (T).IsSubclassOf(typeof (SmoObject)))
      {
        this.Dispose();
        throw SmoExceptionHelper.GetInvalidCast("Cannot cast objects from type '{0}' to '{1}'", (object) typeof (T), (object) typeof (SmoObject));
      }
      this._readOnly = false;
      this._parent = parent;
      this._list = new List<T>();
      this.SetLoggingProvider((ILoggingProvider) parent);
    }

    public void Dispose()
    {
      if ((object) this._enumerator == null)
        return;
      this._enumerator.Dispose();
    }

    public bool Contains(string name) => this.Contains(name, (SmoScriptOptions) null);

    public bool Contains(string name, SmoScriptOptions options) => !object.ReferenceEquals((object) this.FindByName(name, options), (object) null);

    public virtual T FindByName(string name) => this.FindByName(name, (SmoScriptOptions) null);

    public virtual T FindByName(string name, SmoScriptOptions options)
    {
      name = name.ToLower();
      foreach (T obj in this)
      {
        object byName = (object) obj;
        SmoObject smoObject = (SmoObject) byName;
        if (options == null)
        {
          if (smoObject.Name.ToLower() == name)
            return (T) byName;
        }
        else if (smoObject.GetName(options).ToLower() == name)
          return (T) byName;
      }
      return (T) null;
    }

    public T FindById(int id)
    {
      foreach (T obj in this)
      {
        object byId = (object) obj;
        if (((SmoObject) byId).Id == id)
          return (T) byId;
      }
      return (T) null;
    }

    public T[] ToArray() => this._list.ToArray();

    internal void add(T obj)
    {
      if (this.ReadOnly)
      {
        InvalidOperationException invalidOperation = SmoExceptionHelper.GetInvalidOperation("Collection is read only");
        this.LogEx((Exception) invalidOperation);
        throw invalidOperation;
      }
      this._list.Add(obj);
    }

    internal void clear()
    {
      if (this.ReadOnly)
      {
        InvalidOperationException invalidOperation = SmoExceptionHelper.GetInvalidOperation("Collection is read only");
        this.LogEx((Exception) invalidOperation);
        throw invalidOperation;
      }
      this._list.Clear();
    }

    internal void remove(T obj)
    {
      if (this.ReadOnly)
      {
        InvalidOperationException invalidOperation = SmoExceptionHelper.GetInvalidOperation("Collection is read only");
        this.LogEx((Exception) invalidOperation);
        throw invalidOperation;
      }
      this._list.Remove(obj);
    }

    internal void setReadOnly(bool value) => this._readOnly = value;

    [DebuggerNonUserCode]
    public IEnumerator<T> GetEnumerator() => (IEnumerator<T>) this._list.GetEnumerator();

    [DebuggerNonUserCode]
    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this._list.GetEnumerator();

    public T Current
    {
      [DebuggerNonUserCode] get => this._enumerator.Current;
    }

    object IEnumerator.Current
    {
      [DebuggerNonUserCode] get => (object) this._enumerator.Current;
    }

    [DebuggerNonUserCode]
    public bool MoveNext() => this._enumerator.MoveNext();

    [DebuggerNonUserCode]
    public void Reset() => ((IEnumerator) this._enumerator).Reset();

    void IDisposable.Dispose() => this.Dispose();
  }
}
