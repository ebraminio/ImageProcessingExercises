using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace ImageProcessing1
{
    public static class ReusableTools
    {   
        public static byte[,] Threshould(this byte[,] image, int threshould)
        {
            var w = image.GetLength(0);
            var h = image.GetLength(1);
            var result = new byte[w, h];

            for (var i = 0; i < w; ++i)
                for (var j = 0; j < h; ++j)
                    result[i, j] = (byte)(image[i, j] < threshould ? 0 : 0xFF);

            return result;
        }

        public static byte[,] Logarithmic(this byte[,] image, int logParameter)
        {
            var w = image.GetLength(0);
            var h = image.GetLength(1);
            var result = new byte[w, h];

            for (var i = 0; i < w; ++i)
                for (var j = 0; j < h; ++j)
                    result[i, j] = (byte)(Math.Log10(image[i, j]) * logParameter).Clamp(0, 0xFF);

            return result;
        }

        public static Dictionary<int, int> Histogram(this byte[,] image) =>
            image.Cast<byte>()
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToDictionary(g => (int)g.Key, g => g.Count());

        public static byte[,] HistogramEqualization(this byte[,] image)
        {
            var w = image.GetLength(0);
            var h = image.GetLength(1);
            var result = new byte[w, h];

            var histogram = image.Histogram();
            var total = h * w;
            var pdf = histogram.ToDictionary(x => x.Key, x => (float)x.Value / total);

            float accumulation = 0;
            var cdf = pdf.ToDictionary(x => x.Key, x => accumulation += x.Value);

            for (var i = 0; i < h; ++i)
                for (var j = 0; j < w; ++j)
                    result[j, i] = (byte)(cdf[image[j, i]] * 256);

            return result;
        }
 
        public static int[,] GLCM(this byte[,] inputImage, Action<string> extraInfoCallback = null)
        {
            const int levels = 32;
            var glcm = new double[levels, levels];

            var w = inputImage.GetLength(0);
            var h = inputImage.GetLength(1);

            var reducedLevelImage = new byte[w, h];
            var dividend = 256 / levels;
            for (var i = 0; i < w; ++i)
                for (var j = 0; j < h; ++j)
                    reducedLevelImage[i, j] = (byte)(inputImage[i, j] / dividend);

            // https://en.wikipedia.org/wiki/Co-occurrence_matrix
            for (var i = 0; i < levels; ++i)
                for (var j = 0; j < levels; ++j)
                    for (var x = 0; x < w - 1; ++x)
                        for (var y = 0; y < h - 1; ++y)
                            // default matrix, [[0, 1, 0], [0, 0, 1], [0, 0, 0]]
                            if ((reducedLevelImage[x, y] == i) && (reducedLevelImage[x + 1, y + 1] == j))
                                ++glcm[i, j];

            var cSum = glcm.Cast<double>().Sum();
            var normalizedGlcm = new double[levels, levels];
            // See Matlab's `open graycoprops` and `open entropy`
            for (var i = 0; i < levels; ++i)
                for (var j = 0; j < levels; ++j)
                    normalizedGlcm[i, j] = glcm[i, j] / cSum;

            var serializedNormalGlcm = normalizedGlcm.Cast<double>();

            // http://www.code.ucsd.edu/pcosman/glcm.pdf
            var entropy = -serializedNormalGlcm.Select(x => x == 0 ? 0 : x * Math.Log(x)).Sum();
            var energy = serializedNormalGlcm.Select(x => x * x).Sum();

            var contrast = .0;
            var homogeneity = .0;
            for (var i = 0; i < levels; ++i)
                for (var j = 0; j < levels; ++j)
                {
                    var c = normalizedGlcm[i, j];
                    contrast += Math.Pow(i - j, 2) * c; // contrast, or, element difference moment of order 2
                    homogeneity += c / (1 + Math.Abs(i - j));
                }

            extraInfoCallback?.Invoke($"Entropy: {entropy}, Homogeneity: {homogeneity}, Contrast: {contrast}, Uniformity/Energy: {energy}");

            var result = new int[levels, levels];
            for (var i = 0; i < levels; ++i)
                for (var j = 0; j < levels; ++j)
                    result[i, j] = (int)(normalizedGlcm[i, j] * 0xFFFFFF);

            return result;
        }

        // http://lodev.org/cgtutor/filtering.html
        public static byte[,] Convolution(this byte[,] inputImage, float[,] filter, double factor, double bias)
        {
            var w = inputImage.GetLength(0);
            var h = inputImage.GetLength(1);
            var outputImage = new byte[w, h];

            var filterSize = filter.GetLength(0);
            // Apply the filter
            for (var i = 0; i < h; ++i)
                for (var j = 0; j < w; ++j)
                {
                    double color = 0;

                    // multiply every value of the filter with corresponding image pixel
                    for (var filterY = 0; filterY < filterSize; filterY++)
                        for (var filterX = 0; filterX < filterSize; filterX++)
                        {
                            var imageX = (i - filterSize / 2 + filterX + h) % h;
                            var imageY = (j - filterSize / 2 + filterY + w) % w;
                            color += inputImage[imageY, imageX] * filter[filterY, filterX];
                        }

                    outputImage[j, i] = (byte)(factor * color + bias).Clamp(0, 0xFF);
                }

            return outputImage;
        }

        public static byte[,] Dilation(this byte[,] inputImage, bool[,] structering)
        {
            var w = inputImage.GetLength(0);
            var h = inputImage.GetLength(1);
            var outputImage = new byte[w, h];

            var filterSize = structering.GetLength(0);
            for (var i = 0; i < h; ++i)
                for (var j = 0; j < w; ++j)
                {
                    bool black = false;

                    for (var filterY = 0; filterY < filterSize; filterY++)
                        for (var filterX = 0; filterX < filterSize; filterX++)
                        {
                            var imageX = (i - filterSize / 2 + filterX + h) % h;
                            var imageY = (j - filterSize / 2 + filterY + w) % w;
                            if (structering[filterY, filterX] && inputImage[imageY, imageX] == 0xFF)
                                black = true;
                        }

                    outputImage[j, i] = (byte)(black ? 0xFF : 0);
                }

            return outputImage;
        }

        public static byte[,] Erosion(this byte[,] inputImage, bool[,] structering)
        {
            var w = inputImage.GetLength(0);
            var h = inputImage.GetLength(1);
            var outputImage = new byte[w, h];

            var filterSize = structering.GetLength(0);
            for (var i = 0; i < h; ++i)
                for (var j = 0; j < w; ++j)
                {
                    bool black = true;

                    for (var filterY = 0; filterY < filterSize; filterY++)
                        for (var filterX = 0; filterX < filterSize; filterX++)
                        {
                            var imageX = (i - filterSize / 2 + filterX + h) % h;
                            var imageY = (j - filterSize / 2 + filterY + w) % w;
                            if (structering[filterY, filterX] && inputImage[imageY, imageX] != 0xFF)
                                black = false;
                        }

                    outputImage[j, i] = (byte)(black ? 0xFF : 0);
                }

            return outputImage;
        }

        // http://stackoverflow.com/a/22085423/1414809
        static int[] dx = { -1, 0, 1, 1, 1, 0, -1, -1 };
        static int[] dy = { 1, 1, 1, 0, -1, -1, -1, 0 };

        private static void dfs(int x, int y, int c, byte[,] g, int[,] w)
        {
            w[x, y] = c;
            for (var i = 0; i < 8; i++)
            {
                int nx = x + dx[i], ny = y + dy[i];
                if ((g[nx, ny] == 0xFF) && (w[nx, ny] == 0))
                    dfs(nx, ny, c, g, w);
            }
        }

        public static int[,] ColorConnectedComponents(this byte[,] inputImage, Action<string> extraInfoCallback = null)
        {
            var row = inputImage.GetLength(0);
            var col = inputImage.GetLength(1);

            var g = new byte[row + 2, col + 2];

            for (var i = 1; i <= row; i++)
                for (var j = 1; j <= col; j++)
                    g[i, j] = inputImage[i - 1, j - 1];

            var w = new int[row + 2, col + 2];

            int set = 1;

            for (var i = 1; i <= row; i++)
                for (var j = 1; j <= col; j++)
                    if ((g[i, j] == 0xFF) && (w[i, j] == 0))
                        dfs(i, j, set++, g, w);

            var random = new Random();
            var colors = Enumerable.Range(0, set).Select(x => random.Next(0, 0xFFFFFF)).ToList();

            for (var i = 1; i <= row; i++)
                for (var j = 1; j <= col; j++)
                    w[i, j] = w[i, j] == 0 ? 0 : colors[w[i, j]];

            extraInfoCallback?.Invoke($"Connected components: {set}");

            return w;
        }

        // http://stackoverflow.com/a/2683487/1414809
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
    }

    public class DirectBitmap : IDisposable
    {
        public Bitmap Bitmap { get; }
        public int[] Bits { get; }
        public bool Disposed { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        protected GCHandle BitsHandle { get; }

        // http://stackoverflow.com/a/34801225/1414809
        public DirectBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new int[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppRgb, BitsHandle.AddrOfPinnedObject());
        }

        public DirectBitmap(Image img) : this(img.Width, img.Height)
        {
            Graphics.FromImage(Bitmap).DrawImageUnscaled(img, 0, 0);
        }

        public DirectBitmap(byte[,] image) : this(image.GetLength(0), image.GetLength(1))
        {
            var pixelIndex = 0;
            for (var i = 0; i < Height; ++i)
                for (var j = 0; j < Width; ++j, ++pixelIndex)
                    Bits[pixelIndex] = image[j, i] + image[j, i] * 0x100 + image[j, i] * 0x10000;
        }

        public DirectBitmap(int[,] image) : this(image.GetLength(0), image.GetLength(1))
        {
            var pixelIndex = 0;
            for (var i = 0; i < Height; ++i)
                for (var j = 0; j < Width; ++j, ++pixelIndex)
                    Bits[pixelIndex] = image[j, i];
        }

        public byte[,] AsArray()
        {
            byte[,] result = new byte[Width, Height];
            for (int i = 0, pixelIndex = 0; i < Height; ++i)
                for (var j = 0; j < Width; ++j, ++pixelIndex)
                {
                    var rgb = Bits[pixelIndex];
                    var red = (rgb >> 16) & 0xFF;
                    var green = (rgb >> 8) & 0xFF;
                    var blue = rgb & 0xFF;
                    result[j, i] = (byte)((red + green + blue) / 3);
                }
            return result;
        }

        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }
    }
}
