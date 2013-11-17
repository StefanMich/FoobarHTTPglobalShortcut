using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using Stufkan.Forms;
using HtmlAgilityPack;

namespace FoobarHTTPGlobalShortcut
{

    public partial class Main : Form
    {
        List<GlobalHotkey> hotkeys;
        


        /// <summary>
        /// Initializes a new instance of the <see cref="Main"/> class.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        public Main(string prefix)
        {
            InitializeComponent();

            notifyIcon1.Icon = Properties.Resources.icon;
            Properties.Settings.Default.Prefix = prefix;
#if debug
            string http = "http://";
            string ip = "192.168.1.3";
            ip = "localhost";
            string port = "8888";
            string template = "default";

            prefix = http + ip + ":" + port + "/" + template + "/";
#endif
            hotkeys = new List<GlobalHotkey>();
            hotkeys.Add(new GlobalHotkey("Play", Constants.ALT + Constants.CTRL, Keys.P, this));
            hotkeys.Add(new GlobalHotkey("Random", Constants.ALT + Constants.CTRL, Keys.R, this));
            hotkeys.Add(new GlobalHotkey("Next", Constants.ALT + Constants.CTRL, Keys.N, this));

        }

        protected override void WndProc(ref Message m)
        {

            if (m.Msg == Constants.WM_HOTKEY_MSG_ID)

                switch (GetKey(m.LParam))
                {
                    case Keys.P:
                        PlayPause_Click(this, EventArgs.Empty);
                        break;
                    case Keys.N:
                        Next_Click(this, EventArgs.Empty);
                        break;
                    case Keys.R:
                        Random_Click(this, EventArgs.Empty);
                        break;

                }

            base.WndProc(ref m);

        }

        private Keys GetKey(IntPtr LParam)
        {
            return (Keys)((LParam.ToInt32()) >> 16); // not all of the parenthesis are needed, I just found it easier to see what's happening
        }

        
 
        private void PlayPause_Click(object sender, EventArgs e)
        {
            HttpController.webrequest("?cmd=PlayOrPause&param1=");
        }

        private void Random_Click(object sender, EventArgs e)
        {
            HttpController.webrequest("?cmd=StartRandom&param1=");
        }

        private void Next_Click(object sender, EventArgs e)
        {
            HttpController.webrequest("?cmd=StartNext&param1=");
        }

        private void Mute_Click(object sender, EventArgs e)
        {
            HttpController.webrequest("?cmd=VolumeMuteToggle");
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            HttpController.webrequest("?cmd=Volume&param1=" + numericUpDown1.Value);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var hk in hotkeys)
            {
                if (hk.Register())
                    Console.WriteLine(hk.Name + " successfully registered");
                else
                    Console.WriteLine("Error registering hotkey \"" + hk.Name + "\"");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var hk in hotkeys)
            {
                if (!hk.Unregister())
                    Console.WriteLine("Failed to unregister " + hk.Name + " hotkey");
            }
        }

        private void btnChangeServer_Click(object sender, EventArgs e)
        {
            Choose_url url = new Choose_url();
            url.ShowDialog();
            if (url.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                Properties.Settings.Default.Prefix = url.Url;
                Properties.Settings.Default.Save();
                
            }
        }

        private void Main_Resize(object sender, EventArgs e)
        {

            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                this.Hide();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HtmlAgilityPack.HtmlDocument page = HttpController.getPage();

            HtmlNodeCollection nodes = HttpController.getNodes(page);
            nodes.Remove(nodes.Last());

            foreach (var item in nodes)
            {

                Console.WriteLine(Track.parseNode(item));
            }
        }

    }
}