# ScannableCode

A simple C# console app that generates a CODE_128 barcode image from a string argument.

## Prerequisites

- Windows OS
- [.NET Framework 4.8 Developer Pack](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)
- [Visual Studio Code](https://code.visualstudio.com/)
- [C# extension for VS Code](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
- [NuGet CLI](https://learn.microsoft.com/en-us/nuget/install-nuget-client-tools) (for package restore)
- [MSBuild](https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild) (included with Visual Studio Build Tools)

## Setup

1. Open the project folder in Visual Studio Code.
2. Restore NuGet packages using one of these methods:
   - In VS Code terminal: `nuget restore ScannableCode.sln`
   - Or build the project (which will automatically restore packages)

## Build

Use MSBuild to compile the project. In the VS Code terminal, navigate to the project root and run:

```powershell
# Build in Debug mode (default)
msbuild ScannableCode.sln

# Or build in Release mode
msbuild ScannableCode.sln /p:Configuration=Release
```

Alternatively, if you have Visual Studio Build Tools installed:
```powershell
# Using dotnet CLI (may work with .NET Framework projects)
dotnet build ScannableCode.sln
```

## Run

After building, run the executable from the output directory:

```powershell
# Navigate to the output directory
cd ScannableCode\bin\Debug

# Run the application
.\ScannableCode.exe "Your text here"
```

Or run directly from the project root:
```powershell
ScannableCode\bin\Debug\ScannableCode.exe "Your text here"
```

## Command Line Usage

```
ScannableCode <string_to_encode> [--type qr|barcode|both] [--filename base_filename.png]
```

### Arguments:
- `<string_to_encode>` - The text to encode in the QR code and/or barcode (required)
- `--type` - Optional: Specify what to generate (`qr`, `barcode`, or `both`). Defaults to `both`
- `--filename` - Optional: Base filename for output. Defaults to `encoded.png`
  - QR codes will be saved as `QR_<base_filename>`
  - Barcodes will be saved as `BC_<base_filename>`

### Examples:

```powershell
# Generate both QR and barcode with default filename
.\ScannableCode.exe "Hello World"

# Generate only QR code with custom filename
.\ScannableCode.exe "Hello World" --type qr --filename myfile.png

# Generate only barcode
.\ScannableCode.exe "Product-12345" --type barcode

# Show help
.\ScannableCode.exe --help
```

### Output Files:
- When generating both (default): Creates `QR_encoded.png` and `BC_encoded.png`
- When using custom filename: Creates `QR_<your_filename>` and/or `BC_<your_filename>`
- Files are saved in the current working directory