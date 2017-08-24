using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SNPBot
{
    class MutPred
    {

        public static void enterTextMutPred(UVSearch main, ref WebBrowser webBrowserProvean)
        {
            HtmlElementCollection collection;
            collection = webBrowserProvean.Document.GetElementsByTagName("textarea");

            //change fasta header to put gene name first
            string fasta = ">" + main._Gene + "_" + main._RefSeq + "_" + main._protRefSeq + "\n" + main._FastaNH;
            foreach (HtmlElement element in collection)
            {
                string name = element.GetAttribute("name");
                if (name == "pseq")
                {
                    element.SetAttribute("Value", fasta);
                }
            }

            collection = webBrowserProvean.Document.GetElementsByTagName("input");
            foreach (HtmlElement element in collection)
            {
                string name = element.GetAttribute("name");
                if (name == "mut")
                {
                    element.SetAttribute("Value", main._Variant);
                }
                if (name == "email")
                {
                    element.SetAttribute("Value", main._Email);
                }
            }
        }
    }
}
