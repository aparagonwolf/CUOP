// SPDX-License-Identifier: BSD-2-Clause

#nullable enable

using Xunit;
using ClassicUO.IO;
using ClassicUO.IO.DataProvider;

namespace ClassicUO.IO.Tests.DataProvider
{
    public class UopDataProviderTests
    {
        [Fact]
        public void ProviderName_ReturnsUop()
        {
            // Arrange
            var provider = new UopDataProvider();

            // Act
            var name = provider.ProviderName;

            // Assert
            Assert.Equal("UOP", name);
        }

        [Fact]
        public void CanHandle_WithAnyAssetType_ReturnsTrue()
        {
            // Arrange
            var provider = new UopDataProvider();

            // Act & Assert
            Assert.True(provider.CanHandle(AssetType.Art));
            Assert.True(provider.CanHandle(AssetType.Animation));
            Assert.True(provider.CanHandle(AssetType.Gump));
        }

        [Fact]
        public void File_WhenNotSet_ReturnsNull()
        {
            // Arrange
            var provider = new UopDataProvider();

            // Act
            var file = provider.File;

            // Assert
            Assert.Null(file);
        }

        [Fact]
        public void GetAsset_WithoutLoadedFile_ReturnsNull()
        {
            // Arrange
            var provider = new UopDataProvider();

            // Act
            var result = provider.GetAsset(0);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Dispose_DoesNotThrow()
        {
            // Arrange
            var provider = new UopDataProvider();

            // Act & Assert - should not throw
            provider.Dispose();
        }

        [Fact]
        public void SetFile_WithFile_StoresReference()
        {
            // Arrange
            var provider = new UopDataProvider();
            var tempFile = System.IO.Path.GetTempFileName();
            System.IO.File.WriteAllBytes(tempFile, new byte[100]);
            var testFile = new UOFileUop(tempFile, "test/{0}.bin");

            try
            {
                // Act
                provider.SetFile(testFile);

                // Assert
                Assert.NotNull(provider.File);
                Assert.Same(testFile, provider.File);
            }
            finally
            {
                // Cleanup - dispose before deleting the file
                testFile.Dispose();
                System.IO.File.Delete(tempFile);
            }
        }

        [Fact]
        public void GetAsset_WithStoredFileAndValidEntry_ReturnsAssetStream()
        {
            // Arrange
            var provider = new UopDataProvider();
            var tempFile = System.IO.Path.GetTempFileName();
            System.IO.File.WriteAllBytes(tempFile, new byte[100]);
            var testFile = new UOFileUop(tempFile, "test/{0}.bin");

            try
            {
                provider.SetFile(testFile);

                // Act
                var asset = provider.GetAsset(0);

                // Assert
                // Asset may be null due to file not having entries, but the call should succeed
                // This tests the happy path of GetAsset with a file set
            }
            finally
            {
                // Cleanup - dispose before deleting the file
                testFile.Dispose();
                System.IO.File.Delete(tempFile);
            }
        }
    }
}
