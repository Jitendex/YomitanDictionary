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
using Jitendex.Yomitan.Options.ImageOptions;
using Jitendex.Yomitan.Options.StyleOptions;

namespace Jitendex.Yomitan.Definitions.Contents;

public sealed class ImageContent : ObjectContent
{
    protected override string Tag => "img";
    public ContentData? Data { get; set; }

    /// <summary>
    /// Path to the image file in the archive.
    /// </summary>
    public required string Path { get; set; }

    /// <summary>
    /// Preferred width of the image.
    /// </summary>
    public double? Width { get; set; }

    /// <summary>
    /// Preferred height of the image.
    /// </summary>
    public double? Height { get; set; }

    /// <summary>
    /// Hover text for the image.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Alt text for the image.
    /// </summary>
    public string? AltText { get; set; }

    /// <summary>
    /// Description of the image.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Whether or not the image should appear pixelated at sizes larger than the image's native resolution.
    /// </summary>
    public bool? IsPixelated { get; set; }

    /// <summary>
    /// Controls how the image is rendered. The value of this field supersedes the pixelated field.
    /// </summary>
    public RenderingOption? RenderingOption { get; set; }

    /// <summary>
    /// Controls the appearance of the image. The "monochrome" value will
    /// mask the opaque parts of the image using the current text color.
    /// </summary>
    public AppearanceOption? AppearanceOption { get; set; }

    /// <summary>
    /// Whether or not a background color is displayed behind the image.
    /// </summary>
    public bool? Background { get; set; }

    /// <summary>
    /// Whether or not the image is collapsed by default.
    /// </summary>
    public bool? Collapsed { get; set; }

    /// <summary>
    /// Whether or not the image can be collapsed.
    /// </summary>
    public bool? Collapsible { get; set; }

    /// <summary>
    /// The vertical alignment of the image.
    /// </summary>
    public VerticalAlignOption? VerticalAlignOption { get; set; }

    /// <summary>
    /// Shorthand for border width, style, and color.
    /// </summary>
    public string? Border { get; set; }

    /// <summary>
    /// Roundness of the corners of the image's outer border edge.
    /// </summary>
    public string? BorderRadius { get; set; }

    /// <summary>
    /// The units for the width and height.
    /// </summary>
    public SizeUnitOption? SizeUnitOption { get; set; }

    internal override JsonNode ToJsonNode()
    {
        var obj = new JsonObject
        {
            ["tag"] = Tag,
            ["path"] = Path,
        };
        if (Data is not null)
        {
            obj["data"] = Data.ToJsonObject();
        }
        if (Width.HasValue)
        {
            obj["width"] = Width.Value;
        }
        if (Height.HasValue)
        {
            obj["height"] = Height.Value;
        }
        if (Title is not null)
        {
            obj["title"] = Title;
        }
        if (AltText is not null)
        {
            obj["alt"] = AltText;
        }
        if (Description is not null)
        {
            obj["description"] = Description;
        }
        if (IsPixelated.HasValue)
        {
            obj["pixelated"] = IsPixelated.Value;
        }
        if (RenderingOption.HasValue)
        {
            obj["imageRendering"] = RenderingOption.Value.ToJsonNode();
        }
        if (AppearanceOption.HasValue)
        {
            obj["appearance"] = AppearanceOption.Value.ToJsonNode();
        }
        if (Background.HasValue)
        {
            obj["background"] = Background.Value;
        }
        if (Collapsed.HasValue)
        {
            obj["collapsed"] = Collapsed.Value;
        }
        if (Collapsible.HasValue)
        {
            obj["collapsible"] = Collapsible.Value;
        }
        if (VerticalAlignOption.HasValue)
        {
            obj["verticalAlign"] = VerticalAlignOption.Value.ToJsonNode();
        }
        if (Border is not null)
        {
            obj["border"] = Border;
        }
        if (BorderRadius is not null)
        {
            obj["borderRadius"] = BorderRadius;
        }
        if (SizeUnitOption.HasValue)
        {
            obj["sizeUnits"] = SizeUnitOption.Value.ToJsonNode();
        }
        return obj;
    }
}
