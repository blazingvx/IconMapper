using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Configuration;
using System.Drawing;

namespace IconMapper
{
    public partial class MainForm : Form
    {
        private string selectedIconPath;

        public MainForm()
        {
            InitializeComponent();
            LoadDrives();
            LoadIcons();
        }

        /// <summary>
        /// Loads the available drives and adds them to the TreeView.
        /// </summary>
        private void LoadDrives()
        {
            folderTreeView.Nodes.Clear();
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType == DriveType.Fixed || drive.DriveType == DriveType.Removable)
                {
                    TreeNode driveNode = new TreeNode(drive.Name)
                    {
                        Tag = drive.RootDirectory.FullName
                    };
                    folderTreeView.Nodes.Add(driveNode);
                }
            }
        }

        /// <summary>
        /// Event handler for TreeView node selection. Loads subfolders if they are not already loaded.
        /// </summary>
        private void FolderTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count == 0)
            {
                LoadSubFolders(e.Node);
            }
        }

        /// <summary>
        /// Event handler for TreeView node expansion. Adds a "Loading..." node if the node is not already loaded.
        /// </summary>
        private void FolderTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count > 0 && e.Node.Nodes[0].Text != "Loading...")
            {
                return;
            }

            e.Node.Nodes.Clear();
            e.Node.Nodes.Add(new TreeNode("Loading..."));
        }

        /// <summary>
        /// Loads subfolders into the TreeView node.
        /// </summary>
        private void LoadSubFolders(TreeNode node)
        {
            string path = node.Tag.ToString();
            try
            {
                foreach (var dir in Directory.GetDirectories(path))
                {
                    TreeNode subNode = new TreeNode(Path.GetFileName(dir))
                    {
                        Tag = dir
                    };
                    node.Nodes.Add(subNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading directories: " + ex.Message);
            }

            // Replace the "Loading..." node with actual subfolder nodes
            node.Nodes.Clear();
            foreach (var dir in Directory.GetDirectories(path))
            {
                TreeNode subNode = new TreeNode(Path.GetFileName(dir))
                {
                    Tag = dir
                };
                node.Nodes.Add(subNode);
            }
        }

        /// <summary>
        /// Loads icon files from a folder specified in App.config into the ListBox.
        /// </summary>
        private void LoadIcons()
        {
            // Get the path from App.config
            string iconFolder = ConfigurationManager.AppSettings["IconFolderPath"];

            if (string.IsNullOrEmpty(iconFolder))
            {
                MessageBox.Show("Icon folder path is not configured in App.config.");
                return;
            }

            if (!Directory.Exists(iconFolder))
            {
                Directory.CreateDirectory(iconFolder);
            }

            string[] iconFiles = Directory.GetFiles(iconFolder, "*.ico");
            iconListBox.Items.AddRange(iconFiles);
        }

        /// <summary>
        /// Applies the selected icon to the selected folder.
        /// </summary>
        private void ApplyIconButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedIconPath))
            {
                MessageBox.Show("No icon selected.");
                return;
            }

            if (folderTreeView.SelectedNode == null)
            {
                MessageBox.Show("No folder selected.");
                return;
            }

            string folderPath = folderTreeView.SelectedNode.Tag.ToString();
            if (Directory.Exists(folderPath))
            {
                ApplyIconToFolder(folderPath, selectedIconPath);
                MessageBox.Show("Icon applied to the selected folder.");
            }
            else
            {
                MessageBox.Show("The selected folder does not exist.");
            }
        }

        /// <summary>
        /// Applies the icon to the folder by creating or updating the desktop.ini file.
        /// </summary>
        private void ApplyIconToFolder(string folderPath, string iconPath)
        {
            try
            {
                string scriptPath = Path.Combine(Path.GetTempPath(), "UpdateIconResource.ps1");

                // Create PowerShell script file
                File.WriteAllText(scriptPath, @"
param (
    [string]$folderPath,
    [string]$iconPath
)

$desktopIniPath = Join-Path -Path $folderPath -ChildPath ""desktop.ini""

try {
    if (Test-Path $desktopIniPath) {
        # Update existing desktop.ini file
        (Get-Content $desktopIniPath) -replace 'IconResource=.*', ""IconResource=$iconPath"" | Set-Content $desktopIniPath
    } else {
        # Create new desktop.ini file
        @""
[.ShellClassInfo]
IconResource=$iconPath
[ViewState]
Mode=
Vid=
FolderType=Generic
""@ | Set-Content $desktopIniPath
    }

    # Set attributes for desktop.ini
    $desktopIniAttributes = [System.IO.FileAttributes]::Hidden -bor [System.IO.FileAttributes]::System
    [System.IO.File]::SetAttributes($desktopIniPath, $desktopIniAttributes)
    Write-Output ""Path: $iconPath""
    # Set folder attributes
    $folderAttributes = [System.IO.File]::GetAttributes($folderPath)
    $folderAttributes = $folderAttributes -bor [System.IO.FileAttributes]::System -bor [System.IO.FileAttributes]::ReadOnly
    [System.IO.File]::SetAttributes($folderPath, $folderAttributes)

    Write-Output ""Success: Icon applied and folder attributes updated.""
}
catch {
    Write-Output ""Error: $_""
}
");

                // Execute PowerShell script in silent mode and capture output
                using (var process = new Process())
                {
                    process.StartInfo.FileName = "powershell.exe";
                    process.StartInfo.Arguments = "-ExecutionPolicy Bypass -NoProfile -File \"" + scriptPath + "\" -folderPath \"" + folderPath + "\" -iconPath \"" + iconPath + "\"";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show("PowerShell Error: " + error);
                    }
                    else
                    {
                        MessageBox.Show(output);
                    }
                }

                // Optionally, clean up the script file
                File.Delete(scriptPath);

                // Refresh the folder view to apply changes
                RefreshFolderView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error applying icon: " + ex.Message);
            }
        }

        /// <summary>
        /// Refreshes the folder view by notifying the system of changes.
        /// </summary>
        private void RefreshFolderView()
        {
            // Use Windows API to refresh folder view
            const int SHCNF_FLUSH = 0x00000002;
            const int SHCNE_UPDATEDIR = 0x00000001;

            SHChangeNotify(SHCNE_UPDATEDIR, SHCNF_FLUSH, IntPtr.Zero, IntPtr.Zero);
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern void SHChangeNotify(int wEventId, int uFlags, IntPtr dwItem1, IntPtr dwItem2);

        /// <summary>
        /// Event handler for icon ListBox selection change. Updates the selected icon path.
        /// </summary>
        private void IconListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (iconListBox.SelectedItem != null)
            {
                selectedIconPath = iconListBox.SelectedItem.ToString();

                // Load and preview the icon
                try
                {
                    using (System.Drawing.Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(selectedIconPath))
                    {
                        iconPreviewPictureBox.Image = icon.ToBitmap();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading icon preview: " + ex.Message);
                    iconPreviewPictureBox.Image = null; // Clear the preview on error
                }
            }
            else
            {
                iconPreviewPictureBox.Image = null; // Clear the preview if no icon is selected
            }
        }

        /// <summary>
        /// Recentre preview box.
        /// </summary>
        private void CenterPictureBox()
        {
            // Center the PictureBox in the form
            iconPreviewPictureBox.Location = new Point(
                (this.splitContainer1.Panel1.Width - iconPreviewPictureBox.Width) / 2,
                (this.splitContainer1.Panel1.Height - iconPreviewPictureBox.Height) / 2);
        }

        /// <summary>
        /// Event handler for Center Picture Box.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // Recenter the PictureBox when the form is resized
            CenterPictureBox();
        }
    }
}
