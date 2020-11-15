﻿using System;
using System.Linq;

namespace Milou.Deployer.Core
{
    /// <summary>
    ///     Detects if we are running inside a unit test.
    /// </summary>
    public static class UnitTestDetector
    {
        private static readonly Lazy<bool> _isInUnitTest = new Lazy<bool>(Initialize);

        public static bool HasUnitTestInAppDomain => _isInUnitTest.Value;

        public static bool Initialize()
        {
            string[] testAssemblyName =
            {
                "Microsoft.VisualStudio.QualityTools.UnitTestFramework",
                "xunit"
            };

            return AppDomain.CurrentDomain.GetAssemblies()
                .Any(assembly => !assembly.IsDynamic && assembly.FullName is {} name &&
                                 testAssemblyName.Any(lookupName => name.StartsWith(lookupName, StringComparison.OrdinalIgnoreCase)));
        }
    }
}