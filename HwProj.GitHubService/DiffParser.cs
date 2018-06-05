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

        public IEnumerable<DiffFile> DiffFiles
        {
            get
            {
                if (diffFiles == null)
                    diffFiles = ParseDiffByFiles();
                return diffFiles;
            }
        }

        private string diffText;
        private IEnumerable<DiffFile> diffFiles;

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
            var matches = Parse($@"(?<=diff --git a\/)([\w|\W]*?)(?= b\/) b\/\1\n[\w|\W]*?(\@\@[\w|\W]*?)\n(?=diff --git a\/|$)");
            var length = matches.Count;
            var diffFiles = new DiffFile[length];
            for (int i = 0; i < length; i++)
            {
                var groups = matches[i].Groups;
                diffFiles[i] = new DiffFile
                {
                    Name = groups[1].Value,
                    DiffLines = DiffLineParser.GetDiffLines(groups[2].Value, groups[1].Value)                    
                };
            }
            return diffFiles;
        }

        private MatchCollection Parse(string pattern)
        {
            var regex = new Regex(pattern);
            return regex.Matches(Html);
        } 
    }
}
