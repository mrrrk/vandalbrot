using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Vandalbrot.Utils {

    /// <summary>
    /// Set of colours through all the hues
    /// </summary>
    class Pallete {

        private readonly Int32[] myColours;

        public Pallete(int max) {
            myColours = new Int32[max + 1];
            for (var i = 0; i < max; i++) {
                var val = ((double)i / max) * 255;
                myColours[i] = ((Color)new HslColor(val, 255, 128)).ToArgb();
            }
            myColours = Rotate(myColours, 50).ToArray();
            myColours[max] = Color.Black.ToArgb();
        }

        public Int32 Colour(int index) => myColours[index];

        // shift em round a bit
        private IEnumerable<T> Rotate<T>(IList<T> values, int shift) {
            for (var index = 0; index < values.Count; index++) {
                yield return values[MathMod(index - shift, values.Count)];
            }
        }

        private int MathMod(int a, int b) {
            return ((a % b) + b) % b;
        }

    }
}
