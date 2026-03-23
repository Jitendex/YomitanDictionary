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

using System.Text.RegularExpressions;

namespace Jitendex.Yomitan;

/// <summary>
/// Index file containing information about the data contained in the dictionary.
/// </summary>
public partial class Index
{
    /// <summary>
    /// Title of the dictionary.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Revision of the dictionary. This value is displayed, and used to check for dictionary updates.
    /// </summary>
    public required string Revision { get; set; }

    /// <summary>
    /// Minimum version of Yomitan that is compatible with this dictionary.
    /// </summary>
    public string? MinimumVersion { get; set; }

    /// <summary>
    /// Whether or not this dictionary contains sequencing information for related terms.
    /// </summary>
    public bool? IsSequenced { get; set; }

    /// <summary>
    /// Creator of the dictionary.
    /// </summary>
    public string? Author { get; set; }

    /// <summary>
    /// Whether this dictionary contains links to its latest version.
    /// </summary>
    public bool IsUpdatable { get; set; } = false;

    /// <summary>
    /// URL for the index file of the latest revision of the dictionary, used to check for updates.
    /// </summary>
    public string? UpdatesUrl { get; set; }

    /// <summary>
    /// URL for the download of the latest revision of the dictionary.
    /// </summary>
    public string? DownloadUrl { get; set; }

    /// <summary>
    /// URL for the source of the dictionary, displayed in the dictionary details.
    /// </summary>
    public string? SourceUrl { get; set; }

    /// <summary>
    /// Description of the dictionary data.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Attribution information for the dictionary data.
    /// </summary>
    public string? Attribution { get; set; }

    [GeneratedRegex(@"^[a-z]{2,3}$", RegexOptions.None)]
    private static partial Regex LanguageCodeRegex { get; }

    /// <summary>
    /// Language of the terms in the dictionary.
    /// </summary>
    public string? SourceLanguageCode
    {
        get;
        set
        {
            if (value is not null && LanguageCodeRegex.IsMatch(value) is false)
            {
                throw new ArgumentException($"Value `{value}` is invalid for {nameof(SourceLanguageCode)}");
            }
            field = value;
        }
    }

    /// <summary>
    /// Main language of the definitions in the dictionary.
    /// </summary>
    public string? TargetLanguageCode
    {
        get;
        set
        {
            if (value is not null && LanguageCodeRegex.IsMatch(value) is false)
            {
                throw new ArgumentException($"Value `{value}` is invalid for {nameof(TargetLanguageCode)}");
            }
            field = value;
        }
    }

    public FrequencyModeOption? FrequencyMode { get; set; }

    internal JsonObject ToJsonObject()
    {
        var obj = new JsonObject
        {
            ["title"] = Title,
            ["revision"] = Revision,
            ["format"] = 3,
        };
        if (MinimumVersion is not null)
        {
            obj["minimumYomitanVersion"] = MinimumVersion;
        }
        if (IsSequenced.HasValue)
        {
            obj["sequenced"] = IsSequenced.Value;
        }
        if (Author is not null)
        {
            obj["author"] = Author;
        }
        if (IsUpdatable)
        {
            if (UpdatesUrl is not null && DownloadUrl is not null)
            {
                obj["isUpdatable"] = true;
                obj["indexUrl"] = UpdatesUrl;
                obj["downloadUrl"] = DownloadUrl;
            }
            else
            {
                throw new InvalidOperationException($"{nameof(UpdatesUrl)} and {nameof(DownloadUrl)} must not be null when {nameof(IsUpdatable)} is true.");
            }
        }
        else if (UpdatesUrl is not null || DownloadUrl is not null)
        {
            throw new InvalidOperationException($"{nameof(UpdatesUrl)} and {nameof(DownloadUrl)} must be null when {nameof(IsUpdatable)} is false.");
        }
        if (SourceUrl is not null)
        {
            obj["url"] = SourceUrl;
        }
        if (Description is not null)
        {
            obj["description"] = Description;
        }
        if (Attribution is not null)
        {
            obj["attribution"] = Attribution;
        }
        if (SourceLanguageCode is not null)
        {
            obj["sourceLanguage"] = SourceLanguageCode;
        }
        if (TargetLanguageCode is not null)
        {
            obj["targetLanguage"] = TargetLanguageCode;
        }
        if (FrequencyMode.HasValue)
        {
            obj["frequencyMode"] = FrequencyMode.Value.ToJsonNode();
        }
        return obj;
    }
}
