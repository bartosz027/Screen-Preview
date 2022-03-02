using System.Drawing;
using System.Drawing.Imaging;

using System.Windows;
using System.Windows.Forms;

using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ScreenPreview.Model {

    class ScreenRecorder {
        public static void Init() {
            int width = Screen.PrimaryScreen.Bounds.Width;
            int height = Screen.PrimaryScreen.Bounds.Height;

            Bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            _BitmapRectangle = new Rectangle(0, 0, Bitmap.Width, Bitmap.Height);

            WriteableBitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Pbgra32, null);
            _WriteableBitmapRectangle = new Int32Rect(0, 0, WriteableBitmap.PixelWidth, WriteableBitmap.PixelHeight);
        }

        public static void TakeScreenshot() {
            using (Graphics screenshot = Graphics.FromImage(Bitmap)) {
                screenshot.CopyFromScreen(_BitmapRectangle.X, _BitmapRectangle.Y, 0, 0, _BitmapRectangle.Size, CopyPixelOperation.SourceCopy);
            }

            BitmapData data = Bitmap.LockBits(_BitmapRectangle, ImageLockMode.WriteOnly, Bitmap.PixelFormat);
            WriteableBitmap.WritePixels(_WriteableBitmapRectangle, data.Scan0, data.Width * data.Height * 4, data.Stride);
            Bitmap.UnlockBits(data);
        }


        // WriteableBitmap
        public static WriteableBitmap WriteableBitmap { get; set; }
        private static Int32Rect _WriteableBitmapRectangle;

        // Bitmap
        public static Bitmap Bitmap { get; set; }
        private static Rectangle _BitmapRectangle;
    }

}