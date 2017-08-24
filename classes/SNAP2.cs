using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SNPBot
{
    class SNAP2
    {

        public static void enterTextSNAP(UVSearch main, ref WebBrowser webBrowserMain)
        {
            HtmlElement element;
            element = webBrowserMain.Document.GetElementById("sequence");
            element.SetAttribute("Value", main._Fasta);
            element = webBrowserMain.Document.GetElementById("email");
            element.SetAttribute("Value", main._Email);

            submitSNAP(ref webBrowserMain);
        }

        private static void submitSNAP(ref WebBrowser webBrowserMain)
        {
            HtmlElement element;
            element = webBrowserMain.Document.GetElementById("runBttn");
            element.InvokeMember("click");
            Thread.Sleep(3000);
        }
    }
}
