// SPDX-License-Identifier: BSD-2-Clause

using Xunit;
using ClassicUO.IO;
using ClassicUO.IO.DataProvider;

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
    }
}
