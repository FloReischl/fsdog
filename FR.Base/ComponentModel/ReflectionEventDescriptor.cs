// Decompiled with JetBrains decompiler
// Type: FR.ComponentModel.ReflectionEventDescriptor
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace FR.ComponentModel
{
  public class ReflectionEventDescriptor : EventDescriptor
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private EventInfo _eventInfo;

    public ReflectionEventDescriptor(EventInfo eventInfo)
      : base(eventInfo.Name, (Attribute[]) null)
    {
      this._eventInfo = eventInfo;
    }

    public override Type ComponentType => this.EventInfo.DeclaringType;

    public EventInfo EventInfo
    {
      [DebuggerNonUserCode] get => this._eventInfo;
    }

    public override Type EventType => this.EventInfo.EventHandlerType;

    public override bool IsMulticast => this.EventInfo.IsMulticast;

    public override void AddEventHandler(object component, Delegate value) => this.EventInfo.AddEventHandler(component, value);

    public override void RemoveEventHandler(object component, Delegate value) => this.EventInfo.RemoveEventHandler(component, value);
  }
}
