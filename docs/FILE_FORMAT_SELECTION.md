# File Format Selection

ClassicUO now supports loading Ultima Online assets from both legacy MUL and modern UOP file formats. This guide explains how to configure which format your client uses.

## Quick Start

Edit your `settings.json` file and look for the `file_format_preference` setting:

```json
{
  "file_format_preference": "auto"
}
```

Valid values are:
- `"auto"` (default) — Auto-detect format based on client version and available files
- `"mul"` — Use legacy MUL format (requires `art.mul`, `anim*.mul`, `gumpart.mul`, etc.)
- `"uop"` — Use modern UOP format (requires `artLegacyMUL.uop`, `AnimationFrame*.uop`, `gupartlegacymul.uop`, etc.)

## Detailed Configuration

### Auto-Detection (Default)

When `file_format_preference` is set to `"auto"` (or not specified), ClassicUO will:
1. Check your client version
2. Look for available files in your Ultima Online directory
3. Prefer UOP format for modern clients (version 7000+)
4. Fall back to MUL format if UOP files are not available

This is the recommended setting for most users.

### MUL Format

Set `file_format_preference` to `"mul"` to force legacy format:

```json
{
  "file_format_preference": "mul"
}
```

**Required files:**
- `art.mul` / `artidx.mul` — Graphics assets
- `anim0.mul` through `anim5.mul` (with .idx files) — Character animations
- `gumpart.mul` / `gumpidx.mul` — UI graphics
- `map0.mul` through `map5.mul` (with .idx files) — Map data
- `sounds.mul` / `soundidx.mul` — Sound effects
- Additional files for hues, textures, etc.

### UOP Format

Set `file_format_preference` to `"uop"` to use modern format:

```json
{
  "file_format_preference": "uop"
}
```

**Required files:**
- `artLegacyMUL.uop` — Graphics assets
- `AnimationFrame1.uop` through `AnimationFrame6.uop` — Character animations
- `gupartlegacymul.uop` — UI graphics
- `map0LegacyMUL.uop` through `map5LegacyMUL.uop` — Map data
- `Audio.uop` — Sound effects
- `Hues.uop` — Color data
- Additional .uop files for textures, fonts, etc.

## Testing with kruo Data

If you want to test ClassicUO with pre-configured UOP files:

1. Download or copy UOP files to a test directory
2. Update your `settings.json`:
   ```json
   {
     "ultimaonlinedirectory": "C:\\path\\to\\your\\uop\\files",
     "file_format_preference": "uop"
   }
   ```
3. Start ClassicUO
4. Check the console log for a message like: "Using UOP format"

## Troubleshooting

### "Failed to initialize data provider" Error

This error means the files for your selected format were not found.

**Solutions:**
1. Check that the correct files exist in your Ultima Online directory
2. Verify the file paths in your settings are correct
3. Change `file_format_preference` to `"auto"` and verify the client detects the correct format
4. Check the console log for more details

### Format Not Detected

If `file_format_preference` is set to `"auto"` but the format is not what you expected:

**MUL Format Issues:**
- Ensure `art.mul` and `artidx.mul` exist in your UO directory
- Check that the directory path in settings is correct

**UOP Format Issues:**
- Ensure `artLegacyMUL.uop` and other .uop files exist
- Verify your client version is 7000 or higher (UOP is for modern clients)
- Check that files are not corrupted

## Format Differences

### Performance
- UOP: Generally slower for initial load (larger files to decompress)
- MUL: Faster initial load but slightly slower per-asset access

### Compatibility
- UOP: Required for Ultima Online clients version 7000+
- MUL: Legacy format, works with older clients

### Asset Coverage
- Both formats support all asset types (textures, animations, gumps, maps, sounds, fonts)
- Some newer assets may only be available in UOP format

## Advanced

### Checking Which Format is Active

Start ClassicUO and check the console output. You should see a message like:
```
Using UOP format
```
or
```
Using MUL format
```

This confirms which provider is active.

### Creating Your Own Settings

If you have multiple Ultima Online installations, you can create a custom `settings.json` for each:

```json
{
  "ultimaonlinedirectory": "C:\\UO\\Classic",
  "file_format_preference": "mul",
  "username": "MyCharacter",
  "port": 2593,
  "ip": "127.0.0.1"
}
```

Save this file and start ClassicUO pointing to it (configuration varies by platform).

## Need Help?

If you encounter issues:
1. Check your `ultimaonlinedirectory` setting points to a valid path
2. Verify the required files exist for your chosen format
3. Check console logs for error messages
4. Try setting `file_format_preference` to `"auto"` to let ClassicUO detect the format
5. Report issues with file format detection to the ClassicUO project

---

**Last Updated:** May 2026
**ClassicUO Version:** 0.X.X
