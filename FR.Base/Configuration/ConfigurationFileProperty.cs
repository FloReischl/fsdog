// Decompiled with JetBrains decompiler
// Type: FR.Configuration.ConfigurationFileProperty
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace FR.Configuration {
    [DebuggerDisplay("Path: {Path} | Name: {Name}[{Index}] | Value: {ToString()}")]
    public class ConfigurationFileProperty : IConfigurationProperty, IConvertible {
        private ConfigurationFile _configurationSource;
        private XmlElement _element;
        private readonly string _path;
        private readonly int _index;

        internal ConfigurationFileProperty(
          ConfigurationFile configFile,
          XmlElement element,
          string path,
          int index) {
            this.ConfigurationSource = configFile;
            this.Element = element;
            this._path = path;
            this._index = index;
        }

        public ConfigurationFileProperty this[string subPropertyName] => this.GetSubProperty(subPropertyName, true);

        public ConfigurationFile ConfigurationSource {
            [DebuggerNonUserCode]
            get => this._configurationSource;
            [DebuggerNonUserCode]
            set => this._configurationSource = value;
        }

        internal XmlElement Element {
            get => this._element;
            set => this._element = value;
        }

        public string Path => this._path;

        public string Name => this.Element.Name;

        public int Index => this._index;

        public ConfigurationFileProperty Parent => new ConfigurationFileProperty(this.ConfigurationSource, (XmlElement)this.Element.ParentNode, this.Path.Substring(0, this.Path.LastIndexOf('/')), 0);

        public void Delete() => this.Element.ParentNode.RemoveChild((XmlNode)this.Element);

        public void Set(bool value) {
            XmlText textNode = this._getTextNode(true);
            this._setTextValue(ref textNode, value.ToString());
            this.ConfigurationSource.autoSave();
        }

        public void Set(string subProperty, bool value) => this.GetSubProperty(subProperty, true).Set(value);

        public bool ToBoolean() => (bool)this._getTypedValue((this._getTextNode(false) ?? throw new ConfigurationFilePropertyDoesNotExistException(this.Path, this.Name)).Value, typeof(bool), false);

        public bool ToBoolean(bool defaultValue) {
            XmlText textNode = this._getTextNode(false);
            if (textNode != null)
                return (bool)this._getTypedValue(textNode.Value, typeof(bool), false);
            this.Set(defaultValue);
            return defaultValue;
        }

        public bool ToBoolean(string subProperty, bool defaultValue) => this.GetSubProperty(subProperty, true).ToBoolean(defaultValue);

        public void Set(byte value) {
            XmlText textNode = this._getTextNode(true);
            this._setTextValue(ref textNode, value.ToString());
            this.ConfigurationSource.autoSave();
        }

        public void Set(string subProperty, byte value) => this.GetSubProperty(subProperty, true).Set(value);

        public byte ToByte() => (byte)this._getTypedValue((this._getTextNode(false) ?? throw new ConfigurationFilePropertyDoesNotExistException(this.Path, this.Name)).Value, typeof(byte), false);

        public byte ToByte(byte defaultValue) {
            XmlText textNode = this._getTextNode(false);
            if (textNode != null)
                return (byte)this._getTypedValue(textNode.Value, typeof(byte), false);
            this.Set(defaultValue);
            return defaultValue;
        }

        public byte ToByte(string subProperty, byte defaultValue) => this.GetSubProperty(subProperty, true).ToByte(defaultValue);

        public void Set(DateTime value) {
            XmlText textNode = this._getTextNode(true);
            this._setTextValue(ref textNode, value.ToString(this.ConfigurationSource.DateTimeFormat));
            this.ConfigurationSource.autoSave();
        }

        public void Set(string subProperty, DateTime value) => this.GetSubProperty(subProperty, true).Set(value);

        public DateTime ToDateTime() => (DateTime)this._getTypedValue((this._getTextNode(false) ?? throw new ConfigurationFilePropertyDoesNotExistException(this.Path, this.Name)).Value, typeof(DateTime), false);

        public DateTime ToDateTime(DateTime defaultValue) {
            XmlText textNode = this._getTextNode(false);
            if (textNode != null)
                return (DateTime)this._getTypedValue(textNode.Value, typeof(DateTime), false);
            this.Set(defaultValue);
            return defaultValue;
        }

        public DateTime ToDateTime(string subProperty, DateTime defaultValue) => this.GetSubProperty(subProperty, true).ToDateTime(defaultValue);

        public void Set(double value) {
            XmlText textNode = this._getTextNode(true);
            this._setTextValue(ref textNode, value.ToString());
            this.ConfigurationSource.autoSave();
        }

        public void Set(string subProperty, double value) => this.GetSubProperty(subProperty, true).Set(value);

        public double ToDouble() => (double)this._getTypedValue((this._getTextNode(false) ?? throw new ConfigurationFilePropertyDoesNotExistException(this.Path, this.Name)).Value, typeof(double), false);

        public double ToDouble(double defaultValue) {
            XmlText textNode = this._getTextNode(false);
            if (textNode != null)
                return (double)this._getTypedValue(textNode.Value, typeof(double), false);
            this.Set(defaultValue);
            return defaultValue;
        }

        public double ToDouble(string subProperty, double defaultValue) => this.GetSubProperty(subProperty, true).ToDouble(defaultValue);

        public void Set(short value) {
            XmlText textNode = this._getTextNode(true);
            this._setTextValue(ref textNode, value.ToString());
            this.ConfigurationSource.autoSave();
        }

        public void Set(string subProperty, short value) => this.GetSubProperty(subProperty, true).Set(value);

        public short ToInt16() => (short)this._getTypedValue((this._getTextNode(false) ?? throw new ConfigurationFilePropertyDoesNotExistException(this.Path, this.Name)).Value, typeof(short), false);

        public short ToInt16(short defaultValue) {
            XmlText textNode = this._getTextNode(false);
            if (textNode != null)
                return (short)this._getTypedValue(textNode.Value, typeof(short), false);
            this.Set(defaultValue);
            return defaultValue;
        }

        public short ToInt16(string subProperty, short defaultValue) => this.GetSubProperty(subProperty, true).ToInt16(defaultValue);

        public void Set(int value) {
            XmlText textNode = this._getTextNode(true);
            this._setTextValue(ref textNode, value.ToString());
            this.ConfigurationSource.autoSave();
        }

        public void Set(string subProperty, int value) => this.GetSubProperty(subProperty, true).Set(value);

        public int ToInt32() => (int)this._getTypedValue((this._getTextNode(false) ?? throw new ConfigurationFilePropertyDoesNotExistException(this.Path, this.Name)).Value, typeof(int), false);

        public int ToInt32(int defaultValue) {
            XmlText textNode = this._getTextNode(false);
            if (textNode != null)
                return (int)this._getTypedValue(textNode.Value, typeof(int), false);
            this.Set(defaultValue);
            return defaultValue;
        }

        public int ToInt32(string subProperty, int defaultValue) => this.GetSubProperty(subProperty, true).ToInt32(defaultValue);

        public void Set(long value) {
            XmlText textNode = this._getTextNode(true);
            this._setTextValue(ref textNode, value.ToString());
            this.ConfigurationSource.autoSave();
        }

        public void Set(string subProperty, long value) => this.GetSubProperty(subProperty, true).Set(value);

        public long ToInt64() => (long)this._getTypedValue((this._getTextNode(false) ?? throw new ConfigurationFilePropertyDoesNotExistException(this.Path, this.Name)).Value, typeof(long), false);

        public long ToInt64(long defaultValue) {
            XmlText textNode = this._getTextNode(false);
            if (textNode != null)
                return (long)this._getTypedValue(textNode.Value, typeof(long), false);
            this.Set(defaultValue);
            return defaultValue;
        }

        public long ToInt64(string subProperty, long defaultValue) => this.GetSubProperty(subProperty, true).ToInt64(defaultValue);

        public void Set(sbyte value) {
            XmlText textNode = this._getTextNode(true);
            this._setTextValue(ref textNode, value.ToString());
            this.ConfigurationSource.autoSave();
        }

        public void Set(string subProperty, sbyte value) => this.GetSubProperty(subProperty, true).Set(value);

        public sbyte ToSByte() => (sbyte)this._getTypedValue((this._getTextNode(false) ?? throw new ConfigurationFilePropertyDoesNotExistException(this.Path, this.Name)).Value, typeof(sbyte), false);

        public sbyte ToSByte(sbyte defaultValue) {
            XmlText textNode = this._getTextNode(false);
            if (textNode != null)
                return (sbyte)this._getTypedValue(textNode.Value, typeof(sbyte), false);
            this.Set(defaultValue);
            return defaultValue;
        }

        public sbyte ToSByte(string subProperty, sbyte defaultValue) => this.GetSubProperty(subProperty, true).ToSByte(defaultValue);

        public void Set(float value) {
            XmlText textNode = this._getTextNode(true);
            this._setTextValue(ref textNode, value.ToString());
            this.ConfigurationSource.autoSave();
        }

        public void Set(string subProperty, float value) => this.GetSubProperty(subProperty, true).Set(value);

        public float ToSingle() => (float)this._getTypedValue((this._getTextNode(false) ?? throw new ConfigurationFilePropertyDoesNotExistException(this.Path, this.Name)).Value, typeof(float), false);

        public float ToSingle(float defaultValue) {
            XmlText textNode = this._getTextNode(false);
            if (textNode != null)
                return (float)this._getTypedValue(textNode.Value, typeof(float), false);
            this.Set(defaultValue);
            return defaultValue;
        }

        public float ToSingle(string subProperty, float defaultValue) => this.GetSubProperty(subProperty, true).ToSingle(defaultValue);

        public void Set(string value) {
            XmlText textNode = this._getTextNode(true);
            this._setTextValue(ref textNode, value);
            this.ConfigurationSource.autoSave();
        }

        public void Set(string subProperty, string value) => this.GetSubProperty(subProperty, true).Set(value);

        public override string ToString() => (string)this._getTypedValue((this._getTextNode(false) ?? throw new ConfigurationFilePropertyDoesNotExistException(this.Path, this.Name)).Value, typeof(string), false);

        public string ToString(string defaultValue) {
            XmlText textNode = this._getTextNode(false);
            if (textNode != null)
                return (string)this._getTypedValue(textNode.Value, typeof(string), false);
            this.Set(defaultValue);
            return defaultValue;
        }

        public string ToString(string subProperty, string defaultValue) => this.GetSubProperty(subProperty, true).ToString(defaultValue);

        public void Set(ushort value) {
            XmlText textNode = this._getTextNode(true);
            this._setTextValue(ref textNode, value.ToString());
            this.ConfigurationSource.autoSave();
        }

        public void Set(string subProperty, ushort value) => this.GetSubProperty(subProperty, true).Set(value);

        public ushort ToUInt16() => (ushort)this._getTypedValue((this._getTextNode(false) ?? throw new ConfigurationFilePropertyDoesNotExistException(this.Path, this.Name)).Value, typeof(ushort), false);

        public ushort ToUInt16(ushort defaultValue) {
            XmlText textNode = this._getTextNode(false);
            if (textNode != null)
                return (ushort)this._getTypedValue(textNode.Value, typeof(ushort), false);
            this.Set(defaultValue);
            return defaultValue;
        }

        public ushort ToUInt16(string subProperty, ushort defaultValue) => this.GetSubProperty(subProperty, true).ToUInt16(defaultValue);

        public void Set(uint value) {
            XmlText textNode = this._getTextNode(true);
            this._setTextValue(ref textNode, value.ToString());
            this.ConfigurationSource.autoSave();
        }

        public void Set(string subProperty, uint value) => this.GetSubProperty(subProperty, true).Set(value);

        public uint ToUInt32() => (uint)this._getTypedValue((this._getTextNode(false) ?? throw new ConfigurationFilePropertyDoesNotExistException(this.Path, this.Name)).Value, typeof(uint), false);

        public uint ToUInt32(uint defaultValue) {
            XmlText textNode = this._getTextNode(false);
            if (textNode != null)
                return (uint)this._getTypedValue(textNode.Value, typeof(uint), false);
            this.Set(defaultValue);
            return defaultValue;
        }

        public uint ToUInt32(string subProperty, uint defaultValue) => this.GetSubProperty(subProperty, true).ToUInt32(defaultValue);

        public void Set(ulong value) {
            XmlText textNode = this._getTextNode(true);
            this._setTextValue(ref textNode, value.ToString());
            this.ConfigurationSource.autoSave();
        }

        public void Set(string subProperty, ulong value) => this.GetSubProperty(subProperty, true).Set(value);

        public ulong ToUInt64() => (ulong)this._getTypedValue((this._getTextNode(false) ?? throw new ConfigurationFilePropertyDoesNotExistException(this.Path, this.Name)).Value, typeof(ulong), false);

        public ulong ToUInt64(ulong defaultValue) {
            XmlText textNode = this._getTextNode(false);
            if (textNode != null)
                return (ulong)this._getTypedValue(textNode.Value, typeof(ulong), false);
            this.Set(defaultValue);
            return defaultValue;
        }

        public ulong ToUInt64(string subProperty, ulong defaultValue) => this.GetSubProperty(subProperty, true).ToUInt64(defaultValue);

        public void Set(Array value) {
            this.Element.RemoveAll();
            foreach (object obj in value) {
                XmlElement element = this.ConfigurationSource.Dom.CreateElement("Item");
                if (this.ConfigurationSource.WriteType) {
                    XmlAttribute attribute = this.ConfigurationSource.Dom.CreateAttribute("Type");
                    attribute.Value = obj.GetType().ToString();
                    element.Attributes.Append(attribute);
                }
                XmlText textNode = this.ConfigurationSource.Dom.CreateTextNode(obj.ToString());
                element.AppendChild((XmlNode)textNode);
                this.Element.AppendChild((XmlNode)element);
            }
            this.ConfigurationSource.setChanged();
            this.ConfigurationSource.autoSave();
        }

        public void Set(string subProperty, Array value) => this.GetSubProperty(subProperty, true).Set(value);

        public Array ToArray(bool bFallbackToString) {
            XmlNodeList xmlNodeList = this.Element != null ? this.Element.SelectNodes("Item") : throw new ConfigurationFilePropertyDoesNotExistException(this.Path, this.Name);
            ArrayList arrayList = new ArrayList(xmlNodeList.Count);
            foreach (XmlElement xmlElement in xmlNodeList) {
                string attribute = xmlElement.GetAttribute("Type");
                XmlText xmlText = (XmlText)xmlElement.SelectSingleNode("text()");
                arrayList.Add(this._getTypedValue(xmlText.Value, attribute, bFallbackToString));
            }
            return (Array)arrayList.ToArray();
        }

        public Array ToArray(bool bFallbackToString, Array defaultValue) {
            if (this.Element == null) {
                this.Set(defaultValue);
                return defaultValue;
            }
            XmlNodeList xmlNodeList = this.Element.SelectNodes("Item");
            ArrayList arrayList = new ArrayList(xmlNodeList.Count);
            foreach (XmlElement xmlElement in xmlNodeList) {
                string attribute = xmlElement.GetAttribute("Type");
                XmlText xmlText = (XmlText)xmlElement.SelectSingleNode("text()");
                arrayList.Add(this._getTypedValue(xmlText.Value, attribute, bFallbackToString));
            }
            return (Array)arrayList.ToArray();
        }

        public Array ToArray(string subProperty, bool fallbackToString, Array defaultValue) => this.GetSubProperty(subProperty, true).ToArray(fallbackToString, defaultValue);

        public void Set(Hashtable value) {
            this.Element.RemoveAll();
            foreach (object key in (IEnumerable)value.Keys) {
                object oValue = value[key];
                this._setKeyValuePair(this.Element, key, oValue);
            }
            this.ConfigurationSource.setChanged();
            this.ConfigurationSource.autoSave();
        }

        public void Set(string subProperty, Hashtable value) => this.GetSubProperty(subProperty, true).Set(value);

        public Hashtable ToHashtable(bool bFallbackToString) {
            XmlNodeList xmlNodeList = this.Element != null ? this.Element.SelectNodes("Item") : throw new ConfigurationFilePropertyDoesNotExistException(this.Path, this.Name);
            Hashtable hashtable = new Hashtable(xmlNodeList.Count);
            foreach (XmlElement eItem in xmlNodeList) {
                this._getKeyValuePair(eItem, out object key, out object value, bFallbackToString);
                hashtable.Add(key, value);
            }
            return hashtable;
        }

        public Hashtable ToHashtable(bool bFallbackToString, Hashtable defaultValue) {
            if (this.Element == null) {
                this.Set(defaultValue);
                return defaultValue;
            }
            XmlNodeList xmlNodeList = this.Element.SelectNodes("Item");
            Hashtable hashtable = new Hashtable(xmlNodeList.Count);
            foreach (XmlElement element in xmlNodeList) {
                this._getKeyValuePair(element, out object key, out object value, bFallbackToString);
                hashtable.Add(key, value);
            }
            return hashtable;
        }

        public Hashtable ToHashtable(
          string subProperty,
          bool fallbackToString,
          Hashtable defaultValue) {
            return this.GetSubProperty(subProperty, true).ToHashtable(fallbackToString, defaultValue);
        }

        public void Set(IDictionary value) {
            this.Element.RemoveAll();
            Type[] genericArguments = value.GetType().GetGenericArguments();
            XmlAttribute attribute1 = this.ConfigurationSource.Dom.CreateAttribute("KeyType");
            attribute1.Value = genericArguments[0].ToString();
            this.Element.Attributes.Append(attribute1);
            XmlAttribute attribute2 = this.ConfigurationSource.Dom.CreateAttribute("ValueType");
            attribute2.Value = genericArguments[1].ToString();
            this.Element.Attributes.Append(attribute2);
            foreach (object key in (IEnumerable)value.Keys) {
                object oValue = value[key];
                this._setKeyValuePair(this.Element, key, oValue);
            }
            this.ConfigurationSource.setChanged();
            this.ConfigurationSource.autoSave();
        }

        public void Set(string subProperty, IDictionary value) => this.GetSubProperty(subProperty, true).Set(value);

        public IDictionary ToDictionary(bool bFallbackToString) {
            ConstructorInfo constructorInfo = this.Element != null ? Type.GetType("System.Collections.Generic.Dictionary`2" + "[" + this.Element.GetAttribute("KeyType") + "," + this.Element.GetAttribute("ValueType") + "]").GetConstructor(new Type[1]
            {
        typeof (int)
            }) : throw new ConfigurationFilePropertyDoesNotExistException(this.Path, this.Name);
            XmlNodeList xmlNodeList = this.Element.SelectNodes("Item");
            IDictionary dictionary = (IDictionary)constructorInfo.Invoke(new object[1]
            {
        (object) xmlNodeList.Count
            });
            foreach (XmlElement element in xmlNodeList) {
                this._getKeyValuePair(element, out object key, out object value, bFallbackToString);
                dictionary.Add(key, value);
            }
            return dictionary;
        }

        public IDictionary ToDictionary(bool bFallbackToString, IDictionary defaultValue) {
            if (this.Element == null) {
                this.Set(defaultValue);
                return defaultValue;
            }
            ConstructorInfo constructor = Type.GetType("System.Collections.Generic.Dictionary`2" + "[" + this.Element.GetAttribute("KeyType") + "," + this.Element.GetAttribute("ValueType") + "]").GetConstructor(new Type[1]
            {
        typeof (int)
            });
            XmlNodeList xmlNodeList = this.Element.SelectNodes("Item");
            IDictionary dictionary = (IDictionary)constructor.Invoke(new object[1]
            {
        (object) xmlNodeList.Count
            });
            foreach (XmlElement element in xmlNodeList) {
                this._getKeyValuePair(element, out object key, out object value, bFallbackToString);
                dictionary.Add(key, value);
            }
            return dictionary;
        }

        public IDictionary ToDictionary(
          string subProperty,
          bool fallbackToString,
          IDictionary defaultValue) {
            return this.GetSubProperty(subProperty, true).ToDictionary(fallbackToString, defaultValue);
        }

        public void Set(object value) {
            XmlText textNode = this._getTextNode(true);
            this._setTextValue(ref textNode, value.ToString());
            this.ConfigurationSource.autoSave();
        }

        public void Set(string subProperty, object value) => this.GetSubProperty(subProperty, true).Set(value);

        public object ToType(Type resultType) {
            string attribute = this.Element.GetAttribute("type");
            Type type1 = resultType;
            if (attribute != null)
                type1 = Type.GetType(attribute);
            bool flag = false;
            foreach (Type type2 in type1.GetInterfaces()) {
                if (type2 == typeof(IXmlSerializable)) {
                    flag = true;
                    break;
                }
            }
            if (!flag)
                throw ExceptionHelper.GetNotImplemented("Type {0} does not implement interface {1}", (object)type1, (object)typeof(IXmlSerializable));
            IXmlSerializable type3 = (IXmlSerializable)(type1.GetConstructor(Type.EmptyTypes) ?? throw ExceptionHelper.GetNotImplemented("Type {0} does not implement an empty constructor", (object)type1)).Invoke(new object[0]);
            XmlReader reader = this.Element.CreateNavigator().ReadSubtree();
            type3.ReadXml(reader);
            return (object)type3;
        }

        public void Set(XmlElement value) {
            XmlNode parentNode = this.Element.ParentNode;
            parentNode.RemoveChild((XmlNode)this.Element);
            value = (XmlElement)this.ConfigurationSource.Dom.ImportNode((XmlNode)value, true);
            parentNode.AppendChild((XmlNode)value);
            this.ConfigurationSource.setChanged();
            this.ConfigurationSource.autoSave();
        }

        public void Set(string subProperty, XmlElement value) => this.GetSubProperty(subProperty, true).Set(value);

        public XmlElement ToXmlElement() => this.Element != null ? (XmlElement)new XmlDocument().ImportNode((XmlNode)this.Element, true) : throw new ConfigurationFilePropertyDoesNotExistException(this.Path, this.Name);

        public XmlElement ToXmlElement(XmlElement defaultValue) {
            if (this.Element.FirstChild == null) {
                this.Set(defaultValue);
                return defaultValue;
            }
            return this.Element.FirstChild == null || this.Element.FirstChild is XmlElement ? (XmlElement)new XmlDocument().ImportNode(this.Element.FirstChild, true) : throw new InvalidCastException(string.Format("Child of property {0} at {1} is not a XmlElement", (object)this.Name, (object)this.Path));
        }

        public XmlElement ToXmlElement(string subProperty, XmlElement defaultValue) => this.GetSubProperty(subProperty, true).ToXmlElement(defaultValue);

        public ConfigurationFileProperty GetSubProperty(
          string name,
          int index,
          bool autoCreate) {
            return this.ConfigurationSource.GetProperty(this.GetSubPropertyPath(), name, index, autoCreate);
        }

        public ConfigurationFileProperty GetSubProperty(
          string name,
          bool autoCreate) {
            return this.GetSubProperty(name, 0, autoCreate);
        }

        public ConfigurationFileProperty GetSubProperty(string name) => this.GetSubProperty(name, 0, false);

        public ConfigurationFileProperty[] GetSubProperties() => this.ConfigurationSource.GetProperties(this.GetSubPropertyPath(), (string)null);

        public ConfigurationFileProperty[] GetSubProperties(string name) => this.ConfigurationSource.GetProperties(this.GetSubPropertyPath(), name);

        public bool ExistsSubProperty(string name) => this.ConfigurationSource.ExistsProperty(this.GetSubPropertyPath(), name);

        public ConfigurationFileProperty AddSubProperty(string name) {
            int count = this.GetCount(name);
            return this.ConfigurationSource.GetProperty(this.GetSubPropertyPath(), name, count, true);
        }

        public int GetCount(string name) => this.ConfigurationSource.GetCount(this, name);

        internal string GetSubPropertyPath() => this.Path == "//" ? string.Format("//{0} [position()={1}]", (object)this.Name, (object)(this.Index + 1)) : string.Format("{0}/{1} [position()={2}]", (object)this.Path, (object)this.Name, (object)(this.Index + 1));

        public override int GetHashCode() => this.ToString().GetHashCode();

        public override bool Equals(object obj) => obj is ConfigurationFileProperty configurationFileProperty && configurationFileProperty.Path == this.Path && configurationFileProperty.Name == this.Name && configurationFileProperty.Index == this.Index;

        private object _getTypedValue(string sValue, Type type, bool bFallbackToString) {
            object typedValue;
            if (type == typeof(string))
                typedValue = (object)sValue;
            else if (type == typeof(bool))
                typedValue = (object)Convert.ToBoolean(sValue);
            else if (type == typeof(byte))
                typedValue = (object)Convert.ToByte(sValue);
            else if (type == typeof(sbyte))
                typedValue = (object)Convert.ToSByte(sValue);
            else if (type == typeof(short))
                typedValue = (object)Convert.ToInt16(sValue);
            else if (type == typeof(ushort))
                typedValue = (object)Convert.ToUInt16(sValue);
            else if (type == typeof(int))
                typedValue = (object)Convert.ToInt32(sValue);
            else if (type == typeof(uint))
                typedValue = (object)Convert.ToUInt32(sValue);
            else if (type == typeof(long))
                typedValue = (object)Convert.ToInt64(sValue);
            else if (type == typeof(ulong))
                typedValue = (object)Convert.ToUInt64(sValue);
            else if (type == typeof(float))
                typedValue = (object)Convert.ToSingle(sValue);
            else if (type == typeof(double))
                typedValue = (object)Convert.ToDouble(sValue);
            else if (type == typeof(DateTime)) {
                typedValue = (object)DateTime.ParseExact(sValue, this.ConfigurationSource.DateTimeFormat, (IFormatProvider)this.ConfigurationSource.FormatCulture);
            }
            else {
                StringConverter stringConverter = new StringConverter();
                if (stringConverter.CanConvertTo(type)) {
                    typedValue = stringConverter.ConvertTo((object)sValue, type);
                }
                else {
                    ConstructorInfo constructor = type.GetConstructor(new Type[1]
                    {
            typeof (string)
                    });
                    if (constructor == null) {
                        if (!bFallbackToString)
                            throw new InvalidCastException(string.Format("Object type {0} has no constructor for 'string'", (object)type.ToString()));
                        typedValue = (object)sValue;
                    }
                    else
                        typedValue = constructor.Invoke(new object[1]
                        {
              (object) sValue
                        });
                }
            }
            return typedValue;
        }

        private object _getTypedValue(string sValue, string sType, bool bFallbackToString) {
            if (string.IsNullOrEmpty(sType))
                sType = "System.String";
            Type type = Type.GetType(sType);
            return this._getTypedValue(sValue, type, bFallbackToString);
        }

        private XmlText _getTextNode(bool autoCreate) {
            XmlText newChild = (XmlText)null;
            if (this.Element.HasChildNodes)
                newChild = this.Element.FirstChild is XmlText ? (XmlText)this.Element.FirstChild : throw new InvalidCastException(string.Format("Element {0} at position {1} exists, but is not a text element", (object)this.Name, (object)this.Path));
            else if (autoCreate) {
                newChild = this.ConfigurationSource.Dom.CreateTextNode("");
                this.Element.AppendChild((XmlNode)newChild);
                this.ConfigurationSource.autoSave();
            }
            return newChild;
        }

        private void _setKeyValuePair(XmlElement eParent, object oKey, object oValue) {
            XmlElement element1 = this.ConfigurationSource.Dom.CreateElement("Item");
            XmlElement element2 = this.ConfigurationSource.Dom.CreateElement("Key");
            if (this.ConfigurationSource.WriteType) {
                XmlAttribute attribute = this.ConfigurationSource.Dom.CreateAttribute("Type");
                attribute.Value = oKey.GetType().ToString();
                element2.Attributes.Append(attribute);
            }
            XmlText textNode1 = this.ConfigurationSource.Dom.CreateTextNode(oKey.ToString());
            element2.AppendChild((XmlNode)textNode1);
            element1.AppendChild((XmlNode)element2);
            XmlElement element3 = this.ConfigurationSource.Dom.CreateElement("Value");
            if (this.ConfigurationSource.WriteType) {
                XmlAttribute attribute = this.ConfigurationSource.Dom.CreateAttribute("Type");
                attribute.Value = oValue.GetType().ToString();
                element3.Attributes.Append(attribute);
            }
            XmlText textNode2 = this.ConfigurationSource.Dom.CreateTextNode(oValue.ToString());
            element3.AppendChild((XmlNode)textNode2);
            element1.AppendChild((XmlNode)element3);
            eParent.AppendChild((XmlNode)element1);
        }

        private void _getKeyValuePair(
          XmlElement eItem,
          out object oKey,
          out object oValue,
          bool bFallbackToString) {
            XmlElement xmlElement1 = (XmlElement)eItem.SelectSingleNode("Key");
            string attribute1 = xmlElement1.GetAttribute("Type");
            XmlText xmlText1 = (XmlText)xmlElement1.SelectSingleNode("text()");
            oKey = this._getTypedValue(xmlText1.Value, attribute1, bFallbackToString);
            XmlElement xmlElement2 = (XmlElement)eItem.SelectSingleNode("Value");
            string attribute2 = xmlElement2.GetAttribute("Type");
            XmlText xmlText2 = (XmlText)xmlElement2.SelectSingleNode("text()");
            oValue = this._getTypedValue(xmlText2.Value, attribute2, bFallbackToString);
        }

        private void _setTextValue(ref XmlText tValue, string value) {
            if (tValue.Value == null || !(tValue.Value != value))
                return;
            tValue.Value = value;
            this.ConfigurationSource.setChanged();
        }

        TypeCode IConvertible.GetTypeCode() => throw new Exception("The method or operation is not implemented.");

        bool IConvertible.ToBoolean(IFormatProvider provider) => this.ToBoolean();

        byte IConvertible.ToByte(IFormatProvider provider) => this.ToByte();

        char IConvertible.ToChar(IFormatProvider provider) => throw new Exception("The method or operation is not implemented.");

        DateTime IConvertible.ToDateTime(IFormatProvider provider) => this.ToDateTime();

        Decimal IConvertible.ToDecimal(IFormatProvider provider) => throw new Exception("The method or operation is not implemented.");

        double IConvertible.ToDouble(IFormatProvider provider) => this.ToDouble();

        short IConvertible.ToInt16(IFormatProvider provider) => this.ToInt16();

        int IConvertible.ToInt32(IFormatProvider provider) => this.ToInt32();

        long IConvertible.ToInt64(IFormatProvider provider) => this.ToInt64();

        sbyte IConvertible.ToSByte(IFormatProvider provider) => this.ToSByte();

        float IConvertible.ToSingle(IFormatProvider provider) => this.ToSingle();

        string IConvertible.ToString(IFormatProvider provider) => this.ToString();

        object IConvertible.ToType(Type conversionType, IFormatProvider provider) => throw new Exception("The method or operation is not implemented.");

        ushort IConvertible.ToUInt16(IFormatProvider provider) => this.ToUInt16();

        uint IConvertible.ToUInt32(IFormatProvider provider) => this.ToUInt32();

        ulong IConvertible.ToUInt64(IFormatProvider provider) => this.ToUInt64();

        IConfigurationProperty IConfigurationProperty.this[string subPropertyName] {
            get { return (IConfigurationProperty)this[subPropertyName]; }
        }

        IConfigurationSource IConfigurationProperty.ConfigurationSource => (IConfigurationSource)this.ConfigurationSource;

        string IConfigurationProperty.Name => this.Name;

        int IConfigurationProperty.Index => this.Index;

        string IConfigurationProperty.Path => this.Path;

        IConfigurationProperty IConfigurationProperty.Parent => (IConfigurationProperty)this.Parent;

        void IConfigurationProperty.Delete() => this.Delete();

        IConfigurationProperty IConfigurationProperty.GetSubProperty(
          string name) {
            return (IConfigurationProperty)this.GetSubProperty(name);
        }

        IConfigurationProperty IConfigurationProperty.GetSubProperty(
          string name,
          bool autoCreate) {
            return (IConfigurationProperty)this.GetSubProperty(name, autoCreate);
        }

        IConfigurationProperty IConfigurationProperty.GetSubProperty(
          string name,
          int index,
          bool autoCreate) {
            return (IConfigurationProperty)this.GetSubProperty(name, index, autoCreate);
        }

        IConfigurationProperty[] IConfigurationProperty.GetSubProperties() => (IConfigurationProperty[])this.GetSubProperties();

        IConfigurationProperty[] IConfigurationProperty.GetSubProperties(
          string name) {
            return (IConfigurationProperty[])this.GetSubProperties(name);
        }

        IConfigurationProperty IConfigurationProperty.AddSubProperty(
          string name) {
            return (IConfigurationProperty)this.AddSubProperty(name);
        }

        bool IConfigurationProperty.ExistsSubProperty(string name) => this.ExistsSubProperty(name);

        int IConfigurationProperty.GetCount(string name) => this.GetCount(name);

        bool IConfigurationProperty.ToBoolean() => this.ToBoolean();

        bool IConfigurationProperty.ToBoolean(bool defaultValue) => this.ToBoolean(defaultValue);

        bool IConfigurationProperty.ToBoolean(
          string subProperty,
          bool defaultValue) {
            return this.ToBoolean(subProperty, defaultValue);
        }

        byte IConfigurationProperty.ToByte() => this.ToByte();

        byte IConfigurationProperty.ToByte(byte defaultValue) => this.ToByte(defaultValue);

        byte IConfigurationProperty.ToByte(string subProperty, byte defaultValue) => this.ToByte(subProperty, defaultValue);

        short IConfigurationProperty.ToInt16() => this.ToInt16();

        short IConfigurationProperty.ToInt16(short defaultValue) => this.ToInt16(defaultValue);

        short IConfigurationProperty.ToInt16(
          string subProperty,
          short defaultValue) {
            return this.ToInt16(subProperty, defaultValue);
        }

        int IConfigurationProperty.ToInt32() => this.ToInt32();

        int IConfigurationProperty.ToInt32(int defaultValue) => this.ToInt32(defaultValue);

        int IConfigurationProperty.ToInt32(string subProperty, int defaultValue) => this.ToInt32(subProperty, defaultValue);

        long IConfigurationProperty.ToInt64() => this.ToInt64();

        long IConfigurationProperty.ToInt64(long defaultValue) => this.ToInt64(defaultValue);

        long IConfigurationProperty.ToInt64(string subProperty, long defaultValue) => this.ToInt64(subProperty, defaultValue);

        DateTime IConfigurationProperty.ToDateTime() => this.ToDateTime();

        DateTime IConfigurationProperty.ToDateTime(DateTime defaultValue) => this.ToDateTime(defaultValue);

        DateTime IConfigurationProperty.ToDateTime(
          string subProperty,
          DateTime defaultValue) {
            return this.ToDateTime(subProperty, defaultValue);
        }

        double IConfigurationProperty.ToDouble() => this.ToDouble();

        double IConfigurationProperty.ToDouble(double defaultValue) => this.ToDouble(defaultValue);

        double IConfigurationProperty.ToDouble(
          string subProperty,
          double defaultValue) {
            return this.ToDouble(subProperty, defaultValue);
        }

        sbyte IConfigurationProperty.ToSByte() => this.ToSByte();

        sbyte IConfigurationProperty.ToSByte(sbyte defaultValue) => this.ToSByte(defaultValue);

        sbyte IConfigurationProperty.ToSByte(
          string subProperty,
          sbyte defaultValue) {
            return this.ToSByte(subProperty, defaultValue);
        }

        float IConfigurationProperty.ToSingle() => this.ToSingle();

        float IConfigurationProperty.ToSingle(float defaultValue) => this.ToSingle(defaultValue);

        float IConfigurationProperty.ToSingle(
          string subProperty,
          float defaultValue) {
            return this.ToSingle(subProperty, defaultValue);
        }

        string IConfigurationProperty.ToString() => this.ToString();

        string IConfigurationProperty.ToString(string defaultValue) => this.ToString(defaultValue);

        string IConfigurationProperty.ToString(
          string subProperty,
          string defaultValue) {
            return this.ToString(subProperty, defaultValue);
        }

        ushort IConfigurationProperty.ToUInt16() => this.ToUInt16();

        ushort IConfigurationProperty.ToUInt16(ushort defaultValue) => this.ToUInt16(defaultValue);

        ushort IConfigurationProperty.ToUInt16(
          string subProperty,
          ushort defaultValue) {
            return this.ToUInt16(subProperty, defaultValue);
        }

        uint IConfigurationProperty.ToUInt32() => this.ToUInt32();

        uint IConfigurationProperty.ToUInt32(uint defaultValue) => this.ToUInt32(defaultValue);

        uint IConfigurationProperty.ToUInt32(
          string subProperty,
          uint defaultValue) {
            return this.ToUInt32(subProperty, defaultValue);
        }

        ulong IConfigurationProperty.ToUInt64() => (ulong)this.ToUInt32();

        ulong IConfigurationProperty.ToUInt64(ulong defaultValue) => this.ToUInt64(defaultValue);

        ulong IConfigurationProperty.ToUInt64(
          string subProperty,
          ulong defaultValue) {
            return this.ToUInt64(subProperty, defaultValue);
        }

        Array IConfigurationProperty.ToArray(bool fallbackToString) => this.ToArray(fallbackToString);

        Array IConfigurationProperty.ToArray(
          bool fallbackToString,
          Array defaultValue) {
            return this.ToArray(fallbackToString, defaultValue);
        }

        Array IConfigurationProperty.ToArray(
          string subProperty,
          bool fallbackToString,
          Array defaultValue) {
            return this.ToArray(subProperty, fallbackToString, defaultValue);
        }

        Hashtable IConfigurationProperty.ToHashtable(
          bool fallbackToString) {
            return this.ToHashtable(fallbackToString);
        }

        Hashtable IConfigurationProperty.ToHashtable(
          bool fallbackToString,
          Hashtable defaultValue) {
            return this.ToHashtable(fallbackToString, defaultValue);
        }

        Hashtable IConfigurationProperty.ToHashtable(
          string subProperty,
          bool fallbackToString,
          Hashtable defaultValue) {
            return this.ToHashtable(subProperty, fallbackToString, defaultValue);
        }

        IDictionary IConfigurationProperty.ToDictionary(
          bool fallbackToString) {
            return this.ToDictionary(fallbackToString);
        }

        IDictionary IConfigurationProperty.ToDictionary(
          bool fallbackToString,
          IDictionary defaultValue) {
            return this.ToDictionary(fallbackToString, defaultValue);
        }

        IDictionary IConfigurationProperty.ToDictionary(
          string subProperty,
          bool fallbackToString,
          IDictionary defaultValue) {
            return this.ToDictionary(subProperty, fallbackToString, defaultValue);
        }

        XmlElement IConfigurationProperty.ToXmlElement() => this.ToXmlElement();

        XmlElement IConfigurationProperty.ToXmlElement(
          XmlElement defaultValue) {
            return this.ToXmlElement(defaultValue);
        }

        XmlElement IConfigurationProperty.ToXmlElement(
          string subProperty,
          XmlElement defaultValue) {
            return this.ToXmlElement(subProperty, defaultValue);
        }

        void IConfigurationProperty.Set(bool value) => this.Set(value);

        void IConfigurationProperty.Set(string subProperty, bool value) => this.Set(subProperty, value);

        void IConfigurationProperty.Set(byte value) => this.Set(value);

        void IConfigurationProperty.Set(string subProperty, byte value) => this.Set(subProperty, value);

        void IConfigurationProperty.Set(DateTime value) => this.Set(value);

        void IConfigurationProperty.Set(string subProperty, DateTime value) => this.Set(subProperty, value);

        void IConfigurationProperty.Set(double value) => this.Set(value);

        void IConfigurationProperty.Set(string subProperty, double value) => this.Set(subProperty, value);

        void IConfigurationProperty.Set(short value) => this.Set(value);

        void IConfigurationProperty.Set(string subProperty, short value) => this.Set(subProperty, value);

        void IConfigurationProperty.Set(int value) => this.Set(value);

        void IConfigurationProperty.Set(string subProperty, int value) => this.Set(subProperty, value);

        void IConfigurationProperty.Set(long value) => this.Set(value);

        void IConfigurationProperty.Set(string subProperty, long value) => this.Set(subProperty, value);

        void IConfigurationProperty.Set(sbyte value) => this.Set(value);

        void IConfigurationProperty.Set(string subProperty, sbyte value) => this.Set(subProperty, value);

        void IConfigurationProperty.Set(float value) => this.Set(value);

        void IConfigurationProperty.Set(string subProperty, float value) => this.Set(subProperty, value);

        void IConfigurationProperty.Set(string value) => this.Set(value);

        void IConfigurationProperty.Set(string subProperty, string value) => this.Set(subProperty, value);

        void IConfigurationProperty.Set(ushort value) => this.Set(value);

        void IConfigurationProperty.Set(string subProperty, ushort value) => this.Set(subProperty, value);

        void IConfigurationProperty.Set(uint value) => this.Set(value);

        void IConfigurationProperty.Set(string subProperty, uint value) => this.Set(subProperty, value);

        void IConfigurationProperty.Set(ulong value) => this.Set(value);

        void IConfigurationProperty.Set(string subProperty, ulong value) => this.Set(subProperty, value);

        void IConfigurationProperty.Set(Array value) => this.Set(value);

        void IConfigurationProperty.Set(string subProperty, Array value) => this.Set(subProperty, value);

        void IConfigurationProperty.Set(IDictionary value) => this.Set(value);

        void IConfigurationProperty.Set(string subProperty, IDictionary value) => this.Set(subProperty, value);

        void IConfigurationProperty.Set(Hashtable value) => this.Set(value);

        void IConfigurationProperty.Set(string subProperty, Hashtable value) => this.Set(subProperty, value);

        void IConfigurationProperty.Set(XmlElement value) => this.Set(value);

        void IConfigurationProperty.Set(string subProperty, XmlElement value) => this.Set(subProperty, value);
    }
}
