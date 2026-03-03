# Xerin 5.9.3 Resource Decompressor

A lightweight .NET console application designed to extract and decompress embedded resources protected specifically by the **Xerin 5.9.3** obfuscator.

This tool is intended for research, reverse engineering practice, and studying protected .NET resource handling mechanisms.

---

## Important Notice

This tool is designed **exclusively for Xerin version 5.9.3**.

It is not guaranteed to work with:

- Earlier versions of Xerin  
- Later versions of Xerin  
- Modified or custom builds  
- Other obfuscators  

Internal patterns, resource formats, encryption routines, and compression logic are version-specific. If the protection structure differs, the tool may fail or produce invalid output.

---

## Acknowledgment

This project acknowledges and respects the work of **INX**, the author of the Xerin obfuscator.

Xerin is a technically sophisticated .NET protection system. This tool exists strictly for:

- Educational research  
- Reverse engineering practice  
- Academic study of protection mechanisms  
- Defensive security analysis  

Full credit and appreciation go to **INX** for their contribution to .NET software protection.

---

## Overview

Xerin 5.9.3 Resource Decompressor analyzes a protected .NET assembly, identifies encrypted resource streams, extracts the decryption key, restores the encrypted data, decompresses it, and writes the recovered output to disk.

---

## Features

- Detects Xerin 5.9.3 encrypted resource streams  
- Automatically locates the decryption routine within `<Module>`  
- Extracts the encryption key from the target assembly  
- Decrypts protected resources  
- Decompresses data (GZip supported)  
- Outputs restored assembly or resource  
- Clean and structured console interface  

---

## Requirements

- .NET 8.0 or higher  
- Windows 10 / 11  

### Dependencies

- dnlib — .NET assembly reader and writer library  
- Colorful.Console — Enhanced console output formatting  

Install via NuGet:

```bash
dotnet add package dnlib
dotnet add package Colorful.Console
```

---

## Usage

```bash
Xerin 5.9.3 Resource Decompressor.exe <ProtectedAssembly.exe>
```

### Example

```bash
Xerin 5.9.3 Resource Decompressor.exe CrackMev1.exe
```

---

## How It Works

1. Loads the protected .NET assembly.
2. Scans for Xerin 5.9.3 specific encrypted resource streams.
3. Identifies the decryption routine inside `<Module>`.
4. Extracts the encryption key.
5. Decrypts the resource.
6. Decompresses the data (GZip).
7. Writes the restored output to disk.

---

## Project Structure

```
/Xerin 5.9.3 Resource Decompressor
 ├── Program.cs
 ├── GZip.cs
 ├── Logger.cs
 └── README.md
```

---

## Intended Use

This project is intended strictly for:

- Educational purposes  
- Malware analysis research  
- Reverse engineering practice  
- Testing and analyzing software you own or have permission to examine  

Do not use this tool on software without proper authorization.

---

## Author

Developed by Urban  

---

If this project is useful to you, consider starring the repository.