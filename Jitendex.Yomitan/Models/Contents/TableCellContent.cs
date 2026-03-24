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

public abstract class TableCellContent : ObjectContent
{
    public StructuredContent? Content { get; set; }
    public DataAttributes? Data { get; set; }
    public StyleAttributes? Style { get; set; }

    public int? ColSpan
    {
        get;
        set
        {
            if (value.HasValue && value.Value < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(ColSpan)} must be greater than 0.");
            }
            field = value;
        }
    }

    public int? RowSpan
    {
        get;
        set
        {
            if (value.HasValue && value.Value < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(RowSpan)} must be greater than 0.");
            }
            field = value;
        }
    }

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
        if (Style is not null)
        {
            obj["style"] = Style.ToJsonObject();
        }
        if (ColSpan.HasValue)
        {
            obj["colSpan"] = ColSpan.Value;
        }
        if (RowSpan.HasValue)
        {
            obj["rowSpan"] = RowSpan.Value;
        }
        if (Language is not null)
        {
            obj["lang"] = Language;
        }
        return obj;
    }
}

public sealed class DataCellContent : TableCellContent
{
    protected override string Tag => "td";
}

public sealed class HeadCellContent : TableCellContent
{
    protected override string Tag => "th";
}
