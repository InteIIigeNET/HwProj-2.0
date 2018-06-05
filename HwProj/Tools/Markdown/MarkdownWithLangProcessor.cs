using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using ColorCode;
using ColorCode.Common;
using Markdig;
using Markdig.Syntax;
using Markdig.SyntaxHighlighting;

namespace HwProj.Tools.Markdown
{
	public class MarkdownWithLangProcessor : IMarkdownInterpreter
	{
        private readonly MarkdownPipeline _pipeline;

        public MarkdownWithLangProcessor()
        {
            var styleSheet = StyleSheets.Default;
            styleSheet.Styles[ScopeName.PlainText].Background = null;
            _pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .UseSyntaxHighlighting(styleSheet)
                .Build();
        }

		public string ConvertToHtml(string text)
		{
            //TODO: убрать блоки <pre>
			return Markdig.Markdown.ToHtml(text, _pipeline);
		}
	}
}