// Decompiled with JetBrains decompiler
// Type: FR.ComponentModel.EventsFilterCache
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FR.ComponentModel
{
  public class EventsFilterCache
  {
    private Dictionary<Type, EventsFilterCache.FilterItem> _cache;

    public EventsFilterCache() => this._cache = new Dictionary<Type, EventsFilterCache.FilterItem>();

    public void SetProperties(Type objectType, Attribute[] attrs, EventDescriptorCollection events)
    {
      if (this._cache.ContainsKey(objectType))
        this._cache.Remove(objectType);
      this._cache.Add(objectType, new EventsFilterCache.FilterItem(events, attrs));
    }

    public EventDescriptorCollection FindProperties(
      Type objectType,
      Attribute[] attrs)
    {
      if (this._cache.ContainsKey(objectType))
      {
        EventsFilterCache.FilterItem filterItem = this._cache[objectType];
        if (filterItem.IsValid(attrs))
          return filterItem.Events;
      }
      return (EventDescriptorCollection) null;
    }

    private class FilterItem
    {
      private Attribute[] _attributes;
      private EventDescriptorCollection _events;

      public FilterItem(EventDescriptorCollection events, Attribute[] attributes)
      {
        this._events = events;
        this._attributes = attributes;
      }

      public Attribute[] Attributes => this._attributes;

      public EventDescriptorCollection Events => this._events;

      public bool IsValid(Attribute[] filter)
      {
        if (filter == null && this._attributes == null)
          return true;
        if (filter == null || this._attributes == null || this._attributes.Length != filter.Length)
          return false;
        for (int index = 0; index < filter.Length; ++index)
        {
          if (!this._attributes[index].Match((object) filter[index]))
            return false;
        }
        return true;
      }
    }
  }
}
