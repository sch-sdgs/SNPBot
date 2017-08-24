using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace SNPBot
{
    class Panther
    {
        public static void enterTextPanther(UVSearch main, WebBrowser webBrowserMain)
        {
            //Comma or blank separated mutations

            HtmlElementCollection collection;
            collection = webBrowserMain.Document.GetElementsByTagName("textarea");
            foreach (HtmlElement element in collection)
            {
                string name = element.GetAttribute("name");
                if (name == "sequence")
                {
                    element.SetAttribute("Value", main._FastaNH);
                }
                if (name == "substitutions")
                {
                    element.SetAttribute("Value", main._Variant);
                }
            }
        }

        public static void submitPanther(ref WebBrowser webBrowserMain)
        {
            HtmlElementCollection collection;
            collection = webBrowserMain.Document.GetElementsByTagName("a");
            foreach (HtmlElement element in collection)
            {
                if (element.InnerText != null)
                {
                    if (element.InnerText.Equals("Submit"))
                    {
                        element.InvokeMember("Click");
                    }
                }
            }
            while (webBrowserMain.ReadyState.ToString() != "Complete")
            {
                Application.DoEvents();
            }
        }

    }
}
