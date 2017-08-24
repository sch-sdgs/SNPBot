using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;

namespace SNPBot
{
    class NCBI
    {
        public static XmlDocument downloadXml(string url)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                // Stream the XML from the URL, and load it into an XML document. 
                string xml = "";
                using (var webClient = new WebClient())
                {

                    IWebProxy defaultProxy = WebRequest.DefaultWebProxy;
                    if (defaultProxy != null)
                    {
                        defaultProxy.Credentials = CredentialCache.DefaultCredentials;
                        webClient.Proxy = defaultProxy;
                    }

                    xml = webClient.DownloadString(url);
                }


                doc.LoadXml(xml);
            }
            catch
            {

            }

            return doc;
        }

        public static string getGeneId(string refSeq)
        {
            string urlBase = "http://eutils.ncbi.nlm.nih.gov/entrez/eutils/";
            string url = urlBase + "esearch.fcgi?db=nucleotide&term=" + refSeq + "[accn]&usehistory=y";

            XmlDocument doc = downloadXml(url);
            if (doc == null)
            {
                return "";
            }
            else
            {
                XmlNodeList node = doc.GetElementsByTagName("Count");
                if (node.Count == 0)
                {
                    return "";
                }
                string searchCount = node[0].InnerText;
                if (searchCount == "0")
                {
                    return "";
                }

                XmlNodeList nodeId = doc.GetElementsByTagName("Id");
                string geneId = nodeId[0].InnerText;
                return geneId;
            }
        }

        public static string loadFasta(string geneId)
        {
            string fastaUrl = "http://eutils.ncbi.nlm.nih.gov/entrez/eutils/efetch.fcgi?db=nuccore&id=" + geneId + "&rettype=fasta_cds_aa";
            string fastaPage = "";
            using (var webClient = new WebClient())
            {
                try
                {
                    fastaPage = webClient.DownloadString(fastaUrl);
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
            return fastaPage;
        }

        public static string getIDs(string gene)
        {
            // Use the gene symbol to find the gene IDs.
            string url = "http://eutils.ncbi.nlm.nih.gov/entrez/eutils/esearch.fcgi?db=nuccore&term=human[orgn]+and+\"" + gene + "\"[sym]";
            XmlDocument doc = NCBI.downloadXml(url);

            if (doc == null)
            {
                return "0";
            }
            else
            {
                // If there are none then return that the gene symbol is incorrect.
                XmlNodeList node = doc.GetElementsByTagName("Count");
                string searchCount = node[0].InnerText;

                return searchCount;
            }
        }
    }
}
