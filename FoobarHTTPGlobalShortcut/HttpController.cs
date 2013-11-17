using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace FoobarHTTPGlobalShortcut
{
    public class HttpController
    {
        public static  void webrequest(string suffix)
        {
            string url = Properties.Settings.Default.Prefix + suffix;
            WebRequest wr = WebRequest.Create(url);
            wr.Proxy = null;
            wr.Timeout = 1000;
            WebResponse rs = null;
            try
            {
                rs = wr.GetResponse();
                rs.Close();
            }
            catch (WebException)
            {
                MessageBox.Show("Could not connect to " + Properties.Settings.Default.Prefix + "\nPlease check the IP and port and ensure that the host is able to accept connections \nCurrent Timeout: " + wr.Timeout);
            }
        }

        public static HtmlAgilityPack.HtmlDocument getPage()
        {
            HttpWebRequest wr = WebRequest.Create(Properties.Settings.Default.Prefix) as HttpWebRequest;
            wr.Proxy = null;

            wr.GetResponse();

            StreamReader responseReader = new StreamReader(wr.GetResponse().GetResponseStream());

            string response = responseReader.ReadToEnd();
            responseReader.Close();

            HtmlAgilityPack.HtmlDocument htmldocument = new HtmlAgilityPack.HtmlDocument();
            htmldocument.LoadHtml(response);
            return htmldocument;
        }

        public static HtmlNodeCollection getNodes(HtmlAgilityPack.HtmlDocument page)
        {
            //HtmlNodeCollection nodes = page.DocumentNode.SelectNodes("//table[@id='pl']//tr[@class='t']");
            return page.DocumentNode.SelectNodes("//table[@id='pl']//tr");
        }
    }
}
