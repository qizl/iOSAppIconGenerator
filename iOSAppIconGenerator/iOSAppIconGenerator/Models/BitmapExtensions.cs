using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Com.EnjoyCodes.iOSAppIconGenerator.Models
{
    public static class BitmapExtensions
    {
        public static Bitmap Resize(this Bitmap b, int w, int h)
        {
            Image imgSource = b;
            ImageFormat thisFormat = imgSource.RawFormat;
            int sW = 0, sH = 0;
            // 按比例缩放           
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;
            if (sHeight > h || sWidth > w)
            {
                if ((sWidth * h) > (sHeight * w))
                {
                    sW = w;
                    sH = (w * sHeight) / sWidth;
                }
                else
                {
                    sH = h;
                    sW = (sWidth * h) / sHeight;
                }
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }

            Bitmap outBmp = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.Transparent);

            // 设置画布的描绘质量         
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(imgSource, new Rectangle((w - sW) / 2, (h - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
            g.Dispose();

            // 设置压缩质量     
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;
            EncoderParameter encoderParam = new EncoderParameter(Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            imgSource.Dispose();

            return outBmp;
        }
    }
}
