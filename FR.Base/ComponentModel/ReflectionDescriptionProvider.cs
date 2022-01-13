// Decompiled with JetBrains decompiler
// Type: FR.ComponentModel.ReflectionDescriptionProvider
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.ComponentModel;
using System.Diagnostics;

namespace FR.ComponentModel
{
  public class ReflectionDescriptionProvider : TypeDescriptionProvider
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private EventsFilterCache _eventsCache;
    private PropertiesFilterCache _propertiesCache;

    public ReflectionDescriptionProvider(Type t) => TypeDescriptor.AddProvider((TypeDescriptionProvider) this, t);

    public ReflectionDescriptionProvider()
    {
    }

    public EventsFilterCache EventsFilterCache
    {
      [DebuggerNonUserCode] get
      {
        if (this._eventsCache == null)
          this._eventsCache = new EventsFilterCache();
        return this._eventsCache;
      }
    }

    public PropertiesFilterCache PropertiesCache
    {
      get
      {
        if (this._propertiesCache == null)
          this._propertiesCache = new PropertiesFilterCache();
        return this._propertiesCache;
      }
    }

    public override ICustomTypeDescriptor GetTypeDescriptor(
      Type objectType,
      object instance)
    {
      return (ICustomTypeDescriptor) new ReflectionTypeDescriptor(this, objectType, instance);
    }
  }
}
