using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SNPBot
{
    class SNPs_GO
    {

        public static bool enterTextSNPsandGO(UVSearch main, WebBrowser webBrowserSandGo)
        {
            //Find GO Terms
            string terms = GoTerms.GetGOs(main._RefSeq);

            if (!Regex.IsMatch(terms, @"^GO:\d+(?:\sGO:\d+){0,}") && terms != "")
            {
                Go_Manual frm = new Go_Manual();
                DialogResult dlg = frm.ShowDialog();

                if (dlg == System.Windows.Forms.DialogResult.OK)
                {
                    terms = GoTerms.GetGOs(frm.Terms);
                }
            }

            if (terms == "")
            {
                terms = GOTerms();
                if (terms == "" || terms == null)
                {
                    return false;
                }

            }

            //save GO terms
            Utilities.saveTextFile(main,"GOTerms.txt", terms);

            //Comma or blank separated mutations

            HtmlElementCollection collection;
            collection = webBrowserSandGo.Document.GetElementsByTagName("textarea");
            foreach (HtmlElement element in collection)
            {
                string name = element.GetAttribute("name");
                if (name == "proteina")
                {
                    element.SetAttribute("Value", main._Fasta);
                }
                else if (name == "posizione")
                {
                    element.SetAttribute("Value", main._Variant);
                }
                else if (name == "gos")
                {
                    element.SetAttribute("Value", terms);
                }
            }
            return true;

        }

        private static string GOTerms()
        {
            Go_Manual frm = new Go_Manual();
            frm.ShowDialog();
            string terms = frm.Terms;

            if (frm.Terms == null || frm.Terms == "")
            {
                DialogResult dlg = MessageBox.Show("SNPs&GO cannot be run without the GO terms. Would you like to enter the GO terms?", "No Terms Entered", MessageBoxButtons.YesNo);
                if (dlg == System.Windows.Forms.DialogResult.Yes)
                {
                    GOTerms();
                    return terms;
                }
                else
                {
                    return terms;
                }
            }
            else
            {
                return terms;
            }

            
        }

    }
}
