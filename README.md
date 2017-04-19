# Vandalbrot

A Windows-forms based Mandelbrot fractal thingy.

Inspired by [this](https://www.amazon.co.uk/Make-Your-Mandelbrot-Tariq-Rashid-ebook/dp/B01EET6WUE/ref=sr_1_1).

I used to run something called [Fractint](https://fractint.net) on my first PC back in the early 90s and it used to take minutes to render a Mandelbrot set on a 640 x 480 screen; now it takes less than a second...

The mandelbrot set is the set of complex (two-dimensional) numbers that when a particular function is repeatedly applied, it converges on a value.  Sounds complicated but it really isn't.  I recommend the book above for a better explaination!  Here's the bit of code that does the maths:

```c#
        private int GetDivergence(Complex c, int maxIterations) {
            var z = new Complex(0, 0);
            int i;
            for (i = 0; iter < maxIterations; iter++) {
                z = z * z + c;
                // If magnitude > 4, it's diverging for sure so skip out.
                if (z.Magnitude() > 4) {
                    break;
                }
            }
            return i;
        }
```

Than all you do is set up a two-dimensional canvas where each pixel represents a particular complex-number and apply that function to each one.  If you then assign a colour to how quickly it diverges from the initial value, you get some pretty amazing patterns!

## Features

  * Zooming in and out with mouse-wheel
  * panning with mouse
  * Low-fi rendering of intermediate view during mouse operations

## To Do

  * zoom is buggy
  * debouncing
  * save coords
  * reset button
  * adjust max iterations
  * buttons to replicate mouse operations (zoom, pan)

## Nice to have

  * customise pallete offset?
  * cycle the palette?
