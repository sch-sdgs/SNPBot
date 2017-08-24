using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SNPBot
{
    public partial class PickID : Form
    {
        List<string> _idList = new List<string>();
        string _id = "";

        public string ID
        {
            get
            {
                return _id;
            }
            private set
            {
                _id = value;
            }
        }

        public PickID(string IDs)
        {
            InitializeComponent();

            string[] list = IDs.Split(new char[] { ' ' });
            _idList = list.ToList<string>();

        }

        private void PickID_Load(object sender, EventArgs e)
        {
            foreach (string id in _idList)
            {
                RadioButton rb = new RadioButton();
                rb.Text = id;
                rb.CheckedChanged += new EventHandler(rbCheckChanged);
                rb.Checked = true;

                flowLayoutPanelIDs.Controls.Add(rb);
            }

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (_id == "")
            {
                MessageBox.Show("Please select and identifier");
            }
            else
            {
                this.Hide();
            }
        }

        private void rbCheckChanged(object sender, EventArgs e)
        {
            RadioButton rb= (RadioButton)sender;
            if (rb.Checked)
            {
                _id = rb.Text;
            }

        }
    }
}
