// Decompiled with JetBrains decompiler
// Type: FsDog.Properties.Resources
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace FsDog.Properties {
    [CompilerGenerated]
    [DebuggerNonUserCode]
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    internal class Resources {
        private static ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        internal Resources() {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, (object)null))
                    resourceMan = new ResourceManager("FsDog.Properties.Resources", typeof(FsDog.Properties.Resources).Assembly);
                return resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture {
            get => resourceCulture;
            set => resourceCulture = value;
        }

        internal static Bitmap DirectoryClosed => (Bitmap)ResourceManager.GetObject(nameof(DirectoryClosed), resourceCulture);

        internal static Bitmap DirectoryNew => (Bitmap)ResourceManager.GetObject(nameof(DirectoryNew), resourceCulture);

        internal static Bitmap DirectoryOpen => (Bitmap)ResourceManager.GetObject(nameof(DirectoryOpen), resourceCulture);

        internal static Bitmap DosShell => (Bitmap)ResourceManager.GetObject(nameof(DosShell), resourceCulture);

        internal static Bitmap Drive => (Bitmap)ResourceManager.GetObject(nameof(Drive), resourceCulture);

        internal static Bitmap Favorites => (Bitmap)ResourceManager.GetObject(nameof(Favorites), resourceCulture);

        internal static Bitmap FavoritesEdit => (Bitmap)ResourceManager.GetObject(nameof(FavoritesEdit), resourceCulture);

        internal static Bitmap FavoritesItem => (Bitmap)ResourceManager.GetObject(nameof(FavoritesItem), resourceCulture);

        internal static Bitmap File => (Bitmap)ResourceManager.GetObject(nameof(File), resourceCulture);

        internal static Bitmap FsDog => (Bitmap)ResourceManager.GetObject(nameof(FsDog), resourceCulture);

        internal static Bitmap FsDogSplash => (Bitmap)ResourceManager.GetObject(nameof(FsDogSplash), resourceCulture);

        internal static Bitmap Help => (Bitmap)ResourceManager.GetObject(nameof(Help), resourceCulture);

        internal static Bitmap MyComputer => (Bitmap)ResourceManager.GetObject(nameof(MyComputer), resourceCulture);

        internal static Bitmap NetworkDomain => (Bitmap)ResourceManager.GetObject(nameof(NetworkDomain), resourceCulture);

        internal static Bitmap NetworkProvider => (Bitmap)ResourceManager.GetObject(nameof(NetworkProvider), resourceCulture);

        internal static Bitmap NetworkProviderDisabled => (Bitmap)ResourceManager.GetObject(nameof(NetworkProviderDisabled), resourceCulture);

        internal static Bitmap NetworkRoot => (Bitmap)ResourceManager.GetObject(nameof(NetworkRoot), resourceCulture);

        internal static Bitmap NetworkServer => (Bitmap)ResourceManager.GetObject(nameof(NetworkServer), resourceCulture);

        internal static Bitmap NetworkShare => (Bitmap)ResourceManager.GetObject(nameof(NetworkShare), resourceCulture);

        internal static Bitmap PowerShell => (Bitmap)ResourceManager.GetObject(nameof(PowerShell), resourceCulture);

        internal static Bitmap Preview => (Bitmap)ResourceManager.GetObject(nameof(Preview), resourceCulture);

        internal static Bitmap Properties => (Bitmap)ResourceManager.GetObject(nameof(Properties), resourceCulture);

        internal static Bitmap Script => (Bitmap)ResourceManager.GetObject(nameof(Script), resourceCulture);
    }
}
