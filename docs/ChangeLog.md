# Change Log

## Version 1.1.0
- **Enhancements and New Features**
  - **Icon Preview**: Added functionality to preview the selected icon in a `PictureBox` when an icon is selected from the `ListBox`.
  - **Centering Logic**: Implemented automatic centering of the icon preview box when the form is resized, ensuring a better user experience.
  - **Settings Menu**: Introduced a settings option to change the icon folder path, allowing users to update the path stored in `App.config` through a folder browser dialog.
  - **Import Icons**: Added an import functionality for `.ico` files, enabling users to select multiple files and copy them to the configured icon folder.
  - **Help and About Dialogs**: Created help and about menu items to provide users with information about the application and its functionalities.

### Key Changes
- **Load Subfolders Method**: Cleaned up the subfolder loading logic by ensuring the "Loading..." node is only added when necessary.
- **Error Handling**: Improved error handling for icon preview loading and updated messages for clarity.
- **Menu Item Click Events**: Implemented logic for menu items to enhance user interaction, including settings for icon paths and importing icons.

### User Interface Improvements
- Enhanced user interaction through message boxes that provide feedback on actions taken (e.g., icon imports, updates).
- Improved layout management by dynamically centering the icon preview in the form.

### Known Issues
- None reported in this release.

### Future Improvements
- Consider adding a feature for deleting icons from the icon folder.
- Explore the possibility of adding more icon formats for import.

---

## Version 1.0.0
- **Initial Release**
  - Created `MainForm` class within the `IconMapper` namespace.
  - Implemented functionality to load available drives and display them in a `TreeView`.
  - Added event handling for `TreeView` selection and expansion to dynamically load subfolders.
  - Developed methods to load icon files from a specified directory, as configured in `App.config`.
  - Enabled applying selected icons to folders by modifying `desktop.ini` files.
  - Incorporated PowerShell script execution to manage folder icon settings.
  - Implemented folder view refresh after icon application using Windows API.
  - Provided user feedback through message boxes for error handling and confirmations.

### Key Features
- **Drive and Folder Navigation**
  - Displays fixed and removable drives in a `TreeView`.
  - Dynamically loads subfolders on selection and expansion of nodes.

- **Icon Management**
  - Loads `.ico` files from a specified folder path.
  - Allows users to select an icon and apply it to a folder.

- **Error Handling**
  - Shows messages for issues with directory loading, icon application, and PowerShell script execution.

### Known Issues
- None reported in this initial release.

### Future Improvements
- Potential integration of icon preview functionality.
- Enhanced error logging for debugging purposes.
- User interface improvements for better usability.