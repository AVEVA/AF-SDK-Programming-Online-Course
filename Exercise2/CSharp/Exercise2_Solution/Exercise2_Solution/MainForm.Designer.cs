namespace Exercise2_Solution
{
    partial class MainForm
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
            this.piSystemPicker1 = new OSIsoft.AF.UI.PISystemPicker();
            this.afDatabasePicker1 = new OSIsoft.AF.UI.AFDatabasePicker();
            this.afTreeView1 = new OSIsoft.AF.UI.AFTreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGetData = new System.Windows.Forms.Button();
            this.gboxData = new System.Windows.Forms.GroupBox();
            this.rbSummary = new System.Windows.Forms.RadioButton();
            this.rbInterpolatedValues = new System.Windows.Forms.RadioButton();
            this.rbRecordedValues = new System.Windows.Forms.RadioButton();
            this.tbEndTime = new System.Windows.Forms.TextBox();
            this.tbStartTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnViewElement = new System.Windows.Forms.Button();
            this.lboxDataValues = new System.Windows.Forms.ListBox();
            this.gboxWeather = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboxUom = new System.Windows.Forms.ComboBox();
            this.rbHumidity = new System.Windows.Forms.RadioButton();
            this.rbPressure = new System.Windows.Forms.RadioButton();
            this.rbVisibility = new System.Windows.Forms.RadioButton();
            this.rbWindSpeed = new System.Windows.Forms.RadioButton();
            this.rbCloudCover = new System.Windows.Forms.RadioButton();
            this.rbTemperature = new System.Windows.Forms.RadioButton();
            this.lblMetaInfo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboxInterval = new System.Windows.Forms.ComboBox();
            this.gboxData.SuspendLayout();
            this.gboxWeather.SuspendLayout();
            this.SuspendLayout();
            // 
            // piSystemPicker1
            // 
            this.piSystemPicker1.AccessibleDescription = "PI System Picker";
            this.piSystemPicker1.AccessibleName = "PI System Picker";
            this.piSystemPicker1.Cursor = System.Windows.Forms.Cursors.Default;
            this.piSystemPicker1.Location = new System.Drawing.Point(12, 21);
            this.piSystemPicker1.LoginPromptSetting = OSIsoft.AF.UI.PISystemPicker.LoginPromptSettingOptions.Default;
            this.piSystemPicker1.Name = "piSystemPicker1";
            this.piSystemPicker1.ShowBegin = false;
            this.piSystemPicker1.ShowDelete = false;
            this.piSystemPicker1.ShowEnd = false;
            this.piSystemPicker1.ShowFind = false;
            this.piSystemPicker1.ShowImages = false;
            this.piSystemPicker1.ShowList = false;
            this.piSystemPicker1.ShowNavigation = false;
            this.piSystemPicker1.ShowNew = false;
            this.piSystemPicker1.ShowNext = false;
            this.piSystemPicker1.ShowPrevious = false;
            this.piSystemPicker1.ShowProperties = false;
            this.piSystemPicker1.Size = new System.Drawing.Size(320, 21);
            this.piSystemPicker1.TabIndex = 0;
            this.piSystemPicker1.SelectionChange += new OSIsoft.AF.UI.SelectionChangeEventHandler(this.piSystemPicker1_SelectionChange);
            // 
            // afDatabasePicker1
            // 
            this.afDatabasePicker1.AccessibleDescription = "Database Picker";
            this.afDatabasePicker1.AccessibleName = "Database Picker";
            this.afDatabasePicker1.Location = new System.Drawing.Point(12, 62);
            this.afDatabasePicker1.Name = "afDatabasePicker1";
            this.afDatabasePicker1.ShowBegin = false;
            this.afDatabasePicker1.ShowConfigurationDatabase = OSIsoft.AF.UI.ShowConfigurationDatabase.Hide;
            this.afDatabasePicker1.ShowDelete = false;
            this.afDatabasePicker1.ShowEnd = false;
            this.afDatabasePicker1.ShowFind = false;
            this.afDatabasePicker1.ShowImages = false;
            this.afDatabasePicker1.ShowList = false;
            this.afDatabasePicker1.ShowNavigation = false;
            this.afDatabasePicker1.ShowNew = false;
            this.afDatabasePicker1.ShowNext = false;
            this.afDatabasePicker1.ShowPrevious = false;
            this.afDatabasePicker1.ShowProperties = false;
            this.afDatabasePicker1.Size = new System.Drawing.Size(320, 21);
            this.afDatabasePicker1.TabIndex = 1;
            this.afDatabasePicker1.SelectionChange += new OSIsoft.AF.UI.SelectionChangeEventHandler(this.afDatabasePicker1_SelectionChange);
            // 
            // afTreeView1
            // 
            this.afTreeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.afTreeView1.HideSelection = false;
            this.afTreeView1.Location = new System.Drawing.Point(12, 102);
            this.afTreeView1.Name = "afTreeView1";
            this.afTreeView1.ShowAnalyses = false;
            this.afTreeView1.ShowAnalysisTemplates = false;
            this.afTreeView1.ShowCases = false;
            this.afTreeView1.ShowCaseTemplates = false;
            this.afTreeView1.ShowCategories = false;
            this.afTreeView1.ShowConfigurationDatabase = OSIsoft.AF.UI.ShowConfigurationDatabase.Hide;
            this.afTreeView1.ShowContacts = false;
            this.afTreeView1.ShowDatabases = false;
            this.afTreeView1.ShowElementTemplates = false;
            this.afTreeView1.ShowEnumerations = false;
            this.afTreeView1.ShowEventFrameTemplates = false;
            this.afTreeView1.ShowModelTemplates = false;
            this.afTreeView1.ShowNodeToolTips = true;
            this.afTreeView1.ShowNotificationTemplates = false;
            this.afTreeView1.ShowPlugIns = false;
            this.afTreeView1.ShowReferenceTypes = false;
            this.afTreeView1.ShowTableConnections = false;
            this.afTreeView1.ShowTables = false;
            this.afTreeView1.ShowTransferTemplates = false;
            this.afTreeView1.Size = new System.Drawing.Size(320, 542);
            this.afTreeView1.TabIndex = 2;
            this.afTreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.afTreeView1_AfterSelect);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Asset Server:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "AF Database:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Element Tree:";
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(782, 52);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(105, 28);
            this.btnGetData.TabIndex = 6;
            this.btnGetData.Text = "Get Data!";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // gboxData
            // 
            this.gboxData.Controls.Add(this.cboxInterval);
            this.gboxData.Controls.Add(this.rbSummary);
            this.gboxData.Controls.Add(this.rbInterpolatedValues);
            this.gboxData.Controls.Add(this.rbRecordedValues);
            this.gboxData.Controls.Add(this.tbEndTime);
            this.gboxData.Controls.Add(this.tbStartTime);
            this.gboxData.Controls.Add(this.label3);
            this.gboxData.Controls.Add(this.label7);
            this.gboxData.Controls.Add(this.label6);
            this.gboxData.Controls.Add(this.label8);
            this.gboxData.Location = new System.Drawing.Point(358, 123);
            this.gboxData.Name = "gboxData";
            this.gboxData.Size = new System.Drawing.Size(529, 114);
            this.gboxData.TabIndex = 7;
            this.gboxData.TabStop = false;
            this.gboxData.Text = "Data Settings";
            // 
            // rbSummary
            // 
            this.rbSummary.AutoSize = true;
            this.rbSummary.Location = new System.Drawing.Point(351, 81);
            this.rbSummary.Name = "rbSummary";
            this.rbSummary.Size = new System.Drawing.Size(68, 17);
            this.rbSummary.TabIndex = 6;
            this.rbSummary.Text = "Summary";
            this.rbSummary.UseVisualStyleBackColor = true;
            this.rbSummary.CheckedChanged += new System.EventHandler(this.rbMethod_CheckChanged);
            // 
            // rbInterpolatedValues
            // 
            this.rbInterpolatedValues.AutoSize = true;
            this.rbInterpolatedValues.Location = new System.Drawing.Point(351, 58);
            this.rbInterpolatedValues.Name = "rbInterpolatedValues";
            this.rbInterpolatedValues.Size = new System.Drawing.Size(116, 17);
            this.rbInterpolatedValues.TabIndex = 6;
            this.rbInterpolatedValues.Text = "Interpolated Values";
            this.rbInterpolatedValues.UseVisualStyleBackColor = true;
            this.rbInterpolatedValues.CheckedChanged += new System.EventHandler(this.rbMethod_CheckChanged);
            // 
            // rbRecordedValues
            // 
            this.rbRecordedValues.AutoSize = true;
            this.rbRecordedValues.Checked = true;
            this.rbRecordedValues.Location = new System.Drawing.Point(351, 35);
            this.rbRecordedValues.Name = "rbRecordedValues";
            this.rbRecordedValues.Size = new System.Drawing.Size(107, 17);
            this.rbRecordedValues.TabIndex = 6;
            this.rbRecordedValues.TabStop = true;
            this.rbRecordedValues.Text = "Recorded Values";
            this.rbRecordedValues.UseVisualStyleBackColor = true;
            this.rbRecordedValues.CheckedChanged += new System.EventHandler(this.rbMethod_CheckChanged);
            // 
            // tbEndTime
            // 
            this.tbEndTime.Location = new System.Drawing.Point(89, 55);
            this.tbEndTime.Name = "tbEndTime";
            this.tbEndTime.Size = new System.Drawing.Size(180, 20);
            this.tbEndTime.TabIndex = 5;
            this.tbEndTime.Text = "*";
            this.tbEndTime.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TimeBox_KeyUp);
            // 
            // tbStartTime
            // 
            this.tbStartTime.Location = new System.Drawing.Point(89, 24);
            this.tbStartTime.Name = "tbStartTime";
            this.tbStartTime.Size = new System.Drawing.Size(180, 20);
            this.tbStartTime.TabIndex = 5;
            this.tbStartTime.Text = "y";
            this.tbStartTime.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TimeBox_KeyUp);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Interval";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Start Time";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(348, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Method";
            // 
            // btnViewElement
            // 
            this.btnViewElement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnViewElement.Location = new System.Drawing.Point(99, 692);
            this.btnViewElement.Name = "btnViewElement";
            this.btnViewElement.Size = new System.Drawing.Size(147, 30);
            this.btnViewElement.TabIndex = 6;
            this.btnViewElement.Text = "View Element Settings";
            this.btnViewElement.UseVisualStyleBackColor = true;
            this.btnViewElement.Click += new System.EventHandler(this.btnViewElement_Click);
            // 
            // lboxDataValues
            // 
            this.lboxDataValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lboxDataValues.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lboxDataValues.FormattingEnabled = true;
            this.lboxDataValues.ItemHeight = 14;
            this.lboxDataValues.Location = new System.Drawing.Point(359, 269);
            this.lboxDataValues.Name = "lboxDataValues";
            this.lboxDataValues.Size = new System.Drawing.Size(527, 494);
            this.lboxDataValues.TabIndex = 8;
            // 
            // gboxWeather
            // 
            this.gboxWeather.Controls.Add(this.label5);
            this.gboxWeather.Controls.Add(this.cboxUom);
            this.gboxWeather.Controls.Add(this.rbHumidity);
            this.gboxWeather.Controls.Add(this.rbPressure);
            this.gboxWeather.Controls.Add(this.rbVisibility);
            this.gboxWeather.Controls.Add(this.rbWindSpeed);
            this.gboxWeather.Controls.Add(this.rbCloudCover);
            this.gboxWeather.Controls.Add(this.rbTemperature);
            this.gboxWeather.Location = new System.Drawing.Point(358, 5);
            this.gboxWeather.Name = "gboxWeather";
            this.gboxWeather.Size = new System.Drawing.Size(406, 112);
            this.gboxWeather.TabIndex = 9;
            this.gboxWeather.TabStop = false;
            this.gboxWeather.Text = "Weather Feature";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Choose unit of measure:";
            // 
            // cboxUom
            // 
            this.cboxUom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxUom.FormattingEnabled = true;
            this.cboxUom.Location = new System.Drawing.Point(172, 78);
            this.cboxUom.Name = "cboxUom";
            this.cboxUom.Size = new System.Drawing.Size(176, 21);
            this.cboxUom.TabIndex = 5;
            this.cboxUom.SelectedIndexChanged += new System.EventHandler(this.cboxUom_SelectedIndexChanged);
            // 
            // rbHumidity
            // 
            this.rbHumidity.AutoSize = true;
            this.rbHumidity.Location = new System.Drawing.Point(307, 47);
            this.rbHumidity.Name = "rbHumidity";
            this.rbHumidity.Size = new System.Drawing.Size(65, 17);
            this.rbHumidity.TabIndex = 0;
            this.rbHumidity.Text = "Humidity";
            this.rbHumidity.UseVisualStyleBackColor = true;
            this.rbHumidity.CheckedChanged += new System.EventHandler(this.rbFeature_CheckChanged);
            // 
            // rbPressure
            // 
            this.rbPressure.AutoSize = true;
            this.rbPressure.Location = new System.Drawing.Point(307, 20);
            this.rbPressure.Name = "rbPressure";
            this.rbPressure.Size = new System.Drawing.Size(66, 17);
            this.rbPressure.TabIndex = 0;
            this.rbPressure.Text = "Pressure";
            this.rbPressure.UseVisualStyleBackColor = true;
            this.rbPressure.CheckedChanged += new System.EventHandler(this.rbFeature_CheckChanged);
            // 
            // rbVisibility
            // 
            this.rbVisibility.AutoSize = true;
            this.rbVisibility.Location = new System.Drawing.Point(172, 47);
            this.rbVisibility.Name = "rbVisibility";
            this.rbVisibility.Size = new System.Drawing.Size(61, 17);
            this.rbVisibility.TabIndex = 0;
            this.rbVisibility.Text = "Visibility";
            this.rbVisibility.UseVisualStyleBackColor = true;
            this.rbVisibility.CheckedChanged += new System.EventHandler(this.rbFeature_CheckChanged);
            // 
            // rbWindSpeed
            // 
            this.rbWindSpeed.AutoSize = true;
            this.rbWindSpeed.Location = new System.Drawing.Point(172, 20);
            this.rbWindSpeed.Name = "rbWindSpeed";
            this.rbWindSpeed.Size = new System.Drawing.Size(84, 17);
            this.rbWindSpeed.TabIndex = 0;
            this.rbWindSpeed.Text = "Wind Speed";
            this.rbWindSpeed.UseVisualStyleBackColor = true;
            this.rbWindSpeed.CheckedChanged += new System.EventHandler(this.rbFeature_CheckChanged);
            // 
            // rbCloudCover
            // 
            this.rbCloudCover.AutoSize = true;
            this.rbCloudCover.Location = new System.Drawing.Point(31, 47);
            this.rbCloudCover.Name = "rbCloudCover";
            this.rbCloudCover.Size = new System.Drawing.Size(83, 17);
            this.rbCloudCover.TabIndex = 0;
            this.rbCloudCover.Text = "Cloud Cover";
            this.rbCloudCover.UseVisualStyleBackColor = true;
            this.rbCloudCover.CheckedChanged += new System.EventHandler(this.rbFeature_CheckChanged);
            // 
            // rbTemperature
            // 
            this.rbTemperature.AutoSize = true;
            this.rbTemperature.Checked = true;
            this.rbTemperature.Location = new System.Drawing.Point(31, 20);
            this.rbTemperature.Name = "rbTemperature";
            this.rbTemperature.Size = new System.Drawing.Size(85, 17);
            this.rbTemperature.TabIndex = 0;
            this.rbTemperature.TabStop = true;
            this.rbTemperature.Text = "Temperature";
            this.rbTemperature.UseVisualStyleBackColor = true;
            this.rbTemperature.CheckedChanged += new System.EventHandler(this.rbFeature_CheckChanged);
            // 
            // lblMetaInfo
            // 
            this.lblMetaInfo.AutoSize = true;
            this.lblMetaInfo.Location = new System.Drawing.Point(356, 253);
            this.lblMetaInfo.Name = "lblMetaInfo";
            this.lblMetaInfo.Size = new System.Drawing.Size(65, 13);
            this.lblMetaInfo.TabIndex = 4;
            this.lblMetaInfo.Text = "Data Values";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "End Time";
            // 
            // cboxInterval
            // 
            this.cboxInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxInterval.FormattingEnabled = true;
            this.cboxInterval.Location = new System.Drawing.Point(89, 84);
            this.cboxInterval.Name = "cboxInterval";
            this.cboxInterval.Size = new System.Drawing.Size(179, 21);
            this.cboxInterval.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 786);
            this.Controls.Add(this.gboxWeather);
            this.Controls.Add(this.lboxDataValues);
            this.Controls.Add(this.gboxData);
            this.Controls.Add(this.btnGetData);
            this.Controls.Add(this.btnViewElement);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblMetaInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.afTreeView1);
            this.Controls.Add(this.afDatabasePicker1);
            this.Controls.Add(this.piSystemPicker1);
            this.Name = "MainForm";
            this.Text = "Weather AF Applet";
            this.gboxData.ResumeLayout(false);
            this.gboxData.PerformLayout();
            this.gboxWeather.ResumeLayout(false);
            this.gboxWeather.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OSIsoft.AF.UI.PISystemPicker piSystemPicker1;
        private OSIsoft.AF.UI.AFDatabasePicker afDatabasePicker1;
        private OSIsoft.AF.UI.AFTreeView afTreeView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.GroupBox gboxData;
        private System.Windows.Forms.TextBox tbEndTime;
        private System.Windows.Forms.TextBox tbStartTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnViewElement;
        private System.Windows.Forms.RadioButton rbSummary;
        private System.Windows.Forms.RadioButton rbInterpolatedValues;
        private System.Windows.Forms.RadioButton rbRecordedValues;
        private System.Windows.Forms.ListBox lboxDataValues;
        private System.Windows.Forms.GroupBox gboxWeather;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboxUom;
        private System.Windows.Forms.RadioButton rbHumidity;
        private System.Windows.Forms.RadioButton rbPressure;
        private System.Windows.Forms.RadioButton rbVisibility;
        private System.Windows.Forms.RadioButton rbWindSpeed;
        private System.Windows.Forms.RadioButton rbCloudCover;
        private System.Windows.Forms.RadioButton rbTemperature;
        private System.Windows.Forms.Label lblMetaInfo;
        private System.Windows.Forms.ComboBox cboxInterval;
        private System.Windows.Forms.Label label3;
    }
}

