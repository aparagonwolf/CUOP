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
    }
}
