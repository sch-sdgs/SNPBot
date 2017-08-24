using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Web;
using System.Collections;
using mshtml;
using System.Net;
using System.Xml;
using System.Timers;
using System.Diagnostics;

namespace SNPBot
{
    public partial class UVSearch : Form
    {
        public UVSearch()
        {
            InitializeComponent();
        }

        #region Class variables
        System.Timers.Timer _pMutTimer = new System.Timers.Timer(600000);
        System.Timers.Timer _pp2Timer = new System.Timers.Timer(30000);
        System.Timers.Timer _mainTimer = new System.Timers.Timer(90000);
        string _pMutURL = "";
        int _hansaRetry = 0;
        string[] _sites = new string[9];
        MainTools _currentTool = MainTools.None;
        public static string _path;
        static string[] _tools = new string[9];
        static string _refSeq;
        static string _variant;
        static string _fasta;
        public static string _fastaNH;
        static string _geneF;
        static string _email;
        static string[] _varArray;
        static string _pRefSeq;
        bool _problemTranscript;
        static string _tryGene;
        static string _pp2JobID = "";
        Status _HANSAState = Status.Null;
        Status _sAndGoState = Status.Null;
        Status _proveanState = Status.Null;
        Status _mutPredState = Status.Null;
        Status _pp2State = Status.Null;
        Status _pMutState = Status.Null;
        Status _pantherState = Status.Null;
        Status _mutAState = Status.Null;

        DataTable runList = new DataTable();
        #endregion

        #region Properties
        public string _Path
        {
            get
            {
                return _path;
            }
        }

        public string _Gene
        {

            get
            {
                return _geneF;
            }
        }

        public string _TryGene
        {

            get
            {
                return _tryGene;
            }
        }

        public string _Variant
        {
            get
            {
                return _variant;
            }
        }

        public string _FastaNH
        {
            get
            {
                return _fastaNH;
            }
        }

        public string _RefSeq
        {
            get
            {
                return _refSeq;
            }
        }

        public string _protRefSeq
        {
            get
            {
                return _pRefSeq;
            }
        }

        public string[] _VarArray
        {
            get
            {
                return _varArray;
            }

        }

        public string _Fasta
        {
            get
            {
                return _fasta;
            }
        }

        public string _Email
        {
            get
            {
                return _email;
            }
        }
        #endregion

        #region Enums

        enum MainTools : int
        {
            None = 0,
            MutA = 1,
            Panther = 2,
            MutPred = 3,
            Snap = 4,
            Provean = 5
        };

        enum Status : int
        {
            Null = 0,
            Entered = 1,
            Submitted = 2,
            Running = 3,
            Ready = 4,
            Error = 5
        }

        #endregion

        #region Form Events
        private void Form1_Load(object sender, EventArgs e)
        {

            // Adds 3 columns to the status box.
            runList.Columns.Add(new DataColumn(" ", typeof(bool)));
            runList.Columns.Add("Analysis tool");
            runList.Columns.Add("Status");

            // Adds these tool names to the status box. 
            _tools[0] = "SNPsandGO";
            _tools[1] = "MutationAssessor";
            _tools[2] = "PANTHER";
            _tools[3] = "PROVEAN";
            _tools[4] = "MutPred";
            _tools[5] = "PMut";
            _tools[6] = "SNAP2";
            _tools[7] = "PolyPhen2";
            _tools[8] = "HANSA";

            foreach (string tool in _tools)
            {
                runList.Rows.Add(true, tool, "Not run");
            }

            //format dataGridView
            dataGridViewTools.DataSource = runList;
            dataGridViewTools.Columns[0].ReadOnly = false;
            dataGridViewTools.Columns[1].ReadOnly = true;
            dataGridViewTools.Columns[2].ReadOnly = true;
            dataGridViewTools.Columns[0].Width = 25;
            dataGridViewTools.Columns[1].Width = 125;
            dataGridViewTools.ClearSelection();

            //add PMut timer event
            _pMutTimer.Elapsed += new ElapsedEventHandler(_pMutTimer_Elapsed);
            //add PP2 timer event
            _pp2Timer.Elapsed += new ElapsedEventHandler(_pp2Timer_Elapsed);
            //add main timer event
            _mainTimer.Elapsed += new ElapsedEventHandler(_mainTimer_Elapsed);
        }

        void _mainTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            string next = MainBrowserNavigate();
        }

        public delegate string NextTool();

        public string MainBrowserNavigate()
        {
            if (this.InvokeRequired)
            {
                return Invoke(new NextTool(MainBrowserNavigate)) as string;
            }
            else
            {
                if (_currentTool == MainTools.MutA)
                {
                    runList.Rows[1].SetField("Status", "Timeout. Not Run.");
                    if ((bool)runList.Rows[2].ItemArray[0])
                    {
                        runPANTHER(_sites[2]);
                    }
                    else if ((bool)runList.Rows[3].ItemArray[0])
                    {
                        runPROVEAN(_sites[3]);
                    }
                }
                else if (_currentTool == MainTools.MutPred)
                {
                    runList.Rows[4].SetField("Status", "Timeout. Not Run.");
                    if ((bool)runList.Rows[1].ItemArray[0])
                    {
                        runMutAssessor(_sites[1]);
                    }
                    else if ((bool)runList.Rows[2].ItemArray[0])
                    {
                        runPANTHER(_sites[2]);
                    }
                    else if ((bool)runList.Rows[3].ItemArray[0])
                    {
                        runPROVEAN(_sites[3]);
                    }
                }
                else if (_currentTool == MainTools.Panther)
                {
                    runList.Rows[2].SetField("Status", "Timeout. Not Run.");
                    if ((bool)runList.Rows[3].ItemArray[0])
                    {
                        runPROVEAN(_sites[3]);
                    }
                }
                else if (_currentTool == MainTools.Provean)
                {
                    runList.Rows[3].SetField("Status", "Timeout. Not Run.");
                }
                else if (_currentTool == MainTools.Snap)
                {
                    runList.Rows[6].SetField("Status", "Timeout. Not Run.");
                    if ((bool)runList.Rows[4].ItemArray[0])
                    {
                        runMutPred(_sites[4]);
                    }
                    else if ((bool)runList.Rows[1].ItemArray[0])
                    {
                        runMutAssessor(_sites[1]);
                    }
                    else if ((bool)runList.Rows[2].ItemArray[0])
                    {
                        runPANTHER(_sites[2]);
                    }
                    else if ((bool)runList.Rows[3].ItemArray[0])
                    {
                        runPROVEAN(_sites[3]);
                    }
                }

                return "complete";
            }
        }

        void _pp2Timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            string pp2 = RefreshPP2();


        }

        public delegate string GetWebBrowser();

        public string RefreshPP2()
        {
            if (this.InvokeRequired)
            {
                return Invoke(new GetWebBrowser(RefreshPP2)) as string;
            }
            else
            {
                Utilities.submit(ref webBrowserPP2);

                return "complete";
            }
        }
        void _pMutTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _pMutState = Status.Error;
            //if (_pMutURL != "")
            //{
            //    webBrowserPMut.Navigate(_pMutURL);
            //}
        }

        private void UVSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            webBrowserMain.Dispose();
            webBrowserHansa.Dispose();
            webBrowserPP2.Dispose();
            webBrowserSandGo.Dispose();
        }

        private void webBrowser2_NewWindow(object sender, CancelEventArgs e)
        {
            webBrowserHansa.Navigate(webBrowserHansa.StatusText);
            e.Cancel = true;
        }

        private void webBrowser3_NewWindow(object sender, CancelEventArgs e)
        {
            webBrowserPP2.Navigate(webBrowserPP2.StatusText);
            e.Cancel = true;
        }

        #endregion

        #region buttons
        private void getFastaButton_Click(object sender, EventArgs e)
        {

            bool passed = checkRefSeq();

            // If it has not passed the initial input checks and a transcript has not already 
            // been tried and failed, return with the error message from in CheckInitialInput.
            if (!passed && !_problemTranscript)
            {
                return;
            }

            string geneId = NCBI.getGeneId(refSeqIDTextBox.Text);

            // If couldn't get a gene ID the transcript is wrong/not live, so need to find a correct one.
            if (geneId == "")
            {
                // If there has been no gene entered throw an error message.
                if (geneBox.Text == "")
                {
                    _problemTranscript = true;
                    MessageBox.Show("Could not find the FASTA. \nEnter a gene symbol and click the Get FASTA button again to select from a list, or look for a live transcript in NCBI.\nNOTE: a small number of tools may not work with the newest transcripts.");
                    return;
                }
                // If not open the dialog (SelectTranscript form) to select a correct transcript.
                else
                {
                    string searchCount = NCBI.getIDs(geneBox.Text);
                    if (searchCount == "0")
                    {
                        MessageBox.Show("Gene symbol has not linked to any transcripts. Check the current transcript code on NCBI.");
                        return;
                    }

                    // Otherwise open a list to select from.
                    _tryGene = geneBox.Text;
                    SelectTranscript transcriptForm = new SelectTranscript(this);

                    if (transcriptForm.ShowDialog() == DialogResult.OK)
                    {
                        geneId = SelectTranscript._GeneId;

                        if (geneId == "")
                        {
                            MessageBox.Show("Could not find the gene ID. \nCheck that the gene symbol is correct, and that a live transcript is selected. Alternatively check for a live mRNA transcript on NCBI.");
                            return;
                        }
                    }
                    else // If cancel clicked on the form.
                    {
                        return;
                    }
                }
            }

            // Once we have a geneId ID then continue to try to get the FASTA.

            _fasta = NCBI.loadFasta(geneId);
            textBoxFasta.Text = _fasta;
            if (!_fasta.StartsWith(">"))
            {
                MessageBox.Show("Could not find the FASTA. \nEnter a gene symbol and click the Get FASTA button again to select from a list, or look for a live transcript in NCBI.\nNOTE: a small number of tools may not work with the newest transcripts.");
                return;
            }

            // Once the FASTA has been found, return the problem transcript flag to false.
            _problemTranscript = false;

            // Split the FASTA header to get the protein Refseq code and the gene symbol.Also pull the mRNA refseq code from the header.
            try
            {
                string[] splitFasta = _fasta.Split('\n');
                string refLine = splitFasta.First();
                for (int idx = 1; idx < splitFasta.Length; idx++)
                {
                    if (idx == splitFasta.Length)
                        _fastaNH += splitFasta[idx];
                    else
                        _fastaNH += splitFasta[idx] + "\n";
                }
                string[] firstLine = refLine.Split('[', ']');
                foreach (string str in firstLine)
                {
                    if (str.IndexOf("gene") == 0)
                    {
                        string[] subStr = str.Split('=');
                        _geneF = subStr[1];
                    }
                    else if (str.IndexOf("protein_id") == 0)
                    {
                        string[] subStr = str.Split('=');
                        _pRefSeq = subStr[1];
                    }

                }
                string[] findNMNum = refLine.Split('|', '_');
                string refSeqID = "NM_" + findNMNum[2];
                refSeqIDTextBox.Text = refSeqID;
                geneBox.Text = _geneF;
            }
            catch (Exception ex)
            {
                MessageBox.Show("The protein RefSeq code could not be extracted from the FASTA header. Error:" + ex.ToString());
            }

            if (_pRefSeq == "")
            {
                MessageBox.Show("Please enter the RefSeq code for the protein. It has not been found automatically.");
                return;
            }
            refSeqProteinBox.Text = _pRefSeq;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            //reset all check enums in case button has been pressed a second time
            _HANSAState = Status.Null;
            _sAndGoState = Status.Null;
            _proveanState = Status.Null;
            _mutPredState = Status.Null;
            _pp2State = Status.Null;
            _pMutState = Status.Null;
            _pantherState = Status.Null;
            _mutAState = Status.Null;
            _currentTool = MainTools.None;
            _pp2JobID = "";

            bool passed = checkInput();
            if (!passed)
            {
                return;
            }

            summariseBtn.Enabled = true;

            // Set some variables from the text boxes.
            _variant = variantTextBox.Text;
            _email = emailTextBox.Text;
            _path = @pathBox.Text;

            // Save the FASTA to a text file.
            Utilities.saveTextFile(this, "FASTA.fasta", _fasta);

            // Set the URLs for the sites to visit.
            _sites[0] = @"http://snps.biofold.org/snps-and-go/snps-and-go.html";
            _sites[1] = @"http://mutationassessor.org/";
            _sites[2] = @"http://www.pantherdb.org/tools/csnpScoreForm.jsp?";
            _sites[3] = @"http://provean.jcvi.org/protein_batch_submit.php?species=human";
            _sites[4] = @"http://mutpred1.mutdb.org/";
            _sites[5] = @"http://www.cdfd.org.in/HANSA/";
            _sites[6] = @"http://genetics.bwh.harvard.edu/pph2/";
            _sites[7] = @"https://www.rostlab.org/services/snap/";
            _sites[8] = @"http://mmb.pcb.ub.es/pmut2017/analyses/new/";

            //split the variant into ref, pos, alt
            _varArray = Utilities.splitVariant(_variant);

            // Run the various tools if they are selected in the status box.
            if ((bool)runList.Rows[0].ItemArray[0])
            {
                runSNPSandGO(_sites[0]);
            }

            if ((bool)runList.Rows[5].ItemArray[0])
            {
                runPMut(_sites[8]);
            }

            //maintools tab starts with mutation assessor but will only complete the ones that have been checked.
            _currentTool = MainTools.Snap;
            if ((bool)runList.Rows[6].ItemArray[0])
            {
                runSNAP(_sites[7]);
            }
            else if ((bool)runList.Rows[4].ItemArray[0])
            {
                runMutPred(_sites[4]);
            }
            else if ((bool)runList.Rows[1].ItemArray[0])
            {
                runMutAssessor(_sites[1]);
            }
            else if ((bool)runList.Rows[2].ItemArray[0])
            {
                runPANTHER(_sites[2]);
            }
            else if ((bool)runList.Rows[3].ItemArray[0])
            {
                runPROVEAN(_sites[3]);
            }

            if ((bool)runList.Rows[7].ItemArray[0])
            {
                runPolyPhen2(_sites[6]);
            }

            if ((bool)runList.Rows[8].ItemArray[0])
            {
                if (_fastaNH.Length < 2500)
                {
                    runHANSA(_sites[5]);
                }
                else
                {
                    int idx = findRowIndex(_tools[8]);
                    runList.Rows[idx].SetField("Status", "Skipped. Protein too long.");
                }
            }
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            // Show the dialog and get path.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = @folderBrowserDialog1.SelectedPath;
                pathBox.Text = folder;
            }
        }

        private void summariseBtn_Click(object sender, EventArgs e)
        {
            bool created = Summarise.GenerateSumamry(_path, _Gene, _Variant);

            if (created)
            {
                DialogResult mb = MessageBox.Show("The summary file was created successfully.\nWould you like to open it now?", "Summary File", MessageBoxButtons.YesNo);
                if (mb == System.Windows.Forms.DialogResult.Yes)
                {
                    string filepath = "\"" + _path + "\\" + _Gene + "_" + _Variant + "_Summary.docx\"";
                    Process.Start("WINWORD.EXE", filepath);
                }
            }
            else
            {
                MessageBox.Show("The file could not be created.\nIf the problem persists, Please contact the system administrator.");
            }
        }

        #endregion

        #region Checks
        private bool checkRefSeq()
        {
            bool passed;

            Regex rgxUP = new Regex("[N][M][_][0-9]{6,12}[.][0-9]+");

            if (refSeqIDTextBox.Text == "")
            {
                MessageBox.Show("Enter a RefSeq mRNA code.");
                passed = false;
                return passed;
            }
            else if (!rgxUP.IsMatch(refSeqIDTextBox.Text))
            {
                MessageBox.Show("Check the RefSeq mRNA code.");
                passed = false;
                return passed;
            }
            _refSeq = refSeqIDTextBox.Text;

            passed = true;
            return passed;
        }

        private bool checkInput()
        {
            bool passed = false; // False unless passes all the hurdles.

            if (_fasta == null || _fasta == "")
            {
                MessageBox.Show("Get a FASTA sequence.");
                return passed;
            }
            Regex rgxRS = new Regex("[N][M][_][0-9]{6,12}[.][0-9]+");

            if (refSeqIDTextBox.Text == "")
            {
                MessageBox.Show("Enter a RefSeq mRNA code.");
                return passed;
            }
            else if (!rgxRS.IsMatch(refSeqIDTextBox.Text))
            {
                MessageBox.Show("Check the RefSeq mRNA code.");
                return passed;
            }
            _refSeq = refSeqIDTextBox.Text;

            Regex rgxUP = new Regex("[N][P][_][0-9]{6,12}[.][0-9]+");

            if (refSeqProteinBox.Text == "")
            {
                MessageBox.Show("Enter a RefSeq protein code.");
                return passed;
            }
            else if (!rgxUP.IsMatch(refSeqProteinBox.Text))
            {
                MessageBox.Show("Check the RefSeq protein code.");
                return passed;
            }
            _pRefSeq = refSeqProteinBox.Text;

            Regex rgxVar = new Regex("[A-Z][0-9]*[A-Z]");
            if (variantTextBox.Text == "")
            {
                MessageBox.Show("Enter a variant code for the protein in the format XNumY.");
                return passed;
            }
            else if (!rgxVar.IsMatch(variantTextBox.Text))
            {
                MessageBox.Show("Check the variant code is in the format XNumY.");
                return passed;
            }

            if (emailTextBox.Text == "")
            {
                MessageBox.Show("Enter an email address to receive the results.");
                return passed;
            }

            if (pathBox.Text == "")
            {
                MessageBox.Show("Enter a folder (with the full path) to save the results to.");
                return passed;
            }
            _path = @pathBox.Text;
            try
            {
                if (Path.IsPathRooted(_path))
                {
                    Directory.CreateDirectory(_path);
                }
                else
                {
                    MessageBox.Show(@"Check the path is a valid full path (e.g. N:\folder\gene\variant).");
                    return passed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Check the path is valid (e.g. N:\folder\gene\variant), and that you have permission to access it." + ex.ToString());
                return passed;
            }

            passed = true;
            return passed;
        }
        #endregion

        #region Run Tools
        private void runSNPSandGO(string site)
        {
            int idx = findRowIndex(_tools[0]);
            //change status
            runList.Rows[idx].SetField("Status", "Running.");
            //open page
            webBrowserSandGo.Navigate(site);
        }

        private void runMutAssessor(string site)
        {
            int idx = findRowIndex(_tools[1]);
            runList.Rows[idx].SetField("Status", "Running");
            _currentTool = MainTools.MutA;
            _mainTimer.Start();
            webBrowserMain.Navigate(site);

        }

        private void runPANTHER(string site)
        {
            int idx = findRowIndex(_tools[2]);
            runList.Rows[idx].SetField("Status", "Running.");
            _currentTool = MainTools.Panther;
            _mainTimer.Start();
            webBrowserMain.Navigate(site);
        }

        private void runPROVEAN(string site)
        {
            int idx = findRowIndex(_tools[3]);
            runList.Rows[idx].SetField("Status", "Running. HTML to file once complete.");
            _currentTool = MainTools.Provean;
            _mainTimer.Start();
            webBrowserMain.Navigate(site);

        }

        private void runMutPred(string site)
        {
            int idx = findRowIndex(_tools[4]);
            runList.Rows[idx].SetField("Status", "Running.");
            _currentTool = MainTools.MutPred;
            _mainTimer.Start();
            webBrowserMain.Navigate(site);
        }

        private void runHANSA(string site)
        {
            int idx = findRowIndex(_tools[8]);
            runList.Rows[idx].SetField("Status", "Running. HTML to file once complete.");
            webBrowserHansa.Navigate(site);
        }

        private void runPolyPhen2(string site)
        {
            int idx = findRowIndex(_tools[7]);
            runList.Rows[idx].SetField("Status", "Running. HTML to file once complete.");
            webBrowserPP2.ScriptErrorsSuppressed = true;
            webBrowserPP2.Navigate(site);
        }

        private void runSNAP(string site)
        {
            int idx = findRowIndex(_tools[6]);
            runList.Rows[idx].SetField("Status", "Running.");
            _currentTool = MainTools.Snap;
            _mainTimer.Start();
            webBrowserMain.Navigate(site);
        }

        private void runPMut(string site)
        {
            int idx = findRowIndex(_tools[5]);
            runList.Rows[idx].SetField("Status", "Running.");
            webBrowserPMut.Navigate(site);
        }
        #endregion

        private int findRowIndex(string p)
        {
            String searchValue = p;
            int rowIndex = -1;
            foreach (DataGridViewRow row in dataGridViewTools.Rows)
            {
                if (row.Cells[1].Value.ToString().Equals(searchValue))
                {
                    rowIndex = row.Index;
                    break;
                }
            }
            return rowIndex;
        }

        #region Doc Completed Events
        private void webBrowserHansa_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                if ((webBrowserHansa.DocumentTitle.Contains("404") || webBrowserHansa.DocumentTitle.Contains("504")) && _hansaRetry == 0)
                {
                    _hansaRetry = 1;
                    webBrowserHansa.Navigate("http://cdfd.org.in/HANSA/");
                }
                else if ((webBrowserHansa.DocumentTitle.Contains("404") || webBrowserHansa.DocumentTitle.Contains("504")) && _hansaRetry == 1)
                {
                    _hansaRetry = 1;
                    webBrowserHansa.Navigate("http://hansa.cdfd.org.in:8080/");
                }
                else if (webBrowserHansa.DocumentTitle.Contains("404") || webBrowserHansa.DocumentTitle.Contains("504"))
                {
                    int idx = findRowIndex(_tools[8]);
                    runList.Rows[idx].SetField("Status", "Error loading page.");
                }
                else if (_HANSAState == Status.Null)
                {
                    Hansa.enterTextHANSA(this, ref webBrowserHansa);
                    _HANSAState = Status.Submitted;
                }
                else if (_HANSAState == Status.Submitted)
                {
                    string URL = "";
                    HtmlElementCollection collection;
                    collection = webBrowserHansa.Document.GetElementsByTagName("a");
                    foreach (HtmlElement element in collection)
                    {
                        if (element.InnerText == "results page")
                        {
                            URL = element.GetAttribute("href");
                            break;
                        }
                    }

                    _HANSAState = Status.Running;
                    webBrowserHansa.Navigate(URL);
                    Utilities.saveTextFile(this, "HANSAResultsUrl.txt", URL);
                }
                else if (_HANSAState == Status.Running)
                {
                    HtmlElementCollection collection = webBrowserHansa.Document.GetElementsByTagName("FORM");

                    if (collection.Count != 0)
                    {
                        //save page with prediction on 
                        int idx = findRowIndex(_tools[8]);
                        runList.Rows[idx].SetField("Status", "Run. Saving Pages.");
                        Utilities.savePage(this, "HANSA.html", webBrowserHansa.DocumentText, Encoding.GetEncoding(webBrowserHansa.Document.Encoding));

                        //click button to load details page
                        _HANSAState = Status.Ready;
                        HtmlElementCollection inputs = collection[0].GetElementsByTagName("input");
                        foreach (HtmlElement element in inputs)
                        {
                            string type = element.GetAttribute("type");

                            if (type == "submit")
                            {
                                element.InvokeMember("click");
                                break;
                            }
                        }

                    }

                }
                else if (_HANSAState == Status.Ready)
                {
                    //save details page
                    Utilities.savePage(this, "HANSAdetails.html", webBrowserHansa.DocumentText, Encoding.GetEncoding(webBrowserHansa.Document.Encoding));
                    int idx = findRowIndex(_tools[8]);
                    runList.Rows[idx].SetField("Status", "Run. Details saved.");
                    _HANSAState = Status.Null;
                }
            }
            catch
            {
                int idx = findRowIndex(_tools[8]);
                runList.Rows[idx].SetField("Status", "Error loading page.");
            }
        }

        private void webBrowserSandGo_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (_sAndGoState == Status.Null)
            {
                int idx = findRowIndex(_tools[0]);
                //enter text
                bool entered = SNPs_GO.enterTextSNPsandGO(this, webBrowserSandGo);
                //submit
                if (entered)
                {
                    Utilities.submit(ref webBrowserSandGo);
                    _sAndGoState = Status.Submitted;
                    //update status
                    runList.Rows[idx].SetField("Status", "Running. HTML to file once complete.");

                }
                else
                {
                    //update status
                    runList.Rows[idx].SetField("Status", "Not Submitted, No GO terms entered.");
                }
            }
            //if submitted, get results url and navigate to page
            else if (_sAndGoState == Status.Submitted)
            {
                string link = "0";
                HtmlElementCollection collection = webBrowserSandGo.Document.GetElementsByTagName("a");
                foreach (HtmlElement element in collection)
                {
                    link = element.GetAttribute("href");
                    if (link.IndexOf("output") != -1)
                    {
                        break;
                    }
                    else
                    {
                        link = "0";
                    }
                }

                if (link != "0")
                {
                    webBrowserSandGo.Navigate(link);
                }

                _sAndGoState = Status.Running;
            }
            else if (_sAndGoState == Status.Running)
            {
                if (webBrowserSandGo.ReadyState == WebBrowserReadyState.Interactive)
                {
                    _sAndGoState = Status.Ready;
                }
            }
            else if (_sAndGoState == Status.Ready)
            {
                Utilities.savePage(this, "SNPsandGO.html", webBrowserSandGo.DocumentText, Encoding.GetEncoding(webBrowserSandGo.Document.Encoding));
                // Should be saved to file
                int idx = findRowIndex(_tools[0]);
                runList.Rows[idx].SetField("Status", "Run. HTML saved.");
            }
        }

        private void webBrowserMain_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (_currentTool == MainTools.MutA)
            {
                if (webBrowserMain.DocumentTitle.Contains("504") || webBrowserMain.DocumentTitle.Contains("404"))
                {
                    _mainTimer.Stop();
                    int idx = findRowIndex(_tools[1]);
                    runList.Rows[idx].SetField("Status", "Error loading page.");
                    if ((bool)runList.Rows[2].ItemArray[0])
                    {
                        runPANTHER(_sites[2]);
                    }
                    else if ((bool)runList.Rows[3].ItemArray[0])
                    {
                        runPROVEAN(_sites[3]);
                    }
                }
                HtmlElement element = webBrowserMain.Document.GetElementById("toggle");
                if (_mutAState == Status.Null)
                {
                    _mainTimer.Stop();
                    int idx = findRowIndex(_tools[1]);
                    MutAssessor.enterTextMutAssess(this, webBrowserMain);
                    Utilities.submit(ref webBrowserMain);
                    _mutAState = Status.Submitted;
                    runList.Rows[idx].SetField("Status", "Running. HTML to file once complete.");
                }
                else if (element != null)
                {
                    Utilities.savePage(this, "MutationAssessor.html", webBrowserMain.DocumentText, Encoding.GetEncoding(webBrowserMain.Document.Encoding));
                    int idx = findRowIndex(_tools[1]);
                    runList.Rows[idx].SetField("Status", "Run. HTML saved.");
                    _currentTool = MainTools.None;

                    if ((bool)runList.Rows[2].ItemArray[0])
                    {
                        runPANTHER(_sites[2]);
                    }
                    else if ((bool)runList.Rows[3].ItemArray[0])
                    {
                        runPROVEAN(_sites[3]);
                    }
                }
            }
            else if (_currentTool == MainTools.Panther)
            {
                if (webBrowserMain.DocumentTitle.Contains("504") || webBrowserMain.DocumentTitle.Contains("404"))
                {
                    _mainTimer.Stop();
                    int idx = findRowIndex(_tools[2]);
                    runList.Rows[idx].SetField("Status", "Error loading page.");
                    if ((bool)runList.Rows[3].ItemArray[0])
                    {
                        runPROVEAN(_sites[3]);
                    }
                }
                HtmlDocument document = webBrowserMain.Document;
                if (_pantherState == Status.Null)
                {
                    _mainTimer.Stop();
                    int idx = findRowIndex(_tools[2]);
                    Panther.enterTextPanther(this, webBrowserMain);
                    Panther.submitPanther(ref webBrowserMain);
                    runList.Rows[idx].SetField("Status", "Running. HTML to file once complete.");
                    _pantherState = Status.Submitted;
                }
                else if (document.Title != "Please wait...")
                {
                    Utilities.savePage(this, "Panther.html", webBrowserMain.DocumentText, Encoding.GetEncoding(webBrowserMain.Document.Encoding));
                    int idx = findRowIndex(_tools[2]);
                    runList.Rows[idx].SetField("Status", "Run. HTML saved.");
                    _currentTool = MainTools.None;

                    if ((bool)runList.Rows[3].ItemArray[0])
                    {
                        runPROVEAN(_sites[3]);
                    }
                }
            }
            else if (_currentTool == MainTools.MutPred)
            {
                if (webBrowserMain.DocumentTitle.Contains("504") || webBrowserMain.DocumentTitle.Contains("404"))
                {
                    _mainTimer.Stop();
                    int idx = findRowIndex(_tools[4]);
                    runList.Rows[idx].SetField("Status", "Error loading page.");
                    //run next tool
                    _currentTool = MainTools.None;
                    _mutPredState = Status.Null;
                    if ((bool)runList.Rows[1].ItemArray[0])
                    {
                        runMutAssessor(_sites[1]);
                    }
                    else if ((bool)runList.Rows[2].ItemArray[0])
                    {
                        runPANTHER(_sites[2]);
                    }
                    else if ((bool)runList.Rows[3].ItemArray[0])
                    {
                        runPROVEAN(_sites[3]);
                    }
                }
                if (_mutPredState == Status.Null)
                {
                    _mainTimer.Stop();
                    int idx = findRowIndex(_tools[4]);
                    MutPred.enterTextMutPred(this, ref webBrowserMain);
                    //query is submitted from enterText method
                    _mutPredState = Status.Submitted;
                    Utilities.submit(ref webBrowserMain);
                    runList.Rows[idx].SetField("Status", "Run. Results by email.");
                }
                else if (_mutPredState == Status.Submitted)
                {

                    try
                    {
                        HtmlElementCollection collection = webBrowserMain.Document.GetElementsByTagName("p");
                        foreach (HtmlElement element in collection)
                        {
                            if (element.InnerText.StartsWith("Your job has been successfully submitted")) 
                            {
                                string pattern = @".+\s([a-z0-9\-]+)\.";
                                Match m = Regex.Match(element.InnerText, pattern);

                                string jobid = m.Groups[1].Value.ToString();
                                string url = @"http://mutpred1.mutdb.org/cgi-bin/mutpred_output.py?jobid=" + jobid;
                                Utilities.saveTextFile(this, "MutPredURL.txt", url);
                                break;
                            }

                        }

                    }
                    catch
                    {

                    }


                    //run next tool
                    _currentTool = MainTools.None;
                    _mutPredState = Status.Null;
                    if ((bool)runList.Rows[1].ItemArray[0])
                    {
                        runMutAssessor(_sites[1]);
                    }
                    else if ((bool)runList.Rows[2].ItemArray[0])
                    {
                        runPANTHER(_sites[2]);
                    }
                    else if ((bool)runList.Rows[3].ItemArray[0])
                    {
                        runPROVEAN(_sites[3]);
                    }
                }
            }
            else if (_currentTool == MainTools.Snap)
            {
                if (webBrowserMain.DocumentTitle.Contains("504") || webBrowserMain.DocumentTitle.Contains("404") || webBrowserMain.DocumentTitle.Contains("Navigation"))
                {
                    _mainTimer.Stop();
                    int idx = findRowIndex(_tools[6]);
                    runList.Rows[idx].SetField("Status", "Error loading page.");
                }
                else
                {
                    _mainTimer.Stop();
                    SNAP2.enterTextSNAP(this, ref webBrowserMain);
                    int idx = findRowIndex(_tools[6]);
                    runList.Rows[idx].SetField("Status", "Run. Results by email.");
                }

                //run next tool
                _currentTool = MainTools.None;

                if ((bool)runList.Rows[4].ItemArray[0])
                {
                    runMutPred(_sites[4]);
                }
                else if ((bool)runList.Rows[1].ItemArray[0])
                {
                    runMutAssessor(_sites[1]);
                }
                else if ((bool)runList.Rows[2].ItemArray[0])
                {
                    runPANTHER(_sites[2]);
                }
                else if ((bool)runList.Rows[3].ItemArray[0])
                {
                    runPROVEAN(_sites[3]);
                }

            }
            else if (_currentTool == MainTools.Provean)
            {
                if (webBrowserMain.DocumentTitle.Contains("504") || webBrowserMain.DocumentTitle.Contains("404"))
                {
                    _mainTimer.Stop();
                    int idx = findRowIndex(_tools[3]);
                    runList.Rows[idx].SetField("Status", "Error loading page.");
                }
                else if (_proveanState == Status.Null)
                {
                    _mainTimer.Stop();
                    Provean.enterTextPROVEAN(this, ref webBrowserMain);
                    _proveanState = Status.Submitted;
                }
                else if (_proveanState == Status.Ready)
                {
                    Utilities.savePage(this, "PROVEAN.html", webBrowserMain.DocumentText, Encoding.GetEncoding(webBrowserMain.Document.Encoding));
                    int idx = findRowIndex(_tools[3]);
                    runList.Rows[idx].SetField("Status", "Run. HTML saved.");
                    _currentTool = MainTools.None;
                    _proveanState = Status.Null;
                }
                else if (webBrowserMain.Document.Title.Contains("results"))
                {
                    string link = webBrowserMain.Url.AbsoluteUri.ToString();
                    string[] splitUrl = link.Split('?');
                    string jobID = splitUrl[1];
                    string resultsLink = "http://provean.jcvi.org/protein_batch_view_table.php?" + jobID;
                    webBrowserMain.Navigate(resultsLink);
                    _proveanState = Status.Ready;
                }
            }
        }

        private void webBrowserPP2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (_pp2State == Status.Null)
            {
                PP2.enterTextPolyPhen2(this, ref webBrowserPP2);
                _pp2State = Status.Submitted;
            }
            else if (_pp2State == Status.Submitted)
            {
                HtmlElementCollection collection = webBrowserPP2.Document.GetElementsByTagName("form");
                if (collection.Count == 2)
                {
                    HtmlElementCollection input = collection[1].GetElementsByTagName("input");
                    int index = input.Count - 1;

                    _pp2JobID = input[index].GetAttribute("value");
                }

                _pp2State = Status.Running;
                _pp2Timer.Start();
            }
            else if (_pp2State == Status.Running)
            {
                HtmlElementCollection links = webBrowserPP2.Document.GetElementsByTagName("a");
                string results = "";
                string log = "";
                foreach (HtmlElement link in links)
                {
                    string url = link.GetAttribute("href");
                    string end = _pp2JobID + ".html";
                    if (url.EndsWith(end))
                    {
                        results = url;
                        _pp2Timer.Stop();
                    }
                    else if (url.EndsWith(_pp2JobID + ".log"))
                    {
                        url = "";
                        log = url;
                    }
                    else
                    {
                        url = "";
                    }
                }
                if (results != "")
                {
                    _pp2State = Status.Ready;
                    webBrowserPP2.Navigate(results);
                }
                if (log != "")
                {
                    using (WebClient wc = new WebClient())
                    {
                        string error = wc.DownloadString(log);
                        Utilities.saveTextFile(this, "PolyPhen2Error.txt", error);
                    }
                }
            }
            else if (_pp2State == Status.Ready)
            {
                Utilities.savePage(this, "PolyPhen2.html", webBrowserPP2.DocumentText, Encoding.GetEncoding(webBrowserPP2.Document.Encoding));
                int idx = findRowIndex(_tools[7]);
                runList.Rows[idx].SetField("Status", "Run. HTML saved.");
            }

        }

        private void webBrowserPMut_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (_pMutState == Status.Null)
            {
                PMut.enterTextPMut(this, ref webBrowserPMut);
                _pMutState = Status.Submitted;

            }
            else if (_pMutState == Status.Submitted)
            {
                _pMutTimer.Start();

                if (webBrowserPMut.DocumentText.Contains("Running"))
                {
                    int idx = findRowIndex(_tools[5]);
                    runList.Rows[idx].SetField("Status", "Running. HTML to file once complete.");
                }

                else if (webBrowserPMut.DocumentText.Contains("In queue"))
                {
                    int idx = findRowIndex(_tools[5]);
                    runList.Rows[idx].SetField("Status", "Queued. HTML to file once complete.");
                }


                else if (webBrowserPMut.DocumentText.Contains("Completed"))
                {
                    _pMutTimer.Stop();
                    int idx = findRowIndex(_tools[5]);
                    runList.Rows[idx].SetField("Status", "Run. HTML saved.");
                    Utilities.savePage(this, "PMut.html", webBrowserPMut.DocumentText, Encoding.GetEncoding(webBrowserPMut.Document.Encoding));
         
                }
                   

                }
                else if (_pMutState == Status.Error)
                {
                    int idx = findRowIndex(_tools[5]);
                    runList.Rows[idx].SetField("Status", "Error!. Error in transcript/Server busy.");
                }

                
               
                else if (_pMutURL != "")
                {
                    webBrowserPMut.Navigate(_pMutURL);
                    _pMutState = Status.Ready;
                }

                else
                {
                    int idx = findRowIndex(_tools[5]);
                    runList.Rows[idx].SetField("Status", "Error loading page.");
                }
            }
        }
    }

        
    


 #endregion
            
        
    

                
                
                //HtmlElementCollection panel = webBrowserPMut.Document.GetElementsByTagName("div");
                //foreach (HtmlElement panelElement in panel)
                //{
                //    if (panelElement.GetAttribute("className") == "Computation status")
                //    {
                //        HtmlElementCollection spanElement;
                //        spanElement = panelElement.Document.GetElementsByTagName("span");

                //        foreach (HtmlElement span in spanElement)
                //        {
                //            if (span.GetAttribute("classname") == "badge")
                //            {
                                

                //                if (span.InnerText == "In queue")
                //                {
                //                    int idx = findRowIndex(_tools[5]);
                //                    runList.Rows[idx].SetField("Status", "In queue. Waiting to run.");
                //                    //_pMutURL = element.GetAttribute("href");
                //                    _pMutState = Status.Running;
                //                    _pMutTimer.Start();
                //                    break;

                //                }
                //                else if (span.InnerText == "Running")
                //                {
                //                    int idx = findRowIndex(_tools[5]);
                //                    runList.Rows[idx].SetField("Status", "Running. CSV to file once complete.");
                //                    //_pMutURL = element.GetAttribute("href");
                //                    _pMutState = Status.Running;
                //                    _pMutTimer.Start();
                //                    break;
                //                }

                //                else if (span.InnerText == "Completed")
                //                {

                //                    int idx = findRowIndex(_tools[5]);
                //                    runList.Rows[idx].SetField("Status", "Completed. CSV saved to file");
                //                    //_pMutURL = element.GetAttribute("href");

                //                    HtmlElementCollection panel = webBrowserPMut.Document.GetElementsByTagName("div");
                //                        foreach (HtmlElement element in panel)
                //                        {
                //                            if (element.GetAttribute("className") == "panel-heading")
                //                            {
                //                                HtmlElementCollection span = element.Document.GetElementsByTagName("span");
                //                                foreach (HtmlElement download in span)
                //                                {
                //                                    if (download.GetAttribute("className") == "pull-right")
                //                                    {
                //                                        HtmlElementCollection url = download.Document.GetElementsByTagName("a");
                //                                        foreach (HtmlElement link in url)
                //                                            {
                                                               

                //                                            }
                //                                    }

                //                                }

                //                            }

                //                        }
                
                //                    HtmlElementCollection download = webBrowserPMut.Document.GetElementsByTagName("a");
                //                    foreach (HtmlElement element in download)
                //                    {                                     
                //                       string link = element.GetAttribute("href");
                                        
                //                       if (link.Contains("/predictions.csv"))
                //                        {
                //                           using (WebClient wc = new WebClient())
                //                        {
                //                            wc.DownloadFile(link, _path);
                //                        }
                                        

                //                        break;
                //                    }
                //               // }

                //            //}

                //            else if (_pMutURL != "")
                //            {
                //                webBrowserPMut.Navigate(_pMutURL);
                //                _pMutState = Status.Ready;
                //            }
                //            else
                //            {
                //                int idx = findRowIndex(_tools[5]);
                //                runList.Rows[idx].SetField("Status", "Error loading page.");
                //            }



      //                  }
      //              }
      //          }
      //      }
      //// }
   // }
//}
        
                
       

        

             