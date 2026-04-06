using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconMapper
{
    class ContextMenuOptions
    {
        public static void contextMenuOptions()
        {
        // Define the folder containing the icon files
            string iconDirectory = @"C:\Path\To\Your\IconFolder";
            string contextMenuPath = @"HKEY_CLASSES_ROOT\Directory\shell\ChangeIcon\submenus";

            if (Directory.Exists(iconDirectory))
            {
                // Scan the directory for .ico files
                string[] icoFiles = Directory.GetFiles(iconDirectory, "*.ico");

                // Create registry key for sub-menu
                foreach (var iconFile in icoFiles)
                {
                    string iconName = Path.GetFileNameWithoutExtension(iconFile);

                    // Add a new key for each icon file in the submenus
                    string iconRegistryPath = contextMenuPath+"\\"+iconName;

                    // Create a registry entry for each .ico file
                    Registry.SetValue(iconRegistryPath, "", iconName);
                    Registry.SetValue(iconRegistryPath+"\\command", "", "C:\\Path\\To\\YourApp\\ChangeIconApp.exe\" \"%1\" \"{iconFile}\"");

                    Console.WriteLine("Added {iconFile} to context menu.");
                }
            }
            else
            {
                Console.WriteLine("The directory '{iconDirectory}' does not exist.");
            }
        }
    }
}
