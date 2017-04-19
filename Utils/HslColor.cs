using System.Drawing;

namespace Vandalbrot.Utils {

    // colour set using hue, saturation and luminosity...
    public class HslColor {

        // Private data members below are on scale 0-1
        // They are scaled for use externally based on scale
        private double myHue = 1.0;
        private double mySaturation = 1.0;
        private double myLuminosity = 1.0;

        private const double Scale = 255.0;

        public HslColor() { }

        public HslColor(Color color) {
            SetRgb(color.R, color.G, color.B);
        }
        public HslColor(int red, int green, int blue) {
            SetRgb(red, green, blue);
        }
        public HslColor(double hue, double saturation, double luminosity) {
            this.Hue = hue;
            this.Saturation = saturation;
            this.Luminosity = luminosity;
        }
        
        public double Hue {
            get { return myHue * Scale; }
            set { myHue = CheckRange(value / Scale); }
        }

        public double Saturation {
            get { return mySaturation * Scale; }
            set { mySaturation = CheckRange(value / Scale); }
        }

        public double Luminosity {
            get { return myLuminosity * Scale; }
            set { myLuminosity = CheckRange(value / Scale); }
        }

        public override string ToString() {
            return $"H: {Hue:#0.##} S: {Saturation:#0.##} L: {Luminosity:#0.##}";
        }

        private double CheckRange(double value) {
            if (value < 0.0)
                value = 0.0;
            else if (value > 1.0)
                value = 1.0;
            return value;
        }
        
        public string ToRgbString() {
            var color = (Color)this;
            return $"R: {color.R:#0.##} G: {color.G:#0.##} B: {color.B:#0.##}";
        }

        #region Casts to/from System.Drawing.Color

        public static implicit operator Color(HslColor hslColor) {
            double r = 0, g = 0, b = 0;
            if (hslColor.myLuminosity != 0) {
                if (hslColor.mySaturation == 0)
                    r = g = b = hslColor.myLuminosity;
                else {
                    double temp2 = GetTemp2(hslColor);
                    double temp1 = 2.0 * hslColor.myLuminosity - temp2;

                    r = GetColorComponent(temp1, temp2, hslColor.myHue + 1.0 / 3.0);
                    g = GetColorComponent(temp1, temp2, hslColor.myHue);
                    b = GetColorComponent(temp1, temp2, hslColor.myHue - 1.0 / 3.0);
                }
            }
            return Color.FromArgb((int)(255 * r), (int)(255 * g), (int)(255 * b));
        }

        private static double GetColorComponent(double temp1, double temp2, double temp3) {
            temp3 = MoveIntoRange(temp3);
            if (temp3 < 1.0 / 6.0)
                return temp1 + (temp2 - temp1) * 6.0 * temp3;
            else if (temp3 < 0.5)
                return temp2;
            else if (temp3 < 2.0 / 3.0)
                return temp1 + ((temp2 - temp1) * ((2.0 / 3.0) - temp3) * 6.0);
            else
                return temp1;
        }

        private static double MoveIntoRange(double temp3) {
            if (temp3 < 0.0)
                temp3 += 1.0;
            else if (temp3 > 1.0)
                temp3 -= 1.0;
            return temp3;
        }

        private static double GetTemp2(HslColor hslColor) {
            double temp2;
            if (hslColor.myLuminosity < 0.5)  //<=??
                temp2 = hslColor.myLuminosity * (1.0 + hslColor.mySaturation);
            else
                temp2 = hslColor.myLuminosity + hslColor.mySaturation - (hslColor.myLuminosity * hslColor.mySaturation);
            return temp2;
        }

        public static implicit operator HslColor(Color color) {
            HslColor hslColor = new HslColor();
            hslColor.myHue = color.GetHue() / 360.0; // we store hue as 0-1 as opposed to 0-360 
            hslColor.myLuminosity = color.GetBrightness();
            hslColor.mySaturation = color.GetSaturation();
            return hslColor;
        }

        #endregion

        public void SetRgb(int red, int green, int blue) {
            HslColor hslColor = (HslColor)Color.FromArgb(red, green, blue);
            this.myHue = hslColor.myHue;
            this.mySaturation = hslColor.mySaturation;
            this.myLuminosity = hslColor.myLuminosity;
        }


    }
}
