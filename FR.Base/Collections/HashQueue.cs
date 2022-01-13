// Decompiled with JetBrains decompiler
// Type: FR.Collections.HashQueue
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections;
using System.Diagnostics;

namespace FR.Collections
{
  public class HashQueue : IDictionary, ICollection, IEnumerable
  {
    private Hashtable _hash;
    private HashQueue.QueueItem _root;
    private HashQueueDuplicateHandling _duplicateHandling;

    public HashQueue()
      : this(100, false)
    {
    }

    public HashQueue(int capacity)
      : this(capacity, false)
    {
    }

    public HashQueue(bool synchronized)
      : this(100, synchronized)
    {
    }

    public HashQueue(int capacity, bool synchronized)
    {
      this._hash = new Hashtable(capacity);
      if (synchronized)
        this._hash = Hashtable.Synchronized(this._hash);
      this._root = new HashQueue.QueueItem();
      this._root.Previous = (HashQueue.QueueItem) null;
      this._root.Last = this._root;
      this._duplicateHandling = HashQueueDuplicateHandling.None;
    }

    public object this[object key] => ((HashQueue.QueueItem) this._hash[key])?.Content;

    public int Count => this._hash.Count;

    public HashQueueDuplicateHandling DuplicateHandling
    {
      get => this._duplicateHandling;
      set => this._duplicateHandling = value;
    }

    public ICollection Keys => this._hash.Keys;

    public bool IsSynchronized => this._hash.IsSynchronized;

    public object SyncRoot => this._hash.SyncRoot;

    public ICollection Values
    {
      [DebuggerNonUserCode] get => this._hash.Values;
    }

    public virtual void Enqueue(object key, object value)
    {
      if (this.IsSynchronized)
      {
        lock (this._hash.SyncRoot)
          this.EnqueueInternal(key, value);
      }
      else
        this.EnqueueInternal(key, value);
    }

    public virtual object Dequeue()
    {
      object obj = (object) null;
      if (this.IsSynchronized)
      {
        lock (this._hash.SyncRoot)
          obj = this.DequeueInternal();
      }
      else
        obj = this.DequeueInternal();
      return obj;
    }

    public virtual object Dequeue(object key)
    {
      if (!this.IsSynchronized)
        return this.DequeueInternal(key);
      lock (this._hash.SyncRoot)
        return this.DequeueInternal(key);
    }

    public virtual void Clear()
    {
      this._root.Last = this._root;
      this._hash.Clear();
    }

    public virtual bool ContainsKey(object key) => this._hash.ContainsKey(key);

    public void CopyTo(Array array, int index) => this._hash.CopyTo(array, index);

    public virtual IDictionaryEnumerator GetEnumerator() => (IDictionaryEnumerator) new HashQueue.Enumerator(this._root);

    public virtual object Peek()
    {
      object obj = (object) null;
      if (this.IsSynchronized)
      {
        lock (this._hash.SyncRoot)
          obj = this.PeekInternal();
      }
      else
        obj = this.PeekInternal();
      return obj;
    }

    private void EnqueueInternal(object key, object value)
    {
      HashQueue.QueueItem queueItem1 = (HashQueue.QueueItem) this._hash[key];
      if (queueItem1 != null)
      {
        if (this._duplicateHandling == HashQueueDuplicateHandling.None)
          throw ExceptionHelper.GetArgument("Duplicate key queing is not allowed");
        if (this._duplicateHandling != HashQueueDuplicateHandling.ReEnqueue)
          return;
        queueItem1.Content = value;
        if (queueItem1.Equals((object) this._root.Last))
          return;
        queueItem1.Previous.Next = queueItem1.Next;
        if (queueItem1.Next != null)
          queueItem1.Next.Previous = queueItem1.Previous;
        this._root.Last.Next = queueItem1;
        this._root.Last = queueItem1;
        queueItem1.Next = (HashQueue.QueueItem) null;
      }
      else
      {
        HashQueue.QueueItem queueItem2 = new HashQueue.QueueItem();
        queueItem2.Content = value;
        queueItem2.Key = key;
        queueItem2.Previous = this._root.Last;
        this._root.Last.Next = queueItem2;
        this._root.Last = queueItem2;
        this._hash.Add(key, (object) queueItem2);
      }
    }

    private object DequeueInternal()
    {
      object obj = (object) null;
      HashQueue.QueueItem next = this._root.Next;
      if (next != null)
      {
        obj = next.Content;
        if (obj != null)
        {
          this._root.Next = next.Next;
          if (next.Next != null)
            next.Next.Previous = this._root;
          this._hash.Remove(next.Key);
        }
        next.Last = (HashQueue.QueueItem) null;
        next.Next = (HashQueue.QueueItem) null;
        next.Previous = (HashQueue.QueueItem) null;
        next.Key = (object) null;
        next.Content = (object) null;
      }
      return obj;
    }

    private object PeekInternal() => this._root.Next?.Content;

    private object DequeueInternal(object key)
    {
      object obj = (object) null;
      HashQueue.QueueItem queueItem = (HashQueue.QueueItem) this._hash[key];
      if (queueItem != null)
      {
        obj = queueItem.Content;
        queueItem.Previous.Next = queueItem.Next;
        if (queueItem.Next != null)
          queueItem.Next.Previous = queueItem.Previous;
      }
      return obj;
    }

    void IDictionary.Add(object key, object value) => this.Enqueue(key, value);

    void IDictionary.Clear() => this.Clear();

    bool IDictionary.Contains(object key) => this.ContainsKey(key);

    IDictionaryEnumerator IDictionary.GetEnumerator() => this.GetEnumerator();

    bool IDictionary.IsFixedSize => false;

    bool IDictionary.IsReadOnly => false;

    ICollection IDictionary.Keys => this.Keys;

    void IDictionary.Remove(object key) => this.Dequeue(key);

    ICollection IDictionary.Values => this.Values;

    object IDictionary.this[object key]
    {
      get => this[key];
      set => this.Enqueue(key, value);
    }

    void ICollection.CopyTo(Array array, int index) => this.CopyTo(array, index);

    int ICollection.Count => this.Count;

    bool ICollection.IsSynchronized => this.IsSynchronized;

    object ICollection.SyncRoot => this.SyncRoot;

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public class Enumerator : IDictionaryEnumerator, IEnumerator
    {
      private HashQueue.QueueItem _root;
      private HashQueue.QueueItem _current;

      internal Enumerator(HashQueue.QueueItem root)
      {
        this._root = root;
        this.Reset();
      }

      public DictionaryEntry Entry => new DictionaryEntry(this.Key, this.Value);

      public object Key => this._current.Key;

      public object Value => this._current.Content;

      public object Current => (object) new DictionaryEntry(this.Key, this.Value);

      public bool MoveNext()
      {
        if (this._current.Next == null)
          return false;
        this._current = this._current.Next;
        return true;
      }

      public void Reset() => this._current = this._root;
    }

    internal class QueueItem
    {
      public HashQueue.QueueItem Previous;
      public HashQueue.QueueItem Next;
      public HashQueue.QueueItem Last;
      public object Key;
      public object Content;

      public override int GetHashCode() => this.Key.GetHashCode();

      public override bool Equals(object obj) => obj is HashQueue.QueueItem queueItem ? this.Key.Equals(queueItem.Key) : this.Key.Equals(obj);
    }
  }
}
