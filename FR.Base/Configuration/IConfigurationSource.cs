// Decompiled with JetBrains decompiler
// Type: FR.Configuration.IConfigurationSource
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

namespace FR.Configuration {
    public interface IConfigurationSource {
        //bool AutoSave { get; set; }

        //bool WriteType { get; set; }

        //IConfigurationProperty RootProperty { get; }

        //int GetCount(string path, string name);

        //int GetCount(IConfigurationProperty parent, string name);

        //IConfigurationProperty[] GetProperties(string path);

        //IConfigurationProperty[] GetProperties(string path, string name);

        //IConfigurationProperty GetProperty(
        //  string path,
        //  string name,
        //  int index,
        //  bool autoCreate);

        //IConfigurationProperty GetProperty(
        //  string path,
        //  string name,
        //  bool autoCreate);

        //IConfigurationProperty GetProperty(string path, string name);

        //string GetPropertyString(string path, string name, string defaultValue);

        //string GetPropertyString(string path, string name);

        void Save();

        bool ExistsProperty(string path, string name);

        T TryGetConfig<T>(string path) where T : class;

        bool TryGetConfig<T>(string path, out T config) where T : class;

        T GetConfig<T>(string path) where T : class;

        //bool ExistsProperty(string path, string name, int index);

        //void SetProperty(string path, string name, string value);
    }
}
