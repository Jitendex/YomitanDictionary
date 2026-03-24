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

namespace Jitendex.Yomitan.Models.Options.ImageOptions;

public enum RenderingOption : byte
{
    Auto,
    Pixelated,
    CrispEdges,
}

internal static class RenderingOptionExtensions
{
    public static JsonNode ToJsonNode(this RenderingOption option) => option switch
    {
        RenderingOption.Auto => "auto",
        RenderingOption.Pixelated => "pixelated",
        RenderingOption.CrispEdges => "crisp-edges",
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };
}
