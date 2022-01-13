// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.RichTextBoxBase
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FR.Windows.Forms {
    public class RichTextBoxBase : RichTextBox {
        private const int WM_USER = 1024;
        private const int EM_GETCHARFORMAT = 1082;
        private const int EM_SETCHARFORMAT = 1092;
        private const int SCF_SELECTION = 1;
        private const int SCF_WORD = 2;
        private const int SCF_ALL = 4;
        private const uint CFE_BOLD = 1;
        private const uint CFE_ITALIC = 2;
        private const uint CFE_UNDERLINE = 4;
        private const uint CFE_STRIKEOUT = 8;
        private const uint CFE_PROTECTED = 16;
        private const uint CFE_LINK = 32;
        private const uint CFE_AUTOCOLOR = 1073741824;
        private const uint CFE_SUBSCRIPT = 65536;
        private const uint CFE_SUPERSCRIPT = 131072;
        private const int CFM_SMALLCAPS = 64;
        private const int CFM_ALLCAPS = 128;
        private const int CFM_HIDDEN = 256;
        private const int CFM_OUTLINE = 512;
        private const int CFM_SHADOW = 1024;
        private const int CFM_EMBOSS = 2048;
        private const int CFM_IMPRINT = 4096;
        private const int CFM_DISABLED = 8192;
        private const int CFM_REVISED = 16384;
        private const int CFM_BACKCOLOR = 67108864;
        private const int CFM_LCID = 33554432;
        private const int CFM_UNDERLINETYPE = 8388608;
        private const int CFM_WEIGHT = 4194304;
        private const int CFM_SPACING = 2097152;
        private const int CFM_KERNING = 1048576;
        private const int CFM_STYLE = 524288;
        private const int CFM_ANIMATION = 262144;
        private const int CFM_REVAUTHOR = 32768;
        private const uint CFM_BOLD = 1;
        private const uint CFM_ITALIC = 2;
        private const uint CFM_UNDERLINE = 4;
        private const uint CFM_STRIKEOUT = 8;
        private const uint CFM_PROTECTED = 16;
        private const uint CFM_LINK = 32;
        private const uint CFM_SIZE = 2147483648;
        private const uint CFM_COLOR = 1073741824;
        private const uint CFM_FACE = 536870912;
        private const uint CFM_OFFSET = 268435456;
        private const uint CFM_CHARSET = 134217728;
        private const uint CFM_SUBSCRIPT = 196608;
        private const uint CFM_SUPERSCRIPT = 196608;
        private const byte CFU_UNDERLINENONE = 0;
        private const byte CFU_UNDERLINE = 1;
        private const byte CFU_UNDERLINEWORD = 2;
        private const byte CFU_UNDERLINEDOUBLE = 3;
        private const byte CFU_UNDERLINEDOTTED = 4;
        private const byte CFU_UNDERLINEDASH = 5;
        private const byte CFU_UNDERLINEDASHDOT = 6;
        private const byte CFU_UNDERLINEDASHDOTDOT = 7;
        private const byte CFU_UNDERLINEWAVE = 8;
        private const byte CFU_UNDERLINETHICK = 9;
        private const byte CFU_UNDERLINEHAIRLINE = 10;

        public RichTextBoxBase() => this.DetectUrls = false;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(
          IntPtr hWnd,
          int msg,
          IntPtr wParam,
          IntPtr lParam);

        public void InsertLink(string text) => this.InsertLink(text, this.SelectionStart);

        public void InsertLink(string text, int position) {
            if (position < 0 || position > this.Text.Length)
                throw new ArgumentOutOfRangeException(nameof(position));
            this.SelectionStart = position;
            this.SelectedText = text;
            this.Select(position, text.Length);
            this.SetSelectionLink(true);
            this.Select(position + text.Length, 0);
        }

        public void InsertLink(string text, string hyperlink) => this.InsertLink(text, hyperlink, this.SelectionStart);

        public void InsertLink(string text, string hyperlink, int position) {
            if (position < 0 || position > this.Text.Length)
                throw new ArgumentOutOfRangeException(nameof(position));
            this.SelectionStart = position;
            this.SelectedRtf = "{\\rtf1\\ansi " + text + "\\v #" + hyperlink + "\\v0}";
            this.Select(position, text.Length + hyperlink.Length + 1);
            this.SetSelectionLink(true);
            this.Select(position + text.Length + hyperlink.Length + 1, 0);
        }

        public void SetSelectionLink(bool link) => this.SetSelectionStyle(32U, link ? 32U : 0U);

        public int GetSelectionLink() => this.GetSelectionStyle(32U, 32U);

        private void SetSelectionStyle(uint mask, uint effect) {
            RichTextBoxBase.CHARFORMAT2_STRUCT structure = new RichTextBoxBase.CHARFORMAT2_STRUCT();
            structure.cbSize = (uint)Marshal.SizeOf((object)structure);
            structure.dwMask = mask;
            structure.dwEffects = effect;
            IntPtr wParam = new IntPtr(1);
            IntPtr num = Marshal.AllocCoTaskMem(Marshal.SizeOf((object)structure));
            Marshal.StructureToPtr((object)structure, num, false);
            RichTextBoxBase.SendMessage(this.Handle, 1092, wParam, num);
            Marshal.FreeCoTaskMem(num);
        }

        private int GetSelectionStyle(uint mask, uint effect) {
            RichTextBoxBase.CHARFORMAT2_STRUCT structure1 = new RichTextBoxBase.CHARFORMAT2_STRUCT();
            structure1.cbSize = (uint)Marshal.SizeOf((object)structure1);
            structure1.szFaceName = new char[32];
            IntPtr wParam = new IntPtr(1);
            IntPtr num = Marshal.AllocCoTaskMem(Marshal.SizeOf((object)structure1));
            Marshal.StructureToPtr((object)structure1, num, false);
            RichTextBoxBase.SendMessage(this.Handle, 1082, wParam, num);
            RichTextBoxBase.CHARFORMAT2_STRUCT structure2 = (RichTextBoxBase.CHARFORMAT2_STRUCT)Marshal.PtrToStructure(num, typeof(RichTextBoxBase.CHARFORMAT2_STRUCT));
            int selectionStyle = ((int)structure2.dwMask & (int)mask) != (int)mask ? -1 : (((int)structure2.dwEffects & (int)effect) != (int)effect ? 0 : 1);
            Marshal.FreeCoTaskMem(num);
            return selectionStyle;
        }

        private struct CHARFORMAT2_STRUCT {
#pragma warning disable 0649
            public uint cbSize;
            public uint dwMask;
            public uint dwEffects;
            public int yHeight;
            public int yOffset;
            public int crTextColor;
            public byte bCharSet;
            public byte bPitchAndFamily;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public char[] szFaceName;
            public ushort wWeight;
            public ushort sSpacing;
            public int crBackColor;
            public int lcid;
            public int dwReserved;
            public short sStyle;
            public short wKerning;
            public byte bUnderlineType;
            public byte bAnimation;
            public byte bRevAuthor;
            public byte bReserved1;
#pragma warning restore 0649
        }
    }
}
