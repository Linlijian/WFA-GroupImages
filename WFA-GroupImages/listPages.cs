using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFA_GroupImages
{
    public partial class listPages : Form
    {
        public listPages(string _listPages)
        {
            InitializeComponent();
            textBox1.Text = _listPages;
        }
    }
}
