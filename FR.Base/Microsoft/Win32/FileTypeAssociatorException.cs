// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.FileTypeAssociatorException
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Runtime.Serialization;

namespace Microsoft.Win32 {
    [Serializable]
    public class FileTypeAssociatorException : Exception {
        public FileTypeAssociatorException() {
        }

        public FileTypeAssociatorException(string message, params object[] args)
          : base(string.Format(message, args)) {
        }

        public FileTypeAssociatorException(
          Exception innerException,
          string message,
          params object[] args)
          : base(string.Format(message, args), innerException) {
        }

        public FileTypeAssociatorException(SerializationInfo info, StreamingContext context)
          : base(info, context) {
        }
    }
}
