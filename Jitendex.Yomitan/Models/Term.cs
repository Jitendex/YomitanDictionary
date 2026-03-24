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

using Jitendex.Yomitan.Definitions;

namespace Jitendex.Yomitan;

public sealed class Term
{
    /// <summary>
    /// The text for the term.
    /// </summary>
    public required string Text { get; set; }

    /// <summary>
    /// Reading of the term, or an empty string if the reading is the same as the term.
    /// </summary>
    public required string Reading { get; set; }

    /// <summary>
    /// String of space-separated tags for the definition. An empty string is treated as no tags.
    /// </summary>
    private HashSet<string> DefinitionTags { get; init; } = [];

    /// <summary>
    /// String of space-separated rule identifiers for the definition which is used to validate deinflection.
    /// An empty string should be used for words which aren't inflected.
    /// </summary>
    private HashSet<string> Rules { get; init; } = [];

    /// <summary>
    /// Score used to determine popularity. Negative values are more rare and positive values are more frequent.
    /// This score is also used to sort search results.
    /// </summary>
    public required int Score { get; set; }

    /// <summary>
    /// Definitions for the term.
    /// </summary>
    public List<Definition> Definitions { get; init; } = [];

    /// <summary>
    /// Sequence number for the term. Terms with the same sequence number can be
    /// shown together when the "resultOutputMode" option is set to "merge."
    /// </summary>
    public required int Sequence { get; set; }

    /// <summary>
    /// String of space-separated tags for the term. An empty string is treated as no tags.
    /// </summary>
    private HashSet<string> TermTags { get; init; } = [];

    private const char Separator = ' ';
    private const char ReplacementSeparator = '�';

    public bool AddDefinitionTag(string tag)
        => DefinitionTags.Add(tag.Replace(Separator, ReplacementSeparator));

    public bool AddTermTag(string tag)
        => TermTags.Add(tag.Replace(Separator, ReplacementSeparator));

    public bool AddDeinflectionRule(string rule)
    {
        if (rule.Contains(Separator))
        {
            throw new InvalidOperationException($"Deinflection rules must not contain the character `{Separator}`");
        }
        return Rules.Add(rule);
    }

    internal JsonArray ToJsonArray() =>
    [
        Text,
        string.Equals(Text, Reading, StringComparison.Ordinal) ? string.Empty : Reading,
        string.Join(Separator, DefinitionTags),
        string.Join(Separator, Rules),
        Score,
        new JsonArray(Definitions.Select(static d => d.ToJsonNode()).ToArray()),
        Sequence,
        string.Join(Separator, TermTags),
    ];
}
