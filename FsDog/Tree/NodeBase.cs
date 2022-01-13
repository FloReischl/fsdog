// Decompiled with JetBrains decompiler
// Type: FsDog.NodeBase
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Windows.Forms;

namespace FsDog.Tree {
    public abstract class NodeBase : TreeNodeBase {
        public const string DirectoryImageKeyClosed = "DirectoryClosed";
        public const string DirectoryImageKeyOpen = "DirectoryOpen";

        public NodeBase() => this.CreateDummy();

        public NodeBase FindByText(string text) {
            if (!this.IsExpanded)
                this.Expand();
            foreach (NodeBase node in this.Nodes) {
                if (node.Text == text)
                    return node;
            }
            return (NodeBase)null;
        }

        public virtual void Refresh() {
        }
    }
}
