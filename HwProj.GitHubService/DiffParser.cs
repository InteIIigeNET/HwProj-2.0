using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
                    diffText = Parse($@"<pre [/w|/W]+?>([/w|/W]*?)</pre>");
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

        private IEnumerable<string> ParseDiffByFiles()
        {
            throw new NotImplementedException();
        }

        private string Parse(string pattern)
        {
            throw new NotImplementedException();
        }

        
    }
}
