using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SNPBot
{
    class Provean
    {
        public static void enterTextPROVEAN(UVSearch main, ref WebBrowser webBrowserProvean)
        {
            // Format <uniprot code> <position> <wild type AA> <variant AA>
            string variantEntry = main._protRefSeq + " " + main._VarArray[1] + " " + main._VarArray[0] + " " + main._VarArray[2];

            HtmlElement element;
            element = webBrowserProvean.Document.GetElementById("variants");
            element.SetAttribute("Value", variantEntry);
            submitProvean(ref webBrowserProvean);

        }

        private static void submitProvean(ref WebBrowser webBrowserProvean)
        {
            HtmlElementCollection collection;
            collection = webBrowserProvean.Document.GetElementsByTagName("input");
            foreach (HtmlElement element in collection)
            {
                string name = element.GetAttribute("name");
                string type = element.GetAttribute("type");
                if (name == "submit" || type == "submit")
                {
                    element.InvokeMember("click");
                }
            }
        }

    }
}
