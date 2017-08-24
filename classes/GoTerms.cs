using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;

namespace SNPBot
{
    class GoTerms
    {

        public static string GetGOs(string accession)
        {
            string terms = "";

            if(accession.StartsWith("NM_"))
            {
                string[] accs = GetAccessions(accession);
                if (accs.Length > 1)
                {
                    //if more than one result is returned get the IDs to differentiate between the reviewed and unreviewed records
                    List<string[]> pairs = GetIDs(accs);
                    List<string> ids = new List<string>();

                    //IDs for unreviewed proteins contain the identifier in the name
                    foreach (string[] pair in pairs)
                    {
                        if (!pair[1].Contains(pair[0]))
                        {
                            ids.Add(pair[0]);
                        }
                    }

                    //if more than one reviewed protein was returned, get the user to choose
                    if (ids.Count > 1)
                    {
                        foreach (string id in ids)
                        {
                            terms = terms + id + " ";
                        }
                    }
                    else if (ids.Count == 0)
                    {

                    }
                    else
                    {
                        terms = QuickGO(ids[0]);
                    }
                }
                else if(accs.Length == 1)
                {
                    terms = QuickGO(accs[0]);
                }
                
            }


            return terms;
        }


        private static string[] GetAccessions(string accession)
        {
            //get the matching UniProt accessions for the RefSeq NM_ number
            string url = @"http://uniprot.org/mapping/?from=REFSEQ_NT_ID&to=ACC&format=tab&query=" + accession.Substring(0, accession.IndexOf('.'));

            string results;
            using (WebClient web = new WebClient())
            {
                results = web.DownloadString(url);
            }

            //separate the results into lines
            string[] lines = results.Split(new char[] { '\n' });
            //declare string array for results (it will be one less than the number of lines (due to header line)
            List<string> accs = new List<string>();

            //start at index 1 to skip header line and add the UniProt accession to the results array
            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i] != "")
                {
                    string a = lines[i].Split(new char[] { '\t' })[1];

                    accs.Add(a);
                }
            }

            string[] accessions = accs.ToArray<string>();

            return accessions;
        }

        private static List<string[]> GetIDs(string[] accessions)
        {
            List<string[]> pairs = new List<string[]>();

            foreach (string accession in accessions)
            {
                string url = @"http://uniprot.org/mapping/?from=ACC&to=ID&format=tab&query=" + accession;

                string result;
                using (WebClient web = new WebClient())
                {
                    result = web.DownloadString(url);
                }

                //separate the results into lines
                string[] lines = result.Split(new char[] { '\n' });
                //start at index 1 to skip header line and add the UniProt accession and ID to the list
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(new char[] { '\t' });
                    if (fields.Length == 2)
                    {
                        pairs.Add(fields);
                    }
                }
            }

            return pairs;
        }

        private static string QuickGO(string accession)
        {
            string url = @"http://www.ebi.ac.uk/QuickGO-Old/GAnnotation?protein=" + accession + "&format=tsv";
            string terms = "";

            string result;
            using (WebClient web = new WebClient())
            {
                result = web.DownloadString(url);
            }

            //separate the results into lines
            string[] lines = result.Split(new char[] { '\n' });
            foreach (string line in lines)
            {
                if (line != lines[0] && line != "")
                {
                    string[] fields = line.Split(new char[] { '\t' });
                    if (fields[5] != "NOT" && !terms.Contains(fields[6]))
                    {
                        terms = terms + fields[6] + " ";
                    }

                }
            }

            return terms;
        }
    }
}
