using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bughouse
{
    public partial class Profile : Form
    {
        String newname = "";
        String select = "";

        public String Select1
        {
            get { return select; }
            set { select = value; }
        }
        public Profile()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newname = textBox1.Text;
            textBox1.Text = "";
            if (newname.Length > 0)
            {
                listBox1.Items.Add(newname);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("You don't have an item selected", "Nothing Deleted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("You don't have an item selected", "Nothing Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                textBox2.Text = listBox1.SelectedItem.ToString();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                select = textBox2.Text;
                this.Close();
            }
        }


    }
}
