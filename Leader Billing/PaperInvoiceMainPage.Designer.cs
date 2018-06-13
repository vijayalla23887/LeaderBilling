namespace Leader
{
    partial class PaperInvoiceMainPage
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
            this.InvoiceStockEntry = new System.Windows.Forms.Button();
            this.StockDetails = new System.Windows.Forms.Button();
            this.JobEntry = new System.Windows.Forms.Button();
            this.btnInvoiceEntryDetails = new System.Windows.Forms.Button();
            this.btnJobEntryDetails = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InvoiceStockEntry
            // 
            this.InvoiceStockEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InvoiceStockEntry.Location = new System.Drawing.Point(286, 109);
            this.InvoiceStockEntry.Name = "InvoiceStockEntry";
            this.InvoiceStockEntry.Size = new System.Drawing.Size(183, 34);
            this.InvoiceStockEntry.TabIndex = 0;
            this.InvoiceStockEntry.Text = "Invoice Stock Entry";
            this.InvoiceStockEntry.UseVisualStyleBackColor = true;
            this.InvoiceStockEntry.Click += new System.EventHandler(this.InvoiceStockEntry_Click);
            // 
            // StockDetails
            // 
            this.StockDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StockDetails.Location = new System.Drawing.Point(286, 171);
            this.StockDetails.Name = "StockDetails";
            this.StockDetails.Size = new System.Drawing.Size(183, 34);
            this.StockDetails.TabIndex = 1;
            this.StockDetails.Text = "Stock Details";
            this.StockDetails.UseVisualStyleBackColor = true;
            this.StockDetails.Click += new System.EventHandler(this.StockDetails_Click);
            // 
            // JobEntry
            // 
            this.JobEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JobEntry.Location = new System.Drawing.Point(286, 236);
            this.JobEntry.Name = "JobEntry";
            this.JobEntry.Size = new System.Drawing.Size(183, 34);
            this.JobEntry.TabIndex = 2;
            this.JobEntry.Text = "Job Entry";
            this.JobEntry.UseVisualStyleBackColor = true;
            this.JobEntry.Click += new System.EventHandler(this.JobEntry_Click);
            // 
            // btnInvoiceEntryDetails
            // 
            this.btnInvoiceEntryDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInvoiceEntryDetails.Location = new System.Drawing.Point(539, 109);
            this.btnInvoiceEntryDetails.Name = "btnInvoiceEntryDetails";
            this.btnInvoiceEntryDetails.Size = new System.Drawing.Size(183, 34);
            this.btnInvoiceEntryDetails.TabIndex = 3;
            this.btnInvoiceEntryDetails.Text = "Invoice Entry Details";
            this.btnInvoiceEntryDetails.UseVisualStyleBackColor = true;
            this.btnInvoiceEntryDetails.Click += new System.EventHandler(this.btnInvoiceEntryDetails_Click);
            // 
            // btnJobEntryDetails
            // 
            this.btnJobEntryDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJobEntryDetails.Location = new System.Drawing.Point(539, 236);
            this.btnJobEntryDetails.Name = "btnJobEntryDetails";
            this.btnJobEntryDetails.Size = new System.Drawing.Size(183, 34);
            this.btnJobEntryDetails.TabIndex = 4;
            this.btnJobEntryDetails.Text = "Job Entry Details";
            this.btnJobEntryDetails.UseVisualStyleBackColor = true;
            this.btnJobEntryDetails.Click += new System.EventHandler(this.btnJobEntryDetails_Click);
            // 
            // PaperInvoiceMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 430);
            this.Controls.Add(this.btnJobEntryDetails);
            this.Controls.Add(this.btnInvoiceEntryDetails);
            this.Controls.Add(this.JobEntry);
            this.Controls.Add(this.StockDetails);
            this.Controls.Add(this.InvoiceStockEntry);
            this.Name = "PaperInvoiceMainPage";
            this.Text = "MainPage";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button InvoiceStockEntry;
        private System.Windows.Forms.Button StockDetails;
        private System.Windows.Forms.Button JobEntry;
        private System.Windows.Forms.Button btnInvoiceEntryDetails;
        private System.Windows.Forms.Button btnJobEntryDetails;
    }
}