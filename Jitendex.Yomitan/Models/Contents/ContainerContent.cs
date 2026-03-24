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

using Jitendex.Yomitan.Models.ContentAttributes;

namespace Jitendex.Yomitan.Models.Contents;

public abstract class ContainerContent : ObjectContent
{
    public StructuredContent? Content { get; set; }
    public ContentData? Data { get; set; }

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
        if (Language is not null)
        {
            obj["lang"] = Language;
        }
        return obj;
    }
}

public sealed class RubyContent : ContainerContent
{
    protected override string Tag => "ruby";
}

public sealed class RubyTextContent : ContainerContent
{
    protected override string Tag => "rt";
}

public sealed class RubyParenContent : ContainerContent
{
    protected override string Tag => "rp";
}

public sealed class TableContent : ContainerContent
{
    protected override string Tag => "table";
}

public sealed class TableHeadContent : ContainerContent
{
    protected override string Tag => "thead";
}

public sealed class TableBodyContent : ContainerContent
{
    protected override string Tag => "tbody";
}

public sealed class TableFootContent : ContainerContent
{
    protected override string Tag => "tfoot";
}

public sealed class TableRowContent : ContainerContent
{
    protected override string Tag => "tr";
}
