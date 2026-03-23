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

using System.Text.Json.Nodes;

namespace Jitendex.Yomitan.Definitions.ContentAttributes;

public sealed class ContentData
{
    public Dictionary<string, string> Attributes { get; init; } = [];

    internal JsonObject ToJsonObject()
    {
        var obj = new JsonObject();
        foreach (var (key, value) in Attributes)
        {
            obj[key] = value;
        }
        return obj;
    }
}
