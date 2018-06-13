namespace Leader
{
    partial class CirculationMainPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CirculationMainPage));
            this.btnAAreaEntryandList = new System.Windows.Forms.Button();
            this.btnCirculationReport = new System.Windows.Forms.Button();
            this.btnDailyCirculationEntryForm = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAAreaEntryandList
            // 
            this.btnAAreaEntryandList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAAreaEntryandList.Location = new System.Drawing.Point(300, 315);
            this.btnAAreaEntryandList.Name = "btnAAreaEntryandList";
            this.btnAAreaEntryandList.Size = new System.Drawing.Size(183, 34);
            this.btnAAreaEntryandList.TabIndex = 41;
            this.btnAAreaEntryandList.Text = "Area Entry and List";
            this.btnAAreaEntryandList.UseVisualStyleBackColor = true;
            this.btnAAreaEntryandList.Click += new System.EventHandler(this.btnAAreaEntryandList_Click);
            // 
            // btnCirculationReport
            // 
            this.btnCirculationReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCirculationReport.Location = new System.Drawing.Point(300, 238);
            this.btnCirculationReport.Name = "btnCirculationReport";
            this.btnCirculationReport.Size = new System.Drawing.Size(183, 34);
            this.btnCirculationReport.TabIndex = 40;
            this.btnCirculationReport.Text = "Circulation Report";
            this.btnCirculationReport.UseVisualStyleBackColor = true;
            this.btnCirculationReport.Click += new System.EventHandler(this.btnCirculationReport_Click);
            // 
            // btnDailyCirculationEntryForm
            // 
            this.btnDailyCirculationEntryForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDailyCirculationEntryForm.Location = new System.Drawing.Point(261, 169);
            this.btnDailyCirculationEntryForm.Name = "btnDailyCirculationEntryForm";
            this.btnDailyCirculationEntryForm.Size = new System.Drawing.Size(280, 34);
            this.btnDailyCirculationEntryForm.TabIndex = 39;
            this.btnDailyCirculationEntryForm.Text = "Daily Circulation Entry Form";
            this.btnDailyCirculationEntryForm.UseVisualStyleBackColor = true;
            this.btnDailyCirculationEntryForm.Click += new System.EventHandler(this.btnDailyCirculationEntryForm_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(284, 52);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(232, 56);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 38;
            this.pictureBox1.TabStop = false;
            // 
            // CirculationMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 484);
            this.Controls.Add(this.btnAAreaEntryandList);
            this.Controls.Add(this.btnCirculationReport);
            this.Controls.Add(this.btnDailyCirculationEntryForm);
            this.Controls.Add(this.pictureBox1);
            this.Name = "CirculationMainPage";
            this.Text = "CirculationMainPage";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAAreaEntryandList;
        private System.Windows.Forms.Button btnCirculationReport;
        private System.Windows.Forms.Button btnDailyCirculationEntryForm;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}