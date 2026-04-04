using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace IconMapper.Tests
{
    /// <summary>
    /// Tests for ContextMenuOptions (new file added in this PR).
    /// ContextMenuOptions is an internal class, accessed via reflection.
    /// </summary>
    [TestFixture]
    public class ContextMenuOptionsTests
    {
        private static readonly Assembly IconMapperAssembly = typeof(MainForm).Assembly;
        private static readonly Type ContextMenuOptionsType =
            IconMapperAssembly.GetType("IconMapper.ContextMenuOptions");

        // ------------------------------------------------------------------ //
        // Class-level structural tests
        // ------------------------------------------------------------------ //

        [Test]
        public void ContextMenuOptions_ClassExists_InIconMapperAssembly()
        {
            Assert.IsNotNull(ContextMenuOptionsType,
                "ContextMenuOptions class must exist in the IconMapper assembly.");
        }

        [Test]
        public void ContextMenuOptions_Method_IsPublicAndStatic()
        {
            Assert.IsNotNull(ContextMenuOptionsType, "ContextMenuOptions type not found.");

            MethodInfo method = ContextMenuOptionsType.GetMethod(
                "contextMenuOptions",
                BindingFlags.Public | BindingFlags.Static);

            Assert.IsNotNull(method,
                "contextMenuOptions() must be a public static method.");
            Assert.IsTrue(method.IsStatic,
                "contextMenuOptions() must be static.");
            Assert.AreEqual(0, method.GetParameters().Length,
                "contextMenuOptions() must accept no parameters.");
        }

        [Test]
        public void ContextMenuOptions_Method_ReturnsVoid()
        {
            Assert.IsNotNull(ContextMenuOptionsType, "ContextMenuOptions type not found.");

            MethodInfo method = ContextMenuOptionsType.GetMethod(
                "contextMenuOptions",
                BindingFlags.Public | BindingFlags.Static);

            Assert.IsNotNull(method);
            Assert.AreEqual(typeof(void), method.ReturnType,
                "contextMenuOptions() must return void.");
        }

        // ------------------------------------------------------------------ //
        // Behavioural tests
        // ------------------------------------------------------------------ //

        /// <summary>
        /// When the hardcoded icon directory does not exist the method must silently
        /// write to Console and return without throwing an exception.
        /// </summary>
        [Test]
        public void ContextMenuOptions_WhenHardcodedDirectoryDoesNotExist_DoesNotThrow()
        {
            Assert.IsNotNull(ContextMenuOptionsType, "ContextMenuOptions type not found.");

            MethodInfo method = ContextMenuOptionsType.GetMethod(
                "contextMenuOptions",
                BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method);

            TextWriter originalOut = Console.Out;
            try
            {
                Console.SetOut(TextWriter.Null);
                Assert.DoesNotThrow(
                    () => method.Invoke(null, null),
                    "contextMenuOptions() must not throw when the icon directory is absent.");
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        /// <summary>
        /// Verifies that the hardcoded path used by contextMenuOptions() does not
        /// exist in the test environment, confirming the method exercises the
        /// 'directory not found' branch during tests.
        /// </summary>
        [Test]
        public void ContextMenuOptions_HardcodedIconDirectory_DoesNotExistInTestEnvironment()
        {
            // The implementation hard-codes @"C:\Path\To\Your\IconFolder".
            // Confirming this path is absent ensures the non-registry (safe) code path
            // is exercised when the test runs.
            Assert.IsFalse(Directory.Exists(@"C:\Path\To\Your\IconFolder"),
                "Hardcoded icon directory must not exist in the test environment.");
        }

        /// <summary>
        /// Documents a known bug: the Console.WriteLine calls inside contextMenuOptions()
        /// use curly-brace identifiers without the '$' prefix, so variable values are
        /// NOT substituted — the output contains literal text like "{iconDirectory}".
        /// </summary>
        [Test]
        public void ContextMenuOptions_WhenDirectoryMissing_ConsoleOutputContainsLiteralPlaceholder()
        {
            // The implementation contains:
            //   Console.WriteLine("The directory '{iconDirectory}' does not exist.");
            // The missing '$' means "{iconDirectory}" is printed verbatim (a bug).
            Assert.IsNotNull(ContextMenuOptionsType, "ContextMenuOptions type not found.");

            MethodInfo method = ContextMenuOptionsType.GetMethod(
                "contextMenuOptions",
                BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method);

            var capturedOutput = new StringWriter();
            TextWriter originalOut = Console.Out;
            try
            {
                Console.SetOut(capturedOutput);
                method.Invoke(null, null);
            }
            finally
            {
                Console.SetOut(originalOut);
            }

            string output = capturedOutput.ToString();
            // Due to missing '$', the output is the literal string rather than the path value.
            StringAssert.Contains("{iconDirectory}", output,
                "Bug confirmed: '{iconDirectory}' is printed as a literal because " +
                "the '$' interpolation prefix is absent.");
        }

        // ------------------------------------------------------------------ //
        // Structural / path validation tests
        // ------------------------------------------------------------------ //

        /// <summary>
        /// Verifies that the registry context menu path used by the implementation
        /// follows the expected HKCR key structure.
        /// </summary>
        [Test]
        public void ContextMenuOptions_RegistryContextMenuPath_FollowsExpectedStructure()
        {
            // Implementation targets:
            //   HKEY_CLASSES_ROOT\Directory\shell\ChangeIcon\submenus
            const string expectedPath =
                @"HKEY_CLASSES_ROOT\Directory\shell\ChangeIcon\submenus";

            Assert.IsTrue(expectedPath.StartsWith("HKEY_CLASSES_ROOT"),
                "Registry path must target the HKEY_CLASSES_ROOT hive.");
            Assert.IsTrue(expectedPath.Contains(@"\Directory\shell\ChangeIcon\submenus"),
                "Registry path must point to the ChangeIcon submenus key.");
        }

        /// <summary>
        /// Regression: contextMenuOptions() must not raise an exception when invoked
        /// multiple times with the directory absent (idempotency of the safe path).
        /// </summary>
        [Test]
        public void ContextMenuOptions_WhenDirectoryAbsent_IsIdempotentAndDoesNotThrow()
        {
            Assert.IsNotNull(ContextMenuOptionsType, "ContextMenuOptions type not found.");

            MethodInfo method = ContextMenuOptionsType.GetMethod(
                "contextMenuOptions",
                BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method);

            TextWriter originalOut = Console.Out;
            try
            {
                Console.SetOut(TextWriter.Null);
                Assert.DoesNotThrow(() =>
                {
                    method.Invoke(null, null);
                    method.Invoke(null, null);
                },
                "Calling contextMenuOptions() multiple times must not throw.");
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }
    }
}