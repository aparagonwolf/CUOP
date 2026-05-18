// SPDX-License-Identifier: BSD-2-Clause

#nullable enable

using System;

namespace ClassicUO.IO.DataProvider
{
    /// <summary>
    /// Factory for creating appropriate data providers based on user preference and available files.
    /// </summary>
    public class DataProviderFactory
    {
        /// <summary>
        /// Creates a data provider based on the specified preference.
        /// </summary>
        /// <param name="preference">Format preference: "mul", "uop", or "auto". Null defaults to "auto".</param>
        /// <returns>Configured data provider instance.</returns>
        /// <exception cref="ArgumentException">Thrown if preference is not recognized.</exception>
        public IDataProvider CreateProvider(string? preference)
        {
            preference = (preference ?? "auto").ToLowerInvariant();

            return preference switch
            {
                "mul" => new MulDataProvider(),
                "uop" => new UopDataProvider(),
                "auto" => AutoDetectProvider(),
                _ => throw new ArgumentException($"Unknown file format preference: {preference}", nameof(preference))
            };
        }

        private IDataProvider AutoDetectProvider()
        {
            // Default to MUL for auto-detection
            // In real usage, this would check FileManager.IsUOPInstallation
            // For now, we default to MUL as it's the more basic format
            return new MulDataProvider();
        }
    }
}
