using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Web;
using System.Net;



namespace SNPBot
{
    public partial class SelectTranscript : Form
    {
        UVSearch _main;
        
        public SelectTranscript(UVSearch main)
        {
            InitializeComponent();
            _main = main;
        }

        public static string geneID;
        DataTable summaryTable = new DataTable();

        private void SelectTranscript_Load(object sender, EventArgs e)
        {

            string url = "http://eutils.ncbi.nlm.nih.gov/entrez/eutils/esearch.fcgi?db=nuccore&term=human[orgn]+and+\"" + _main._TryGene + "\"[sym]";
            XmlDocument doc = downloadXml(url);

            XmlNodeList nodesId = doc.GetElementsByTagName("Id");

            summaryTable.Columns.Add("Transcript");
            summaryTable.Columns.Add("Name");
            summaryTable.Columns.Add("Updated");
            summaryTable.Columns.Add("Length");
            summaryTable.Columns.Add("Status");
            summaryTable.Columns.Add("Gene ID");

            foreach (XmlNode nodeId in nodesId)
            {
                DataRow row = summaryTable.NewRow();
                string geneId = nodeId.InnerText;
                url = @"http://eutils.ncbi.nlm.nih.gov/entrez/eutils/esummary.fcgi?db=nuccore&id=" + geneId;
                XmlDocument docSummary = downloadXml(url);
                XmlNodeList summaryNodes = docSummary.GetElementsByTagName("Item");
                foreach (XmlNode node in summaryNodes)
                {
                    
                    if (node.Attributes["Name"].Value == "Caption")
                    {
                        row["Transcript"] = node.InnerText;
                    }
                    else if (node.Attributes["Name"].Value == "Title")
                    {
                        row["Name"] = node.InnerText;
                    }
                    else if (node.Attributes["Name"].Value == "UpdateDate")
                    {
                        row["Updated"] = node.InnerText;
                    }
                    else if (node.Attributes["Name"].Value == "Length")
                    {
                        row["Length"] = node.InnerText;
                    }
                    else if (node.Attributes["Name"].Value == "Status")
                    {
                        row["Status"] = node.InnerText;
                    }
                    else if (node.Attributes["Name"].Value == "Gi")
                    {
                        row["Gene ID"] = node.InnerText;
                    }
                    
                }
                summaryTable.Rows.Add(row);
                
            }

            dataGridView1.DataSource = summaryTable;
        }

        private XmlDocument downloadXml(string url)
        {
            string xml;
            using (var webClient = new WebClient())
            {
                xml = webClient.DownloadString(url);
            }

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc;
        }

        private void selectBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows == null || dataGridView1.SelectedRows.Count > 1)
            {
                MessageBox.Show("Please select a transcript or click Cancel");
                return;
            }
            else
            {
                int rowNum = dataGridView1.CurrentCell.RowIndex;
                geneID = summaryTable.Rows[rowNum]["Gene ID"].ToString();
            }
            
        }
        public static string _GeneId
        {
            get
            {
                return geneID;
            }
        }

    }
}
