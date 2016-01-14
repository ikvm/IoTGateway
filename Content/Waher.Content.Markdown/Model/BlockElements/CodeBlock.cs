﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Waher.Content.Markdown.Model.BlockElements
{
	/// <summary>
	/// Represents a code block in a markdown document.
	/// </summary>
	public class CodeBlock : MarkdownElement
	{
		private string[] rows;
		private string indent;
		private int start, end;

		/// <summary>
		/// Represents a code block in a markdown document.
		/// </summary>
		/// <param name="Document">Markdown document.</param>
		/// <param name="Rows">Rows</param>
		/// <param name="Start">Start index of code.</param>
		/// <param name="End">End index of code.</param>
		/// <param name="Indent">Additional indenting.</param>
		public CodeBlock(MarkdownDocument Document, string[] Rows, int Start, int End, int Indent)
			: base(Document)
		{
			this.rows = Rows;
			this.start = Start;
			this.end = End;
			this.indent = Indent <= 0 ? string.Empty : new string('\t', Indent);
		}

		/// <summary>
		/// Generates HTML for the markdown element.
		/// </summary>
		/// <param name="Output">HTML will be output here.</param>
		public override void GenerateHTML(StringBuilder Output)
		{
			int i;

			Output.Append("<pre><code>");

			for (i = this.start; i <= this.end; i++)
			{
				Output.Append(this.indent);
				Output.AppendLine(MarkdownDocument.HtmlValueEncode(this.rows[i]));
			}

			Output.AppendLine("</code></pre>");
		}

		/// <summary>
		/// Generates plain text for the markdown element.
		/// </summary>
		/// <param name="Output">Plain text will be output here.</param>
		public override void GeneratePlainText(StringBuilder Output)
		{
			int i;

			for (i = this.start; i <= this.end; i++)
			{
				Output.Append(this.indent);
				Output.AppendLine(this.rows[i]);
			}

			Output.AppendLine();
		}

		/// <summary>
		/// Generates XAML for the markdown element.
		/// </summary>
		/// <param name="Output">XAML will be output here.</param>
		/// <param name="Settings">XAML settings.</param>
		/// <param name="TextAlignment">Alignment of text in element.</param>
		public override void GenerateXAML(XmlWriter Output, XamlSettings Settings, TextAlignment TextAlignment)
		{
			bool First = true;

			Output.WriteStartElement("TextBlock");
			Output.WriteAttributeString("xml", "space", null, "preserve");
			Output.WriteAttributeString("TextWrapping", "NoWrap");
			Output.WriteAttributeString("Margin", Settings.ParagraphMargins);
			Output.WriteAttributeString("FontFamily", "Courier New");
			if (TextAlignment != TextAlignment.Left)
				Output.WriteAttributeString("TextAlignment", TextAlignment.ToString());

			foreach (string Row in this.rows)
			{
				if (First)
					First = false;
				else
					Output.WriteElementString("LineBreak", string.Empty);

				Output.WriteValue(Row);
			}

			Output.WriteEndElement();
		}

		/// <summary>
		/// If the element is an inline span element.
		/// </summary>
		internal override bool InlineSpanElement
		{
			get { return false; }
		}
	}
}