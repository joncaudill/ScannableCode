using System;
using System.Drawing;
using ZXing;

namespace ScannableCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Check for help argument
            if (args.Length == 1 && args[0] == "--help")
            {
                ShowHelp();
                return;
            }

            // Check if a string argument is provided
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a string to encode as a command line argument.");
                Console.WriteLine("Use --help for usage information.");
                return;
            }

            string strToEncode = args[0];
            string baseFileName = "encoded.png";
            string codeType = "both"; // Default to both

            // Parse additional arguments
            for (int i = 1; i < args.Length; i++)
            {
                if (args[i] == "--type" && i + 1 < args.Length)
                {
                    codeType = args[i + 1].ToLower();
                    i++; // Skip the next argument as it's the value
                }
                else if (args[i] == "--filename" && i + 1 < args.Length)
                {
                    baseFileName = args[i + 1];
                    i++; // Skip the next argument as it's the value
                }
            }

            // Validate code type
            if (codeType != "qr" && codeType != "barcode" && codeType != "both")
            {
                Console.WriteLine("Invalid code type. Use 'qr', 'barcode', or 'both'.");
                return;
            }

            // Create QR code if requested
            if (codeType == "qr" || codeType == "both")
            {
                CreateQRCode(strToEncode, "QR_" + baseFileName);
            }

            // Create barcode if requested
            if (codeType == "barcode" || codeType == "both")
            {
                CreateBarcode(strToEncode, "BC_" + baseFileName);
            }
        }

        static void CreateQRCode(string text, string fileName)
        {
            var qrWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 400,
                    Height = 400,
                    Margin = 13,
                    PureBarcode = true,
                }
            };

            Bitmap qrBitmap = qrWriter.Write(text);
            qrBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
            Console.WriteLine($"QR code saved to {fileName}");
        }

        static void CreateBarcode(string text, string fileName)
        {
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

            Bitmap barcodeBitmap = barcodeWriter.Write(text);
            barcodeBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
            Console.WriteLine($"Barcode saved to {fileName}");
        }

        static void ShowHelp()
        {
            Console.WriteLine("ScannableCode - Generate QR codes and barcodes");
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine("  ScannableCode.exe <text_to_encode> [options]");
            Console.WriteLine();
            Console.WriteLine("Arguments:");
            Console.WriteLine("  <text_to_encode>    The text string to encode (required)");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  --type <type>       Type of code to generate: 'qr', 'barcode', or 'both' (default: both)");
            Console.WriteLine("  --filename <name>   Base filename for output (default: encoded.png)");
            Console.WriteLine("  --help              Show this help message");
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("  ScannableCode.exe \"Hello World\"");
            Console.WriteLine("  ScannableCode.exe \"Hello World\" --type qr");
            Console.WriteLine("  ScannableCode.exe \"Hello World\" --type barcode --filename mycode.png");
            Console.WriteLine();
            Console.WriteLine("Output files:");
            Console.WriteLine("  QR codes are prefixed with 'QR_'");
            Console.WriteLine("  Barcodes are prefixed with 'BC_'");
        }
    }
}
