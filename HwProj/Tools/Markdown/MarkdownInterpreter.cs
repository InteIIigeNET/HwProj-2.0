using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeyRed.MarkdownSharp;
using HwProj.IoC;
using HwProj.Tools.Markdown;
using Ninject;

namespace HwProj.Tools.Markdown
{
	public static class MarkdownInterpreter
	{
		private static readonly IMarkdownInterpreter Interpreter =
			Kernel.Instance.Get<IMarkdownInterpreter>();

		public static string AsMarkdown(this string text)
		{
			try
			{
				return Interpreter.ConvertToHtml(text);
			}
			catch (Exception ex)
			{
				Log.Log.Instance.Error(ex);
				return text;
			}
		}
	}
}