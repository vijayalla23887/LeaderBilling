namespace Leader
{
    partial class ADMainPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ADMainPage));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnAdEntryForm = new System.Windows.Forms.Button();
            this.btnAdDetailsList = new System.Windows.Forms.Button();
            this.btnAdPaymentsList = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(299, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(232, 56);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 34;
            this.pictureBox1.TabStop = false;
            // 
            // btnAdEntryForm
            // 
            this.btnAdEntryForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdEntryForm.Location = new System.Drawing.Point(315, 127);
            this.btnAdEntryForm.Name = "btnAdEntryForm";
            this.btnAdEntryForm.Size = new System.Drawing.Size(183, 34);
            this.btnAdEntryForm.TabIndex = 35;
            this.btnAdEntryForm.Text = "New Ad Entry Form";
            this.btnAdEntryForm.UseVisualStyleBackColor = true;
            this.btnAdEntryForm.Click += new System.EventHandler(this.btnAdEntryForm_Click);
            // 
            // btnAdDetailsList
            // 
            this.btnAdDetailsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdDetailsList.Location = new System.Drawing.Point(315, 198);
            this.btnAdDetailsList.Name = "btnAdDetailsList";
            this.btnAdDetailsList.Size = new System.Drawing.Size(183, 34);
            this.btnAdDetailsList.TabIndex = 36;
            this.btnAdDetailsList.Text = "Ad Details List";
            this.btnAdDetailsList.UseVisualStyleBackColor = true;
            this.btnAdDetailsList.Click += new System.EventHandler(this.btnAdDetailsList_Click);
            // 
            // btnAdPaymentsList
            // 
            this.btnAdPaymentsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdPaymentsList.Location = new System.Drawing.Point(315, 264);
            this.btnAdPaymentsList.Name = "btnAdPaymentsList";
            this.btnAdPaymentsList.Size = new System.Drawing.Size(183, 34);
            this.btnAdPaymentsList.TabIndex = 37;
            this.btnAdPaymentsList.Text = "Ad Payments List";
            this.btnAdPaymentsList.UseVisualStyleBackColor = true;
            this.btnAdPaymentsList.Click += new System.EventHandler(this.btnAdPaymentsList_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(315, 334);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(183, 34);
            this.button1.TabIndex = 38;
            this.button1.Text = "Cash Deposit Entry";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(759, 34);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(110, 18);
            this.linkLabel1.TabIndex = 193;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Admin Report";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(28, 198);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 34);
            this.button2.TabIndex = 194;
            this.button2.Text = "Agent Entry";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ADMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 508);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAdPaymentsList);
            this.Controls.Add(this.btnAdDetailsList);
            this.Controls.Add(this.btnAdEntryForm);
            this.Controls.Add(this.pictureBox1);
            this.Name = "ADMainPage";
            this.Text = "ADMainPage";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnAdEntryForm;
        private System.Windows.Forms.Button btnAdDetailsList;
        private System.Windows.Forms.Button btnAdPaymentsList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button button2;
    }
}