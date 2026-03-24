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

using Jitendex.Yomitan.Options;

namespace Jitendex.Yomitan;

/// <summary>
/// Data file containing tag information for terms and kanji.
/// </summary>
public sealed class Tag
{
    /// <summary>
    /// Tag name.
    /// </summary>
    public required string Name
    {
        get;
        set { field = value.Replace(' ', '�'); }
    }

    /// <summary>
    /// Category for the tag.
    /// </summary>
    public required TagCategory Category { get; set; }

    /// <summary>
    /// Sorting order for the tag.
    /// </summary>
    public required double SortOrder { get; set; }

    /// <summary>
    /// Notes for the tag.
    /// </summary>
    public required string Notes { get; set; }

    /// <summary>
    /// Score used to determine popularity. Negative values are more rare and positive
    /// values are more frequent. This score is also used to sort search results.
    /// </summary>
    public required double Score { get; set; }

    internal JsonArray ToJsonArray() =>
    [
        Name,
        Category.ToJsonNode(),
        SortOrder,
        Notes,
        Score,
    ];
}
