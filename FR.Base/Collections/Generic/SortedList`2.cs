// Decompiled with JetBrains decompiler
// Type: FR.Collections.Generic.SortedList`2
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System.Collections.Generic;

namespace FR.Collections.Generic
{
  public class SortedList<TKey, TValue>
  {
    private SortedDictionary<TKey, SortedList<TKey, TValue>.ValueItem<TValue>> _list;
    private bool _allowDuplicates;

    public SortedList()
    {
      this._list = new SortedDictionary<TKey, SortedList<TKey, TValue>.ValueItem<TValue>>();
      this.AllowDuplicates = false;
    }

    public SortedList(IComparer<TKey> comparer)
    {
      this._list = new SortedDictionary<TKey, SortedList<TKey, TValue>.ValueItem<TValue>>(comparer);
      this.AllowDuplicates = false;
    }

    public bool AllowDuplicates
    {
      get => this._allowDuplicates;
      set => this._allowDuplicates = value;
    }

    public void Add(TKey key, TValue value)
    {
      if (!this.AllowDuplicates)
        this._list.Add(key, new SortedList<TKey, TValue>.ValueItem<TValue>()
        {
          List = {
            value
          }
        });
      else if (!this._list.ContainsKey(key))
      {
        SortedList<TKey, TValue>.ValueItem<TValue> valueItem = new SortedList<TKey, TValue>.ValueItem<TValue>();
        this._list.Add(key, valueItem);
        valueItem.List.Add(value);
      }
      else
        this._list[key].List.Add(value);
    }

    public List<TKey> GetKeyList()
    {
      List<TKey> keyList = new List<TKey>();
      foreach (TKey key in this._list.Keys)
        keyList.Add(key);
      return keyList;
    }

    public List<TValue> GetValueList()
    {
      List<TValue> valueList = new List<TValue>();
      foreach (SortedList<TKey, TValue>.ValueItem<TValue> valueItem in this._list.Values)
      {
        foreach (TValue obj in valueItem.List)
          valueList.Add(obj);
      }
      return valueList;
    }

    private class ValueItem<TValueInternal>
    {
      public List<TValueInternal> List;

      public ValueItem() => this.List = new List<TValueInternal>();
    }
  }
}
