using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeyRed.MarkdownSharp;

namespace HwProj.Tools
{
	public static class MarkdownInterpreter
	{
		static readonly Markdown Engine = new Markdown(new MarkdownOptions
		{
			AutoHyperlink = true,
			AutoNewLines = true,
			LinkEmails = true,
			QuoteSingleLine = true
			//StrictBoldItalic = true	
		});

		public static string AsMarkdown(this string text)
		{
			try
			{
				return Engine.Transform(text); //.Replace("<code>", "<code style = \"color: brown; background-color: ivory;\">");
			}
			catch (Exception)
			{
				//TODO: logging
				return text;
			}
		}
	}
}