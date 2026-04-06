using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using NUnit.Framework;

namespace IconMapper.Tests
{
    /// <summary>
    /// Tests for the theme-switching features added to MainForm in this PR:
    ///   - Theme enum values (Light = 0, Dark = 1)
    ///   - DarkTheme() private method
    ///   - LightTheme() private method
    ///   - SetApplicationTheme(Theme) private method (and its known bug)
    ///   - changeThemeToolStripMenuItem_Click toggle behaviour
    ///
    /// MainForm is a WinForms form; all private members are accessed via reflection.
    /// Tests are run on an STA thread as required by WinForms.
    /// </summary>
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class MainFormThemeTests
    {
        // Nested type helpers resolved once per fixture.
        private static readonly Type ThemeType =
            typeof(MainForm).GetNestedType("Theme", BindingFlags.NonPublic);

        // ------------------------------------------------------------------ //
        // Theme enum contract
        // ------------------------------------------------------------------ //

        [Test]
        public void Theme_Enum_Exists_AsPrivateNestedType()
        {
            Assert.IsNotNull(ThemeType,
                "Theme must be a private nested enum inside MainForm.");
            Assert.IsTrue(ThemeType.IsEnum, "Theme must be an enum type.");
        }

        [Test]
        public void Theme_Light_HasIntegerValueZero()
        {
            Assert.IsNotNull(ThemeType, "Theme enum not found.");
            object lightValue = Enum.Parse(ThemeType, "Light");
            Assert.AreEqual(0, Convert.ToInt32(lightValue),
                "Theme.Light must equal 0 (persisted as '0' in App.config).");
        }

        [Test]
        public void Theme_Dark_HasIntegerValueOne()
        {
            Assert.IsNotNull(ThemeType, "Theme enum not found.");
            object darkValue = Enum.Parse(ThemeType, "Dark");
            Assert.AreEqual(1, Convert.ToInt32(darkValue),
                "Theme.Dark must equal 1 (persisted as '1' in App.config).");
        }

        [Test]
        public void Theme_Enum_HasExactlyTwoValues()
        {
            Assert.IsNotNull(ThemeType, "Theme enum not found.");
            string[] names = Enum.GetNames(ThemeType);
            Assert.AreEqual(2, names.Length,
                "Theme enum must have exactly two values: Light and Dark.");
        }

        // ------------------------------------------------------------------ //
        // DarkTheme() — colour assertions
        // ------------------------------------------------------------------ //

        [Test]
        public void DarkTheme_SetsFormBackColor_ToDark()
        {
            using (var form = CreateUninitializedForm())
            {
                InvokePrivateMethod(form, "DarkTheme");
                Assert.AreEqual(Color.FromArgb(30, 30, 30), form.BackColor,
                    "DarkTheme() must set the form BackColor to (30, 30, 30).");
            }
        }

        [Test]
        public void DarkTheme_SetsFormForeColor_ToWhite()
        {
            using (var form = CreateUninitializedForm())
            {
                InvokePrivateMethod(form, "DarkTheme");
                Assert.AreEqual(Color.White, form.ForeColor,
                    "DarkTheme() must set the form ForeColor to White.");
            }
        }

        [Test]
        public void DarkTheme_SetsThemeSelectedField_ToDark()
        {
            using (var form = CreateUninitializedForm())
            {
                InvokePrivateMethod(form, "DarkTheme");

                object themeValue = GetPrivateField(form, "themeselected");
                Assert.AreEqual(1, Convert.ToInt32(themeValue),
                    "DarkTheme() must set themeselected to Theme.Dark (1).");
            }
        }

        [Test]
        public void DarkTheme_SetsTreeViewBackColor_ToDarkGray()
        {
            using (var form = CreateUninitializedForm())
            {
                InvokePrivateMethod(form, "DarkTheme");

                var treeView = GetControlByName<TreeView>(form, "folderTreeView");
                Assert.IsNotNull(treeView, "folderTreeView control must exist.");
                Assert.AreEqual(Color.FromArgb(45, 45, 48), treeView.BackColor,
                    "DarkTheme() must set folderTreeView.BackColor to (45, 45, 48).");
            }
        }

        [Test]
        public void DarkTheme_SetsTreeViewForeColor_ToWhite()
        {
            using (var form = CreateUninitializedForm())
            {
                InvokePrivateMethod(form, "DarkTheme");

                var treeView = GetControlByName<TreeView>(form, "folderTreeView");
                Assert.IsNotNull(treeView, "folderTreeView control must exist.");
                Assert.AreEqual(Color.White, treeView.ForeColor,
                    "DarkTheme() must set folderTreeView.ForeColor to White.");
            }
        }

        [Test]
        public void DarkTheme_SetsListBoxForeColor_ToLightGreen()
        {
            using (var form = CreateUninitializedForm())
            {
                InvokePrivateMethod(form, "DarkTheme");

                var listBox = GetControlByName<ListBox>(form, "iconListBox");
                Assert.IsNotNull(listBox, "iconListBox control must exist.");
                Assert.AreEqual(Color.LightGreen, listBox.ForeColor,
                    "DarkTheme() must set iconListBox.ForeColor to LightGreen.");
            }
        }

        [Test]
        public void DarkTheme_SetsApplyButtonBackColor_ToSteelBlue()
        {
            using (var form = CreateUninitializedForm())
            {
                InvokePrivateMethod(form, "DarkTheme");

                var btn = GetControlByName<Button>(form, "applyIconButton");
                Assert.IsNotNull(btn, "applyIconButton control must exist.");
                Assert.AreEqual(Color.FromArgb(70, 130, 180), btn.BackColor,
                    "DarkTheme() must set applyIconButton.BackColor to SteelBlue (70,130,180).");
            }
        }

        [Test]
        public void DarkTheme_SetsApplyButtonFlatStyle_ToFlat()
        {
            using (var form = CreateUninitializedForm())
            {
                InvokePrivateMethod(form, "DarkTheme");

                var btn = GetControlByName<Button>(form, "applyIconButton");
                Assert.IsNotNull(btn, "applyIconButton control must exist.");
                Assert.AreEqual(FlatStyle.Flat, btn.FlatStyle,
                    "DarkTheme() must set applyIconButton.FlatStyle to Flat.");
            }
        }

        // ------------------------------------------------------------------ //
        // LightTheme() — colour assertions
        // ------------------------------------------------------------------ //

        [Test]
        public void LightTheme_SetsFormBackColor_ToSystemControl()
        {
            using (var form = CreateUninitializedForm())
            {
                // Start from dark so there is a meaningful state change to observe.
                InvokePrivateMethod(form, "DarkTheme");
                InvokePrivateMethod(form, "LightTheme");

                Assert.AreEqual(SystemColors.Control, form.BackColor,
                    "LightTheme() must restore the form BackColor to SystemColors.Control.");
            }
        }

        [Test]
        public void LightTheme_SetsFormForeColor_ToSystemControlText()
        {
            using (var form = CreateUninitializedForm())
            {
                InvokePrivateMethod(form, "DarkTheme");
                InvokePrivateMethod(form, "LightTheme");

                Assert.AreEqual(SystemColors.ControlText, form.ForeColor,
                    "LightTheme() must restore the form ForeColor to SystemColors.ControlText.");
            }
        }

        [Test]
        public void LightTheme_SetsThemeSelectedField_ToLight()
        {
            using (var form = CreateUninitializedForm())
            {
                InvokePrivateMethod(form, "DarkTheme"); // set to dark first
                InvokePrivateMethod(form, "LightTheme");

                object themeValue = GetPrivateField(form, "themeselected");
                Assert.AreEqual(0, Convert.ToInt32(themeValue),
                    "LightTheme() must set themeselected to Theme.Light (0).");
            }
        }

        [Test]
        public void LightTheme_SetsListBoxBackColor_ToSystemControl()
        {
            using (var form = CreateUninitializedForm())
            {
                InvokePrivateMethod(form, "DarkTheme");
                InvokePrivateMethod(form, "LightTheme");

                var listBox = GetControlByName<ListBox>(form, "iconListBox");
                Assert.IsNotNull(listBox, "iconListBox control must exist.");
                Assert.AreEqual(SystemColors.Control, listBox.BackColor,
                    "LightTheme() must restore iconListBox.BackColor to SystemColors.Control.");
            }
        }

        [Test]
        public void LightTheme_SetsPictureBoxBorderStyle_ToNone()
        {
            using (var form = CreateUninitializedForm())
            {
                InvokePrivateMethod(form, "DarkTheme"); // dark sets FixedSingle
                InvokePrivateMethod(form, "LightTheme");

                var pic = GetControlByName<PictureBox>(form, "iconPreviewPictureBox");
                Assert.IsNotNull(pic, "iconPreviewPictureBox control must exist.");
                Assert.AreEqual(BorderStyle.None, pic.BorderStyle,
                    "LightTheme() must set iconPreviewPictureBox.BorderStyle to None.");
            }
        }

        // ------------------------------------------------------------------ //
        // SetApplicationTheme — known bug documentation
        // ------------------------------------------------------------------ //

        /// <summary>
        /// Documents the known bug in SetApplicationTheme(Theme theme): the method
        /// ignores its parameter and instead reads the instance field 'themeselected'.
        /// So passing Theme.Dark when themeselected == Theme.Light applies the Light theme.
        /// </summary>
        [Test]
        public void SetApplicationTheme_IgnoresParameter_UsesFielInstead_Bug()
        {
            using (var form = CreateUninitializedForm())
            {
                // Ensure themeselected == Light (0) by calling LightTheme first.
                InvokePrivateMethod(form, "LightTheme");

                // Now call SetApplicationTheme with Dark — but the field is Light.
                object darkEnum = Enum.Parse(ThemeType, "Dark");
                InvokePrivateMethodWithArgs(form, "SetApplicationTheme", new[] { darkEnum });

                // BUG: because themeselected is Light, LightTheme() runs, not DarkTheme().
                // The form should remain in a Light-themed state.
                Assert.AreEqual(SystemColors.Control, form.BackColor,
                    "Bug: SetApplicationTheme(Dark) sets Light theme because it reads " +
                    "themeselected (Light) instead of the supplied parameter.");
            }
        }

        // ------------------------------------------------------------------ //
        // changeThemeToolStripMenuItem_Click — toggle behaviour
        // ------------------------------------------------------------------ //

        [Test]
        public void ChangeThemeClick_WhenThemeIsLight_TogglesThemeSelectedToDark()
        {
            using (var form = CreateUninitializedForm())
            {
                // Force themeselected = Light.
                InvokePrivateMethod(form, "LightTheme");

                // Simulate menu click.
                InvokePrivateMethodWithArgs(
                    form, "changeThemeToolStripMenuItem_Click",
                    new object[] { null, EventArgs.Empty });

                object themeValue = GetPrivateField(form, "themeselected");
                Assert.AreEqual(1, Convert.ToInt32(themeValue),
                    "Clicking Change Theme from Light must toggle themeselected to Dark (1).");
            }
        }

        [Test]
        public void ChangeThemeClick_WhenThemeIsDark_TogglesThemeSelectedToLight()
        {
            using (var form = CreateUninitializedForm())
            {
                // Force themeselected = Dark.
                InvokePrivateMethod(form, "DarkTheme");

                InvokePrivateMethodWithArgs(
                    form, "changeThemeToolStripMenuItem_Click",
                    new object[] { null, EventArgs.Empty });

                object themeValue = GetPrivateField(form, "themeselected");
                Assert.AreEqual(0, Convert.ToInt32(themeValue),
                    "Clicking Change Theme from Dark must toggle themeselected to Light (0).");
            }
        }

        [Test]
        public void ChangeThemeClick_WhenThemeIsLight_AppliesDarkColours()
        {
            using (var form = CreateUninitializedForm())
            {
                InvokePrivateMethod(form, "LightTheme");

                InvokePrivateMethodWithArgs(
                    form, "changeThemeToolStripMenuItem_Click",
                    new object[] { null, EventArgs.Empty });

                // After toggling to dark the form background must be the dark colour.
                Assert.AreEqual(Color.FromArgb(30, 30, 30), form.BackColor,
                    "After toggle to Dark the form BackColor must be (30,30,30).");
            }
        }

        [Test]
        public void ChangeThemeClick_WhenThemeIsDark_AppliesLightColours()
        {
            using (var form = CreateUninitializedForm())
            {
                InvokePrivateMethod(form, "DarkTheme");

                InvokePrivateMethodWithArgs(
                    form, "changeThemeToolStripMenuItem_Click",
                    new object[] { null, EventArgs.Empty });

                Assert.AreEqual(SystemColors.Control, form.BackColor,
                    "After toggle to Light the form BackColor must be SystemColors.Control.");
            }
        }

        /// <summary>
        /// Regression: toggling theme twice must restore the original theme.
        /// </summary>
        [Test]
        public void ChangeThemeClick_DoubleToggle_RestoresOriginalTheme()
        {
            using (var form = CreateUninitializedForm())
            {
                InvokePrivateMethod(form, "LightTheme");
                Color originalBackColor = form.BackColor;

                // Toggle dark.
                InvokePrivateMethodWithArgs(
                    form, "changeThemeToolStripMenuItem_Click",
                    new object[] { null, EventArgs.Empty });

                // Toggle back to light.
                InvokePrivateMethodWithArgs(
                    form, "changeThemeToolStripMenuItem_Click",
                    new object[] { null, EventArgs.Empty });

                Assert.AreEqual(originalBackColor, form.BackColor,
                    "Double-toggling the theme must restore the original BackColor.");
            }
        }

        // ------------------------------------------------------------------ //
        // Helper utilities
        // ------------------------------------------------------------------ //

        /// <summary>
        /// Creates a MainForm instance while bypassing the constructor body (avoiding
        /// the ConfigurationManager and LoadDrives/LoadIcons calls that require a
        /// real environment) but still executing InitializeComponent so that all
        /// designer-created controls exist on the form.
        /// </summary>
        private static MainForm CreateUninitializedForm()
        {
            // Use FormFactory to create instance without invoking the full ctor body.
            var form = (MainForm)System.Runtime.Serialization.FormatterServices
                .GetUninitializedObject(typeof(MainForm));

            // InitializeComponent must run so the child controls exist.
            MethodInfo initMethod = typeof(MainForm).GetMethod(
                "InitializeComponent",
                BindingFlags.NonPublic | BindingFlags.Instance);
            initMethod?.Invoke(form, null);

            return form;
        }

        private static void InvokePrivateMethod(MainForm form, string methodName)
        {
            MethodInfo method = typeof(MainForm).GetMethod(
                methodName,
                BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(method, $"Method '{methodName}' must exist on MainForm.");
            method.Invoke(form, null);
        }

        private static void InvokePrivateMethodWithArgs(
            MainForm form, string methodName, object[] args)
        {
            MethodInfo method = typeof(MainForm).GetMethod(
                methodName,
                BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(method, $"Method '{methodName}' must exist on MainForm.");
            method.Invoke(form, args);
        }

        private static object GetPrivateField(MainForm form, string fieldName)
        {
            FieldInfo field = typeof(MainForm).GetField(
                fieldName,
                BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(field, $"Field '{fieldName}' must exist on MainForm.");
            return field.GetValue(form);
        }

        private static T GetControlByName<T>(MainForm form, string controlName)
            where T : Control
        {
            FieldInfo field = typeof(MainForm).GetField(
                controlName,
                BindingFlags.NonPublic | BindingFlags.Instance);
            return field?.GetValue(form) as T;
        }
    }
}