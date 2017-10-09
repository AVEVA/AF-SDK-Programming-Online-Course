<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NotificationsForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.gridNotificationInstances = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tbEndTime = New System.Windows.Forms.TextBox()
        Me.tbStartTime = New System.Windows.Forms.TextBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label6 = New System.Windows.Forms.Label()
        Me.lboxNotificationRules = New System.Windows.Forms.ListBox()
        Me.lblCity = New System.Windows.Forms.Label()
        Me.btnView = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        CType(Me.gridNotificationInstances, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gridNotificationInstances
        '
        Me.gridNotificationInstances.AllowUserToAddRows = False
        Me.gridNotificationInstances.AllowUserToDeleteRows = False
        Me.gridNotificationInstances.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gridNotificationInstances.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridNotificationInstances.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3})
        Me.gridNotificationInstances.Location = New System.Drawing.Point(36, 232)
        Me.gridNotificationInstances.Name = "gridNotificationInstances"
        Me.gridNotificationInstances.Size = New System.Drawing.Size(502, 171)
        Me.gridNotificationInstances.TabIndex = 19
        '
        'Column1
        '
        Me.Column1.HeaderText = "StartTime"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 160
        '
        'Column2
        '
        Me.Column2.HeaderText = "EndTime"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 160
        '
        'Column3
        '
        Me.Column3.HeaderText = "DefaultAttribute"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 110
        '
        'tbEndTime
        '
        Me.tbEndTime.Location = New System.Drawing.Point(210, 191)
        Me.tbEndTime.Name = "tbEndTime"
        Me.tbEndTime.Size = New System.Drawing.Size(140, 20)
        Me.tbEndTime.TabIndex = 17
        Me.tbEndTime.Text = "*"
        '
        'tbStartTime
        '
        Me.tbStartTime.Location = New System.Drawing.Point(36, 191)
        Me.tbStartTime.Name = "tbStartTime"
        Me.tbStartTime.Size = New System.Drawing.Size(140, 20)
        Me.tbStartTime.TabIndex = 18
        Me.tbStartTime.Text = "y"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(207, 175)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(52, 13)
        Me.label3.TabIndex = 15
        Me.label3.Text = "End Time"
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Location = New System.Drawing.Point(33, 175)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(55, 13)
        Me.label6.TabIndex = 16
        Me.label6.Text = "Start Time"
        '
        'lboxNotificationRules
        '
        Me.lboxNotificationRules.FormattingEnabled = True
        Me.lboxNotificationRules.Location = New System.Drawing.Point(36, 50)
        Me.lboxNotificationRules.Name = "lboxNotificationRules"
        Me.lboxNotificationRules.Size = New System.Drawing.Size(502, 95)
        Me.lboxNotificationRules.TabIndex = 14
        '
        'lblCity
        '
        Me.lblCity.AutoSize = True
        Me.lblCity.Location = New System.Drawing.Point(33, 22)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(223, 13)
        Me.lblCity.TabIndex = 13
        Me.lblCity.Text = "Show Notifications Rules for the selected city:"
        '
        'btnView
        '
        Me.btnView.Location = New System.Drawing.Point(417, 160)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(121, 51)
        Me.btnView.TabIndex = 11
        Me.btnView.Text = "View Notification Instances"
        Me.btnView.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnClose.Location = New System.Drawing.Point(244, 419)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(97, 27)
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'NotificationsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(570, 469)
        Me.Controls.Add(Me.gridNotificationInstances)
        Me.Controls.Add(Me.tbEndTime)
        Me.Controls.Add(Me.tbStartTime)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label6)
        Me.Controls.Add(Me.lboxNotificationRules)
        Me.Controls.Add(Me.lblCity)
        Me.Controls.Add(Me.btnView)
        Me.Controls.Add(Me.btnClose)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "NotificationsForm"
        Me.Text = "View Notifications"
        Me.TopMost = True
        CType(Me.gridNotificationInstances, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents gridNotificationInstances As DataGridView
    Private WithEvents Column1 As DataGridViewTextBoxColumn
    Private WithEvents Column2 As DataGridViewTextBoxColumn
    Private WithEvents Column3 As DataGridViewTextBoxColumn
    Private WithEvents tbEndTime As TextBox
    Private WithEvents tbStartTime As TextBox
    Private WithEvents label3 As Label
    Private WithEvents label6 As Label
    Private WithEvents lboxNotificationRules As ListBox
    Private WithEvents lblCity As Label
    Private WithEvents btnView As Button
    Private WithEvents btnClose As Button
End Class
