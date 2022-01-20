// Decompiled with JetBrains decompiler
// Type: FR.Configuration.IConfigurationProperty
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections;
using System.Xml;

namespace FR.Configuration {
    public interface IConfigurationProperty : IConvertible {
        IConfigurationProperty this[string subPropertyName] { get; }

        IConfigurationSource ConfigurationSource { get; }

        string Name { get; }

        int Index { get; }

        string Path { get; }

        IConfigurationProperty Parent { get; }

        void Delete();

        IConfigurationProperty GetSubProperty(string name);

        IConfigurationProperty GetSubProperty(string name, bool autoCreate);

        IConfigurationProperty GetSubProperty(
          string name,
          int index,
          bool autoCreate);

        IConfigurationProperty[] GetSubProperties();

        IConfigurationProperty[] GetSubProperties(string name);

        IConfigurationProperty AddSubProperty(string name);

        bool ExistsSubProperty(string name);

        int GetCount(string name);

        void Set(bool value);

        void Set(string subProperty, bool value);

        bool ToBoolean();

        bool ToBoolean(bool defaultValue);

        bool ToBoolean(string subProperty, bool defaultValue);

        void Set(byte value);

        void Set(string subProperty, byte value);

        byte ToByte();

        byte ToByte(byte defaultValue);

        byte ToByte(string subProperty, byte defaultValue);

        void Set(DateTime value);

        void Set(string subProperty, DateTime value);

        DateTime ToDateTime();

        DateTime ToDateTime(DateTime defaultValue);

        DateTime ToDateTime(string subProperty, DateTime defaultValue);

        void Set(double value);

        void Set(string subProperty, double value);

        double ToDouble();

        double ToDouble(double defaultValue);

        double ToDouble(string subProperty, double defaultValue);

        void Set(short value);

        void Set(string subProperty, short value);

        short ToInt16();

        short ToInt16(short defaultValue);

        short ToInt16(string subProperty, short defaultValue);

        void Set(int value);

        void Set(string subProperty, int value);

        int ToInt32();

        int ToInt32(int defaultValue);

        int ToInt32(string subProperty, int defaultValue);

        void Set(long value);

        void Set(string subProperty, long value);

        long ToInt64();

        long ToInt64(long defaultValue);

        long ToInt64(string subProperty, long defaultValue);

        void Set(sbyte value);

        void Set(string subProperty, sbyte value);

        sbyte ToSByte();

        sbyte ToSByte(sbyte defaultValue);

        sbyte ToSByte(string subProperty, sbyte defaultValue);

        void Set(float value);

        void Set(string subProperty, float value);

        float ToSingle();

        float ToSingle(float defaultValue);

        float ToSingle(string subProperty, float defaultValue);

        void Set(string value);

        void Set(string subProperty, string value);

        string ToString();

        string ToString(string defaultValue);

        string ToString(string subProperty, string defaultValue);

        void Set(ushort value);

        void Set(string subProperty, ushort value);

        ushort ToUInt16();

        ushort ToUInt16(ushort defaultValue);

        ushort ToUInt16(string subProperty, ushort defaultValue);

        void Set(uint value);

        void Set(string subProperty, uint value);

        uint ToUInt32();

        uint ToUInt32(uint defaultValue);

        uint ToUInt32(string subProperty, uint defaultValue);

        void Set(ulong value);

        void Set(string subProperty, ulong value);

        ulong ToUInt64();

        ulong ToUInt64(ulong defaultValue);

        ulong ToUInt64(string subProperty, ulong defaultValue);

        void Set(Array value);

        void Set(string subProperty, Array value);

        Array ToArray(bool bFallbackToString);

        Array ToArray(bool bFallbackToString, Array defaultValue);

        Array ToArray(string subProperty, bool fallbackToString, Array defaultValue);

        void Set(Hashtable value);

        void Set(string subProperty, Hashtable value);

        Hashtable ToHashtable(bool bFallbackToString);

        Hashtable ToHashtable(bool bFallbackToString, Hashtable defaultValue);

        Hashtable ToHashtable(
          string subProperty,
          bool fallbackToString,
          Hashtable defaultValue);

        void Set(IDictionary value);

        void Set(string subProperty, IDictionary value);

        IDictionary ToDictionary(bool bFallbackToString);

        IDictionary ToDictionary(bool bFallbackToString, IDictionary defaultValue);

        IDictionary ToDictionary(
          string subProperty,
          bool fallbackToString,
          IDictionary defaultValue);

        void Set(object value);

        void Set(string subProperty, object value);

        void Set(XmlElement value);

        void Set(string subProperty, XmlElement value);

        XmlElement ToXmlElement();

        XmlElement ToXmlElement(XmlElement defaultValue);

        XmlElement ToXmlElement(string subProperty, XmlElement defaultValue);
    }
}
