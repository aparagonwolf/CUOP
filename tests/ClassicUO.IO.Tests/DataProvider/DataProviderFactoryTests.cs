// SPDX-License-Identifier: BSD-2-Clause

#nullable enable

using System;
using Xunit;
using ClassicUO.IO;
using ClassicUO.IO.DataProvider;

namespace ClassicUO.IO.Tests.DataProvider
{
    public class DataProviderFactoryTests
    {
        [Fact]
        public void CreateProvider_WithMulPreference_ReturnsMulProvider()
        {
            // Arrange
            var factory = new DataProviderFactory();
            var preference = "mul";

            // Act
            var provider = factory.CreateProvider(preference);

            // Assert
            Assert.NotNull(provider);
            Assert.Equal("MUL", provider.ProviderName);
        }

        [Fact]
        public void CreateProvider_WithUopPreference_ReturnsUopProvider()
        {
            // Arrange
            var factory = new DataProviderFactory();
            var preference = "uop";

            // Act
            var provider = factory.CreateProvider(preference);

            // Assert
            Assert.NotNull(provider);
            Assert.Equal("UOP", provider.ProviderName);
        }

        [Fact]
        public void CreateProvider_WithAutoPreference_ReturnsProvider()
        {
            // Arrange
            var factory = new DataProviderFactory();
            var preference = "auto";

            // Act
            var provider = factory.CreateProvider(preference);

            // Assert
            Assert.NotNull(provider);
            // Auto-detection returns either MUL or UOP
            Assert.True(provider.ProviderName == "MUL" || provider.ProviderName == "UOP");
        }

        [Fact]
        public void CreateProvider_WithNullPreference_DefaultsToAuto()
        {
            // Arrange
            var factory = new DataProviderFactory();

            // Act
            var provider = factory.CreateProvider(null);

            // Assert
            Assert.NotNull(provider);
            Assert.True(provider.ProviderName == "MUL" || provider.ProviderName == "UOP");
        }

        [Fact]
        public void CreateProvider_WithInvalidPreference_ThrowsArgumentException()
        {
            // Arrange
            var factory = new DataProviderFactory();
            var preference = "invalid";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => factory.CreateProvider(preference));
        }

        [Fact]
        public void CreateProvider_WithCaseSensitivePreference_NormalizesToLowercase()
        {
            // Arrange
            var factory = new DataProviderFactory();

            // Act
            var mulProvider = factory.CreateProvider("MUL");
            var uopProvider = factory.CreateProvider("UOP");

            // Assert
            Assert.Equal("MUL", mulProvider.ProviderName);
            Assert.Equal("UOP", uopProvider.ProviderName);
        }
    }
}
