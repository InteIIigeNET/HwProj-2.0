using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeyRed.MarkdownSharp;
using HwProj.Tools.Markdown;

namespace HwProj.Tools
{
	public static class MarkdownInterpreter
	{
		private static IMarkdownInterpreter _interpreter = 
			new StandartMarkdownInterpreter();

		public static string AsMarkdown(this string text)
		{
			try
			{
				return _interpreter.ConvertToHtml(text);
			}
			catch (Exception ex)
			{
				Log.Log.Instance.Error(ex);
				return text;
			}
		}
	}
}