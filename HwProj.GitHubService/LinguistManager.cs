using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HwProj.GitHubService
{
    internal static class LinguistManager
    {
        private static string XMLFilePath = Path.Combine(Environment.CurrentDirectory, "Linguist.xml");
        private static XDocument LinguistXMl = XDocument.Load(XMLFilePath);

        public static string GetAlias(string extension)
        {
            return (from xLanguage in LinguistXMl.Elements("languages")
                    where xLanguage.Elements("extensions").Any(xExtension => xExtension.Value == extension)
                    select xLanguage.Element("alias").Value).FirstOrDefault();
        }
    }
}
