using HwProj.Models.GitHub;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HwProj.GitHubService
{
    public class DiffParser
    {
        public string Html { get; set; }

        public string DiffText
        {
            get
            {
                if (diffText == null)
                    diffText = Parse($@"<pre [/w|/W]+?>([/w|/W]*?)</pre>")[0].Value;
                return diffText;
            }
        }

        public IEnumerable<string> DiffFiles
        {
            get
            {
                if (diffFiles == null)
                    diffFiles = ParseDiffByFiles();
                return diffFiles;
            }
        }

        private string diffText;
        private IEnumerable<string> diffFiles;

        public DiffParser(string diffUrl)
        {
            var request = (HttpWebRequest)WebRequest.Create(diffUrl);
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    Html = readStream.ReadToEnd();

                    readStream.Close();
                }
            }

        }

        private IEnumerable<DiffFile> ParseDiffByFiles()
        {
            var matches = Parse($@"(?<=diff --git a\/)([\w|\W]*?)(?= b\/) b\/\1\n[\w|\W]*?(\@\@[\w|\W]*?)(?=diff --git a\/)|(?<=diff --git a\/)([\w|\W]*?)(?= b\/) b\/\3\n[\w|\W]*?(\@\@[\w|\W]*)(?=$)");
            for (int i = 0; i < matches.Count; i++)
            {
                var m = matches[i];
                var fileName = m.Groups[1].Value;
                var diffText = m.Groups[2].Value;

            }
        }

        private MatchCollection Parse(string pattern)
        {
            var regex = new Regex(pattern);
            return regex.Matches(Html);
        } 
    }
}
