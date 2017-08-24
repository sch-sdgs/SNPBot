using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace SNPBot
{
    class MutAssessor
    {

        public static void enterTextMutAssess(UVSearch main, WebBrowser webBrowserMain)
        {
            //Needs the shorter version of the refseq code (!?)
            string[] splitStr = main._protRefSeq.Split('.');
            string shortPRefSeq = splitStr[0];

            string varMA = shortPRefSeq + " " + main._Variant;

            HtmlElementCollection collection;
            collection = webBrowserMain.Document.GetElementsByTagName("textarea");
            foreach (HtmlElement element in collection)
            {
                string name = element.GetAttribute("name");
                if (name == "vars")
                {
                    element.SetAttribute("Value", varMA);
                }
            }

        }
    }
}
