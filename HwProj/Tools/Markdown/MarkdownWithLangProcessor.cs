using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Markdig;
using Markdig.Syntax;
using Markdig.SyntaxHighlighting;

namespace HwProj.Tools.Markdown
{
	public class MarkdownWithLangProcessor : IMarkdownInterpreter
	{
		readonly MarkdownPipeline _pipeline = new MarkdownPipelineBuilder()
			.UseAdvancedExtensions()
			.UseSyntaxHighlighting()
			.Build();
		public string ConvertToHtml(string text)
		{
			return Markdig.Markdown.ToHtml(text, _pipeline);
		}
	}
}