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

- Open the project folder in Visual Studio Code.
- Restore NuGet packages:

-  ```bash
   dotnet restore
   ```
- Ensure ZXing.Net and System.Drawing.Common packages are installed (if not, install them):

   ```bash
   dotnet add package ZXing.Net
   dotnet add package System.Drawing.Common
   ```

## Build

Use the terminal in VS Code:

```bash
dotnet build
```

## Run

To run the application, use the terminal in VS Code and execute:
```bash
dotnet run "YourStringHere"
```
## Example
```bash
dotnet run "Hello World"
```
This will generate a barcode for the string "Hello World".

Navigate to the output directory:

The barcode will be saved as `barcode.png` in the same directory.