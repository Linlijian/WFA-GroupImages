using System;
using GILibrary;
using System.Windows.Forms;

namespace WFA_GroupImages
{
    public partial class CustomFrom : Form
    {
        public CustomFrom()
        {
            InitializeComponent();
        }

        public static string _searchPatterns;
        public static string _btn;
        private void btnSaveCustom_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustum.Text))
            {
                MessageBox.Show("Input Image name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var GroupImages = new GroupImageLib();
            GroupImages.SearchPatterns(txtCustum.Text);

            _searchPatterns = GroupImages._searchPatterns;
            _btn = "CustomPDF";
            GIFrom _GIFrom = new GIFrom();
            _GIFrom.Update();

            this.Close();
        }
    }
}
