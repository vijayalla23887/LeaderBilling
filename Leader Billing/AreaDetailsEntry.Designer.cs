namespace Leader
{
    partial class AreaDetailsEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AreaDetailsEntry));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtAreaName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtContactName = new System.Windows.Forms.TextBox();
            this.txtContactPhNumber = new System.Windows.Forms.TextBox();
            this.gvAreaGrid = new System.Windows.Forms.DataGridView();
            this.lblADID = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvAreaGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(301, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(291, 55);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 160;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.txtAreaName);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtContactName);
            this.groupBox2.Controls.Add(this.txtContactPhNumber);
            this.groupBox2.Location = new System.Drawing.Point(35, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(348, 204);
            this.groupBox2.TabIndex = 161;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Area Entry Form";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(153, 154);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtAreaName
            // 
            this.txtAreaName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAreaName.Location = new System.Drawing.Point(106, 29);
            this.txtAreaName.Name = "txtAreaName";
            this.txtAreaName.Size = new System.Drawing.Size(209, 21);
            this.txtAreaName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 140;
            this.label3.Text = "Area Name :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(44, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 15);
            this.label6.TabIndex = 138;
            this.label6.Text = "Ph Num :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 15);
            this.label4.TabIndex = 137;
            this.label4.Text = "Contact Person :";
            // 
            // txtContactName
            // 
            this.txtContactName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactName.Location = new System.Drawing.Point(106, 63);
            this.txtContactName.Name = "txtContactName";
            this.txtContactName.Size = new System.Drawing.Size(208, 21);
            this.txtContactName.TabIndex = 2;
            // 
            // txtContactPhNumber
            // 
            this.txtContactPhNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactPhNumber.Location = new System.Drawing.Point(106, 96);
            this.txtContactPhNumber.Name = "txtContactPhNumber";
            this.txtContactPhNumber.Size = new System.Drawing.Size(209, 21);
            this.txtContactPhNumber.TabIndex = 3;
            // 
            // gvAreaGrid
            // 
            this.gvAreaGrid.AllowUserToAddRows = false;
            this.gvAreaGrid.AllowUserToDeleteRows = false;
            this.gvAreaGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvAreaGrid.Location = new System.Drawing.Point(403, 92);
            this.gvAreaGrid.Name = "gvAreaGrid";
            this.gvAreaGrid.ReadOnly = true;
            this.gvAreaGrid.Size = new System.Drawing.Size(480, 343);
            this.gvAreaGrid.TabIndex = 162;
            this.gvAreaGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvAreaGrid_CellContentClick);
            // 
            // lblADID
            // 
            this.lblADID.AutoSize = true;
            this.lblADID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblADID.ForeColor = System.Drawing.Color.Red;
            this.lblADID.Location = new System.Drawing.Point(105, 31);
            this.lblADID.Name = "lblADID";
            this.lblADID.Size = new System.Drawing.Size(0, 25);
            this.lblADID.TabIndex = 164;
            this.lblADID.UseMnemonic = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(60, 31);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 25);
            this.label11.TabIndex = 163;
            this.label11.Text = "ID :";
            this.label11.UseMnemonic = false;
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(239, 154);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 25);
            this.btnClear.TabIndex = 141;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // AreaDetailsEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 490);
            this.Controls.Add(this.lblADID);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.gvAreaGrid);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "AreaDetailsEntry";
            this.Text = "AreaDetailsEntry";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvAreaGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtContactName;
        private System.Windows.Forms.TextBox txtContactPhNumber;
        private System.Windows.Forms.TextBox txtAreaName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView gvAreaGrid;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblADID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnClear;
    }
}