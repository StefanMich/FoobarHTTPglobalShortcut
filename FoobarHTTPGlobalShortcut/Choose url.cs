using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FoobarHTTPGlobalShortcut
{
    public partial class Choose_url : Form
    {
        private string url;
        
        public string Url
        {
            get { return url; }
        }

        public Choose_url()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (IP.Text != "" && Port.Text != "")
            {
                url = "http://" + IP.Text + ":" + Port.Text + "/default";
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
