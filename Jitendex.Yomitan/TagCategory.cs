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

namespace Jitendex.Yomitan;

public enum TagCategory
{
    /// <summary>
    /// #b6327a
    /// </summary>
    Name,

    /// <summary>
    /// #f0ad4e
    /// </summary>
    Expression,

    /// <summary>
    /// #0275d8
    /// </summary>
    Popular,

    /// <summary>
    /// #5bc0de
    /// </summary>
    Frequent,

    /// <summary>
    /// #d9534f
    /// </summary>
    Archaism,

    /// <summary>
    /// #aa66cc
    /// </summary>
    Dictionary,

    /// <summary>
    /// #5cb85c
    /// </summary>
    Frequency,

    /// <summary>
    /// #565656
    /// </summary>
    PartOfSpeech,

    /// <summary>
    /// #8a8a91
    /// </summary>
    Search,

    /// <summary>
    /// #6640be
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
