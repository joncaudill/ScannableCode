using System;
using System.Drawing;
using System.IO;
using System.Linq;
using ZXing;

namespace ScannableCode
{
    internal class Program
    {
        // CODE 128 practical max length
        private const int Code128MaxLength = 80;
        // QR Code practical max length (for readability and compatibility)
        private const int QrCodeMaxLength = 1000;

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

            // Ensure .png extension before sanitization
            if (baseFileName.Length < 4 || baseFileName.Substring(baseFileName.Length - 4).ToLower() != ".png")
            {
                baseFileName += ".png";
            }

            // Sanitize inputs
            strToEncode = SanitizeInputText(strToEncode);
            baseFileName = SanitizeFileName(baseFileName);

            // Validate code type
            if (codeType != "qr" && codeType != "barcode" && codeType != "both")
            {
                Console.WriteLine("Invalid code type. Use 'qr', 'barcode', or 'both'.");
                return;
            }

            // Create QR code if requested
            if (codeType == "qr" || codeType == "both")
            {
                if (strToEncode.Length > 249)
                {
                    Console.WriteLine("[Warning] Your input is over 249 characters. Many non-specialty QR code scanners may only decode the first 250 characters or less.");
                }

                int truncated = strToEncode.Length > QrCodeMaxLength ? strToEncode.Length - QrCodeMaxLength : 0;
                string qrInput = truncated > 0 ? strToEncode.Substring(0, QrCodeMaxLength) : strToEncode;
                CreateQRCode(qrInput, "QR_" + baseFileName);
                if (truncated > 0)
                {
                    Console.WriteLine($"[QR] Input was truncated by {truncated} characters to fit the QR code standard (max {QrCodeMaxLength} characters).");
                }
            }

            // Create barcode if requested
            if (codeType == "barcode" || codeType == "both")
            {
                int truncated = strToEncode.Length > Code128MaxLength ? strToEncode.Length - Code128MaxLength : 0;
                string bcInput = truncated > 0 ? strToEncode.Substring(0, Code128MaxLength) : strToEncode;
                CreateBarcode(bcInput, "BC_" + baseFileName);
                if (truncated > 0)
                {
                    Console.WriteLine($"[Barcode] Input was truncated by {truncated} characters to fit the CODE 128 standard (max {Code128MaxLength} characters).");
                }
            }
        }

        static string SanitizeInputText(string input)
        {
            // Limit length and remove control characters
            if (input.Length > 256)
            {
                input = input.Substring(0, 256);
            }
            // Remove non-printable/control characters
            return new string(input.Where(c => !char.IsControl(c)).ToArray());
        }

        static string SanitizeFileName(string fileName)
        {
            // Remove invalid filename characters
            var invalidChars = Path.GetInvalidFileNameChars();
            var sanitized = new string(fileName.Where(c => !invalidChars.Contains(c)).ToArray());

            // Default to "encoded.png" if empty after sanitization
            if (string.IsNullOrWhiteSpace(sanitized))
                sanitized = "encoded.png";

            // Ensure .png extension
            if (!sanitized.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                sanitized += ".png";

            return sanitized;
        }

        static void CreateQRCode(string text, string fileName)
        {
            var options = new ZXing.Common.EncodingOptions
            {
                Width = 400,
                Height = 400,
                Margin = 13,
                PureBarcode = true,
            };

            // Add QR version and error correction hints
            options.Hints[ZXing.EncodeHintType.QR_VERSION] = 20;
            options.Hints[ZXing.EncodeHintType.ERROR_CORRECTION] = ZXing.QrCode.Internal.ErrorCorrectionLevel.L;

            var qrWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = options
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
