using System;
using System.IO;

public static class ConfigHelper
{
    private static readonly string DocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    private static readonly string ConfigFilePath = Path.Combine(DocumentsPath, "IconMapper", "config.txt");
    private static readonly string IconsFolderPath = Path.Combine(DocumentsPath, "IconMapper");

    public static void EnsureConfig()
    {
        // Create the IconMapper folder if it does not exist
        if (!Directory.Exists(IconsFolderPath))
        {
            Directory.CreateDirectory(IconsFolderPath);
        }

        // Create the config file if it does not exist
        if (!File.Exists(ConfigFilePath))
        {
            File.WriteAllText(ConfigFilePath, "DefaultIcon.ico");
        }
    }

    public static string GetDefaultIconPath()
    {
        return File.ReadAllText(ConfigFilePath).Trim();
    }

    public static void SetDefaultIconPath(string path)
    {
        File.WriteAllText(ConfigFilePath, path);
    }
}
