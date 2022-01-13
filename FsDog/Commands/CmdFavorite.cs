// Decompiled with JetBrains decompiler
// Type: FsDog.CmdFavorite
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Commands;
using FR.Configuration;
using FR.Windows.Forms.Commands;
using FsDog.Properties;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;

namespace FsDog.Commands {
    public class CmdFavorite : CmdFsDogIntern {
        public const string FavoritesMenuName = "F&avorites";

        public static CommandToolItem GetFavoritesToolItem() {
            FsApp instance = FsApp.Instance;
            CommandToolItem favoritesToolItem = new CommandToolItem("F&avorites");
            favoritesToolItem.Name = "F&avorites";
            foreach (FavoriteInfo info in CmdFavorite.GetInfos())
                favoritesToolItem.Items.Add(new CommandToolItem(info.DirectoryName, typeof(CmdFavorite), (Image)Resources.FavoritesItem) {
                    CommandContext = {
            {
              (object) "FavoriteInfo",
              (object) info
            }
          }
                });
            CommandToolItem commandToolItem = new CommandToolItem("-");
            favoritesToolItem.Items.Add(commandToolItem);
            favoritesToolItem.Items.Add(new CommandToolItem("Edit Favorites", typeof(CmdFavoritesEdit)) {
                ShowNeverToolStrip = true
            });
            return favoritesToolItem;
        }

        public static ReadOnlyCollection<FavoriteInfo> GetInfos() {
            List<FavoriteInfo> list = new List<FavoriteInfo>();
            foreach (IConfigurationProperty subProperty in FsApp.Instance.ConfigurationSource.GetProperty(".", "Favorites", true).GetSubProperties("Item"))
                list.Add(new FavoriteInfo() {
                    DirectoryName = subProperty["Directory"].ToString()
                });
            return new ReadOnlyCollection<FavoriteInfo>((IList<FavoriteInfo>)list);
        }

        public override void Execute() {
            FavoriteInfo favoriteInfo = (FavoriteInfo)this.Context[(object)"FavoriteInfo"];
            if (this.CurrentDetailView != null && Directory.Exists(favoriteInfo.DirectoryName)) {
                this.CurrentDetailView.OnRequestParentDirectory(new DirectoryInfo(favoriteInfo.DirectoryName));
                this.ExecutionState = CommandExecutionState.Ok;
            }
            else
                this.ExecutionState = CommandExecutionState.Canceled;
        }
    }
}
