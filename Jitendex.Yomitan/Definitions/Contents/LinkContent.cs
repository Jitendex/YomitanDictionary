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

namespace Jitendex.Yomitan.Definitions.Contents;

public sealed class LinkContent : ObjectContent
{
    protected override string Tag => "a";
    public StructuredContent? Content { get; set; }

    /// <summary>
    /// The URL for the link. URLs starting with a ? are treated as internal links to other dictionary content.
    /// </summary>
    /// <remarks>
    /// "^(?:https?:|\\?)[\\w\\W]*"
    /// </remarks>
    public required string LinkUrl { get; set; }

    /// <summary>
    /// Defines the language of an element in the format defined by RFC 5646.
    /// </summary>
    public string? Language { get; set; }

    internal override JsonNode ToJsonNode()
    {
        var obj = new JsonObject
        {
            ["tag"] = Tag,
            ["href"] = LinkUrl,
        };
        if (Content is not null)
        {
            obj["content"] = Content.ToJsonNode();
        }
        if (Language is not null)
        {
            obj["lang"] = Language;
        }
        return obj;
    }
}
