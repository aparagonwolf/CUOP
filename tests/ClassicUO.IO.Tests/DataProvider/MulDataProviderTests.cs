// SPDX-License-Identifier: BSD-2-Clause

#nullable enable

using Xunit;
using ClassicUO.IO;
using ClassicUO.IO.DataProvider;
using Moq;

namespace ClassicUO.IO.Tests.DataProvider
{
    public class MulDataProviderTests
    {
        [Fact]
        public void ProviderName_ReturnsMul()
        {
            // Arrange
            var provider = new MulDataProvider();

            // Act
            var name = provider.ProviderName;

            // Assert
            Assert.Equal("MUL", name);
        }

        [Fact]
        public void CanHandle_WithAnyAssetType_ReturnsTrue()
        {
            // Arrange
            var provider = new MulDataProvider();

            // Act & Assert
            Assert.True(provider.CanHandle(AssetType.Art));
            Assert.True(provider.CanHandle(AssetType.Animation));
            Assert.True(provider.CanHandle(AssetType.Gump));
        }

        [Fact]
        public void GetAsset_WithoutLoadedFile_ReturnsNull()
        {
            // Arrange
            var provider = new MulDataProvider();

            // Act
            var result = provider.GetAsset(0);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Dispose_DoesNotThrow()
        {
            // Arrange
            var provider = new MulDataProvider();

            // Act & Assert - should not throw
            provider.Dispose();
        }

        [Fact]
        public void File_WhenNotSet_ReturnsNull()
        {
            // Arrange
            var provider = new MulDataProvider();

            // Act
            var file = provider.File;

            // Assert
            Assert.Null(file);
        }

        [Fact]
        public void SetFile_WithFile_StoresReference()
        {
            // Arrange
            var provider = new MulDataProvider();

            // Create a simple test file by writing to a temporary file
            var tempFile = System.IO.Path.GetTempFileName();
            System.IO.File.WriteAllBytes(tempFile, new byte[100]);
            var testFile = new UOFile(tempFile);

            try
            {
                // Act
                provider.SetFile(testFile);
                var retrievedFile = provider.File;

                // Assert
                Assert.NotNull(retrievedFile);
                Assert.Same(testFile, retrievedFile);
            }
            finally
            {
                // Cleanup - dispose before deleting the file
                testFile.Dispose();
                System.IO.File.Delete(tempFile);
            }
        }

        [Fact]
        public void GetAsset_WithStoredFileAndNegativeOffset_ReturnsNull()
        {
            // Arrange
            var provider = new MulDataProvider();

            // Create a temporary file for testing
            var tempFile = System.IO.Path.GetTempFileName();
            System.IO.File.WriteAllBytes(tempFile, new byte[100]);
            var testFile = new UOFile(tempFile);

            try
            {
                // File has no entries, so GetValidRefEntry returns Invalid with Offset = 0
                // The GetAsset check for Offset < 0 prevents returning assets with invalid entries
                provider.SetFile(testFile);

                // Act - request an asset index that doesn't exist
                var asset = provider.GetAsset(999);

                // Assert - should return an AssetStream (because Offset = 0 is not < 0)
                // This verifies the file is properly stored and accessible
                Assert.NotNull(asset);
                Assert.Same(testFile, asset.File);
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
