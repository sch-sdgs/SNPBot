using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.Text.RegularExpressions;

namespace SNPBot
{
    class PMut
    {

        public static void enterTextPMut(UVSearch main, ref WebBrowser webBrowserPMut)
        {
            //Comma or blank separated mutations
            HtmlElement inputFasta = webBrowserPMut.Document.GetElementById("sequence-input");
            HtmlElementCollection collection = inputFasta.GetElementsByTagName("textarea");
            HtmlElement textbox = collection[0];
            textbox.SetAttribute("Value", main._Fasta);

            HtmlElement searchButton = webBrowserPMut.Document.GetElementById("search-sequences");
            searchButton.InvokeMember("click");

            HtmlElement chooseButton = webBrowserPMut.Document.GetElementById("choose-sequence");
            chooseButton.InvokeMember("click");

            HtmlElement mutationList = webBrowserPMut.Document.GetElementById("mutations-list");
            string mutation = main._VarArray[0] + main._VarArray[1] + main._VarArray[2];
            mutationList.SetAttribute("Value",mutation);

            webBrowserPMut.Document.InvokeScript("update_variants");

            HtmlElement analysisButton = webBrowserPMut.Document.GetElementById("start-analysis");
            analysisButton.InvokeMember("click");      
          }      
        }
    }


// public static void savePMut(UVSearch main, HtmlDocument document)
//        {
//            HtmlElementCollection collection = document.GetElementsByTagName("a");

//            string table = "";
//            foreach (HtmlElement element in collection)
//            {
//                string link = element.GetAttribute("href");

//                if (link.StartsWith("http://mmb2.pcb.ub.es/PMut/showText"))
//                {

//                    using (WebClient wc = new WebClient())
//                    {
//                        table = wc.DownloadString(link);
//                    }

//                    break;
//                }
//            }

//            string[] lines = table.Split(new char[] { '\n' });
//            string resultLine = "Variant\tNN Output\tReliability\tPrediction\n";
//            foreach (string line in lines)
//            {
//                string pattern = @"^\S+\s+([A-Z]\s->\s[A-Z])\s+#\s+(\d+)\s+([10]\.\d+)\s+([0-9])\s+(NEUTRAL|PATHOLOGICAL)$";
//                Match match = Regex.Match(line, pattern);

//                if (match.Groups.Count == 6)
//                {
//                    string v = main._VarArray[0] + " -> " + main._VarArray[2];
//                    string position = main._VarArray[1];
//                    if (match.Groups[1].Value.ToString() == v && match.Groups[2].Value.ToString() == position)
//                    {
//                        resultLine = resultLine + main._VarArray[0] + main._VarArray[1] + main._VarArray[2] + "\t" + match.Groups[3].Value.ToString() + "\t" + match.Groups[4].Value.ToString() + "\t" + match.Groups[5].Value.ToString();
//                    }
//                }
//            }

//            Utilities.saveTextFile(main, "PMutResult.txt", resultLine);
//        }
//    }
//}
