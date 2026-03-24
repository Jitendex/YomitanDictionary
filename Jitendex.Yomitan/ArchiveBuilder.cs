/*
Copyright (c) 2026 Stephen Kraus
SPDX-License-Identifier: GPL-3.0-or-later

This file is part of YomitanDictionary.

YomitanDictionary is free software: you can redistribute it and/or modify it under the
terms of the GNU General Public License as published by the Free Software Foundation,
either version 3 of the License, or (at your option) any later version.

YomitanDictionary is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with YomitanDictionary.
If not, see <https://www.gnu.org/licenses/>.
*/

using System.IO.Compression;
using System.Text.Json;
using Jitendex.Yomitan.Models;

namespace Jitendex.Yomitan;

public sealed class ArchiveBuilder : IDisposable
{
    private readonly DirectoryInfo BuildDirectory;
    private readonly string IndexPath;
    private readonly string StylesPath;
    private int TermBank = 1;
    private int TagBank = 1;
    private bool DisposedValue;

    public ArchiveBuilder(string buildDirectoryPath)
    {
        BuildDirectory = new(buildDirectoryPath);
        if (BuildDirectory.Exists)
        {
            BuildDirectory.Delete(recursive: true);
            BuildDirectory.Create();
        }
        IndexPath = Path.Join(BuildDirectory.FullName, "index.json");
        StylesPath = Path.Join(BuildDirectory.FullName, "styles.css");
    }

    public async Task WriteIndexAsync(DictionaryIndex index)
    {
        if (File.Exists(IndexPath))
        {
            throw new InvalidOperationException($"Index file already exists at path `{IndexPath}`");
        }
        await using var stream = new FileStream(IndexPath, FileMode.CreateNew, FileAccess.Write, FileShare.None);
        await JsonSerializer.SerializeAsync(stream, index.ToJsonObject());
    }

    public async Task WriteStylesAsync(string styles)
    {
        if (File.Exists(StylesPath))
        {
            throw new InvalidOperationException($"Styles file already exists at path `{StylesPath}`");
        }
        await using var stream = new FileStream(StylesPath, FileMode.CreateNew, FileAccess.Write, FileShare.None);
        await using var writer = new StreamWriter(stream);
        await writer.WriteAsync(styles);
    }

    public void CopyStylesFile(string pathToStylesFile)
    {
        if (File.Exists(StylesPath))
        {
            throw new InvalidOperationException($"Styles file already exists at path `{StylesPath}`");
        }
        var file = new FileInfo(pathToStylesFile);
        if (file.Exists is false)
        {
            throw new FileNotFoundException($"Styles file not found at path `{file.FullName}`");
        }
        file.CopyTo(StylesPath);
    }

    public async Task WriteTermBankAsync(IEnumerable<Term> terms)
    {
        var bank = new JsonArray();
        foreach (var term in terms)
        {
            bank.Add(term.ToJsonArray());
        }
        var path = Path.Join(BuildDirectory.FullName, $"term_bank_{TermBank++:D3}.json");
        await using var stream = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None);
        await JsonSerializer.SerializeAsync(stream, bank);
    }

    public async Task WriteTagBankAsync(IEnumerable<Tag> tags)
    {
        var bank = new JsonArray();
        foreach (var tag in tags)
        {
            bank.Add(tag.ToJsonArray());
        }
        var path = Path.Join(BuildDirectory.FullName, $"tag_bank_{TagBank++}.json");
        await using var stream = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None);
        await JsonSerializer.SerializeAsync(stream, bank);
    }

    public async Task CopyMediaDirectoryAsync(string sourcePath, string? destinationPathPrefix)
    {
        var mediaDirectory = new DirectoryInfo(sourcePath);

        if (mediaDirectory.Exists is false)
        {
            throw new DirectoryNotFoundException($"Source directory not found at path `{mediaDirectory.FullName}`");
        }

        var subdirs = mediaDirectory.GetDirectories();

        var destinationDirName = destinationPathPrefix is not null
                ? Path.Join(destinationPathPrefix, mediaDirectory.Name)
                : mediaDirectory.Name;

        var destinationDirFullName = Path.Join(BuildDirectory.FullName, destinationDirName);

        if (Directory.Exists(destinationDirFullName))
        {
            throw new InvalidOperationException($"Destination directory already exists at path `{destinationDirFullName}`");
        }

        Directory.CreateDirectory(destinationDirFullName);

        foreach (var file in mediaDirectory.EnumerateFiles())
        {
            var path = Path.Join(destinationDirFullName, file.Name);
            file.CopyTo(path);
        }

        foreach (var subdir in subdirs)
        {
            await CopyMediaDirectoryAsync(subdir.FullName, destinationDirName);
        }
    }

    public async Task BuildArchiveAsync(string pathToOutputFile)
    {
        if (File.Exists(IndexPath) is false)
        {
            throw new InvalidOperationException($"Dictionary index file not found at path `{IndexPath}`");
        }
        if (File.Exists(pathToOutputFile))
        {
            throw new InvalidOperationException($"File already exists at path `{pathToOutputFile}`");
        }
        await ZipFile.CreateFromDirectoryAsync(BuildDirectory.FullName, pathToOutputFile);
    }

    private void Dispose(bool disposing)
    {
        if (!DisposedValue)
        {
            if (BuildDirectory.Exists)
            {
                BuildDirectory.Delete(recursive: true);
            }
            DisposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
