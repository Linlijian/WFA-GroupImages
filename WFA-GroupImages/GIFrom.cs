using System;
using System.Windows.Forms;
using GILibrary;

namespace WFA_GroupImages
{
    public partial class GIFrom : Form
    {
        bool custom = false;

        public GIFrom()
        {
            InitializeComponent();
        }

        private void GIFrom_Load(object sender, EventArgs e)
        {
            if (custom)
            {
                btnGeneratePDF.Text = CustomFrom._btn;
                txtCustom.Text = CustomFrom._searchPatterns;
            }
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

            if (custom)
            {
                CustomGeneratePDF();
                return;
            }
               

            var GroupImages = new GroupImageLib();
            try
            {
                GroupImages.Margin = 0;
                GroupImages.AddDirectory(txtFrom.Text);
                GroupImages.FilePath = txtTo.Text;

                GroupImages.CheckPaths(txtTo.Text);
                GroupImages.GenerateFolderPdf(GroupImages.I2PModel);

                listPages _lp = new listPages(GroupImages.listPaths);
                _lp.Show();

                string msg = GroupImages.ErrorMassage == "" ? "PDF file successfully generated!" : GroupImages.ErrorMassage;
                MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCustom_Click(object sender, EventArgs e)
        {
            CustomFrom cs = new CustomFrom();
            custom = true;
            cs.Show();
        }

        private void CustomGeneratePDF()
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
            txtCustom.Text = CustomFrom._searchPatterns;
            try
            {
                GroupImages.Margin = 0;
                GroupImages.AddDirectory(txtFrom.Text, CustomFrom._searchPatterns, custom);
                GroupImages.FilePath = txtTo.Text;

                GroupImages.CheckPaths(txtTo.Text);
                GroupImages.GenerateFolderPdf(GroupImages.I2PModel, custom);
                //if(GroupImages.ErrorMassage == "")
                //{
                //    GroupImages.GenerateFolderPdf(GroupImages.I2PModel);
                //}

                listPages _lp = new listPages(GroupImages.listPaths);
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
