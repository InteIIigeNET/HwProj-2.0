﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HwProj.Tools;
using HwProj.Tools.Markdown;
using IdentityServer4;
using Ninject;

namespace HwProj.IoC
{
	internal class Kernel
	{
		public static IKernel Instance { get; private set; }= new StandardKernel();

		static Kernel()
		{
			Initialize();
		}

		private static void Initialize()
		{
			Instance.Bind<IAsyncManager>().To<AsyncManager>();
			Instance.Bind<IMarkdownInterpreter>().To<MarkdownWithLangProcessor>();
		}
	}
}