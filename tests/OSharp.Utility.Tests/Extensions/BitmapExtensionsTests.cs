using Xunit;

using System.Drawing;


namespace OSharp.Utility.Extensions.Tests
{
    public class BitmapExtensionsTests
    {
        [Fact()]
        public void RotateTest()
        {
            Bitmap bitmap1 = new Bitmap(100, 200);
            Bitmap bitmap2 = bitmap1.Rotate(90);
            Assert.Equal(bitmap1.Width, bitmap2.Height);
            Assert.Equal(bitmap1.Height, bitmap2.Width);
        }

        [Fact()]
        public void ZoomTest()
        {
            Bitmap bitmap1 = new Bitmap(100, 200);
            Bitmap bitmap2 = bitmap1.Zoom(80, 150);
            Assert.Equal(bitmap2.Width, 80);
            Assert.Equal(bitmap2.Height, 150);

            bitmap2 = bitmap1.Zoom(0.5);
            Assert.Equal(bitmap2.Width, 50);
            Assert.Equal(bitmap2.Height, 100); 
        }
    }
}