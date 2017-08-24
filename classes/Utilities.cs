using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Net;
using System.Windows.Forms;
using System.Threading;
using System.Xml;

namespace SNPBot
{
    class Utilities
    {

        public static void savePage(UVSearch main, string fileName, string documentText, Encoding wbrowserEncoding)
        {
            string fpath = "";
            if (main._Gene != null)
            {
                fpath = main._Path + "\\" + main._Gene + "_" + main._Variant + "_" + fileName;
            }
            File.WriteAllText(fpath,
            documentText,
            wbrowserEncoding);
        }

        public static void saveTextFile(UVSearch main, string filename, string URL)
        {
            string fpath = main._Path + "\\" + main._Gene + "_" + main._Variant + "_" + filename;

            TextWriter tw = new StreamWriter(fpath, false);
            tw.Write(URL);
            tw.Close();
        }

        public static string[] splitVariant(string variant)
        {
            // Some tools need the variant in a different order, so split it into a character array.
            char[] varChars = variant.ToCharArray();
            char firstChar = varChars.First();
            char lastChar = varChars.Last();
            char[] posChars = new char[(varChars.Length - 2)];
            Array.Copy(varChars, 1, posChars, 0, posChars.Length);

            string[] varArray = new string[] { firstChar.ToString(), new string(posChars), lastChar.ToString() };
            return varArray;
        }

        public static void submit(ref WebBrowser wbrowser)
        {
            HtmlElementCollection collection;
            collection = wbrowser.Document.GetElementsByTagName("input");
            foreach (HtmlElement element in collection)
            {
                string name = element.GetAttribute("name");
                string type = element.GetAttribute("type");
                if (name == "submit" || type == "submit")
                {
                    element.InvokeMember("click");
                }
            }
            while (wbrowser.ReadyState.ToString() != "Complete")
            {
                Thread.Sleep(200);
                Application.DoEvents();
            }
        }




    }
}
