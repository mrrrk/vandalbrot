using System.Linq;
using System.Numerics;

namespace Vandalbrot.Utils {

    /// <summary>
    /// Calculate mandelbrot set and generate bitmap
    /// </summary>
    class MandelbrotSpace {
        
        private readonly double[] myReals;
        private readonly double[] myImaginaries;
        private readonly int myBlockSize;

        public MandelbrotSpace(
            Complex from,
            Complex to,
            int width, 
            int height, 
            int maxIterations,
            int blockSize) {

            Width = width;
            Height = height;
            MaxIterations = maxIterations;
            myBlockSize = blockSize;

            myReals = GetRange(from.Real, to.Real, Width);
            myImaginaries = GetRange(from.Imaginary, to.Imaginary, Height);
        }

        public int Width { get; }

        public int Height { get; }

        public int MaxIterations { get; }

        public DirectBitmap GetBitmap(Pallete pallete) {
            var bmp = new DirectBitmap(Width, Height);
            //var pixelIndex = 0;
            for (var y = 0; y < bmp.Height; y += myBlockSize) {
                for (var x = 0; x < bmp.Width; x += myBlockSize) {
                    var re = x >= myReals.Length ? myReals.Length - 1 : x;
                    var im = y >= myImaginaries.Length ? myImaginaries.Length - 1 : y;
                    
                    var c = new Complex(myReals[re], myImaginaries[im]);
                    var val = GetDivergence(c, MaxIterations);

                    bmp.SetPixels(x, y, pallete.Colour(val), myBlockSize);
                    //bmp.Bits[pixelIndex++] = pallete.Colour(val);
                }
            }
            return bmp;
        }
        
        private double[] GetRange(double start, double end, int number) {
            var increment = (end - start) / number;
            return Enumerable
                .Repeat(start, number)
                .Select((val, i) => val + (increment * i))
                .ToArray();
        }
        
        // this is the magic sauce!
        private int GetDivergence(Complex c, int maxIterations) {
            var z = new Complex(0, 0);
            int iter;
            for (iter = 0; iter < maxIterations; iter++) {
                z = z * z + c;
                var r = z.Real;
                var i = z.Imaginary;
                // If magnitude > 4, it's bound to diverge, so give up.
                // (this is faster than calculating magnitude) 
                if (r * r + i * i > 16) {
                    break;
                }
            }
            return iter;
        }
    }
}
