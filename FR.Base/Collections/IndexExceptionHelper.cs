// Decompiled with JetBrains decompiler
// Type: FR.Collections.IndexExceptionHelper
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using FR.ComponentModel;
using System;
using System.Collections.Generic;
using System.Data;

namespace FR.Collections
{
  internal static class IndexExceptionHelper
  {
    public static InvalidOperationException GetEnumerationCollectionChanged() => new InvalidOperationException("Collection was modified; enumeration operation may not execute.");

    public static InvalidConstraintException GetIndexNotEmptyPrimaryKey() => new InvalidConstraintException("Primary cannot be set to an index which is not empty.");

    public static NullReferenceException GetOriginalDataSourceNull() => new NullReferenceException(string.Format("The original data source of a {0} cannot be null.", (object) typeof (DataStoreSource).Name));

    public static KeyNotFoundException GetPrimaryKeyNodeIndexRelationNotFound() => new KeyNotFoundException("A relation for the specified index was not present in the current primary key node.");

    public static InvalidConstraintException GetPrimaryKeyNodeMissing() => new InvalidConstraintException("Index depends on a primary key and no related primary key node was specified.");

    public static InvalidConstraintException GetPrimaryKeyNotSet() => new InvalidConstraintException("There is no parent primary key specified for the current index.");

    public static ConstraintException GetPrimaryKeyViolation(object key)
    {
      if (key == null)
        key = (object) "<NULL>";
      return new ConstraintException(string.Format("Primary key violation! Key '{0}' already exists.", key));
    }

    public static InvalidConstraintException GetViewObjectNotInDataSource(
      object obj)
    {
      return new InvalidConstraintException(string.Format("Specified view object '{0}' does not exist in data store", obj));
    }
  }
}
