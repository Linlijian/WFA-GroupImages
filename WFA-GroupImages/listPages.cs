using GILibrary;
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
        public listPages(List<ResultPathModel> _listPages)
        {
            InitializeComponent();
            // textBox1.Text = _listPages;
            this.resultGrid.DataSource = _listPages;
        }
    }
}
