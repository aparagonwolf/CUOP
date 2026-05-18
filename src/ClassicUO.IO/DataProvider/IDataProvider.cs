// SPDX-License-Identifier: BSD-2-Clause

#nullable enable

using System;
using ClassicUO.IO;

namespace ClassicUO.IO.DataProvider
{
    /// <summary>
    /// Abstraction for loading Ultima Online assets from different file formats (MUL, UOP, etc.)
    /// </summary>
    public interface IDataProvider : IDisposable
    {
        /// <summary>
        /// Gets the underlying UOFile instance. Used for raw file access when needed.
        /// </summary>
        UOFile? File { get; }

        /// <summary>
        /// Gets the name of the provider (e.g., "MUL", "UOP").
        /// </summary>
        string ProviderName { get; }

        /// <summary>
        /// Loads and initializes the provider. Fills file entries and validates format.
        /// </summary>
        void Load();

        /// <summary>
        /// Determines if the provider can handle the specified asset type.
        /// </summary>
        /// <param name="assetType">The asset type to check.</param>
        /// <returns>true if this provider can handle the asset type; otherwise false.</returns>
        bool CanHandle(AssetType assetType);

        /// <summary>
        /// Gets a readable stream for the specified asset. Returns null if not found.
        /// </summary>
        AssetStream? GetAsset(int assetId);
    }

    /// <summary>
    /// Types of assets that can be loaded.
    /// </summary>
    public enum AssetType
    {
        Art,
        Animation,
        Gump,
        Map,
        Sound,
        Font,
        Light,
        Multi,
        Texture
    }

    /// <summary>
    /// Represents an asset stream with decompression info.
    /// </summary>
    public class AssetStream : IDisposable
    {
        public required UOFile File { get; init; }
        public required UOFileIndex Entry { get; init; }

        public void Dispose()
        {
            // This class currently manages no unmanaged resources
            GC.SuppressFinalize(this);
        }
    }
}
