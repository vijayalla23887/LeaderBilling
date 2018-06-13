namespace Leader
{
    partial class DailyCirculationEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DailyCirculationEntry));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gvDailyData = new System.Windows.Forms.DataGridView();
            this.btnShow = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDailyData)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(346, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(291, 55);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 161;
            this.pictureBox1.TabStop = false;
            // 
            // gvDailyData
            // 
            this.gvDailyData.AllowUserToAddRows = false;
            this.gvDailyData.AllowUserToDeleteRows = false;
            this.gvDailyData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvDailyData.Location = new System.Drawing.Point(26, 92);
            this.gvDailyData.Name = "gvDailyData";
            this.gvDailyData.Size = new System.Drawing.Size(950, 378);
            this.gvDailyData.TabIndex = 162;
            // 
            // btnShow
            // 
            this.btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShow.Location = new System.Drawing.Point(189, 51);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 25);
            this.btnShow.TabIndex = 165;
            this.btnShow.Text = "Show";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 16);
            this.label1.TabIndex = 164;
            this.label1.Text = "Date :";
            this.label1.UseMnemonic = false;
            // 
            // dtDate
            // 
            this.dtDate.AllowDrop = true;
            this.dtDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDate.CustomFormat = "";
            this.dtDate.Location = new System.Drawing.Point(83, 21);
            this.dtDate.Name = "dtDate";
            this.dtDate.Size = new System.Drawing.Size(181, 20);
            this.dtDate.TabIndex = 163;
            this.dtDate.Value = new System.DateTime(2017, 5, 18, 0, 0, 0, 0);
            // 
            // DailyCirculationEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 500);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtDate);
            this.Controls.Add(this.gvDailyData);
            this.Controls.Add(this.pictureBox1);
            this.Name = "DailyCirculationEntry";
            this.Text = "DailyCirculationEntry";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDailyData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView gvDailyData;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtDate;
    }
}