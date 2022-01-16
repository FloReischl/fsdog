using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FR.IO {
    public static class TextFile {

        public static bool CouldBeTextFile(string file) {
            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read)) {
                var count = Math.Min(102400L, file.Length);
                for (int i = 0; i < count; i++) {
                    int b = fs.ReadByte();
                    if (b == 0) {
                        return false;
                    }
                }
                return true;
            }
        }

        public static bool Contains(string file, string find, StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase) {
            int chunkSize = Math.Max(102400, find.Length * 2);
            char[] buffer = new char[chunkSize];
            var index = 0;

            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(fs, true)) {
                var size = fs.Length;
                StringBuilder chunk = new StringBuilder(chunkSize + find.Length);
                while (!reader.EndOfStream) {
                    chunk.Remove(0, chunk.Length - Math.Min(chunk.Length, find.Length));
                    var count = (int)Math.Min(chunkSize, size - index);
                    count = reader.ReadBlock(buffer, 0, count);
                    index += count; 
                    chunk.Append(new string(buffer, 0, count));
                    if (chunk.ToString().IndexOf(find, comparisonType) != -1) {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
