// Decompiled with JetBrains decompiler
// Type: FsDog.CmdViewGotoFavorite
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Commands;
using FR.Windows.Forms;
using FsDog.Commands.Favorites;
using FsDog.Properties;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FsDog.Commands.View {
    public class CmdViewGotoFavorite : CmdFsDogIntern {
        public override void Execute() {
            FormListSelect formListSelect = new FormListSelect();
            formListSelect.AddColumn("Path");
            formListSelect.Description = "Select a directory to navigate to";
            formListSelect.SmallImageList = new ImageList();
            formListSelect.SmallImageList.Images.Add("Fav", (Image)Resources.FavoritesItem);
            formListSelect.MaximumSize = new Size(formListSelect.Width, this.Application.MainForm.Height);

            foreach (FavoriteInfo info in CmdFavorite.GetInfos()) {
                FormListSelectItem formListSelectItem = formListSelect.AddItem((object)info.DirectoryName, info.DirectoryName);
                formListSelectItem.ImageKey = "Fav";
                if (formListSelect.SelectedItem == null)
                    formListSelect.SelectedItem = formListSelectItem;
            }

            if (formListSelect.ShowDialog((IWin32Window)this.Application.MainForm) == DialogResult.OK) {
                this.CurrentDetailView.OnRequestParentDirectory(new DirectoryInfo(formListSelect.SelectedItem.Value.ToString()));
            }
        }
    }
}
