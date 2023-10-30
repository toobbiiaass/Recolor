using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Recolor
{
    class RecolorMake
    {
        public BitmapImage RecolorItem(string path, int redy, int greeny, int bluey, bool isBrownColorFilterOn)
        {
            if (!File.Exists(path))
            {
                return new BitmapImage();
            }

            var originalImage = new BitmapImage(new Uri(path));

            int width = originalImage.PixelWidth;
            int height = originalImage.PixelHeight;

            WriteableBitmap modifiedImage = new WriteableBitmap(originalImage);
            modifiedImage.Lock();

            unsafe
            {
                for (int row = 0; row < height; row++)
                {
                    for (int col = 0; col < width; col++)
                    {
                        int offset = row * modifiedImage.BackBufferStride + col * 4;

                        byte alpha = *(byte*)(modifiedImage.BackBuffer + offset + 3);
                        byte red = *(byte*)(modifiedImage.BackBuffer + offset + 2);
                        byte green = *(byte*)(modifiedImage.BackBuffer + offset + 1);
                        byte blue = *(byte*)(modifiedImage.BackBuffer + offset);

                        if (red != green && red != blue)
                        {
                            bool isAllowed = false;
                            if (isBrownColorFilterOn)
                            {
                                if (!HasBrownInRange(red, green, blue))
                                {
                                    isAllowed = true;
                                }
                            }
                            else
                            {
                                isAllowed = true;
                            }
                            if (isAllowed)
                            {

                                if (alpha == 255)
                                {
                                    double L = 0.5 * red + 0.5 * green + 0.5 * blue;
                                    double newR = redy * L / 255;
                                    double newG = greeny * L / 255;
                                    double newB = bluey * L / 255;
                                    newB = ReplaceIfTooHigh(newB);
                                    newG = ReplaceIfTooHigh(newG);
                                    newR = ReplaceIfTooHigh(newR);

                                    *(byte*)(modifiedImage.BackBuffer + offset + 2) = (byte)newR;
                                    *(byte*)(modifiedImage.BackBuffer + offset + 1) = (byte)newG;
                                    *(byte*)(modifiedImage.BackBuffer + offset) = (byte)newB;
                                }
                            }
                        }
                    }
                }
            }

            modifiedImage.AddDirtyRect(new Int32Rect(0, 0, width, height));
            modifiedImage.Unlock();
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream stream = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(modifiedImage));
                encoder.Save(stream);
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }

            return bitmapImage;
        }

        public double ReplaceIfTooHigh(double number)
        {
            return Math.Max(0, Math.Min(255, number));
        }

        public bool HasBrownInRange(byte r, byte g, byte b)
        {
            float max = Math.Max(Math.Max(r, g), b);
            float min = Math.Min(Math.Min(r, g), b);
            float hue = 0;

            if (max == min)
            {
                hue = 0;
            }
            else if (max == r)
            {
                hue = (60 * (g - b) / (max - min) + 360) % 360;
            }
            else if (max == g)
            {
                hue = (60 * (b - r) / (max - min) + 120) % 360;
            }
            else if (max == b)
            {
                hue = (60 * (r - g) / (max - min) + 240) % 360;
            }

            float saturation = max == 0 ? 0 : (max - min) / max;
            float brightness = max / 255f;

            float[] brownHues = { 19, 30, 40, 45, 50, 60 };
            float tolerance = 10;

            foreach (float brownHue in brownHues)
            {
                if (Math.Abs(hue - brownHue) < tolerance)
                {
                    return true;
                }
            }
            return false;
        }
    }
}