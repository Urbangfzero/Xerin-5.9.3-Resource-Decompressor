using System;
using System.IO;
using System.Linq;
using dnlib.DotNet;
using Xerin593ResourceDecompressor.Decompressor;

internal class Program
{
    private const string tfisthis = @"
 =============================================
  Xerin 5.9.3 Resource Decompressor By Urban
 =============================================
";

    private static void Main(string[] args)
    {
        Console.Title = "Xerin 5.9.3 Resource Decompressor";
        Console.Clear();
        Banner();

        if (!ValidateArguments(args, out string filePath))
            return;

        if (!LoadModule(filePath, out ModuleDefMD module))
            return;

        if (!GetEncryptedResource(module, out EmbeddedResource resource))
            return;

        byte[] encryptedData = resource.CreateReader().ToArray();

        if (!DetectKey(module, out int key))
            return;

        if (!Decrypt(encryptedData, key, out byte[] decryptedData))
            return;

        if (!Decompress(decryptedData, out byte[] unpackedAssembly))
            return;

        if (!LoadUnpackedModule(unpackedAssembly, out ModuleDefMD unpackedModule))
            return;

        ReplaceResources(module, unpackedModule);

        SaveOutput(module, filePath);
    }

    private static void Banner()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(tfisthis);
        Console.ResetColor();
    }

    private static bool ValidateArguments(string[] args, out string filePath)
    {
        filePath = null;

        if (args.Length == 0)
        {
            Logger.Error("Usage: ResourceDecoder.exe <target.exe>");
            Console.ReadKey();
            return false;
        }

        filePath = args[0];

        if (!File.Exists(filePath))
        {
            Logger.Error("File not found: " + filePath);
            Console.ReadKey();
            return false;
        }

        return true;
    }

    private static bool LoadModule(string filePath, out ModuleDefMD module)
    {
        module = null;

        try
        {
            Logger.Info("Loading target assembly...");
            module = ModuleDefMD.Load(filePath);
            return true;
        }
        catch (Exception ex)
        {
            Logger.Error("Failed to load assembly: " + ex.Message);
            return false;
        }
    }

    private static bool GetEncryptedResource(ModuleDefMD module, out EmbeddedResource resource)
    {
        resource = module.Resources
            .OfType<EmbeddedResource>()
            .FirstOrDefault();

        if (resource == null)
        {
            Logger.Error("No embedded resources found!");
            return false;
        }

        Logger.Success("Found embedded resource: " + resource.Name);
        return true;
    }

    private static bool DetectKey(ModuleDefMD module, out int key)
    {
        key = 0;

        try
        {
            key = KeyDetector.DetectKey(module);
            Logger.Success("Detected Key: " + key);
            return true;
        }
        catch
        {
            Logger.Error("Failed to detect Key! Unsupported EXE?");
            return false;
        }
    }

    private static bool Decrypt(byte[] encrypted, int key, out byte[] decrypted)
    {
        decrypted = null;

        try
        {
            Logger.Info("Decrypting Resources...");
            decrypted = Decompressor.Decompress(encrypted, key);
            Logger.Success("Decrypting Resources complete!");
            return true;
        }
        catch (Exception ex)
        {
            Logger.Error("Decrypting Resources failed: " + ex.Message);
            return false;
        }
    }

    private static bool Decompress(byte[] decrypted, out byte[] unpacked)
    {
        unpacked = null;

        try
        {
            Logger.Info("Decompressing resource...");
            unpacked = GZip.Decompress(decrypted);
            Logger.Success("Decompression complete!");
            return true;
        }
        catch (Exception ex)
        {
            Logger.Error("Decompression failed: " + ex.Message);
            return false;
        }
    }

    private static bool LoadUnpackedModule(byte[] assemblyData, out ModuleDefMD unpackedModule)
    {
        unpackedModule = null;

        try
        {
            Logger.Info("Loading unpacked assembly...");
            unpackedModule = ModuleDefMD.Load(assemblyData);
            return true;
        }
        catch (Exception ex)
        {
            Logger.Error("Failed to load unpacked assembly: " + ex.Message);
            return false;
        }
    }

    private static void ReplaceResources(ModuleDefMD original, ModuleDefMD unpacked)
    {
        Logger.Info("Replacing matching resources...");

        int removed = 0;
        int added = 0;

        foreach (var resource in unpacked.Resources)
        {
            var existing = original.Resources
                .FirstOrDefault(r => r.Name == resource.Name);

            if (existing != null)
            {
                original.Resources.Remove(existing);
                removed++;
                Logger.Warn("Removed existing resource: " + existing.Name);
            }

            original.Resources.Add(resource);
            added++;
            Logger.Success("Added resource: " + resource.Name);
        }

        Logger.Info($"Resources removed: {removed}, Resources added: {added}");
    }

    private static void SaveOutput(ModuleDefMD module, string inputPath)
    {
        string outputPath =
            Path.GetFileNameWithoutExtension(inputPath) + "-Decompressed.exe";

        try
        {
            module.Write(outputPath);
            Logger.Success("Done! Cleaned EXE saved as: " + outputPath);
        }
        catch (Exception ex)
        {
            Logger.Error("Failed to save cleaned EXE: " + ex.Message);
        }

        Console.ReadKey();
    }
}