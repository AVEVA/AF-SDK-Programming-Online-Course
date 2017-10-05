namespace Exercise2_Solution
{
    partial class AttributesForm
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
            this.afViewControl1 = new OSIsoft.AF.UI.AFViewControl();
            this.lblElement = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // afViewControl1
            // 
            this.afViewControl1.AccessibleDescription = "View Control for displaying AF objects";
            this.afViewControl1.AccessibleName = "View Control";
            this.afViewControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.afViewControl1.BackColor = System.Drawing.Color.Transparent;
            this.afViewControl1.DisplayPathLabel = false;
            this.afViewControl1.HelpContext = ((long)(0));
            this.afViewControl1.Location = new System.Drawing.Point(36, 25);
            this.afViewControl1.MinimumSize = new System.Drawing.Size(200, 200);
            this.afViewControl1.Name = "afViewControl1";
            this.afViewControl1.Size = new System.Drawing.Size(879, 361);
            this.afViewControl1.TabIndex = 0;
            // 
            // lblElement
            // 
            this.lblElement.AutoSize = true;
            this.lblElement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElement.Location = new System.Drawing.Point(9, 9);
            this.lblElement.Name = "lblElement";
            this.lblElement.Size = new System.Drawing.Size(52, 13);
            this.lblElement.TabIndex = 1;
            this.lblElement.Text = "Element";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.Location = new System.Drawing.Point(435, 402);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 27);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // AttributesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 441);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblElement);
            this.Controls.Add(this.afViewControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 480);
            this.Name = "AttributesForm";
            this.Text = "View AF Attributes";
            this.TopMost = true;
            this.Resize += new System.EventHandler(this.AttributesForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OSIsoft.AF.UI.AFViewControl afViewControl1;
        private System.Windows.Forms.Label lblElement;
        private System.Windows.Forms.Button btnClose;
    }
}