using System;
using System.Windows.Forms;
using GILibrary;

namespace WFA_GroupImages
{
    public partial class GIFrom : Form
    {
        public GIFrom()
        {
            InitializeComponent();
        }

        private void btnGroupFolder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFrom.Text))
            {
                MessageBox.Show("Choose folder of images!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtTo.Text))
            {
                MessageBox.Show("Select PDF file!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var GroupImages = new GroupImageLib();
            try
            {
                GroupImages.FindDirectory(txtFrom.Text);
                GroupImages.Move(txtFrom.Text, txtTo.Text);
                string msg = GroupImages.ErrorMassage == "" ? "Group Images successfully!" : GroupImages.ErrorMassage;
                MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFrom.Text))
            {
                MessageBox.Show("Choose folder of images!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtTo.Text))
            {
                MessageBox.Show("Select PDF file!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var GroupImages = new GroupImageLib();
            try
            {
                GroupImages.Margin = 0;
                GroupImages.AddDirectory(txtFrom.Text);
                GroupImages.GILModel.FilePath = txtTo.Text;

                GroupImages.CheckPaths(txtTo.Text);
                GroupImages.GenerateFolderPdf(GroupImages.I2PModel);

                listPages _lp = new listPages(GroupImages.GILModel.listPaths);
                _lp.Show();

                string msg = GroupImages.ErrorMassage == "" ? "PDF file successfully generated!" : GroupImages.ErrorMassage;
                MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
