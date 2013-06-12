using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Flowdock.Services.Util {
    public class ColorConverter {
        // This only parses the ARGB format
        public static Color? ConvertFromArgbString(string value) {
            if (value == null) {
                return null;
            }

            // Get rid of any trailing # symbols (should only be one)
            value = value.TrimStart('#');

            if (value.Length != 8) {
                throw new ArgumentException("The string value needs to be in ARGB format (ex: #FF001122)");
            }
            
            byte alpha = CreateByteValue(value.Substring(0, 2));
            byte red = CreateByteValue(value.Substring(2, 2));
            byte green = CreateByteValue(value.Substring(4, 2));
            byte blue = CreateByteValue(value.Substring(6, 2));

            return Color.FromArgb(alpha, red, green, blue);
        }

        private static byte CreateByteValue(string byteValue) {
            return System.Convert.ToByte(byteValue, 16);
        }
    }
}
