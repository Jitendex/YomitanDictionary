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

namespace Jitendex.Yomitan.Definitions.StyleOptions;

public enum TextAlignOption : byte
{
    Start,
    End,
    Left,
    Right,
    Center,
    Justify,
    JustifyAll,
    MatchParent,
}

internal static class TextAlignOptionExtensions
{
    public static JsonNode ToJsonNode(this TextAlignOption option) => option switch
    {
        TextAlignOption.Start => "start",
        TextAlignOption.End => "end",
        TextAlignOption.Left => "left",
        TextAlignOption.Right => "right",
        TextAlignOption.Center => "center",
        TextAlignOption.Justify => "justify",
        TextAlignOption.JustifyAll => "justify-all",
        TextAlignOption.MatchParent => "match-parent",
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };
}
