using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SNPBot
{
    class PP2
    {

        public static void enterTextPolyPhen2(UVSearch main, ref WebBrowser webBrowserPP2)
        {
            HtmlElementCollection collection;
            collection = webBrowserPP2.Document.GetElementsByTagName("textarea");
            foreach (HtmlElement element in collection)
            {
                string name = element.GetAttribute("name");
                if (name == "seqres")
                {
                    element.SetAttribute("Value", main._Fasta);
                }
            }

            collection = webBrowserPP2.Document.GetElementsByTagName("input");
            foreach (HtmlElement element in collection)
            {
                string name = element.GetAttribute("name");
                if (name == "seqpos")
                {
                    element.SetAttribute("Value", main._VarArray[1]);
                }
                if (name == "description")
                {
                    element.SetAttribute("Value", main._Gene + ' ' + main._Variant);
                }
            }

            HtmlElement AAelement;
            AAelement = webBrowserPP2.Document.GetElementById("v1" + main._VarArray[0]);
            AAelement.InvokeMember("click");
            AAelement = webBrowserPP2.Document.GetElementById("v2" + main._VarArray[2]);
            AAelement.InvokeMember("click");

            PP2.submitPP2(ref webBrowserPP2);
        }

        private static void submitPP2(ref WebBrowser webBrowserPP2)
        {
            HtmlElementCollection collection;
            collection = webBrowserPP2.Document.GetElementsByTagName("input");
            foreach (HtmlElement element in collection)
            {
                string name = element.GetAttribute("name");
                string type = element.GetAttribute("type");
                if (name == "Submit" && type == "submit")
                {
                    element.InvokeMember("click");
                }
            }
        }

    }
}
