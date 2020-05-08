using System;
using System.Windows.Forms;
using GILibrary;
using static GILibrary.GroupImageLib;

namespace WFA_GroupImages
{
    public partial class GIFrom : Form
    {
        FSLibrary state = new FSLibrary();
        CSFrom cs;

        public GIFrom()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void btnGroupFolder_Click(object sender, EventArgs e)
        {
            ReloadFrom();
            if (string.IsNullOrEmpty(txtFrom.Text))
            {
                if (state.state.isDisableMsg)
                {
                    lblResult.Text = "Choose folder of images!";
                    return;
                }
                MessageBox.Show("Choose folder of images!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtTo.Text) && !state.state.isSelectSingle)
            {
                if (state.state.isDisableMsg)
                {
                    lblResult.Text = "Select PDF file!";
                    return;
                }
                MessageBox.Show("Select PDF file!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            var GroupImages = new GroupImageLib();
            try
            {
                if (state.state.isSelectSingle) txtTo.Text = txtFrom.Text;

                GroupImages.FindDirectory(txtFrom.Text);
                GroupImages.Move(txtFrom.Text, txtTo.Text);
                string msg = GroupImages.ErrorMassage == "" ? "Group Images successfully!" : GroupImages.ErrorMassage;

                if (state.state.isDisableMsg)
                {
                    lblResult.Text = msg;
                    return;
                }
                MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (state.state.isDisableMsg)
                {
                    lblResult.Text = ex.Message;
                    return;
                }
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            ReloadFrom();

            if (string.IsNullOrEmpty(txtFrom.Text))
            {
                if (state.state.isDisableMsg)
                {
                    lblResult.Text = "Choose folder of images!";
                    return;
                }
                MessageBox.Show("Choose folder of images!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtTo.Text) && !state.state.isSelectSingle)
            {
                if (state.state.isDisableMsg)
                {
                    lblResult.Text = "Select PDF file!";
                    return;
                }
                MessageBox.Show("Select PDF file!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var GroupImages = new GroupImageLib();
            try
            {
                if (state.state.isSelectSingle) txtTo.Text = txtFrom.Text;

                GroupImages.Margin = 0;
                GroupImages.AddDirectory(txtFrom.Text);
                GroupImages.GILModel.FilePath = txtTo.Text;

                GroupImages.CheckPaths(txtTo.Text);

                if (state.state.isSorting)
                {
                    var I2PModel = GroupImages.I2PModel.SortGI();
                    GroupImages.GenerateFolderPdf(I2PModel, true);
                }
                else
                {
                    GroupImages.GenerateFolderPdf(GroupImages.I2PModel);
                }
                

                listPages _lp = new listPages(GroupImages.GILModel.listPaths);
                _lp.Show();

                string msg = GroupImages.ErrorMassage == "" ? "PDF file successfully generated!" : GroupImages.ErrorMassage;

                if (state.state.isDisableMsg)
                {
                    lblResult.Text = msg;
                    return;
                }
                MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (state.state.isDisableMsg)
                {
                    lblResult.Text = ex.Message;
                    return;
                }
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.AddOwnedForm(cs);
            cs.Show();
            cs.SendToBack();
            this.BringToFront();
        }
        private void ReloadFrom()
        {
            state.loadConfig();
        }        
        private void btnCustom_Click(object sender, EventArgs e)
        {
            cs = new CSFrom();
            if (Application.OpenForms[cs.Name] == null)
            {
                this.Visible = true;
                cs.Show();
            }
            else
            {
                Application.OpenForms[cs.Name].Focus();
            }
        }

        private void GIFrom_Load(object sender, EventArgs e)
        {
            ReloadFrom();
        }
    }
}
