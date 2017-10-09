Imports OSIsoft.AF.Asset

Public Class AttributesForm
    Inherits Form

    Public Sub New(element As AFElement)
        InitializeComponent()

        If (element IsNot Nothing) Then
            lblElement.Text += " " + element.GetPath(element.Database)
            AfViewControl1.AFSetObject(element, Nothing, Nothing, Nothing)
            ' This should jump over to "Attributes" tab
            AfViewControl1.AFSelection = element.Attributes
        End If
    End Sub

End Class