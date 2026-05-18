// SPDX-License-Identifier: BSD-2-Clause

#nullable enable

using System;
using System.Runtime.CompilerServices;
using ClassicUO.IO;

[assembly: InternalsVisibleTo("ClassicUO.IO.Tests")]

namespace ClassicUO.IO.DataProvider
{
    /// <summary>
    /// Data provider for UOP (Mythic Package) format files.
    /// </summary>
    public class UopDataProvider : IDataProvider
    {
        private UOFile? _file;

        public UOFile? File => _file;

        public string ProviderName => "UOP";

        public void Load()
        {
            // Will be called by factory with specific file paths
            // Implementation delegates to factory
        }

        public bool CanHandle(AssetType assetType)
        {
            // UOP format can handle all asset types
            return true;
        }

        public AssetStream? GetAsset(int assetId)
        {
            if (_file == null)
                return null;

            var entry = _file.GetValidRefEntry(assetId);

            if (entry.Offset < 0)
                return null;

            return new AssetStream
            {
                File = _file,
                Entry = entry
            };
        }

        public void Dispose()
        {
            _file?.Dispose();
            _file = null;
        }

        internal void SetFile(UOFile file)
        {
            _file = file;
        }
    }
}
