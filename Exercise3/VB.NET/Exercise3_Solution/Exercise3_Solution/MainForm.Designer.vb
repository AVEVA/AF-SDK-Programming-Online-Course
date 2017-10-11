
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.gboxWeather = New System.Windows.Forms.GroupBox()
        Me.label5 = New System.Windows.Forms.Label()
        Me.cboxUom = New System.Windows.Forms.ComboBox()
        Me.rbHumidity = New System.Windows.Forms.RadioButton()
        Me.rbPressure = New System.Windows.Forms.RadioButton()
        Me.rbVisibility = New System.Windows.Forms.RadioButton()
        Me.rbWindSpeed = New System.Windows.Forms.RadioButton()
        Me.rbCloudCover = New System.Windows.Forms.RadioButton()
        Me.rbTemperature = New System.Windows.Forms.RadioButton()
        Me.btnViewElement = New System.Windows.Forms.Button()
        Me.cboxInterval = New System.Windows.Forms.ComboBox()
        Me.rbSummary = New System.Windows.Forms.RadioButton()
        Me.rbInterpolatedValues = New System.Windows.Forms.RadioButton()
        Me.rbRecordedValues = New System.Windows.Forms.RadioButton()
        Me.tbEndTime = New System.Windows.Forms.TextBox()
        Me.tbStartTime = New System.Windows.Forms.TextBox()
        Me.lblMetaInfo = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label7 = New System.Windows.Forms.Label()
        Me.label6 = New System.Windows.Forms.Label()
        Me.gboxData = New System.Windows.Forms.GroupBox()
        Me.label8 = New System.Windows.Forms.Label()
        Me.btnGetData = New System.Windows.Forms.Button()
        Me.label4 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.afDatabasePicker1 = New OSIsoft.AF.UI.AFDatabasePicker()
        Me.piSystemPicker1 = New OSIsoft.AF.UI.PISystemPicker()
        Me.gridDataValues = New System.Windows.Forms.DataGridView()
        Me.btnNotifications = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.afElementFindCtrl1 = New OSIsoft.AF.UI.AFElementFindCtrl()
        Me.gboxWeather.SuspendLayout()
        Me.gboxData.SuspendLayout()
        CType(Me.gridDataValues, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'gboxWeather
        '
        Me.gboxWeather.Controls.Add(Me.label5)
        Me.gboxWeather.Controls.Add(Me.cboxUom)
        Me.gboxWeather.Controls.Add(Me.rbHumidity)
        Me.gboxWeather.Controls.Add(Me.rbPressure)
        Me.gboxWeather.Controls.Add(Me.rbVisibility)
        Me.gboxWeather.Controls.Add(Me.rbWindSpeed)
        Me.gboxWeather.Controls.Add(Me.rbCloudCover)
        Me.gboxWeather.Controls.Add(Me.rbTemperature)
        Me.gboxWeather.Location = New System.Drawing.Point(15, 249)
        Me.gboxWeather.Name = "gboxWeather"
        Me.gboxWeather.Size = New System.Drawing.Size(406, 112)
        Me.gboxWeather.TabIndex = 21
        Me.gboxWeather.TabStop = False
        Me.gboxWeather.Text = "Weather Feature"
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(45, 81)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(121, 13)
        Me.label5.TabIndex = 4
        Me.label5.Text = "Choose unit of measure:"
        '
        'cboxUom
        '
        Me.cboxUom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboxUom.FormattingEnabled = True
        Me.cboxUom.Location = New System.Drawing.Point(172, 78)
        Me.cboxUom.Name = "cboxUom"
        Me.cboxUom.Size = New System.Drawing.Size(176, 21)
        Me.cboxUom.TabIndex = 5
        '
        'rbHumidity
        '
        Me.rbHumidity.AutoSize = True
        Me.rbHumidity.Location = New System.Drawing.Point(307, 47)
        Me.rbHumidity.Name = "rbHumidity"
        Me.rbHumidity.Size = New System.Drawing.Size(65, 17)
        Me.rbHumidity.TabIndex = 0
        Me.rbHumidity.Text = "Humidity"
        Me.rbHumidity.UseVisualStyleBackColor = True
        '
        'rbPressure
        '
        Me.rbPressure.AutoSize = True
        Me.rbPressure.Location = New System.Drawing.Point(307, 20)
        Me.rbPressure.Name = "rbPressure"
        Me.rbPressure.Size = New System.Drawing.Size(66, 17)
        Me.rbPressure.TabIndex = 0
        Me.rbPressure.Text = "Pressure"
        Me.rbPressure.UseVisualStyleBackColor = True
        '
        'rbVisibility
        '
        Me.rbVisibility.AutoSize = True
        Me.rbVisibility.Location = New System.Drawing.Point(172, 47)
        Me.rbVisibility.Name = "rbVisibility"
        Me.rbVisibility.Size = New System.Drawing.Size(61, 17)
        Me.rbVisibility.TabIndex = 0
        Me.rbVisibility.Text = "Visibility"
        Me.rbVisibility.UseVisualStyleBackColor = True
        '
        'rbWindSpeed
        '
        Me.rbWindSpeed.AutoSize = True
        Me.rbWindSpeed.Location = New System.Drawing.Point(172, 20)
        Me.rbWindSpeed.Name = "rbWindSpeed"
        Me.rbWindSpeed.Size = New System.Drawing.Size(84, 17)
        Me.rbWindSpeed.TabIndex = 0
        Me.rbWindSpeed.Text = "Wind Speed"
        Me.rbWindSpeed.UseVisualStyleBackColor = True
        '
        'rbCloudCover
        '
        Me.rbCloudCover.AutoSize = True
        Me.rbCloudCover.Location = New System.Drawing.Point(31, 47)
        Me.rbCloudCover.Name = "rbCloudCover"
        Me.rbCloudCover.Size = New System.Drawing.Size(83, 17)
        Me.rbCloudCover.TabIndex = 0
        Me.rbCloudCover.Text = "Cloud Cover"
        Me.rbCloudCover.UseVisualStyleBackColor = True
        '
        'rbTemperature
        '
        Me.rbTemperature.AutoSize = True
        Me.rbTemperature.Checked = True
        Me.rbTemperature.Location = New System.Drawing.Point(31, 20)
        Me.rbTemperature.Name = "rbTemperature"
        Me.rbTemperature.Size = New System.Drawing.Size(85, 17)
        Me.rbTemperature.TabIndex = 0
        Me.rbTemperature.TabStop = True
        Me.rbTemperature.Text = "Temperature"
        Me.rbTemperature.UseVisualStyleBackColor = True
        '
        'btnViewElement
        '
        Me.btnViewElement.Location = New System.Drawing.Point(63, 192)
        Me.btnViewElement.Name = "btnViewElement"
        Me.btnViewElement.Size = New System.Drawing.Size(140, 30)
        Me.btnViewElement.TabIndex = 17
        Me.btnViewElement.Text = "View Element Settings"
        Me.btnViewElement.UseVisualStyleBackColor = True
        '
        'cboxInterval
        '
        Me.cboxInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboxInterval.FormattingEnabled = True
        Me.cboxInterval.Location = New System.Drawing.Point(81, 101)
        Me.cboxInterval.Name = "cboxInterval"
        Me.cboxInterval.Size = New System.Drawing.Size(140, 21)
        Me.cboxInterval.TabIndex = 7
        '
        'rbSummary
        '
        Me.rbSummary.AutoSize = True
        Me.rbSummary.Location = New System.Drawing.Point(268, 101)
        Me.rbSummary.Name = "rbSummary"
        Me.rbSummary.Size = New System.Drawing.Size(68, 17)
        Me.rbSummary.TabIndex = 6
        Me.rbSummary.Text = "Summary"
        Me.rbSummary.UseVisualStyleBackColor = True
        '
        'rbInterpolatedValues
        '
        Me.rbInterpolatedValues.AutoSize = True
        Me.rbInterpolatedValues.Location = New System.Drawing.Point(268, 78)
        Me.rbInterpolatedValues.Name = "rbInterpolatedValues"
        Me.rbInterpolatedValues.Size = New System.Drawing.Size(116, 17)
        Me.rbInterpolatedValues.TabIndex = 6
        Me.rbInterpolatedValues.Text = "Interpolated Values"
        Me.rbInterpolatedValues.UseVisualStyleBackColor = True
        '
        'rbRecordedValues
        '
        Me.rbRecordedValues.AutoSize = True
        Me.rbRecordedValues.Checked = True
        Me.rbRecordedValues.Location = New System.Drawing.Point(268, 55)
        Me.rbRecordedValues.Name = "rbRecordedValues"
        Me.rbRecordedValues.Size = New System.Drawing.Size(107, 17)
        Me.rbRecordedValues.TabIndex = 6
        Me.rbRecordedValues.TabStop = True
        Me.rbRecordedValues.Text = "Recorded Values"
        Me.rbRecordedValues.UseVisualStyleBackColor = True
        '
        'tbEndTime
        '
        Me.tbEndTime.Location = New System.Drawing.Point(81, 68)
        Me.tbEndTime.Name = "tbEndTime"
        Me.tbEndTime.Size = New System.Drawing.Size(140, 20)
        Me.tbEndTime.TabIndex = 5
        Me.tbEndTime.Text = "*"
        '
        'tbStartTime
        '
        Me.tbStartTime.Location = New System.Drawing.Point(81, 34)
        Me.tbStartTime.Name = "tbStartTime"
        Me.tbStartTime.Size = New System.Drawing.Size(140, 20)
        Me.tbStartTime.TabIndex = 5
        Me.tbStartTime.Text = "y"
        '
        'lblMetaInfo
        '
        Me.lblMetaInfo.AutoSize = True
        Me.lblMetaInfo.Location = New System.Drawing.Point(437, 12)
        Me.lblMetaInfo.Name = "lblMetaInfo"
        Me.lblMetaInfo.Size = New System.Drawing.Size(65, 13)
        Me.lblMetaInfo.TabIndex = 13
        Me.lblMetaInfo.Text = "Data Values"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(20, 71)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(52, 13)
        Me.label3.TabIndex = 4
        Me.label3.Text = "End Time"
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.Location = New System.Drawing.Point(20, 104)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(42, 13)
        Me.label7.TabIndex = 4
        Me.label7.Text = "Interval"
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Location = New System.Drawing.Point(17, 37)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(55, 13)
        Me.label6.TabIndex = 4
        Me.label6.Text = "Start Time"
        '
        'gboxData
        '
        Me.gboxData.Controls.Add(Me.cboxInterval)
        Me.gboxData.Controls.Add(Me.rbSummary)
        Me.gboxData.Controls.Add(Me.rbInterpolatedValues)
        Me.gboxData.Controls.Add(Me.rbRecordedValues)
        Me.gboxData.Controls.Add(Me.tbEndTime)
        Me.gboxData.Controls.Add(Me.tbStartTime)
        Me.gboxData.Controls.Add(Me.label3)
        Me.gboxData.Controls.Add(Me.label7)
        Me.gboxData.Controls.Add(Me.label6)
        Me.gboxData.Controls.Add(Me.label8)
        Me.gboxData.Location = New System.Drawing.Point(15, 378)
        Me.gboxData.Name = "gboxData"
        Me.gboxData.Size = New System.Drawing.Size(406, 149)
        Me.gboxData.TabIndex = 19
        Me.gboxData.TabStop = False
        Me.gboxData.Text = "Data Settings"
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.Location = New System.Drawing.Point(265, 34)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(69, 13)
        Me.label8.TabIndex = 4
        Me.label8.Text = "Data Method"
        '
        'btnGetData
        '
        Me.btnGetData.Location = New System.Drawing.Point(316, 551)
        Me.btnGetData.Name = "btnGetData"
        Me.btnGetData.Size = New System.Drawing.Size(105, 28)
        Me.btnGetData.TabIndex = 18
        Me.btnGetData.Text = "Get Data!"
        Me.btnGetData.UseVisualStyleBackColor = True
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(47, 122)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(98, 13)
        Me.label4.TabIndex = 14
        Me.label4.Text = "Element of Interest:"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(45, 71)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(72, 13)
        Me.label2.TabIndex = 15
        Me.label2.Text = "AF Database:"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(47, 19)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(70, 13)
        Me.label1.TabIndex = 16
        Me.label1.Text = "Asset Server:"
        '
        'afDatabasePicker1
        '
        Me.afDatabasePicker1.AccessibleDescription = "Database Picker"
        Me.afDatabasePicker1.AccessibleName = "Database Picker"
        Me.afDatabasePicker1.Location = New System.Drawing.Point(48, 87)
        Me.afDatabasePicker1.Name = "afDatabasePicker1"
        Me.afDatabasePicker1.ShowBegin = False
        Me.afDatabasePicker1.ShowConfigurationDatabase = OSIsoft.AF.UI.ShowConfigurationDatabase.Hide
        Me.afDatabasePicker1.ShowDelete = False
        Me.afDatabasePicker1.ShowEnd = False
        Me.afDatabasePicker1.ShowFind = False
        Me.afDatabasePicker1.ShowImages = False
        Me.afDatabasePicker1.ShowList = False
        Me.afDatabasePicker1.ShowNavigation = False
        Me.afDatabasePicker1.ShowNew = False
        Me.afDatabasePicker1.ShowNext = False
        Me.afDatabasePicker1.ShowPrevious = False
        Me.afDatabasePicker1.ShowProperties = False
        Me.afDatabasePicker1.Size = New System.Drawing.Size(320, 21)
        Me.afDatabasePicker1.TabIndex = 11
        '
        'piSystemPicker1
        '
        Me.piSystemPicker1.AccessibleDescription = "PI System Picker"
        Me.piSystemPicker1.AccessibleName = "PI System Picker"
        Me.piSystemPicker1.Cursor = System.Windows.Forms.Cursors.Default
        Me.piSystemPicker1.Location = New System.Drawing.Point(48, 35)
        Me.piSystemPicker1.LoginPromptSetting = OSIsoft.AF.UI.PISystemPicker.LoginPromptSettingOptions.[Default]
        Me.piSystemPicker1.Name = "piSystemPicker1"
        Me.piSystemPicker1.ShowBegin = False
        Me.piSystemPicker1.ShowDelete = False
        Me.piSystemPicker1.ShowEnd = False
        Me.piSystemPicker1.ShowFind = False
        Me.piSystemPicker1.ShowImages = False
        Me.piSystemPicker1.ShowList = False
        Me.piSystemPicker1.ShowNavigation = False
        Me.piSystemPicker1.ShowNew = False
        Me.piSystemPicker1.ShowNext = False
        Me.piSystemPicker1.ShowPrevious = False
        Me.piSystemPicker1.ShowProperties = False
        Me.piSystemPicker1.Size = New System.Drawing.Size(320, 21)
        Me.piSystemPicker1.TabIndex = 10
        '
        'gridDataValues
        '
        Me.gridDataValues.AllowUserToAddRows = False
        Me.gridDataValues.AllowUserToDeleteRows = False
        Me.gridDataValues.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gridDataValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridDataValues.Location = New System.Drawing.Point(440, 31)
        Me.gridDataValues.Name = "gridDataValues"
        Me.gridDataValues.RowHeadersWidth = 80
        Me.gridDataValues.Size = New System.Drawing.Size(580, 548)
        Me.gridDataValues.TabIndex = 22
        '
        'btnNotifications
        '
        Me.btnNotifications.Enabled = False
        Me.btnNotifications.Location = New System.Drawing.Point(243, 192)
        Me.btnNotifications.Name = "btnNotifications"
        Me.btnNotifications.Size = New System.Drawing.Size(140, 30)
        Me.btnNotifications.TabIndex = 23
        Me.btnNotifications.Text = "View Notifications"
        Me.btnNotifications.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.afElementFindCtrl1)
        Me.GroupBox1.Controls.Add(Me.label1)
        Me.GroupBox1.Controls.Add(Me.piSystemPicker1)
        Me.GroupBox1.Controls.Add(Me.afDatabasePicker1)
        Me.GroupBox1.Controls.Add(Me.label2)
        Me.GroupBox1.Controls.Add(Me.label4)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(406, 174)
        Me.GroupBox1.TabIndex = 24
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select City"
        '
        'afElementFindCtrl1
        '
        Me.afElementFindCtrl1.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.afElementFindCtrl1.Location = New System.Drawing.Point(48, 139)
        Me.afElementFindCtrl1.Margin = New System.Windows.Forms.Padding(4)
        Me.afElementFindCtrl1.MinimumSize = New System.Drawing.Size(0, 22)
        Me.afElementFindCtrl1.Name = "afElementFindCtrl1"
        Me.afElementFindCtrl1.Size = New System.Drawing.Size(320, 24)
        Me.afElementFindCtrl1.TabIndex = 17
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1040, 598)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnNotifications)
        Me.Controls.Add(Me.gridDataValues)
        Me.Controls.Add(Me.gboxWeather)
        Me.Controls.Add(Me.btnViewElement)
        Me.Controls.Add(Me.lblMetaInfo)
        Me.Controls.Add(Me.gboxData)
        Me.Controls.Add(Me.btnGetData)
        Me.Name = "MainForm"
        Me.Text = "Weather AF Applet"
        Me.gboxWeather.ResumeLayout(False)
        Me.gboxWeather.PerformLayout()
        Me.gboxData.ResumeLayout(False)
        Me.gboxData.PerformLayout()
        CType(Me.gridDataValues, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents gboxWeather As GroupBox
    Private WithEvents label5 As Label
    Private WithEvents cboxUom As ComboBox
    Private WithEvents rbHumidity As RadioButton
    Private WithEvents rbPressure As RadioButton
    Private WithEvents rbVisibility As RadioButton
    Private WithEvents rbWindSpeed As RadioButton
    Private WithEvents rbCloudCover As RadioButton
    Private WithEvents rbTemperature As RadioButton
    Private WithEvents btnViewElement As Button
    Private WithEvents cboxInterval As ComboBox
    Private WithEvents rbSummary As RadioButton
    Private WithEvents rbInterpolatedValues As RadioButton
    Private WithEvents rbRecordedValues As RadioButton
    Private WithEvents tbEndTime As TextBox
    Private WithEvents tbStartTime As TextBox
    Private WithEvents lblMetaInfo As Label
    Private WithEvents label3 As Label
    Private WithEvents label7 As Label
    Private WithEvents label6 As Label
    Private WithEvents gboxData As GroupBox
    Private WithEvents label8 As Label
    Private WithEvents btnGetData As Button
    Private WithEvents label4 As Label
    Private WithEvents label2 As Label
    Private WithEvents label1 As Label
    Private WithEvents afDatabasePicker1 As OSIsoft.AF.UI.AFDatabasePicker
    Private WithEvents piSystemPicker1 As OSIsoft.AF.UI.PISystemPicker
    Private WithEvents gridDataValues As DataGridView
    Private WithEvents btnNotifications As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents afElementFindCtrl1 As OSIsoft.AF.UI.AFElementFindCtrl
End Class
