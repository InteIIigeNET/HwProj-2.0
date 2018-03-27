using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeyRed.MarkdownSharp;

namespace HwProj.Tools
{
	public static class MarkdownInterpreter
	{
		static Markdown engine = new Markdown(new MarkdownOptions
		{
			AutoHyperlink = true,
			AutoNewLines = true,
			LinkEmails = true,
			QuoteSingleLine = true,
			StrictBoldItalic = true
		});

		public static string AsMarkdown(this string text)
		{
			return engine.Transform(text);
		}
	}
}