namespace Leader
{
    partial class JobDataEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobDataEntry));
            this.gvCountgrid = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbGSMNumber = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtJobDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.txtJobNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPaperDesignType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvCountgrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gvCountgrid
            // 
            this.gvCountgrid.AllowUserToAddRows = false;
            this.gvCountgrid.AllowUserToDeleteRows = false;
            this.gvCountgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvCountgrid.Location = new System.Drawing.Point(582, 12);
            this.gvCountgrid.Name = "gvCountgrid";
            this.gvCountgrid.ReadOnly = true;
            this.gvCountgrid.Size = new System.Drawing.Size(390, 488);
            this.gvCountgrid.TabIndex = 29;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(250, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(232, 74);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            // 
            // cbGSMNumber
            // 
            this.cbGSMNumber.FormattingEnabled = true;
            this.cbGSMNumber.Location = new System.Drawing.Point(156, 247);
            this.cbGSMNumber.Name = "cbGSMNumber";
            this.cbGSMNumber.Size = new System.Drawing.Size(185, 21);
            this.cbGSMNumber.TabIndex = 84;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(55, 248);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 16);
            this.label2.TabIndex = 85;
            this.label2.Text = "GSM Number :";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(156, 117);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(308, 33);
            this.txtDescription.TabIndex = 82;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(64, 127);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 16);
            this.label8.TabIndex = 83;
            this.label8.Text = "Description :";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.Location = new System.Drawing.Point(156, 289);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(183, 22);
            this.txtQuantity.TabIndex = 80;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(88, 291);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 16);
            this.label1.TabIndex = 81;
            this.label1.Text = "Quantity :";
            // 
            // dtJobDate
            // 
            this.dtJobDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtJobDate.Location = new System.Drawing.Point(156, 168);
            this.dtJobDate.Name = "dtJobDate";
            this.dtJobDate.Size = new System.Drawing.Size(218, 20);
            this.dtJobDate.TabIndex = 78;
            this.dtJobDate.Value = new System.DateTime(2017, 5, 12, 17, 27, 11, 0);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(81, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 16);
            this.label7.TabIndex = 79;
            this.label7.Text = "Job Date :";
            this.label7.UseMnemonic = false;
            // 
            // txtJobNumber
            // 
            this.txtJobNumber.BackColor = System.Drawing.Color.White;
            this.txtJobNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtJobNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobNumber.ForeColor = System.Drawing.Color.Crimson;
            this.txtJobNumber.Location = new System.Drawing.Point(122, 25);
            this.txtJobNumber.Name = "txtJobNumber";
            this.txtJobNumber.ReadOnly = true;
            this.txtJobNumber.Size = new System.Drawing.Size(112, 31);
            this.txtJobNumber.TabIndex = 76;
            this.txtJobNumber.Text = "555";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 77;
            this.label3.Text = "Job ID Number :";
            // 
            // cbPaperDesignType
            // 
            this.cbPaperDesignType.FormattingEnabled = true;
            this.cbPaperDesignType.Location = new System.Drawing.Point(156, 210);
            this.cbPaperDesignType.Name = "cbPaperDesignType";
            this.cbPaperDesignType.Size = new System.Drawing.Size(185, 21);
            this.cbPaperDesignType.TabIndex = 74;
            this.cbPaperDesignType.SelectedIndexChanged += new System.EventHandler(this.cbPaperDesignType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(64, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 16);
            this.label4.TabIndex = 75;
            this.label4.Text = "Paper Type :";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(156, 359);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(131, 25);
            this.btnSave.TabIndex = 86;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // JobDataEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 512);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbGSMNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtJobDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtJobNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbPaperDesignType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.gvCountgrid);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "JobDataEntry";
            this.Text = "JobDataEntry";
            ((System.ComponentModel.ISupportInitialize)(this.gvCountgrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvCountgrid;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbGSMNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtJobDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtJobNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbPaperDesignType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSave;
    }
}