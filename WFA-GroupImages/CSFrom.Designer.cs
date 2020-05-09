namespace WFA_GroupImages
{
    partial class CSFrom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkSelect = new System.Windows.Forms.CheckBox();
            this.chkDisable = new System.Windows.Forms.CheckBox();
            this.btnSaveCastom = new System.Windows.Forms.Button();
            this.chkSort = new System.Windows.Forms.CheckBox();
            this.chkMulti = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkSelect
            // 
            this.chkSelect.AutoSize = true;
            this.chkSelect.Font = new System.Drawing.Font("Itim", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSelect.Location = new System.Drawing.Point(22, 41);
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.Size = new System.Drawing.Size(148, 23);
            this.chkSelect.TabIndex = 0;
            this.chkSelect.Text = "Select single part";
            this.chkSelect.UseVisualStyleBackColor = true;
            // 
            // chkDisable
            // 
            this.chkDisable.AutoSize = true;
            this.chkDisable.Font = new System.Drawing.Font("Itim", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisable.Location = new System.Drawing.Point(22, 70);
            this.chkDisable.Name = "chkDisable";
            this.chkDisable.Size = new System.Drawing.Size(172, 23);
            this.chkDisable.TabIndex = 1;
            this.chkDisable.Text = "Disable Massage Box";
            this.chkDisable.UseVisualStyleBackColor = true;
            // 
            // btnSaveCastom
            // 
            this.btnSaveCastom.Font = new System.Drawing.Font("Itim", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveCastom.Location = new System.Drawing.Point(37, 137);
            this.btnSaveCastom.Name = "btnSaveCastom";
            this.btnSaveCastom.Size = new System.Drawing.Size(157, 33);
            this.btnSaveCastom.TabIndex = 2;
            this.btnSaveCastom.Text = "Save";
            this.btnSaveCastom.UseVisualStyleBackColor = true;
            this.btnSaveCastom.Click += new System.EventHandler(this.btnSaveCastom_Click);
            // 
            // chkSort
            // 
            this.chkSort.AutoSize = true;
            this.chkSort.Font = new System.Drawing.Font("Itim", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSort.Location = new System.Drawing.Point(22, 12);
            this.chkSort.Name = "chkSort";
            this.chkSort.Size = new System.Drawing.Size(125, 23);
            this.chkSort.TabIndex = 3;
            this.chkSort.Text = "Sorting Image";
            this.chkSort.UseVisualStyleBackColor = true;
            // 
            // chkMulti
            // 
            this.chkMulti.AutoSize = true;
            this.chkMulti.Font = new System.Drawing.Font("Itim", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMulti.Location = new System.Drawing.Point(22, 99);
            this.chkMulti.Name = "chkMulti";
            this.chkMulti.Size = new System.Drawing.Size(173, 23);
            this.chkMulti.TabIndex = 4;
            this.chkMulti.Text = "Multi Genrate Folder";
            this.chkMulti.UseVisualStyleBackColor = true;
            // 
            // CSFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 184);
            this.Controls.Add(this.chkMulti);
            this.Controls.Add(this.chkSort);
            this.Controls.Add(this.btnSaveCastom);
            this.Controls.Add(this.chkDisable);
            this.Controls.Add(this.chkSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CSFrom";
            this.Text = "CSFrom";
            this.Load += new System.EventHandler(this.CSFrom_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSelect;
        private System.Windows.Forms.CheckBox chkDisable;
        private System.Windows.Forms.Button btnSaveCastom;
        private System.Windows.Forms.CheckBox chkSort;
        private System.Windows.Forms.CheckBox chkMulti;
    }
}