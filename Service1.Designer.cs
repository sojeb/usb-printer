namespace PrintingSerivce
{
    partial class Service1
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.a4PrintDocument = new System.Drawing.Printing.PrintDocument();
            this.thermalPrintDocument = new System.Drawing.Printing.PrintDocument();
            // 
            // a4PrintDocument
            // 
            this.a4PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.a4PrintDocument_PrintPage);
            // 
            // thermalPrintDocument
            // 
            this.thermalPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.thermalPrintDocument_PrintPage);
            // 
            // Service1
            // 
            this.ServiceName = "Service1";

        }

        #endregion

        private System.Drawing.Printing.PrintDocument a4PrintDocument;
        private System.Drawing.Printing.PrintDocument thermalPrintDocument;
    }
}
