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

using Jitendex.Yomitan.Definitions.ContentAttributes;

namespace Jitendex.Yomitan.Definitions.Contents;

public abstract class ConfigurableContent : ObjectContent
{
    public StructuredContent? Content { get; set; }
    public ContentData? Data { get; set; }
    public ContentStyle? Style { get; set; }

    /// <summary>
    /// Hover text for the element.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Whether or not the details element is open by default.
    /// </summary>
    public bool? Open { get; set; }

    /// <summary>
    /// Defines the language of an element in the format defined by RFC 5646.
    /// </summary>
    public string? Language { get; set; }

    internal override JsonNode ToJsonNode()
    {
        var obj = new JsonObject
        {
            ["tag"] = Tag
        };
        if (Content is not null)
        {
            obj["content"] = Content.ToJsonNode();
        }
        if (Data is not null)
        {
            obj["data"] = Data.ToJsonObject();
        }
        if (Title is not null)
        {
            obj["title"] = Title;
        }
        if (Open.HasValue)
        {
            obj["open"] = Open.Value;
        }
        if (Style is not null)
        {
            obj["style"] = Style.ToJsonObject();
        }
        if (Language is not null)
        {
            obj["lang"] = Language;
        }
        return obj;
    }
}

public sealed class SpanContent : ConfigurableContent
{
    protected override string Tag => "span";
}

public sealed class DivContent : ConfigurableContent
{
    protected override string Tag => "div";
}

public sealed class OrderedListContent : ConfigurableContent
{
    protected override string Tag => "ol";
}

public sealed class UnorderedListContent : ConfigurableContent
{
    protected override string Tag => "ul";
}

public sealed class ListItemContent : ConfigurableContent
{
    protected override string Tag => "li";
}

public sealed class DetailsContent : ConfigurableContent
{
    protected override string Tag => "details";
}

public sealed class SummaryContent : ConfigurableContent
{
    protected override string Tag => "summary";
}
