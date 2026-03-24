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

using Jitendex.Yomitan.Options.StyleOptions;
using static Jitendex.Yomitan.Options.StyleOptions.TextDecorationLineOption;

namespace Jitendex.Yomitan.Definitions.ContentAttributes;

public sealed class ContentStyle
{
    public FontStyleOption? FontStyle { get; set; }
    public FontWeightOption? FontWeight { get; set; }
    public string? FontSize { get; set; }
    public string? Color { get; set; }
    public string? Background { get; set; }
    public string? BackgroundColor { get; set; }
    private HashSet<TextDecorationLineOption> TextDecorationLines { get; init; } = [];
    public TextDecorationStyleOption? TextDecorationStyle { get; set; }
    public string? TextDecorationColor { get; set; }
    public string? BorderColor { get; set; }
    public string? BorderStyle { get; set; }
    public string? BorderRadius { get; set; }
    public string? BorderWidth { get; set; }
    public string? ClipPath { get; set; }
    public VerticalAlignOption? VerticalAlign { get; set; }
    public TextAlignOption? TextAlign { get; set; }
    public string? TextEmphasis { get; set; }
    public string? TextShadow { get; set; }
    public string? Margin { get; set; }
    public string? MarginTop { get; set; }
    public string? MarginLeft { get; set; }
    public string? MarginRight { get; set; }
    public string? MarginBottom { get; set; }
    public string? Padding { get; set; }
    public string? PaddingTop { get; set; }
    public string? PaddingLeft { get; set; }
    public string? PaddingRight { get; set; }
    public string? PaddingBottom { get; set; }
    public WordBreakOption? WordBreak { get; set; }
    public string? WhiteSpace { get; set; }
    public string? Cursor { get; set; }
    public string? ListStyleType { get; set; }

    public bool AddTextDecorationLine(TextDecorationLineOption option)
    {
        if (option is not None && TextDecorationLines.Contains(None))
        {
            throw new InvalidOperationException($"Cannot add option `{option}` after `{None}` option has been added");
        }
        if (option is None && TextDecorationLines.Count > 0 && !TextDecorationLines.Contains(option))
        {
            throw new InvalidOperationException($"Cannot add option `{None}` after other options have been added");
        }
        return TextDecorationLines.Add(option);
    }

    internal JsonObject ToJsonObject()
    {
        var obj = new JsonObject();

        if (FontStyle.HasValue)
        {
            obj["fontStyle"] = FontStyle.Value.ToJsonNode();
        }
        if (FontWeight.HasValue)
        {
            obj["fontWeight"] = FontWeight.Value.ToJsonNode();
        }
        if (FontSize is not null)
        {
            obj["fontSize"] = FontSize;
        }
        if (Color is not null)
        {
            obj["color"] = Color;
        }
        if (Background is not null)
        {
            obj["background"] = Background;
        }
        if (BackgroundColor is not null)
        {
            obj["backgroundColor"] = BackgroundColor;
        }
        if (TextDecorationStyle.HasValue)
        {
            obj["textDecorationStyle"] = TextDecorationStyle.Value.ToJsonNode();
        }
        if (TextDecorationLines.Count == 1)
        {
            obj["textDecorationLine"] = TextDecorationLines.Single().ToJsonNode();
        }
        else if (TextDecorationLines.Count > 1)
        {
            var nodes = TextDecorationLines
                .Select(static line => line.ToJsonNode())
                .ToArray();
            obj["textDecorationLine"] = new JsonArray(nodes);
        }
        if (TextDecorationColor is not null)
        {
            obj["textDecorationColor"] = TextDecorationColor;
        }
        if (BorderColor is not null)
        {
            obj["borderColor"] = BorderColor;
        }
        if (BorderStyle is not null)
        {
            obj["borderStyle"] = BorderStyle;
        }
        if (BorderRadius is not null)
        {
            obj["borderRadius"] = BorderRadius;
        }
        if (BorderWidth is not null)
        {
            obj["borderWidth"] = BorderWidth;
        }
        if (ClipPath is not null)
        {
            obj["clipPath"] = ClipPath;
        }
        if (VerticalAlign.HasValue)
        {
            obj["verticalAlign"] = VerticalAlign.Value.ToJsonNode();
        }
        if (TextAlign.HasValue)
        {
            obj["textAlign"] = TextAlign.Value.ToJsonNode();
        }
        if (TextEmphasis is not null)
        {
            obj["textEmphasis"] = TextEmphasis;
        }
        if (TextShadow is not null)
        {
            obj["textShadow"] = TextShadow;
        }
        if (Margin is not null)
        {
            obj["margin"] = Margin;
        }
        if (MarginTop is not null)
        {
            obj["marginTop"] = MarginTop;
        }
        if (MarginLeft is not null)
        {
            obj["marginLeft"] = MarginLeft;
        }
        if (MarginRight is not null)
        {
            obj["marginRight"] = MarginRight;
        }
        if (MarginBottom is not null)
        {
            obj["marginBottom"] = MarginBottom;
        }
        if (Padding is not null)
        {
            obj["padding"] = Padding;
        }
        if (PaddingTop is not null)
        {
            obj["paddingTop"] = PaddingTop;
        }
        if (PaddingLeft is not null)
        {
            obj["paddingLeft"] = PaddingLeft;
        }
        if (PaddingRight is not null)
        {
            obj["paddingRight"] = PaddingRight;
        }
        if (PaddingBottom is not null)
        {
            obj["paddingBottom"] = PaddingBottom;
        }
        if (WordBreak.HasValue)
        {
            obj["wordBreak"] = WordBreak.Value.ToJsonNode();
        }
        if (WhiteSpace is not null)
        {
            obj["whiteSpace"] = WhiteSpace;
        }
        if (Cursor is not null)
        {
            obj["cursor"] = Cursor;
        }
        if (ListStyleType is not null)
        {
            obj["listStyleType"] = ListStyleType;
        }

        return obj;
    }
}
