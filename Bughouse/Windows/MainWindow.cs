using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Collections;

namespace Bughouse
{
    

    public partial class MainWindow : Form
    {
        ChatBoxWindow cb = new ChatBoxWindow();
        Profile profile = new Profile();
        AboutForm ab;
        HostGameWindow hg;
        ChessboardWindow mg;
        
        
        
        public MainWindow()
        {
            InitializeComponent();
            
            profile.Owner = this;
            profile.Location = new Point(500, 300);
            profile.Show();
            profile.Closing += new System.ComponentModel.CancelEventHandler(OnClose);
            
            this.WindowState = FormWindowState.Maximized; 
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void OnClose(object sender, EventArgs e)
        {
            if (profile.Select1.Length > 0)
            {
                this.Text = "House of Bugs - " + profile.Select1;
            }

            if (profile.Select1.Length > 0)
            {
                cb.Show();
                cb.SetText(profile.Select1.ToString() + " has logged in.");
                cb.SetText(Environment.NewLine);
            }

        }


        private void chooseProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            profile = new Profile();
            profile.Owner = this;
            profile.Location = new Point(500, 300);
            profile.Show();
            profile.Closing += new System.ComponentModel.CancelEventHandler(OnClose);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IntializeServer();

            hg = new HostGameWindow();
            hg.Owner = this;
            if (hg.ShowDialog() == DialogResult.OK)
            {
                mg = new ChessboardWindow();
                mg.Owner = this;
                mg.Location = new Point(30, 100);
                mg.Show();
            }
        }


        private void rulesOfBughouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "firefox";
            proc.StartInfo.Arguments = "http://en.wikipedia.org/wiki/Bughouse_chess";
            proc.Start();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ab = new AboutForm();
            ab.Owner = this;
            ab.Location = new Point(200, 200);
            ab.Show();
        }

        private void chatBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cb.Show();
        }

        private void joinAServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionWindow f = new ConnectionWindow();

            if (f.ShowDialog() == DialogResult.OK)
            {
                port = int.Parse(f.Port.Text);
                ConnectToServer(f.TextBox1.Text);
                mg = new ChessboardWindow();
                mg.Owner = this;
                mg.Location = new Point(30, 100);
                mg.Show();
            }
        }

   

    }
}
