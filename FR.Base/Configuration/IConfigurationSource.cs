// Decompiled with JetBrains decompiler
// Type: FR.Configuration.IConfigurationSource
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

namespace FR.Configuration {
    public interface IConfigurationSource {
        void Save();

        T TryGet<T>(string path, string name, int? index = null) where T : class;

        bool TryGet<T>(string path, string name, out T config, int? index = null) where T : class;

        T Get<T>(string path, string name, int? index = null) where T : class;
    }
}
