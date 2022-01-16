// Decompiled with JetBrains decompiler
// Type: FsDog.PreviewInfo
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace FsDog.Detail {
    internal class PreviewInfo {
        private static Dictionary<string, string> _dictTxt;
        private static Dictionary<string, string> _dictImg;

        public static string TypeToString(PreviewType type) {
            switch (type) {
                case PreviewType.Text:
                    return "Text";
                case PreviewType.Image:
                    return "Image";
                case PreviewType.AutoDetect:
                    return "Auto Detect";
                default:
                    return "Unknown";
            }
        }

        public static IPreviewControl GetControl(PreviewType type, string fileName) {
            if (type == PreviewType.AutoDetect)
                type = PreviewInfo.GetTypeForFile(fileName);
            if (type == PreviewType.Unknown)
                return (IPreviewControl)null;
            if (type == PreviewType.Image)
                return (IPreviewControl)new PreviewImage();
            return type == PreviewType.Text ? (IPreviewControl)new PreviewText() : (IPreviewControl)null;
        }

        public static PreviewType GetTypeForFile(string fileName) {
            if (Directory.Exists(fileName))
                return PreviewType.Unknown;

            if (PreviewInfo._dictTxt == null) {
                FsApp instance = FsApp.Instance;
                PreviewInfo._dictTxt = new Dictionary<string, string>((IEqualityComparer<string>)StringComparer.CurrentCultureIgnoreCase);
                string textExtensions = instance.Options.Preview.TextExtensions;
                char[] separator = new char[1] { ';' };
                foreach (string key in textExtensions.Split(separator, StringSplitOptions.RemoveEmptyEntries)) {
                    if (!PreviewInfo._dictTxt.ContainsKey(key))
                        PreviewInfo._dictTxt.Add(key, key);
                }
            }

            if (PreviewInfo._dictTxt.ContainsKey(Path.GetExtension(fileName)))
                return PreviewType.Text;

            if (TextFile.CouldBeTextFile(fileName)) {
                return PreviewType.Text;
            }

            if (PreviewInfo._dictImg == null) {
                FsApp instance = FsApp.Instance;
                PreviewInfo._dictImg = new Dictionary<string, string>((IEqualityComparer<string>)StringComparer.CurrentCultureIgnoreCase);
                string imageExtensions = instance.Options.Preview.ImageExtensions;
                char[] separator = new char[1] { ';' };
                foreach (string key in imageExtensions.Split(separator, StringSplitOptions.RemoveEmptyEntries)) {
                    if (!PreviewInfo._dictImg.ContainsKey(key))
                        PreviewInfo._dictImg.Add(key, key);
                }
            }

            return PreviewInfo._dictImg.ContainsKey(Path.GetExtension(fileName)) ? PreviewType.Image : PreviewType.Unknown;
        }

        public static void RefreshExtensions() {
            PreviewInfo._dictTxt = (Dictionary<string, string>)null;
            PreviewInfo._dictImg = (Dictionary<string, string>)null;
        }
    }
}
