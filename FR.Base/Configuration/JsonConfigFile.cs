using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FR.Configuration {
    public class JsonConfigFile : IConfigurationSource {
        private JObject _root;

        private JsonConfigFile() {

        }

        //private JsonConfigFile GetDefaultConfigFileName(Assembly assembly) {

        //}

#warning autosave not handled
        // NOT HANDLED
        //public bool AutoSave { get; set; }
        // NOT HANDLED
        //public bool WriteType { get; set; }
        // NOT HANDLED
        //public IConfigurationProperty RootProperty => null;

        public bool ExistsProperty(string path, string name) {
            JObject curr = _root;

            foreach (var section in (path ?? string.Empty).Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries)) {
                curr = (JObject)curr[section];
                if (curr == null)
                    return false;
            }

            return curr.ContainsKey(name);
        }

        //public bool ExistsProperty(string path, string name, int index) {
        //    throw new NotImplementedException();
        //}

        //public int GetCount(string path, string name) {
        //    throw new NotImplementedException();
        //}

        //public int GetCount(IConfigurationProperty parent, string name) {
        //    throw new NotImplementedException();
        //}

        //public IConfigurationProperty[] GetProperties(string path) {
        //    throw new NotImplementedException();
        //}

        //public IConfigurationProperty[] GetProperties(string path, string name) {
        //    throw new NotImplementedException();
        //}

        //public IConfigurationProperty GetProperty(string path, string name, int index, bool autoCreate) {
        //    throw new NotImplementedException();
        //}

        public IConfigurationProperty GetProperty(string path, string name, bool autoCreate) {
            throw new NotImplementedException();
        }

        public IConfigurationProperty GetProperty(string path, string name) {
            throw new NotImplementedException();
        }

        //public string GetPropertyString(string path, string name, string defaultValue) {
        //    throw new NotImplementedException();
        //}

        //public string GetPropertyString(string path, string name) {
        //    throw new NotImplementedException();
        //}

        public void Save() {
            throw new NotImplementedException();
        }

        //public void SetProperty(string path, string name, string value) {
        //    throw new NotImplementedException();
        //}
    }
}
