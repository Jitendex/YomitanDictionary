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

namespace Jitendex.Yomitan.Models.Options;

public enum TagCategory
{
    /// <summary>
    /// #b6327a (magenta)
    /// </summary>
    Name,

    /// <summary>
    /// #f0ad4e (orange)
    /// </summary>
    Expression,

    /// <summary>
    /// #0275d8 (blue)
    /// </summary>
    Popular,

    /// <summary>
    /// #5bc0de (lighter blue)
    /// </summary>
    Frequent,

    /// <summary>
    /// #d9534f (red)
    /// </summary>
    Archaism,

    /// <summary>
    /// #aa66cc (violet)
    /// </summary>
    Dictionary,

    /// <summary>
    /// #5cb85c (green)
    /// </summary>
    Frequency,

    /// <summary>
    /// #565656 (dark gray)
    /// </summary>
    PartOfSpeech,

    /// <summary>
    /// #8a8a91 (gray)
    /// </summary>
    Search,

    /// <summary>
    /// #6640be (purple)
    /// </summary>
    PronunciationDictionary,
}

internal static class TagCategoryExtensions
{
    public static JsonNode ToJsonNode(this TagCategory category) => category switch
    {
        TagCategory.Name => "name",
        TagCategory.Expression => "expression",
        TagCategory.Popular => "popular",
        TagCategory.Frequent => "frequent",
        TagCategory.Archaism => "archaism",
        TagCategory.Dictionary => "dictionary",
        TagCategory.Frequency => "frequency",
        TagCategory.PartOfSpeech => "partOfSpeech", // Camel case!
        TagCategory.Search => "search",
        TagCategory.PronunciationDictionary => "pronunciation-dictionary", // Kebab case!
        _ => throw new ArgumentOutOfRangeException(nameof(category))
    };
}
