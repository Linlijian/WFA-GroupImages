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
            state.writeConfig(chkSelect.Checked, chkDisable.Checked, chkSort.Checked, chkMulti.Checked);

            this.Close();

            //restart app
            Application.Restart();
            Environment.Exit(0);
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
                chkMulti.Checked = state.state.isMulti;
                //if(chkMulti.Checked) chkSort.Checked = false;
            }

            this.Focus();
            this.Activate();
        }
    }
}
