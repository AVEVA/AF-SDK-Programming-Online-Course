<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AttributesForm
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
        Me.AfViewControl1 = New OSIsoft.AF.UI.AFViewControl()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblElement = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'AfViewControl1
        '
        Me.AfViewControl1.AccessibleDescription = "View Control for displaying AF objects"
        Me.AfViewControl1.AccessibleName = "View Control"
        Me.AfViewControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AfViewControl1.BackColor = System.Drawing.Color.Transparent
        Me.AfViewControl1.DisplayPathLabel = False
        Me.AfViewControl1.HelpContext = CType(0, Long)
        Me.AfViewControl1.Location = New System.Drawing.Point(32, 25)
        Me.AfViewControl1.Name = "AfViewControl1"
        Me.AfViewControl1.Size = New System.Drawing.Size(874, 336)
        Me.AfViewControl1.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(422, 389)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(105, 28)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'lblElement
        '
        Me.lblElement.AutoSize = True
        Me.lblElement.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblElement.Location = New System.Drawing.Point(12, 9)
        Me.lblElement.Name = "lblElement"
        Me.lblElement.Size = New System.Drawing.Size(32, 13)
        Me.lblElement.TabIndex = 2
        Me.lblElement.Text = "City:"
        '
        'AttributesForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(944, 441)
        Me.Controls.Add(Me.lblElement)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.AfViewControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AttributesForm"
        Me.Text = "View AF Attributes"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents AfViewControl1 As OSIsoft.AF.UI.AFViewControl
    Friend WithEvents btnClose As Button
    Friend WithEvents lblElement As Label
End Class
