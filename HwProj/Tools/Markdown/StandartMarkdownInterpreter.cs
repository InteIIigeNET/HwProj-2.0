using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeyRed.MarkdownSharp;

namespace HwProj.Tools.Markdown
{
	internal class StandartMarkdownInterpreter : IMarkdownInterpreter
	{
		static readonly HeyRed.MarkdownSharp.Markdown Engine = 
			new HeyRed.MarkdownSharp.Markdown(new MarkdownOptions
			{
				AutoHyperlink = true,
				AutoNewLines = true,
				LinkEmails = true,
				QuoteSingleLine = true
			});
		public string ConvertToHtml(string text)
		{
			return Engine.Transform(text);
		}
	}
}