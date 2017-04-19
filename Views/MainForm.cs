using System;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vandalbrot.Utils;

// todo:
// - zoom is buggy
// - debouncing

namespace Vandalbrot.Views {
    public partial class MainForm : Form {

        private int myMaxIter = 120;
        private const int myRoughBlockSize = 10;
        private const int myFineBlockSize = 1;
        private const double myZoomFactor = 0.2;
        private Pallete myPallete;
        private Complex myFrom = new Complex(-2, -1.5);
        private Complex myTo = new Complex(1.5, 1.5);

        // to allow old bitmaps to be disposed
        private DirectBitmap myLastBitmap;

        // monitor UI events
        private bool myIsZooming = false;
        private DateTime myLastZoom;
        private Size myOldSize;
        private Point myMouseWasAt;
        private bool myIsPanning = false;

        public MainForm() {
            InitializeComponent();
            myPallete = new Pallete(myMaxIter);
            BitmapPictureBox.MouseWheel += OnMouseWheel;
        }

        private void MainForm_Load(object sender, EventArgs e) {
            myOldSize = BitmapPictureBox.Size;
            SetInitialCoords();
            Draw(myFineBlockSize);
        }
        
        private async void Draw(int blockSize) {
            // TODO - debounce?

             var newBitmap = await Task.Run(() => {
                var rect = BitmapPictureBox.Bounds;
                var space = new MandelbrotSpace(
                    myFrom,
                    myTo,
                    rect.Width,
                    rect.Height,
                    myMaxIter,
                    blockSize
                );
                var swatch = Stopwatch.StartNew();
                var bmp = space.GetBitmap(myPallete);
                swatch.Stop();
                Debug.WriteLine($"calculated and rendered mandelbrot: {swatch.ElapsedMilliseconds:0.000}ms blocksize={blockSize}");               
                return bmp;
            });
            BitmapPictureBox.Image = (Bitmap)newBitmap;
            myLastBitmap?.Dispose();
            myLastBitmap = newBitmap;
            DisplayCoords();
        }

        private void DisplayCoords() {
            RangeText.Text = $"{myFrom:0.000} => {myTo:0.000}";
        }

        private void SetInitialCoords() {
            var w = BitmapPictureBox.Width;
            var h = BitmapPictureBox.Height;
            var aspect = (double) w / h;
            if (w > h) {
                var diff = ((myFrom.Real * aspect) - myFrom.Real);
                myFrom = new Complex(myFrom.Real + diff, myFrom.Imaginary);
                myTo = new Complex(myTo.Real - diff, myTo.Imaginary);
            }
            else {
                var diff = ((myFrom.Imaginary / aspect) - myFrom.Imaginary);
                myFrom = new Complex(myFrom.Real, myFrom.Imaginary + diff);
                myTo = new Complex(myTo.Real, myTo.Imaginary - diff);
            }
        }

        private bool OnSizeChanged() {
            const double resizeTollerance = 0.0001;
            var size = BitmapPictureBox.Size;
            var rFactor = (double)myOldSize.Width / size.Width;
            var iFactor = (double)myOldSize.Height / size.Height;
            if (Math.Abs(rFactor - 1) < resizeTollerance && Math.Abs(iFactor - 1) < resizeTollerance) return false;
            var rSize = myTo.Real - myFrom.Real;
            var iSize = myTo.Imaginary - myFrom.Imaginary;
            var rDiff = ((rSize * rFactor) - rSize) / 2;
            var iDiff = ((iSize * iFactor) - iSize) / 2;
            myFrom = new Complex(myFrom.Real + rDiff, myFrom.Imaginary + iDiff);
            myTo = new Complex(myTo.Real - rDiff, myTo.Imaginary - iDiff);
            myOldSize = size;
            return true;
        }

        private async void Zoom(double factor) {
            var reDiff = (myTo.Real - myFrom.Real) * factor;
            var imDiff = (myTo.Imaginary - myFrom.Imaginary) * factor;
            myTo = new Complex(myTo.Real - reDiff, myTo.Imaginary - imDiff);
            myFrom = new Complex(myFrom.Real + reDiff, myFrom.Imaginary + imDiff);

            var now = DateTime.Now;
            var sinceLastZoom = now - myLastZoom;
            myLastZoom = now;

            // wait and check if stopped zooming before drawing fine
            await Task.Run(() => {
                System.Threading.Thread.Sleep(250);
            });
            Debug.WriteLine("Time since last zoom = " + sinceLastZoom.Milliseconds + "ms");
            if(sinceLastZoom.Milliseconds >= 250) Draw(myFineBlockSize);
        }

        private void Pan(Point newPoint) {
            if (newPoint == myMouseWasAt) return;
            var size = BitmapPictureBox.Size;
            var xDelta = (double)newPoint.X - myMouseWasAt.X;
            var yDelta = (double)newPoint.Y - myMouseWasAt.Y;
            var xFactor = xDelta / size.Width;
            var yFactor = yDelta / size.Height;
            var reSize = myTo.Real - myFrom.Real;
            var imSize = myTo.Imaginary - myFrom.Imaginary;
            var reDiff = reSize * xFactor / 2;
            var imDiff = imSize * yFactor / 2;
            myTo = new Complex(myTo.Real - reDiff, myTo.Imaginary - imDiff);
            myFrom = new Complex(myFrom.Real - reDiff, myFrom.Imaginary - imDiff);
            myMouseWasAt = newPoint;
        }

        //
        // -- ui events
        //

        private void MainForm_ResizeEnd(object sender, EventArgs e) {
            OnSizeChanged();
            Draw(myFineBlockSize);
        }

        private void BitmapPictureBox_Resize(object sender, EventArgs e) {
            OnSizeChanged();
            Draw(myRoughBlockSize);
        }

        private void OnMouseWheel(object sender, MouseEventArgs e) {
            if (e.Delta == 0) return;
            myIsZooming = true;
            if (e.Delta < 0) Zoom(-myZoomFactor);
            else if (e.Delta > 0) Zoom(myZoomFactor);
            Draw(myRoughBlockSize);
        }
        
        private void BitmapPictureBox_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left) return;
            myMouseWasAt = e.Location;
            myIsPanning = true;
        }

        private void BitmapPictureBox_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left) return;
            myIsPanning = false;
            Draw(myFineBlockSize);
        }

        private void BitmapPictureBox_MouseMove(object sender, MouseEventArgs e) {
            if (!myIsPanning) return;
            Pan(e.Location);
            Draw(myRoughBlockSize);
        }


    }
}

