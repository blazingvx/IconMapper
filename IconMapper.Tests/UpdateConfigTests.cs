using System;
using System.Configuration;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using NUnit.Framework;

namespace IconMapper.Tests
{
    /// <summary>
    /// Tests for the UpdateConfig(string key, string value, string section) private
    /// method introduced in MainForm in this PR, and for the App.config changes
    /// (addition of the "LastTheme" key).
    ///
    /// Key known issue: UpdateConfig always returns false because the local variable
    /// 'retValue' is initialised to false and is never set to true before being returned.
    /// Tests document this behaviour.
    /// </summary>
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class UpdateConfigTests
    {
        private static MethodInfo GetUpdateConfigMethod()
        {
            // Signature: private bool UpdateConfig(string key, string value, string section = "appSettings")
            return typeof(MainForm).GetMethod(
                "UpdateConfig",
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new[] { typeof(string), typeof(string), typeof(string) },
                null);
        }

        private static MainForm CreateUninitializedForm()
        {
            var form = (MainForm)System.Runtime.Serialization.FormatterServices
                .GetUninitializedObject(typeof(MainForm));

            MethodInfo initMethod = typeof(MainForm).GetMethod(
                "InitializeComponent",
                BindingFlags.NonPublic | BindingFlags.Instance);
            initMethod?.Invoke(form, null);

            return form;
        }

        // ------------------------------------------------------------------ //
        // Method signature / existence tests
        // ------------------------------------------------------------------ //

        [Test]
        public void UpdateConfig_Method_Exists_OnMainForm()
        {
            MethodInfo method = GetUpdateConfigMethod();
            Assert.IsNotNull(method,
                "UpdateConfig(string, string, string) must exist as a private instance method on MainForm.");
        }

        [Test]
        public void UpdateConfig_Method_ReturnType_IsBool()
        {
            MethodInfo method = GetUpdateConfigMethod();
            Assert.IsNotNull(method, "UpdateConfig method not found.");
            Assert.AreEqual(typeof(bool), method.ReturnType,
                "UpdateConfig must return bool.");
        }

        [Test]
        public void UpdateConfig_Method_HasThreeParameters()
        {
            MethodInfo method = GetUpdateConfigMethod();
            Assert.IsNotNull(method, "UpdateConfig method not found.");
            ParameterInfo[] parameters = method.GetParameters();
            Assert.AreEqual(3, parameters.Length,
                "UpdateConfig must accept exactly three parameters: key, value, section.");
        }

        // ------------------------------------------------------------------ //
        // Return-value bug documentation
        // ------------------------------------------------------------------ //

        /// <summary>
        /// Documents the known bug: UpdateConfig() always returns false.
        /// The local variable 'retValue' is initialised to false and is never
        /// set to true, even on a successful save.
        /// </summary>
        [Test]
        public void UpdateConfig_AlwaysReturnsFalse_EvenForValidKey()
        {
            MethodInfo method = GetUpdateConfigMethod();
            Assert.IsNotNull(method, "UpdateConfig method not found.");

            using (var form = CreateUninitializedForm())
            {
                // "LastTheme" is a valid key present in the test App.config.
                object result = method.Invoke(form, new object[] { "LastTheme", "1", "appSettings" });
                Assert.IsFalse((bool)result,
                    "Bug: UpdateConfig() always returns false because 'retValue' is " +
                    "initialised to false and never changed to true.");
            }
        }

        [Test]
        public void UpdateConfig_AlwaysReturnsFalse_ForNonExistentKey()
        {
            MethodInfo method = GetUpdateConfigMethod();
            Assert.IsNotNull(method, "UpdateConfig method not found.");

            using (var form = CreateUninitializedForm())
            {
                // Even when the key doesn't exist (will throw internally, caught silently)
                // the method must still return false.
                object result = method.Invoke(
                    form,
                    new object[] { "NonExistentKey_XYZ", "value", "appSettings" });
                Assert.IsFalse((bool)result,
                    "UpdateConfig() must return false for a non-existent key (exception is swallowed).");
            }
        }

        // ------------------------------------------------------------------ //
        // Exception-swallowing behaviour
        // ------------------------------------------------------------------ //

        /// <summary>
        /// UpdateConfig catches all exceptions internally and does not rethrow them.
        /// This means callers can never detect failures via the return value or exceptions.
        /// </summary>
        [Test]
        public void UpdateConfig_WithNullKey_DoesNotThrowToCallerAndReturnsFalse()
        {
            MethodInfo method = GetUpdateConfigMethod();
            Assert.IsNotNull(method, "UpdateConfig method not found.");

            using (var form = CreateUninitializedForm())
            {
                // Passing null as the key will cause an exception inside UpdateConfig.
                // The catch block swallows it; the method must return false without rethrowing.
                object result = null;
                Assert.DoesNotThrow(
                    () => result = method.Invoke(form, new object[] { null, "value", "appSettings" }),
                    "UpdateConfig() must not propagate exceptions to the caller.");
                Assert.IsFalse((bool)result,
                    "UpdateConfig() must return false when an internal exception occurs.");
            }
        }

        [Test]
        public void UpdateConfig_WithNullValue_DoesNotThrowToCallerAndReturnsFalse()
        {
            MethodInfo method = GetUpdateConfigMethod();
            Assert.IsNotNull(method, "UpdateConfig method not found.");

            using (var form = CreateUninitializedForm())
            {
                object result = null;
                Assert.DoesNotThrow(
                    () => result = method.Invoke(form, new object[] { "LastTheme", null, "appSettings" }),
                    "UpdateConfig() must not propagate exceptions for a null value.");
                Assert.IsFalse((bool)result);
            }
        }

        // ------------------------------------------------------------------ //
        // App.config "LastTheme" key — contract tests (new in this PR)
        // ------------------------------------------------------------------ //

        /// <summary>
        /// The PR adds the "LastTheme" key to App.config with a default value of "0".
        /// Verifies the test project's App.config mirrors that key.
        /// </summary>
        [Test]
        public void AppConfig_LastThemeKey_ExistsWithDefaultValueZero()
        {
            string value = ConfigurationManager.AppSettings["LastTheme"];
            Assert.IsNotNull(value,
                "'LastTheme' key must be present in App.config.");
            Assert.AreEqual("0", value,
                "'LastTheme' default value must be '0' (Theme.Light).");
        }

        [Test]
        public void AppConfig_LastThemeValue_CanBeConvertedToThemeEnum()
        {
            string raw = ConfigurationManager.AppSettings["LastTheme"];
            Assert.IsNotNull(raw, "'LastTheme' key must be present.");

            int parsed;
            bool success = int.TryParse(raw, out parsed);
            Assert.IsTrue(success, "'LastTheme' must be a valid integer string.");
            Assert.IsTrue(parsed == 0 || parsed == 1,
                "'LastTheme' value must be 0 (Light) or 1 (Dark).");
        }

        [Test]
        public void AppConfig_IconFolderPathKey_StillPresent()
        {
            // Ensure the pre-existing key was not accidentally removed.
            string value = ConfigurationManager.AppSettings["IconFolderPath"];
            Assert.IsNotNull(value, "'IconFolderPath' key must still be present in App.config.");
        }

        // ------------------------------------------------------------------ //
        // Bonus: default section parameter
        // ------------------------------------------------------------------ //

        /// <summary>
        /// Verifies that the default value of the 'section' parameter is "appSettings",
        /// which determines which section is refreshed after saving the config.
        /// </summary>
        [Test]
        public void UpdateConfig_DefaultSection_IsAppSettings()
        {
            MethodInfo method = GetUpdateConfigMethod();
            Assert.IsNotNull(method, "UpdateConfig method not found.");

            ParameterInfo[] parameters = method.GetParameters();
            ParameterInfo sectionParam = null;
            foreach (var p in parameters)
            {
                if (p.Name == "section")
                {
                    sectionParam = p;
                    break;
                }
            }

            Assert.IsNotNull(sectionParam, "UpdateConfig must have a 'section' parameter.");
            Assert.IsTrue(sectionParam.HasDefaultValue,
                "The 'section' parameter must have a default value.");
            Assert.AreEqual("appSettings", sectionParam.DefaultValue,
                "Default value of 'section' must be \"appSettings\".");
        }
    }
}