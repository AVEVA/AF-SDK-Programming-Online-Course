namespace Exercise3_Solution
{
    partial class NotificationsForm
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
            this.btnClose = new System.Windows.Forms.Button();
            this.lblCity = new System.Windows.Forms.Label();
            this.lboxNotificationRules = new System.Windows.Forms.ListBox();
            this.btnView = new System.Windows.Forms.Button();
            this.tbEndTime = new System.Windows.Forms.TextBox();
            this.tbStartTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.gridNotificationInstances = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridNotificationInstances)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.Location = new System.Drawing.Point(323, 518);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(129, 33);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(41, 30);
            this.lblCity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(294, 17);
            this.lblCity.TabIndex = 1;
            this.lblCity.Text = "Show Notifications Rules for the selected city:";
            // 
            // lboxNotificationRules
            // 
            this.lboxNotificationRules.FormattingEnabled = true;
            this.lboxNotificationRules.ItemHeight = 16;
            this.lboxNotificationRules.Location = new System.Drawing.Point(45, 64);
            this.lboxNotificationRules.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lboxNotificationRules.Name = "lboxNotificationRules";
            this.lboxNotificationRules.Size = new System.Drawing.Size(668, 116);
            this.lboxNotificationRules.TabIndex = 2;
            this.lboxNotificationRules.SelectedIndexChanged += new System.EventHandler(this.lboxNotificationRules_SelectedIndexChanged);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(553, 199);
            this.btnView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(161, 63);
            this.btnView.TabIndex = 0;
            this.btnView.Text = "View Notification Instances";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // tbEndTime
            // 
            this.tbEndTime.Location = new System.Drawing.Point(277, 238);
            this.tbEndTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbEndTime.Name = "tbEndTime";
            this.tbEndTime.Size = new System.Drawing.Size(185, 22);
            this.tbEndTime.TabIndex = 8;
            this.tbEndTime.Text = "*";
            this.tbEndTime.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TimeBox_KeyUp);
            // 
            // tbStartTime
            // 
            this.tbStartTime.Location = new System.Drawing.Point(45, 238);
            this.tbStartTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbStartTime.Name = "tbStartTime";
            this.tbStartTime.Size = new System.Drawing.Size(185, 22);
            this.tbStartTime.TabIndex = 9;
            this.tbStartTime.Text = "y";
            this.tbStartTime.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TimeBox_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(273, 218);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "End Time";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 218);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "Start Time";
            // 
            // gridNotificationInstances
            // 
            this.gridNotificationInstances.AllowUserToAddRows = false;
            this.gridNotificationInstances.AllowUserToDeleteRows = false;
            this.gridNotificationInstances.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridNotificationInstances.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridNotificationInstances.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.gridNotificationInstances.Location = new System.Drawing.Point(45, 288);
            this.gridNotificationInstances.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridNotificationInstances.Name = "gridNotificationInstances";
            this.gridNotificationInstances.Size = new System.Drawing.Size(669, 210);
            this.gridNotificationInstances.TabIndex = 10;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "StartTime";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 160;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "EndTime";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 160;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "DefaultAttribute";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 110;
            // 
            // NotificationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 577);
            this.Controls.Add(this.gridNotificationInstances);
            this.Controls.Add(this.tbEndTime);
            this.Controls.Add(this.tbStartTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lboxNotificationRules);
            this.Controls.Add(this.lblCity);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnClose);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(634, 383);
            this.Name = "NotificationsForm";
            this.Text = "View Notifications";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.gridNotificationInstances)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.ListBox lboxNotificationRules;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.TextBox tbEndTime;
        private System.Windows.Forms.TextBox tbStartTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView gridNotificationInstances;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}