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

public enum VerticalAlignOption : byte
{
    Baseline,
    Sub,
    Super,
    TextTop,
    TextBottom,
    Middle,
    Top,
    Bottom,
}

internal static class VerticalAlignOptionExtensions
{
    public static JsonNode ToJsonNode(this VerticalAlignOption option) => option switch
    {
        VerticalAlignOption.Baseline => "baseline",
        VerticalAlignOption.Sub => "sub",
        VerticalAlignOption.Super => "super",
        VerticalAlignOption.TextTop => "text-top",
        VerticalAlignOption.TextBottom => "text-bottom",
        VerticalAlignOption.Middle => "middle",
        VerticalAlignOption.Top => "top",
        VerticalAlignOption.Bottom => "bottom",
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };
}
