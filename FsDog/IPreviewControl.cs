// Decompiled with JetBrains decompiler
// Type: FsDog.IPreviewControl
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FsDog.Detail;

namespace FsDog
{
  public interface IPreviewControl
  {
    string FileName { get; }

    void SetFile(string fileName);

    PreviewType PreviewType { get; }
  }
}
