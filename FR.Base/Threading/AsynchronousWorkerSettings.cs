// Decompiled with JetBrains decompiler
// Type: FR.Threading.AsynchronousWorkerSettings
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Diagnostics;
using System.Reflection;

namespace FR.Threading
{
  public class AsynchronousWorkerSettings
  {
    private object _targedObject;
    private MethodInfo _methodInfo;
    private Delegate _delegate;
    private object[] _parameters;

    public AsynchronousWorkerSettings(
      object targedObject,
      string method,
      Type[] types,
      object[] parameters)
    {
      this._targedObject = targedObject;
      this._parameters = parameters;
      this._methodInfo = targedObject.GetType().GetMethod(method, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, types, (ParameterModifier[]) null);
    }

    public AsynchronousWorkerSettings(
      object targedObject,
      MethodInfo methodInfo,
      object[] parameters)
    {
      this._targedObject = targedObject;
      this._parameters = parameters;
      this._methodInfo = methodInfo;
    }

    public AsynchronousWorkerSettings(Delegate dlg, object[] parameters)
    {
      this._delegate = dlg;
      this._parameters = parameters;
    }

    public object TargedObject
    {
      [DebuggerNonUserCode] get => this._targedObject;
    }

    public MethodInfo MethodInfo
    {
      [DebuggerNonUserCode] get => this._methodInfo;
    }

    public Delegate Delegate
    {
      [DebuggerNonUserCode] get => this._delegate;
    }

    public object[] Parameters
    {
      [DebuggerNonUserCode] get => this._parameters;
    }
  }
}
