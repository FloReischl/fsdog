// Decompiled with JetBrains decompiler
// Type: FR.Configuration.ConfigurationFile
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;

namespace FR.Configuration {
    [DebuggerDisplay("FileName: {FileName}")]
    public class ConfigurationFile : IConfigurationSource {
        private bool _changed;
        //private ConfigurationFileProperty _rootProperty;
        private JObject _root;

        public ConfigurationFile()
          : this((string)null, ConfigurationFile.FileAccessMode.CreateIfNotExists) {
        }

        public ConfigurationFile(XmlElement root) {
            this.FileName = (string)null;
            this.RootElement = root;
        }

        public ConfigurationFile(string filename, ConfigurationFile.FileAccessMode accessMode) {
            this.FileName = filename;
            this.FormatCulture = CultureInfo.CurrentCulture;
            this.WriteType = true;
            //if (this.FileName == null)
            //    this.FileName = ConfigurationFile.GetDefaultConfigFileName_OLD(Assembly.GetEntryAssembly());
            //if (string.IsNullOrEmpty(this.FileName))
            //    throw new ArgumentException("Property FileName is <empty>");
            //bool flag = false;
            XmlDocument xmlDocument = new XmlDocument();
            switch (accessMode) {
                //case ConfigurationFile.FileAccessMode.OpenExisting:
                //    if (!File.Exists(this.FileName))
                //        throw new FileNotFoundException("Config file {0} does not exist");
                //    xmlDocument.Load(this.FileName);
                //    break;
                //case ConfigurationFile.FileAccessMode.Create:
                //    if (File.Exists(this.FileName))
                //        throw new IOException(string.Format("Config file {0} already exists.", (object)this.FileName));
                //    flag = true;
                //    break;
                case ConfigurationFile.FileAccessMode.CreateIfNotExists:
                    if (File.Exists(this.FileName)) {
                        xmlDocument.Load(this.FileName);

                        string json = Path.Combine(Path.GetDirectoryName(FileName), Path.GetFileNameWithoutExtension(FileName) + ".json");
                        using (StreamReader sr = new StreamReader(json))
                        using (JsonTextReader jr = new JsonTextReader(sr)) {
                            _root = JObject.Load(jr);
                        }
                        break;
                    }
                    //flag = true;
                    break;
                //case ConfigurationFile.FileAccessMode.CreateWithOverwrite:
                //    if (File.Exists(this.FileName))
                //        File.Delete(this.FileName);
                //    flag = true;
                //    break;
            }
            //if (flag) {
            //    this.RootElement = xmlDocument.CreateElement("Configuration");
            //    xmlDocument.AppendChild((XmlNode)this.RootElement);
            //    xmlDocument.Save(this.FileName);
            //}
            this.RootElement = xmlDocument.DocumentElement;
        }

        public string FileName { get; set; } = string.Empty;

        internal XmlDocument Dom => this.RootElement.OwnerDocument;

        internal XmlElement RootElement { get; }

        //public ConfigurationFileProperty RootProperty {
        //    get {
        //        if (this._rootProperty == null)
        //            this._rootProperty = new ConfigurationFileProperty(this, this.RootElement, "//", 0);
        //        return this._rootProperty;
        //    }
        //}

        public CultureInfo FormatCulture { get; set; }

        public bool AutoSave { get; set; }

        public string DateTimeFormat { get; set; } = "yyyyMMdd HH:mm:ss.FFFF";

        public bool WriteType { get; set; }

        public static string GetDefaultConfigFileName_OLD(Assembly asm) => asm.Location + ".cfg";

        public static string GetDefaultConfigFileName(Assembly asm) => asm.Location + ".json";

        public static bool TryGetConfigFile(Assembly asm, out ConfigurationFile config) {
            config = null;
            if (File.Exists(ConfigurationFile.GetDefaultConfigFileName_OLD(asm))) {
                config = new ConfigurationFile();
            }
            return config != null;
        }

        public void Save() {
            if (this.FileName == null)
                throw new NullReferenceException("No output file name defined.");
            this.Dom.Save(this.FileName);
        }

        public void Save(string fileName) {
            this.FileName = fileName;
            this.Save();
        }

        public T TryGetConfig<T>(string path) where T : class {
            var token = _root.SelectToken(path);
            if (token == null) {
                return null;
            }

            using (var jr = token.CreateReader()) {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<T>(jr);
            }
        }

        public bool TryGetConfig<T>(string path, out T config) where T : class {
            config = TryGetConfig<T>(path);
            return config != null;
        }

        public T GetConfig<T>(string path) where T : class {
            var config = TryGetConfig<T>(path);
            if (config == null) {
                throw new ArgumentException($"unknown config path '{path}'");
            }
            return config;
        }

        public void Save(XmlWriter writer) => this.Dom.Save(writer);

        public void Save(Stream stream) => this.Dom.Save(stream);

        [DebuggerNonUserCode]
        public bool ExistsProperty(string path, string name) => this.ExistsProperty(path, name, 0);

        [DebuggerNonUserCode]
        public bool ExistsProperty(string path, string name, int index) => this.GetProperties(path, name).Length > index;

        //public int GetCount(string path, string name) => this.GetProperties(path, name).Length;

        public int GetCount(ConfigurationFileProperty parent, string name) => this.GetProperties(parent.GetSubPropertyPath(), name).Length;

        public void DeleteProperty(string path, string name) => this.GetProperty(path, name, false)?.Delete();

        public void SetProperty(string path, string name, object value) {
            this.GetProperty(path, name, true).Set(value);
            this.AutoSaveInternal();
        }

        public void SetProperty(string path, string name, string value) {
            this.GetProperty(path, name, true).Set(value);
            this.AutoSaveInternal();
        }

        public void SetProperty(string path, string name, bool value) {
            this.GetProperty(path, name, true).Set(value);
            this.AutoSaveInternal();
        }

        public void SetProperty(string path, string name, int value) {
            this.GetProperty(path, name, true).Set(value);
            this.AutoSaveInternal();
        }

        public void SetProperty(string path, string name, long value) {
            this.GetProperty(path, name, true).Set(value);
            this.AutoSaveInternal();
        }

        public void SetProperty(string path, string name, float value) {
            this.GetProperty(path, name, true).Set(value);
            this.AutoSaveInternal();
        }

        public void SetProperty(string path, string name, double value) {
            this.GetProperty(path, name, true).Set(value);
            this.AutoSaveInternal();
        }

        public void SetProperty(string path, string name, DateTime value) {
            this.GetProperty(path, name, true).Set(value);
            this.AutoSaveInternal();
        }

        public void SetProperty(string path, string name, Array value) {
            this.GetProperty(path, name, true).Set(value);
            this.AutoSaveInternal();
        }

        public void SetProperty(string path, string name, Hashtable value) {
            this.GetProperty(path, name, true).Set(value);
            this.AutoSaveInternal();
        }

        public void SetProperty(string path, string name, IDictionary value) {
            this.GetProperty(path, name, true).Set(value);
            this.AutoSaveInternal();
        }

        public void SetProperty(string path, string name, XmlElement value) {
            this.GetProperty(path, name, true).Set(value);
            this.AutoSaveInternal();
        }

        public ConfigurationFileProperty GetProperty(
          string path,
          string name,
          int index,
          bool autoCreate) {
            XmlElement pathElement = this.GetPathElement(path, autoCreate);
            XmlElement element1 = (XmlElement)null;
            if (pathElement != null) {
                string xpath = name == null ? string.Format("[position()={0}]", (object)(index + 1)) : string.Format("{0} [position()={1}]", (object)name, (object)(index + 1));
                element1 = (XmlElement)pathElement.SelectSingleNode(xpath);
                if (element1 == null && autoCreate) {
                    while (element1 == null) {
                        XmlElement element2 = this.Dom.CreateElement(name);
                        pathElement.AppendChild((XmlNode)element2);
                        element1 = (XmlElement)pathElement.SelectSingleNode(xpath);
                        this.SetChanged();
                    }
                }
            }
            if (element1 == null)
                throw new ConfigurationFilePropertyDoesNotExistException(path, name);
            ConfigurationFileProperty property = new ConfigurationFileProperty(this, element1, path, index);
            this.AutoSaveInternal();
            return property;
        }

        public ConfigurationFileProperty GetProperty(string path, string name) => this.GetProperty(path, name, 0, false);

        [DebuggerNonUserCode]
        public ConfigurationFileProperty GetProperty(
          string path,
          string name,
          bool autoCreate) {
            return this.GetProperty(path, name, 0, autoCreate);
        }

        public ConfigurationFileProperty[] GetProperties(string path) => this.GetProperties(path, (string)null);

        public ConfigurationFileProperty[] GetProperties(
          string path,
          string name) {
            List<ConfigurationFileProperty> configurationFilePropertyList = new List<ConfigurationFileProperty>();
            XmlElement pathElement = this.GetPathElement(path, false);
            if (pathElement != null) {
                string xpath = name == null ? string.Format("./*") : string.Format("{0}", (object)name);
                Dictionary<string, int> dictionary = new Dictionary<string, int>();
                foreach (XmlElement selectNode in pathElement.SelectNodes(xpath)) {
                    int index = 0;
                    if (dictionary.ContainsKey(selectNode.Name))
                        index = dictionary[selectNode.Name];
                    dictionary[selectNode.Name] = index + 1;
                    configurationFilePropertyList.Add(new ConfigurationFileProperty(this, selectNode, path, index));
                }
            }
            return configurationFilePropertyList.ToArray();
        }

        public string GetPropertyString(string path, string name) => this.GetProperty(path, name, false).ToString();

        public int GetPropertyInt32(string path, string name) => this.GetProperty(path, name, false).ToInt32();

        public bool GetPropertyBoolean(string path, string name) => this.GetProperty(path, name, false).ToBoolean();

        public long GetPropertyInt64(string path, string name) => this.GetProperty(path, name, false).ToInt64();

        public float GetPropertySingle(string path, string name) => this.GetProperty(path, name, false).ToSingle();

        public double GetPropertyDouble(string path, string name) => this.GetProperty(path, name, false).ToDouble();

        public DateTime GetPropertyDateTime(string path, string name) => this.GetProperty(path, name, false).ToDateTime();

        public Array GetPropertyArray(string path, string name, bool bFallbackToString) => this.GetProperty(path, name, false).ToArray(bFallbackToString);

        public Hashtable GetPropertyHashtable(
          string path,
          string name,
          bool bFallbackToString) {
            return this.GetProperty(path, name, false).ToHashtable(bFallbackToString);
        }

        public IDictionary GetPropertyDictionary(
          string path,
          string name,
          bool bFallbackToString) {
            return this.GetProperty(path, name, false).ToDictionary(bFallbackToString);
        }

        public XmlElement GetPropertyElement(string path, string name) => this.GetProperty(path, name, false).ToXmlElement();

        public string GetPropertyString(string path, string name, string defaultValue) => this.GetProperty(path, name, true).ToString(defaultValue);

        public int GetPropertyInt32(string path, string name, int defaultValue) => this.GetProperty(path, name, true).ToInt32(defaultValue);

        public bool GetPropertyBoolean(string path, string name, bool defaultValue) => this.GetProperty(path, name, true).ToBoolean(defaultValue);

        public long GetPropertyInt64(string path, string name, long defaultValue) => this.GetProperty(path, name, true).ToInt64(defaultValue);

        public float GetPropertySingle(string path, string name, float defaultValue) => this.GetProperty(path, name, true).ToSingle(defaultValue);

        public double GetPropertyDouble(string path, string name, double defaultValue) => this.GetProperty(path, name, true).ToDouble(defaultValue);

        public DateTime GetPropertyDateTime(string path, string name, DateTime defaultValue) => this.GetProperty(path, name, true).ToDateTime(defaultValue);

        public Array GetPropertyArray(
          string path,
          string name,
          bool bFallbackToString,
          Array defaultValue) {
            return this.GetProperty(path, name, true).ToArray(bFallbackToString, defaultValue);
        }

        public Hashtable GetPropertyHashtable(
          string path,
          string name,
          bool bFallbackToString,
          Hashtable defaultValue) {
            return this.GetProperty(path, name, true).ToHashtable(bFallbackToString, defaultValue);
        }

        public IDictionary GetPropertyDictionary(
          string path,
          string name,
          bool bFallbackToString,
          IDictionary defaultValue) {
            return this.GetProperty(path, name, true).ToDictionary(bFallbackToString, defaultValue);
        }

        public XmlElement GetPropertyXmlElement(
          string path,
          string name,
          XmlElement defaultValue) {
            return this.GetProperty(path, name, true).ToXmlElement(defaultValue);
        }

        internal void AutoSaveInternal() {
            if (!this.AutoSave || !this._changed)
                return;
            this.Save();
        }

        internal void SetChanged() => this._changed = true;

        private XmlElement GetPathElement(string path, bool autoCreate) {
            path = path.Replace("//", "\0");
            string[] strArray = path.Split(new char[1] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            for (int index = 0; index < strArray.Length; ++index)
                strArray[index] = strArray[index].Replace("\0", "//");
            path = path.Replace("\0", "//");
            XmlElement pathElement = this.RootElement;
            for (int index = 0; index < strArray.Length; ++index) {
                XmlElement newChild = (XmlElement)pathElement.SelectSingleNode(strArray[index]);
                if (newChild == null) {
                    if (autoCreate) {
                        newChild = this.Dom.CreateElement(strArray[index]);
                        pathElement.AppendChild((XmlNode)newChild);
                        this.SetChanged();
                    }
                    else {
                        pathElement = (XmlElement)null;
                        break;
                    }
                }
                pathElement = newChild;
            }
            return pathElement;
        }

        //bool IConfigurationSource.AutoSave {
        //    [DebuggerNonUserCode]
        //    get => this.AutoSave;
        //    [DebuggerNonUserCode]
        //    set => this.AutoSave = value;
        //}

        //bool IConfigurationSource.WriteType {
        //    [DebuggerNonUserCode]
        //    get => this.WriteType;
        //    [DebuggerNonUserCode]
        //    set => this.WriteType = value;
        //}

        //IConfigurationProperty IConfigurationSource.RootProperty {
        //    [DebuggerNonUserCode]
        //    get => (IConfigurationProperty)this.RootProperty;
        //}

        //[DebuggerNonUserCode]
        //int IConfigurationSource.GetCount(string path, string name) => this.GetCount(path, name);

        //[DebuggerNonUserCode]
        //int IConfigurationSource.GetCount(
        //  IConfigurationProperty parent,
        //  string name)
        //{
        //  return this.GetCount((ConfigurationFileProperty) parent, name);
        //}

        //[DebuggerNonUserCode]
        //IConfigurationProperty[] IConfigurationSource.GetProperties(
        //  string path)
        //{
        //  return (IConfigurationProperty[]) this.GetProperties(path);
        //}

        //[DebuggerNonUserCode]
        //IConfigurationProperty[] IConfigurationSource.GetProperties(
        //  string path,
        //  string name)
        //{
        //  return (IConfigurationProperty[]) this.GetProperties(path, name);
        //}

        //[DebuggerNonUserCode]
        //IConfigurationProperty IConfigurationSource.GetProperty(
        //  string path,
        //  string name,
        //  int index,
        //  bool autoCreate)
        //{
        //  return (IConfigurationProperty) this.GetProperty(path, name, index, autoCreate);
        //}

        //[DebuggerNonUserCode]
        //IConfigurationProperty IConfigurationSource.GetProperty(
        //  string path,
        //  string name,
        //  bool autoCreate) {
        //    return (IConfigurationProperty)this.GetProperty(path, name, autoCreate);
        //}

        //[DebuggerNonUserCode]
        //IConfigurationProperty IConfigurationSource.GetProperty(
        //  string path,
        //  string name) {
        //    return (IConfigurationProperty)this.GetProperty(path, name);
        //}

        //[DebuggerNonUserCode]
        //string IConfigurationSource.GetPropertyString(
        //  string path,
        //  string name,
        //  string defaultValue)
        //{
        //  return this.GetPropertyString(path, name, defaultValue);
        //}

        //[DebuggerNonUserCode]
        //string IConfigurationSource.GetPropertyString(string path, string name) => this.GetPropertyString(path, name);

        [DebuggerNonUserCode]
        void IConfigurationSource.Save() => this.Save();

        [DebuggerNonUserCode]
        bool IConfigurationSource.ExistsProperty(string path, string name) => this.ExistsProperty(path, name);

        //[DebuggerNonUserCode]
        //bool IConfigurationSource.ExistsProperty(
        //  string path,
        //  string name,
        //  int index)
        //{
        //  return this.ExistsProperty(path, name, index);
        //}

        //[DebuggerNonUserCode]
        //void IConfigurationSource.SetProperty(
        //  string path,
        //  string name,
        //  string value)
        //{
        //  this.SetProperty(path, name, value);
        //}

        public enum FileAccessMode {
            OpenExisting,
            Create,
            CreateIfNotExists,
            CreateWithOverwrite,
        }
    }
}
