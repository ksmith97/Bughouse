using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace Bughouse
{
    public partial class ChatBoxWindow : Form
    {
        Socket mySocket;

        public Socket Socket
        {
            get { return mySocket; }
            set { mySocket = value; }
        }
        delegate void SetTextCallback(String text);
        public ChatBoxWindow(Socket s)
        {
            mySocket = s;
            InitializeComponent();
        }

        public ChatBoxWindow()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.SelectionStart = textBox1.TextLength;
            textBox1.ScrollToCaret();
        }
        public void SetText(String t)
        {
            string text = t;
            // Check if this method is running on a different thread
            // than the thread that created the control.
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke
                    (d, new object[] { text });
            }
            else
            {
                this.textBox1.Text += text;
                Application.DoEvents();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] byData = System.Text.Encoding.ASCII.GetBytes(textBox2.Text);
            mySocket.Send(byData);
            textBox2.Text = "";
        }
    }
}
