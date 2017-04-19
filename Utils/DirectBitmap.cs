using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Vandalbrot.Utils {

    // bitmap using bit array so we can directly access the pixels (faster)
    public class DirectBitmap : IDisposable {
        public Bitmap Bitmap { get; }

        public Int32[] Bits { get; }

        public bool Disposed { get; private set; }

        public int Height { get; }

        public int Width { get; }
        
        private GCHandle BitsHandle { get; }

        public void SetPixel(int x, int y, int colour) {
            if (x >= Width || y >= Height) return;
            var index = (y * (Width)) + x;
            Bits[index] = colour;
        }

        // set a bunch of pixels all at once (for quick 'rough' versions during zoom, pan, etc.)
        public void SetPixels(int x, int y, int colour, int blockSize) {
            for (var i = x; i < x + blockSize; i++) {
                for (var j = y; j < y + blockSize; j++) {
                    SetPixel(i, j, colour);
                }
            }
        }
        
        public DirectBitmap(int width, int height) {
            Width = width;
            Height = height;
            Bits = new Int32[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }

        // cast to bitmap
        public static implicit operator Bitmap(DirectBitmap directBitmap) {
            return directBitmap.Bitmap;
        }

        public void Dispose() {
            if (Disposed) return;
            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }
    }
}
