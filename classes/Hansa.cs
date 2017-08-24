using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SNPBot
{
    class Hansa
    {

        public static void enterTextHANSA(UVSearch main, ref WebBrowser webBrowserHansa)
        {
            string variantEntry = main._VarArray[0] + " " + main._VarArray[1] + " " + main._VarArray[2];

            HtmlElementCollection collection;
            collection = webBrowserHansa.Document.GetElementsByTagName("textarea");
            foreach (HtmlElement element in collection)
            {
                string name = element.GetAttribute("name");
                if (name == "fsequence")
                {
                    element.SetAttribute("Value", main._Fasta);
                }
                if (name == "mutlist")
                {
                    element.SetAttribute("Value", variantEntry);
                }
            }

            Utilities.submit(ref webBrowserHansa);
        }

    }
}
