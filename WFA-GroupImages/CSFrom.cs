using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GILibrary;

namespace WFA_GroupImages
{
    public partial class CSFrom : Form
    {
        public CSFrom()
        {
            InitializeComponent();
        }

        private void btnSaveCastom_Click(object sender, EventArgs e)
        {
            FSLibrary state = new FSLibrary();
            state.writeConfig(chkSelect.Checked, chkDisable.Checked, chkSort.Checked);

            this.Close();
        }

        private void CSFrom_Load(object sender, EventArgs e)
        {
            FSLibrary state = new FSLibrary();
            state.loadConfig();

            if (state != null)
            {
                chkDisable.Checked = state.state.isDisableMsg;
                chkSelect.Checked = state.state.isSelectSingle;
                chkSort.Checked = state.state.isSorting;
            }

            this.Focus();
            this.Activate();
        }
    }
}
