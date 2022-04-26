using System;
using System.Drawing;
using System.IO;

namespace HangfireApp.Web.BackgroundJobs
{
    public class DealayedJobs
    {
        public static string AddWatermarkJob(string fileName, string watermarkText)
        {
           return Hangfire.BackgroundJob.Schedule(() => ApplyWatermark(fileName, watermarkText), TimeSpan.FromSeconds(10));
        }

        public static void ApplyWatermark(string fileName, string watermarkText)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pictures", fileName);

            using (var bitMap = Bitmap.FromFile(path))
            {
                using (Bitmap tempBitmap = new Bitmap(bitMap.Width, bitMap.Height))
                {
                    using (Graphics graphics = Graphics.FromImage(tempBitmap))
                    {
                        graphics.DrawImage(bitMap, 0, 0);

                        var font = new Font(FontFamily.GenericSerif, 25, FontStyle.Bold);
                        var color = Color.FromArgb(255, 0, 0);
                        var brush = new SolidBrush(color);
                        var point = new Point(20, bitMap.Height - 50);

                        graphics.DrawString(watermarkText, font, brush, point);

                        string watermarkPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pictures/watermarks", fileName);

                        tempBitmap.Save(watermarkPath);

                    }
                }
            }
        }

    }
}
