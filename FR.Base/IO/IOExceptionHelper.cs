// Decompiled with JetBrains decompiler
// Type: FR.IO.IOExceptionHelper
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System.IO;

namespace FR.IO
{
  public static class IOExceptionHelper
  {
    public static DirectoryNotFoundException GetDirectoryNotFound(
      string directoryName)
    {
      return new DirectoryNotFoundException(string.Format("Directory: '{0}' was not found.", (object) directoryName));
    }
  }
}
