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
using System.Windows.Forms;
using System.Xml;

namespace FR.Configuration {
    [DebuggerDisplay("FileName: {FileName}")]
    public class ConfigurationFile : IConfigurationSource {
        private bool _changed;
        private JObject _root;

        public ConfigurationFile(string filename) {
            this.FileName = filename;
            if (File.Exists(this.FileName)) {

                string json = Path.Combine(Path.GetDirectoryName(FileName), Path.GetFileNameWithoutExtension(FileName) + ".json");
                using (StreamReader sr = new StreamReader(json))
                using (JsonTextReader jr = new JsonTextReader(sr)) {
                    _root = JObject.Load(jr);
                }
            }
        }

        public string FileName { get; set; } = string.Empty;

        public static string GetUserConfigFileName(Assembly asm) {
            DirectoryInfo appLocal = new DirectoryInfo(Application.LocalUserAppDataPath);
            if (!appLocal.Exists)
                appLocal.Create();
            return Path.Combine(appLocal.FullName, Path.GetFileName(asm.Location + ".json"));
        }

        public static bool TryGetUserConfigFile(Assembly asm, out ConfigurationFile config) {
            config = null;
            var file = GetUserConfigFileName(asm);
            if (File.Exists(file)) {
                config = new ConfigurationFile(file);
            }
            return config != null;
        }

        public static ConfigurationFile TryGetUserConfigFile(Assembly asm) {
            TryGetUserConfigFile(asm, out ConfigurationFile result);
            return result;
        }

        public void Save() {
            if (this.FileName == null)
                throw new NullReferenceException("No output file name defined.");

            using (var sr = new StreamWriter(FileName))
            using (var jw = new JsonTextWriter(sr)) {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Newtonsoft.Json.Formatting.Indented;
                serializer.Serialize(jw, _root);
            }
        }

        public T TryGet<T>(string path, string name, int? index = null) where T : class {
            var query = path + (index == 0 ? $".{name}" : $".{name}[{index}]");
            var token = _root.SelectToken(query);
            if (token == null) {
                return null;
            }

            using (var jr = token.CreateReader()) {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<T>(jr);
            }
        }

        public bool TryGet<T>(string path, string name, out T config, int? index = null) where T : class {
            config = TryGet<T>(path, name, index);
            return config != null;
        }

        public T Get<T>(string path, string name, int? index = null) where T : class {
            var config = TryGet<T>(path, name, index);
            if (config == null) {
                throw new ArgumentException($"unknown config path '{path}'");
            }
            return config;
        }

        public T GetRoot<T>() where T : class {
            if (_root == null) {
                throw new NullReferenceException("Root config item was not initialized");
            }

            using (var jr = _root.CreateReader()) {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<T>(jr);
            }
        }

        public void SetRoot<T>(T config) where T : class {
            _root = (JObject)SerializeInternal<T>(config);
        }

        public void Set<T>(string path, string name, T value, int? index = null) {
            if (path.StartsWith("$.")) {
                path = path.Substring(2);
            }

            var parentToken = _root.SelectToken(path);
            if (parentToken == null) {
                throw new ArgumentException($"Path '{path}' does not exist.");
            }

            JToken jvalue = SerializeInternal<T>(value);

            if (index != null) {
                var parentArray = (JArray)parentToken;
                parentArray[(int)index] = jvalue;
            }
            else {
                var parentObject = (JObject)parentToken;
                parentObject.Remove(name);
                parentObject.Add(name, jvalue);
            }
        }

        private JToken SerializeInternal<T>(T value) {
            if (value == null) {
                return null;
            }
            using (var jw = new JTokenWriter()) {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jw, value);
                return jw.Token;
            }

        }

        internal void SetChanged() => this._changed = true;
    }
}
