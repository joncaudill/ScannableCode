using System;
using System.Drawing;
using ZXing;
using System.Collections.Generic;

namespace ScannableCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Check for help request first
            if (args.Length == 1 && args[0] == "--help")
            {
                DisplayHelp();
                return;
            }

            // Parse arguments
            string type = "both";
            string baseFilename = "encoded.png";
            List<string> encodeParts = new List<string>();

            for (int i = 0; i < args.Length;)
            {
                if (args[i] == "--type" && i + 1 < args.Length)
                {
                    type = args[i + 1].ToLower();
                    if (type != "qr" && type != "barcode" && type != "both")
                    {
                        Console.WriteLine($"Error: Invalid --type value '{type}'. Must be 'qr', 'barcode', or 'both'.");
                        return;
                    }
                    i += 2;
                }
                else if (args[i] == "--filename" && i + 1 < args.Length)
                {
                    baseFilename = args[i + 1];
                    i += 2;
                }
                else
                {
                    encodeParts.Add(args[i]);
                    i++;
                }
            }

            if (encodeParts.Count == 0)
            {
                Console.WriteLine("Error: Please provide a string to encode.");
                DisplayHelp();
                return;
            }

            string encodeString = string.Join(" ", encodeParts);

            var barcodeWriter = new BarcodeWriter();
            bool generateQR = type == "both" || type == "qr";
            bool generateBarcode = type == "both" || type == "barcode";

            if (generateQR)
            {
                barcodeWriter.Format = BarcodeFormat.QR_CODE;
                barcodeWriter.Options = new ZXing.Common.EncodingOptions
                {
                    Width = 300,
                    Height = 300,
                    Margin = 10
                };

                Bitmap qrBitmap = barcodeWriter.Write(encodeString);
                string qrPath = "QR_" + baseFilename;
                qrBitmap.Save(qrPath, System.Drawing.Imaging.ImageFormat.Png);
                Console.WriteLine($"QR code saved to {qrPath}");
            }

            if (generateBarcode)
            {
                barcodeWriter.Format = BarcodeFormat.CODE_128;
                barcodeWriter.Options = new ZXing.Common.EncodingOptions
                {
                    Width = 400,
                    Height = 125,
                    Margin = 13,
                    PureBarcode = true
                };

                Bitmap bcBitmap = barcodeWriter.Write(encodeString);
                string bcPath = "BC_" + baseFilename;
                bcBitmap.Save(bcPath, System.Drawing.Imaging.ImageFormat.Png);
                Console.WriteLine($"Barcode saved to {bcPath}");
            }
        }

        private static void DisplayHelp()
        {
            Console.WriteLine("Usage: ScannableCode <string_to_encode> [--type qr|barcode|both] [--filename base_filename.png]");
            Console.WriteLine();
            Console.WriteLine("Arguments:");
            Console.WriteLine("  <string_to_encode>    The text to encode in the QR code and/or barcode");
            Console.WriteLine("  --type                Optional: Specify what to generate (qr, barcode, or both). Defaults to both.");
            Console.WriteLine("  --filename            Optional: Base filename for output. Defaults to 'encoded.png'.");
            Console.WriteLine("                        - QR codes will be saved as 'QR_<base_filename>'");
            Console.WriteLine("                        - Barcodes will be saved as 'BC_<base_filename>'");
            Console.WriteLine();
            Console.WriteLine("Example 1: Generate both QR and barcode with default filename:");
            Console.WriteLine("  ScannableCode \"Hello World\"");
            Console.WriteLine();
            Console.WriteLine("Example 2: Generate only QR code with custom filename:");
            Console.WriteLine("  ScannableCode \"Hello World\" --type qr --filename myfile.png");
            Console.WriteLine();
            Console.WriteLine("For this help message: ScannableCode --help");
        }
    }
}
