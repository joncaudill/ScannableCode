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

Alternative build methods:
```powershell
# Using Visual Studio Developer Command Prompt/PowerShell
msbuild ScannableCode\ScannableCode.csproj

# If you have Visual Studio installed, you can also use:
devenv ScannableCode.sln /build Debug
```

The compiled executable will be placed in:
- Debug build: `ScannableCode\bin\Debug\ScannableCode.exe`
- Release build: `ScannableCode\bin\Release\ScannableCode.exe`

## Run

After building, run the executable from the project root directory:

```powershell
# Run with default settings (generates both QR code and barcode)
.\ScannableCode\bin\Debug\ScannableCode.exe "Your text here"

# Or navigate to the output directory first
cd ScannableCode\bin\Debug
.\ScannableCode.exe "Your text here"
```

For Release builds, use the Release path:
```powershell
.\ScannableCode\bin\Release\ScannableCode.exe "Your text here"
```

## Command Line Usage

```
ScannableCode.exe <text_to_encode> [options]
```

### Arguments:
- `<text_to_encode>` - The text string to encode (required)

### Options:
- `--type <type>` - Type of code to generate: `qr`, `barcode`, or `both` (default: `both`)
- `--filename <name>` - Base filename for output (default: `encoded.png`)
- `--help` - Show help message and usage information

### Examples:

```powershell
# Generate both QR code and barcode with default filename
.\ScannableCode.exe "Hello World"

# Generate only QR code
.\ScannableCode.exe "Hello World" --type qr

# Generate only barcode with custom filename
.\ScannableCode.exe "Product-12345" --type barcode --filename product.png

# Generate both with custom filename
.\ScannableCode.exe "Hello World" --filename mycode.png

# Show help information
.\ScannableCode.exe --help
```

### Output Files:
- **QR codes**: Saved with `QR_` prefix (e.g., `QR_encoded.png`, `QR_mycode.png`)
- **Barcodes**: Saved with `BC_` prefix (e.g., `BC_encoded.png`, `BC_mycode.png`)
- Files are saved in the current working directory
- Generated codes use:
  - QR codes: 400×400 pixels with 13-pixel margin
  - Barcodes: 400×125 pixels with 13-pixel margin (CODE_128 format)