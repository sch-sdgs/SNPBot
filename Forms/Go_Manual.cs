using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SNPBot
{
    public partial class Go_Manual : Form
    {
        public Go_Manual()
        {
            InitializeComponent();
        }

        private string _terms = "";
        public string Terms 
        {
            get
            {
                return _terms;
            }
            private set
            {
                _terms = value;
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            string terms = textBoxGOs.Text;

            string pattern = @"^GO:\d+(?:(?:\s|\n)GO:\d+){0,}";

            bool match = Regex.IsMatch(terms, pattern);

            if (!match)
            {
                MessageBox.Show("The GO terms entered do not match the required format. \nPlease make sure they are in the format \"GO:Number\".");
                return;
            }
            else
            {
                Terms = terms;
                this.Hide();
            }
        }
    }
}
