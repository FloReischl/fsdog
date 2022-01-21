using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsDog.Configuration {
    static class ConfigConverter {
        private static Dictionary<string, Color> _colors = new Dictionary<string, Color>();
        private static ColorConverter _colorConverter = new ColorConverter();
        private static Dictionary<string, Font> _fonts = new Dictionary<string, Font>();
        private static FontConverter _fontConverter = new FontConverter();
        private static readonly PointConverter _pointConverter = new PointConverter();
        private static readonly Dictionary<string, Point> _points = new Dictionary<string, Point>();
        private static readonly SizeConverter _sizeConverter = new SizeConverter();
        private static readonly Dictionary<string, Size> _sizes = new Dictionary<string, Size>();

        internal static Color ColorFromString(string name) {
            if (!_colors.TryGetValue(name, out Color color)) {
                color = (Color)_colorConverter.ConvertFromString(name);
                _colors.Add(name, color);
            }
            return color;
        }

        internal static string ColorToString(Color color) => _colorConverter.ConvertToString(color);

        internal static Font FontFromString(string name) {
            if (!_fonts.TryGetValue(name, out Font font)) {
                font = (Font)_fontConverter.ConvertFromString(name);
                _fonts.Add(name, font);
            }
            return font;
        }

        internal static string FontToString(Font font) => _fontConverter.ConvertToString(font);

        internal static Size SizeFromString(string value) {
            if (!_sizes.TryGetValue(value, out Size result)) {
                result = (Size)_sizeConverter.ConvertFromString(value);
            }
            return result;
        }

        internal static string SizeToString(Size size) => _sizeConverter.ConvertToString(size);

        internal static Point PointFromString(string value) {
            if (!_points.TryGetValue(value, out Point result)) {
                result = (Point)_pointConverter.ConvertFromString(value);
            }
            return result;
        }

        internal static string PointToString(Point point) => _pointConverter.ConvertToString(point);
    }
}
