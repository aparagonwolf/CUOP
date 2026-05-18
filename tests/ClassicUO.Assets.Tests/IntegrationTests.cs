// SPDX-License-Identifier: BSD-2-Clause

#nullable enable

using Xunit;
using ClassicUO.Assets;
using ClassicUO.IO;
using ClassicUO.IO.DataProvider;
using ClassicUO.Utility;
using System;
using System.IO;

namespace ClassicUO.Assets.Tests
{
    public class IntegrationTests
    {
        private const string KRUO_DATA_PATH = @"C:\Users\Tristan\Desktop\kruo";

        private bool IsKruoAvailable()
        {
            return Directory.Exists(KRUO_DATA_PATH);
        }

        [Fact(Skip = "Requires kruo data at C:\\Users\\Tristan\\Desktop\\kruo")]
        public void ArtLoader_WithKruoUop_LoadsSuccessfully()
        {
            // Arrange
            if (!IsKruoAvailable())
            {
                throw new InvalidOperationException("kruo test data not found at: " + KRUO_DATA_PATH);
            }

            var fileManager = new UOFileManager(ClientVersion.CV_7000, KRUO_DATA_PATH);
            var artLoader = new ArtLoader(fileManager);
            artLoader.Load();

            // Act & Assert
            Assert.NotNull(artLoader.File);
            Assert.True(artLoader.File.Entries.Length > 0, "ArtLoader should load entries from kruo");
        }

        [Fact(Skip = "Requires kruo data at C:\\Users\\Tristan\\Desktop\\kruo")]
        public void GumpsLoader_WithKruoUop_LoadsSuccessfully()
        {
            // Arrange
            if (!IsKruoAvailable())
            {
                throw new InvalidOperationException("kruo test data not found");
            }

            var fileManager = new UOFileManager(ClientVersion.CV_7000, KRUO_DATA_PATH);
            var gumpsLoader = new GumpsLoader(fileManager);
            gumpsLoader.Load();

            // Act & Assert
            Assert.NotNull(gumpsLoader.File);
            Assert.True(gumpsLoader.File.Entries.Length > 0, "GumpsLoader should load entries from kruo");
        }

        [Fact(Skip = "Requires kruo data at C:\\Users\\Tristan\\Desktop\\kruo")]
        public void DataProviderFactory_WithUopPreference_CreatesUopProvider()
        {
            // Arrange
            var factory = new DataProviderFactory();

            // Act
            var provider = factory.CreateProvider("uop");

            // Assert
            Assert.NotNull(provider);
            Assert.Equal("UOP", provider.ProviderName);
        }

        [Fact]
        public void DataProviderFactory_WithMulPreference_CreatesMulProvider()
        {
            // Arrange
            var factory = new DataProviderFactory();

            // Act
            var provider = factory.CreateProvider("mul");

            // Assert
            Assert.NotNull(provider);
            Assert.Equal("MUL", provider.ProviderName);
        }

        [Fact]
        public void DataProviderFactory_WithAutoPreference_CreatesProvider()
        {
            // Arrange
            var factory = new DataProviderFactory();

            // Act
            var provider = factory.CreateProvider("auto");

            // Assert
            Assert.NotNull(provider);
            Assert.NotEmpty(provider.ProviderName);
        }

        [Fact]
        public void DataProviderFactory_WithNullPreference_CreatesProvider()
        {
            // Arrange
            var factory = new DataProviderFactory();

            // Act
            var provider = factory.CreateProvider(null);

            // Assert
            Assert.NotNull(provider);
            Assert.NotEmpty(provider.ProviderName);
        }
    }
}
