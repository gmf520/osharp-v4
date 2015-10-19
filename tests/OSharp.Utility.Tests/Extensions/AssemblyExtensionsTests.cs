﻿using OSharp.Utility.Extensions;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

using Smocks;

using Xunit;


namespace OSharp.Utility.Extensions.Tests
{
    public class AssemblyExtensionsTests
    {
        [Fact()]
        public void GetFileVersionTest()
        {
            Assembly assembly = null;
            Assert.Throws<ArgumentNullException>(() => assembly.GetFileVersion());
            assembly = Assembly.GetAssembly(typeof(AssemblyExtensionsTests));
            Version version = assembly.GetFileVersion();
            Assert.Equal(version.Major, 3);
        }

        [Fact()]
        public void GetProductVersionTest()
        {
            Assembly assembly = null;
            Assert.Throws<ArgumentNullException>(() => assembly.GetProductVersion());
            assembly = Assembly.GetAssembly(typeof(AssemblyExtensionsTests));
            Version version = assembly.GetProductVersion();
            Assert.Equal(version.Major, 3);
        }
    }
}