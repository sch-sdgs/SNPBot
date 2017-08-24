using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Novacode;
using System.Net;
using System.Web;
using System.Xml;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace SNPBot
{
    class Summarise
    {

        public static bool GenerateSumamry(string filepath, string gene, string variant)
        {
            try
            {
                string fullPath = filepath + "\\" + gene + "_" + variant;

                string summaryFile = fullPath + "_Summary.docx";
                DocX doc;
                if (!File.Exists(summaryFile))
                {
                    doc = DocX.Load(@"N:\GEN\Shared\Sheffield Diagnostic Genetics Service\Bioinformatics\SNPBot\Source files\UVFormSeearch\UVFormSeearch\SNPBotSummaryTemplate.docx");
                    doc.SaveAs(summaryFile);
                }

                doc = DocX.Load(summaryFile);
                Table results = doc.Tables[0];

                string currentValue = results.Rows[2].Cells[1].Paragraphs[0].Text;

                if (File.Exists(fullPath + "_SNPsandGO.html") && (currentValue == "Check HTML" || currentValue == "Results not available"))
                {
                    string[] output = SNPsGO(fullPath + "_SNPsandGO.html");


                    if (output[0] != null && output[0] != "")
                    {
                        results.Rows[2].Cells[1].ReplaceText(currentValue, output[0]);
                        results.Rows[2].Cells[2].ReplaceText(currentValue, output[1]);
                        results.Rows[2].Cells[3].ReplaceText(currentValue, output[2]);

                        if (output[3] == null || output[3] == "")
                        {
                            results.Rows[3].Cells[1].ReplaceText(currentValue, "N/A");
                            results.Rows[3].Cells[2].ReplaceText(currentValue, "N/A");
                            results.Rows[3].Cells[3].ReplaceText(currentValue, "N/A");
                        }
                        else
                        {
                            results.Rows[3].Cells[1].ReplaceText(currentValue, output[3]);
                            results.Rows[3].Cells[2].ReplaceText(currentValue, output[4]);
                            results.Rows[3].Cells[3].ReplaceText(currentValue, output[5]);
                        }

                        results.Rows[4].Cells[1].ReplaceText(currentValue, output[6]);
                        results.Rows[4].Cells[2].ReplaceText(currentValue, output[7]);
                        results.Rows[4].Cells[3].ReplaceText(currentValue, output[8]);
                    }
                }
                else if (currentValue == "Check HTML")
                {
                    for (int i = 2; i < 5; i++)
                    {
                        results.Rows[i].Cells[1].ReplaceText(currentValue, "Results not available");
                        results.Rows[i].Cells[2].ReplaceText(currentValue, "Results not available");
                        results.Rows[i].Cells[3].ReplaceText(currentValue, "Results not available");
                    }
                }

                currentValue = results.Rows[6].Cells[1].Paragraphs[0].Text;

                if (File.Exists(fullPath + "_PMut.html") && (currentValue == "Check HTML" || currentValue == "Results not available"))
                {
                    string[] output = PMut(fullPath + "_PMut.html");

                    if (output != null )
                    {
                    results.Rows[6].Cells[1].ReplaceText(currentValue, output[0]);
                    results.Rows[7].Cells[1].ReplaceText(currentValue, output[1]);
                    results.Rows[8].Cells[1].ReplaceText(currentValue, output[2]);
                    }
                 }

                
                else if (currentValue == "Check HTML")
                    {
   
                    results.Rows[6].Cells[1].ReplaceText(currentValue, "Results not available");
                    results.Rows[7].Cells[1].ReplaceText(currentValue, "Results not available");
                    results.Rows[8].Cells[1].ReplaceText(currentValue, "Results not available");
                        
                    }

                currentValue = results.Rows[10].Cells[1].Paragraphs[0].Text;

                if (File.Exists(fullPath + "_HANSA.html") && (currentValue == "Check HTML" || currentValue == "Results not available"))
                {

                    string[] output = HANSA(fullPath + "_HANSA.html", false);

                    if (output[0] != null)
                    {
                        results.Rows[10].Cells[1].ReplaceText(currentValue, output[0]);
                    }
                }
                else if (currentValue == "Check HTML")
                {
                    results.Rows[10].Cells[1].ReplaceText(currentValue, "Results not available");
                }
            

                if (File.Exists(fullPath + "_HANSAdetails.html") && (currentValue == "Check HTML" || currentValue == "Results not available"))
                {
                    string[] output = HANSA(fullPath + "_HANSAdetails.html", true);

                    for (int i = 0; i < 10; i++)
                    {
                        if (output[i] != null)
                        {
                            int row = i + 11;
                            results.Rows[row].Cells[1].ReplaceText(currentValue, output[i]);
                        }
                    }
                }
                else if (currentValue == "Check HTML")
                {
                    for (int i = 11; i < 21; i++)
                    {
                        results.Rows[i].Cells[1].ReplaceText(currentValue, "Results not available");
                    }
                }

                currentValue = results.Rows[22].Cells[1].Paragraphs[0].Text;

                if (File.Exists(fullPath + "_Panther.html") && (currentValue == "Check HTML" || currentValue == "Results not available"))
                {
                    string[] output = Panther(fullPath + "_Panther.html");
                    if (output[0] != null)
                    {
                        results.Rows[22].Cells[1].ReplaceText(currentValue, output[1]);
                        results.Rows[23].Cells[1].ReplaceText(currentValue, output[0]);
                        results.Rows[24].Cells[1].ReplaceText(currentValue, output[2]);
                        results.Rows[24].Cells[3].ReplaceText(currentValue, output[3]);
                        results.Rows[25].Cells[1].ReplaceText(currentValue, output[4]);
                        results.Rows[25].Cells[3].ReplaceText(currentValue, output[5]);
                    }
                }
                else if (currentValue == "Check HTML")
                {
                    results.Rows[22].Cells[1].ReplaceText(currentValue, "Results not available");
                    results.Rows[23].Cells[1].ReplaceText(currentValue, "Results not available");
                    results.Rows[24].Cells[1].ReplaceText(currentValue, "Results not available");
                    results.Rows[24].Cells[3].ReplaceText(currentValue, "Results not available");
                    results.Rows[25].Cells[1].ReplaceText(currentValue, "Results not available");
                    results.Rows[25].Cells[3].ReplaceText(currentValue, "Results not available");
                }

                currentValue = results.Rows[27].Cells[1].Paragraphs[0].Text;

                if (File.Exists(fullPath + "_PROVEAN.html") && (currentValue == "Check HTML" || currentValue == "Results not available"))
                {
                    string[] output = Provean(fullPath + "_PROVEAN.html", variant);

                    if (output[0] != null)
                    {
                        results.Rows[27].Cells[1].ReplaceText(currentValue, output[0]);
                        results.Rows[28].Cells[1].ReplaceText(currentValue, output[1]);
                        results.Rows[29].Cells[1].ReplaceText(currentValue, output[2]);
                        results.Rows[31].Cells[1].ReplaceText(currentValue, output[3]);
                        results.Rows[32].Cells[1].ReplaceText(currentValue, output[5]);
                        results.Rows[33].Cells[1].ReplaceText(currentValue, output[6]);
                        results.Rows[34].Cells[1].ReplaceText(currentValue, output[4]);
                    }
                }
                else if (currentValue == "Check HTML")
                {
                    results.Rows[27].Cells[1].ReplaceText(currentValue, "Results not available");
                    results.Rows[28].Cells[1].ReplaceText(currentValue, "Results not available");
                    results.Rows[29].Cells[1].ReplaceText(currentValue, "Results not available");
                    results.Rows[31].Cells[1].ReplaceText(currentValue, "Results not available");
                    results.Rows[32].Cells[1].ReplaceText(currentValue, "Results not available");
                    results.Rows[33].Cells[1].ReplaceText(currentValue, "Results not available");
                    results.Rows[34].Cells[1].ReplaceText(currentValue, "Results not available");
                }

                currentValue = results.Rows[36].Cells[1].Paragraphs[0].Text;

                if (File.Exists(fullPath + "_MutationAssessor.html") && (currentValue == "Check HTML" || currentValue == "Results not available"))
                {
                    string[] output = MutA(fullPath + "_MutationAssessor.html");

                    if (output[0] != null)
                    {
                        results.Rows[36].Cells[1].ReplaceText(currentValue, output[0]);
                        results.Rows[37].Cells[1].ReplaceText(currentValue, output[1]);
                    }
                }
                else if (currentValue == "Check HTML")
                {
                    results.Rows[36].Cells[1].ReplaceText(currentValue, "Results not available");
                    results.Rows[37].Cells[1].ReplaceText(currentValue, "Results not available");
                }

                currentValue = results.Rows[39].Cells[1].Paragraphs[0].Text;

                if(File.Exists(fullPath + "_PolyPhen2Error.txt"))
                {
                    results.Rows[38].Cells[0].Paragraphs[0].Append(" - Check error file");
                }

                if (File.Exists(fullPath + "_PolyPhen2.html") && (currentValue == "Check HTML" || currentValue == "Results not available"))
                {
                    string[] output = PP2(fullPath + "_PolyPhen2.html");

                    if (output[0] != null)
                    {
                        results.Rows[39].Cells[1].ReplaceText(currentValue, output[0]);
                        results.Rows[40].Cells[1].ReplaceText(currentValue, output[1]);
                        results.Rows[41].Cells[1].ReplaceText(currentValue, output[2]);
                        results.Rows[41].Cells[3].ReplaceText(currentValue, output[3]);
                    }
                    if (output[4] != null)
                    {
                        results.Rows[42].Cells[1].ReplaceText(currentValue, output[4]);
                        results.Rows[43].Cells[1].ReplaceText(currentValue, output[5]);
                        results.Rows[44].Cells[1].ReplaceText(currentValue, output[6]);
                        results.Rows[44].Cells[3].ReplaceText(currentValue, output[7]);
                    }
                }
                else if (currentValue == "Check HTML")
                {
                    results.Rows[39].Cells[1].ReplaceText(currentValue, "Results not available");
                    results.Rows[40].Cells[1].ReplaceText(currentValue, "Results not available");
                    results.Rows[41].Cells[1].ReplaceText(currentValue, "Results not available");
                    results.Rows[41].Cells[3].ReplaceText(currentValue, "Results not available");
                    results.Rows[42].Cells[1].ReplaceText(currentValue, "Results not available");
                    results.Rows[43].Cells[1].ReplaceText(currentValue, "Results not available");
                    results.Rows[44].Cells[1].ReplaceText(currentValue, "Results not available");
                    results.Rows[44].Cells[3].ReplaceText(currentValue, "Results not available");

                }

                doc.Save();

                return true;
            }
            catch
            {
                return false;
            }
        }


        private static string[] SNPsGO(string filepath)
        {
            string doc = getFileContents(filepath);

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(doc);

            string[] results = new string[9];
            string pattern = @"(Neutral|Disease)\n\s+(\d{1,2})(\d+\.\d+)\n\s+(.+)";
            foreach (HtmlNode table in document.DocumentNode.SelectNodes(".//table"))
            {
                MatchCollection matches = Regex.Matches(table.InnerText, pattern);

                foreach (Match m in matches)
                {
                    if(m.Groups[4].Value.Contains("PhD-SNP"))
                    {
                        results[0] = m.Groups[1].Value.ToString();
                        results[1] = m.Groups[2].Value.ToString();
                        results[2] = m.Groups[3].Value.ToString();
                    }
                    else if (m.Groups[4].Value.Contains("PANTHER"))
                    {
                        results[3] = m.Groups[1].Value.ToString();
                        results[4] = m.Groups[2].Value.ToString();
                        results[5] = m.Groups[3].Value.ToString();
                    }
                    else if (m.Groups[4].Value.Contains("SNPs&GO"))
                    {
                        results[6] = m.Groups[1].Value.ToString();
                        results[7] = m.Groups[2].Value.ToString();
                        results[8] = m.Groups[3].Value.ToString();
                    }
                }

            }

            return results;
            
        }

        private static string[] PMut(string filename)
        {

            string[] results = new string[3];
            string doc = getFileContents(filename);

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(doc);

            
            

                HtmlNodeCollection _collection = document.DocumentNode.SelectNodes(".//table[@class='table table-condensed table-striped']");
                if (_collection != null )
                {
                   
                    foreach (HtmlNode _element in _collection)
                    {
                        HtmlNodeCollection _tr = _element.SelectNodes(".//tr");


                        foreach (HtmlNode child in _tr)
                        {
                            HtmlNodeCollection _tbody = child.SelectNodes(".//tbody");

                            foreach (HtmlNode _td in _tbody)
                            {
                                string bigString = _td.InnerText.ToString();
                                string[] predictions = bigString.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                                //string protein = predictions[0];
                                //string mutation = predictions[1] + " " + predictions[2] + predictions[3] + predictions[4];
                                //string score = predictions[8] + predictions[9] + predictions[10];
                                string score = predictions[8];
                                string probability = predictions[9];
                                string[] diseaseFull = predictions[10].Split(';');
                                string disease = diseaseFull[3];

                                results[0] = score;
                                results[1] = probability;
                                results[2] = disease;

                                return results;

                            }
                        }
                    }
                }
                else 
                {
                    results = null;
                    return results;
                }
                
                return results;          
        }
           
        


        private static string[] HANSA(string filename, bool details)
        {
            string[] results = new string[10];
            string doc = getFileContents(filename);

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(doc);

            if (details)
            {
                try
                {
                    
                    HtmlNodeCollection tables = document.DocumentNode.SelectNodes(".//table");

                    foreach (HtmlNode table in tables)
                    {
                        if (table.InnerText.StartsWith("For mutation:"))
                        {
                            HtmlNodeCollection rows = table.SelectNodes(".//tr");

                            foreach (HtmlNode row in rows)
                            {
                                if (row.ChildNodes.Count == 2 && !row.InnerText.StartsWith("Feature"))
                                {
                                    if (row.ChildNodes[0].InnerText.StartsWith("Wild Pab"))
                                    {
                                        results[0] = row.ChildNodes[1].InnerText;
                                    }
                                    else if (row.ChildNodes[0].InnerText.StartsWith("Mutant Pab"))
                                    {
                                        results[1] = row.ChildNodes[1].InnerText;
                                    }
                                    else if (row.ChildNodes[0].InnerText.StartsWith("Diff. Pab"))
                                    {
                                        results[2] = row.ChildNodes[1].InnerText;
                                    }
                                    else if (row.ChildNodes[0].InnerText.StartsWith("Wild Gab"))
                                    {
                                        results[3] = row.ChildNodes[1].InnerText;
                                    }
                                    else if (row.ChildNodes[0].InnerText.StartsWith("Mutant Gab"))
                                    {
                                        results[4] = row.ChildNodes[1].InnerText;
                                    }
                                    else if (row.ChildNodes[0].InnerText.StartsWith("Diff. Gab"))
                                    {
                                        results[5] = row.ChildNodes[1].InnerText;
                                    }
                                    else if (row.ChildNodes[0].InnerText.StartsWith("Solvent"))
                                    {
                                        results[6] = row.ChildNodes[1].InnerText;
                                    }
                                    else if (row.ChildNodes[0].InnerText.StartsWith("Secondary"))
                                    {
                                        results[7] = row.ChildNodes[1].InnerText;
                                    }
                                    else if (row.ChildNodes[0].InnerText.StartsWith("BLOSUM62"))
                                    {
                                        results[8] = row.ChildNodes[1].InnerText;
                                    }
                                    else if (row.ChildNodes[0].InnerText.StartsWith("Diff. free"))
                                    {
                                        results[9] = row.ChildNodes[1].InnerText;
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    return results;
                }
            }
            else
            {
                string pattern = @"<input type='submit' value='(.+)' title='Get details'>";
                foreach (HtmlNode table in document.DocumentNode.SelectNodes(".//input"))
                {
                    if(Regex.IsMatch(table.OuterHtml, pattern))
                    {
                        results[0] = Regex.Match(table.OuterHtml, pattern).Groups[1].Value.ToString();
                    }
                    
                }
            }

            return results;
        }

        private static string[] Panther(string filename)
        {
            string[] results = new string[6];

            string doc = getFileContents(filename);

            if (doc.Contains("<SPAN class=scrollTableHead>") || doc.Contains("<span class=\"scrollTableHead\">"))
            {
                string str = "<SPAN class=scrollTableHead>";
                int idx = doc.IndexOf(str);
                if (idx == -1)
                {
                    str = "<span class=\"scrollTableHead\">";
                    idx = doc.IndexOf(str);
                }
                doc = doc.Remove(idx, str.Length);
            }

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.OptionCheckSyntax = false;
            document.LoadHtml(doc);

            string pattern = @"\r\n\s+\r\n\s+subPSEC\r\n\s+Pdeleterious\r\n\s+substitution\r\n\s+MSA position\r\n\s+Pwt\r\n\s+Psubstituted\r\n\s+NIC\r\n\s+(-{0,1}\d+\.\d+)\r\n\s+(\d\.\d+)\r\n\s+[A-Z0-9]+\r\n\s+(\d+)\r\n\s+(\d+\.\d+)\r\n\s+(\d+\.\d+)\r\n\s+(\d+\.\d+)";
            foreach(HtmlNode table in document.DocumentNode.SelectNodes(".//table"))
            {
                if (Regex.IsMatch(table.InnerText, pattern))
                {

                    Match m = Regex.Match(table.InnerText, pattern);

                    results[0] = m.Groups[1].Value.ToString();
                    results[1] = m.Groups[2].Value.ToString();
                    results[2] = m.Groups[3].Value.ToString();
                    results[3] = m.Groups[4].Value.ToString();
                    results[4] = m.Groups[5].Value.ToString();
                    results[5] = m.Groups[6].Value.ToString();
                }
            }

            return results;
        }

        private static string[] Provean(string filename, string variant)
        {
            string[] results = new string[7];
            string doc = getFileContents(filename);
            string[] splitVar = Utilities.splitVariant(variant);

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(doc);

            string pattern = @"\s+" + splitVar[1] + @"\n\s+" + splitVar[0] + @"\n\s+" + splitVar[2] + @"\n\s+(-{0,1}\d+\.\d+)\n\s+(Neutral|Deleterious)\n\s+(\d+)\n\s+\d+\n\s+(-{0,1}\d+\.\d+)\n\s+(Damaging|Tolerated)\n\s+(\d\.\d+)\n\s+(\d+)";
            foreach (HtmlNode table in document.DocumentNode.SelectNodes(".//table"))
            {
                if (Regex.IsMatch(table.InnerText, pattern))
                {
                    Match m = Regex.Match(table.InnerText, pattern);
                    results[0] = m.Groups[1].Value.ToString();
                    results[1] = m.Groups[2].Value.ToString();
                    results[2] = m.Groups[3].Value.ToString();
                    results[3] = m.Groups[4].Value.ToString();
                    results[4] = m.Groups[5].Value.ToString();
                    results[5] = m.Groups[6].Value.ToString();
                    results[6] = m.Groups[7].Value.ToString();
                }
            }

            return results;
        }

        private static string[] MutA(string filename)
        {
            string[] results = new string[2];
            string doc = getFileContents(filename);

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(doc);

            foreach(HtmlNode table in document.DocumentNode.SelectNodes(".//table"))
            {
                string pattern = @"Mutation\nAAvariant\nGene\nMSA\nPDB\nFunc.Impact\nFIscore\nUniprot\nRefseq\nMSAheight\nCodon start position\nFunc.region\nProteinbind.site\nDNA\/RNAbind.site\nsmall.molbind.site\n\n\d\nNP_\d+ [A-Z]\d+[A-Z]\n[A-Z]\d+[A-Z]\n[A-za-z0-9]+\n\n\n\S+(low|neutral|medium|high)\S+\n((?:\-){0,1}\d+(?:\.\d+){0,1})\n[A-Za-z0-9_]";
                if(Regex.IsMatch(table.InnerText, pattern))
                {
                    Match m = Regex.Match(table.InnerText, pattern);

                    results[0] = m.Groups[1].Value.ToString();
                    results[1] = m.Groups[2].Value.ToString();
                    break;
                }
            }

            return results;
        }

        private static string[] PP2(string filename)
        {
            string[] results = new string[8];
            string doc = getFileContents(filename);

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(doc);

            HtmlNodeCollection collection = document.DocumentNode.SelectNodes(".//div");
            HtmlNode humDiv = null;
            HtmlNode humVar = null;
            foreach (HtmlNode node in collection)
            {
                if (node.GetAttributeValue("id", "NA") == "HumDivConf")
                {
                    humDiv = node;
                }
                else if (node.GetAttributeValue("id", "NA") == "HumVarConf")
                {
                    humVar = node;
                }
                else if (humDiv != null && humVar != null)
                {
                    break;
                }
            }

            string pattern = @"This mutation is predicted to be (.+) with a score of (\d+\.\d+) \(sensitivity: (\d+\.\d+); specificity: (\d+\.\d+)\)";
            if (humDiv != null)
            {
                Match m = Regex.Match(humDiv.InnerText, pattern);

                if (m.Groups.Count == 5)
                {
                    results[0] = m.Groups[1].Value.ToString();
                    results[1] = m.Groups[2].Value.ToString();
                    results[2] = m.Groups[3].Value.ToString();
                    results[3] = m.Groups[4].Value.ToString();
                }
            }
            if(humVar != null)
            {
                Match m = Regex.Match(humVar.InnerText, pattern);

                if (m.Groups.Count == 5)
                {
                    results[4] = m.Groups[1].Value.ToString();
                    results[5] = m.Groups[2].Value.ToString();
                    results[6] = m.Groups[3].Value.ToString();
                    results[7] = m.Groups[4].Value.ToString();
                }
            }

            return results;
        }

       




        private static string getFileContents(string fName)
        {
            WebClient file = new WebClient();
            string url = fName;

            try
            {
                byte[] newFileData = file.DownloadData(url);
                string fileString = System.Text.Encoding.UTF8.GetString(newFileData);
                return fileString;
            }
            catch (WebException e)
            {
                return e.ToString();
            }
        }

    }
}
