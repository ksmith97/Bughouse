using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace Bughouse
{
    public partial class ChessboardWindow : Form
    {
        public ChessboardWindow()
        {            
            InitializeComponent();
            
            Application.DoEvents();
        }
    }
}
