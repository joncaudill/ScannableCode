using System;
using System.Drawing;
using ZXing;

namespace ScannableCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Check if a string argument is provided
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a string to encode as a command line argument.");
                return;
            }

            string strBarcodeString = args[0];

            var barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 400,
                    Height = 125,
                    Margin = 13,
                    PureBarcode = true,
                }
            };

            Bitmap barcodeBitmap = barcodeWriter.Write(strBarcodeString);

            string outputFilePath = "barcode.png";
            barcodeBitmap.Save(outputFilePath, System.Drawing.Imaging.ImageFormat.Png);
            Console.WriteLine($"Barcode saved to {outputFilePath}");
        }
    }
}
