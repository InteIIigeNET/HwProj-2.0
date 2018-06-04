using HwProj.GitHubService.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HwProj.GitHubService
{
    public static class LinguistManager
    {
        private static string XMLFileString = Resources.Linguist;
        private static XDocument LinguistXMl = XDocument.Parse(XMLFileString);

        public static string GetAlias(string extension)
        {
            return (from xLanguage in LinguistXMl.Element("languages").Elements("language")
                    where   xLanguage.Element("extensions").Elements("extension")
                            .Any(xExtension => xExtension.Value == extension)
                    select xLanguage.Element("alias").Value).FirstOrDefault();
        }
    }
}
