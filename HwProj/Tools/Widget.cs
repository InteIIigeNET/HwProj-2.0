using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HwProj.Tools
{
	public abstract class Widget
	{
		protected string Text;
		public static implicit operator string(Widget widget)
		{
			return widget.ToString();
		}
		public override string ToString()
		{
			return Text;
		}
	}

	public class Button : Widget
	{
		public Button(string text)
		{
			string[] buttons = text.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
			StringBuilder sB = new StringBuilder();

			foreach (var button in buttons)
			{
				sB.Append($"<button>{button.Trim()}</button> ");
			}
			Text = sB.ToString();
		}
	}

}