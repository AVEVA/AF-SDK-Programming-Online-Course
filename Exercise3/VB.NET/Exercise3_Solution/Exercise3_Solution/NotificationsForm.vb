Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports OSIsoft.AF.Asset
Imports OSIsoft.AF.Time
Imports OSIsoft.AF.Search
Imports OSIsoft.AF.EventFrame
Imports OSIsoft.AF.Notification


Public Class NotificationsForm
    Inherits Form

    Private _element As AFElement

    ' Constructor requires an AFElement and 2 strings for start and end time.
    Public Sub New(ByVal element As AFElement, ByVal startTime As String, ByVal endTime As String)
        _element = element
        InitializeComponent()

        tbStartTime.Text = startTime
        tbEndTime.Text = endTime
        TimeBoxValidate(tbStartTime)
        TimeBoxValidate(tbEndTime)

        If element IsNot Nothing Then
            Text = (Text + $" - {element.Name}")
            Dim rules = element.NotificationRules
            lboxNotificationRules.Items.AddRange(rules.ToArray())
            If (lboxNotificationRules.Items.Count > 0) Then
                lboxNotificationRules.SelectedIndex = 0
            End If
        End If

        CheckAllButtons(AutoFindInstances:=True)
    End Sub

    ' While this seems to be a likely candidate for an extension method,
    ' there is a strong possibility that text could be null, which
    ' doesn't work well with extension methods.
    Public Shared Function ParseToAFTime(ByVal text As String) As AFTime?
        If String.IsNullOrWhiteSpace(text) Then Return Nothing
        Try
            Dim time = AFTime.MaxValue
            ' AFTime.TryParse
            ' https://techsupport.osisoft.com/Documentation/PI-AF-SDK/Html/M_OSIsoft_AF_Time_AFTime_TryParse_2.htm
            ' DateTime.TryParse
            ' https://msdn.microsoft.com/en-us/library/ch92fbc1(v=vs.110).aspx
            If AFTime.TryParse(text, time) Then
                Return time
            End If
        Catch
        End Try

        Return Nothing
    End Function

    Private Function GetStartTime() As AFTime?
        Return ParseToAFTime(tbStartTime.Text)
    End Function

    Private Function GetEndTime() As AFTime?
        Return ParseToAFTime(tbEndTime.Text)
    End Function

    Private Sub TimeBox_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles tbStartTime.KeyUp, tbEndTime.KeyUp
        Dim textBox = TryCast(sender, TextBox)
        TimeBoxValidate(textBox)
        CheckAllButtons(AutoFindInstances:=True)
    End Sub

    Private Sub TimeBoxValidate(ByVal textBox As TextBox)
        If (textBox Is Nothing) Then
            Return
        End If

        Dim time As AFTime? = ParseToAFTime(textBox.Text)
        textBox.Tag = time
        If time.HasValue Then
            textBox.BackColor = Color.White
            textBox.ForeColor = Color.Black
        Else
            textBox.BackColor = Color.LightYellow
            textBox.ForeColor = Color.Red
        End If

    End Sub

    Private Sub CheckAllButtons(Optional ByVal AutoFindInstances = False)
        Dim startTime = CType(tbStartTime.Tag, AFTime?)
        Dim endTime = CType(tbEndTime.Tag, AFTime?)
        Dim okayToView = (_element IsNot Nothing) _
                    AndAlso (startTime.HasValue _
                    AndAlso (endTime.HasValue _
                    AndAlso (lboxNotificationRules.SelectedIndex >= 0)))
        btnView.Enabled = okayToView

        If AutoFindInstances Then FindNotificationInstances()
    End Sub

    Private Sub btnView_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnView.Click
        FindNotificationInstances()
    End Sub

    Private Sub lboxNotificationRules_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lboxNotificationRules.SelectedIndexChanged
        CheckAllButtons(AutoFindInstances:=True)
    End Sub

    'Get instances, which are Event Frames beginning with PI Notifications 2016
    Private Sub FindNotificationInstances()
        gridNotificationInstances.Rows.Clear()

        If Not btnView.Enabled Then
            Return
        End If

        Dim rule = TryCast(lboxNotificationRules.SelectedItem, AFNotificationRule)
        Dim startTime = CType(tbStartTime.Tag, AFTime?)
        Dim endTime = CType(tbEndTime.Tag, AFTime?)
        Dim database = _element.Database

        'We echo the current notification rule search criteria, which includes literal "Criteria:"
        'but include the Target element in the filter.
        Dim query As String = String.Format("Element:'{0}' {1}", _element.GetPath(database), rule.Criteria)
        Dim search As AFEventFrameSearch = New AFEventFrameSearch(database, "", AFSearchMode.Overlapped, startTime.Value, endTime.Value, query)
        search.CacheTimeout = TimeSpan.FromMinutes(5)
        Dim instances As IEnumerable(Of AFEventFrame) = search.FindEventFrames(fullLoad:=True)

        'Populate listbox with notification instances, which are just event frames in PI Notifications 2016 or later.
        For Each instance As AFEventFrame In instances
            ' For multi-trigger analyses, the "Start Trigger Name" would be null on the parent event frame if there were multiple triggers.
            ' We should suppress those.  Note the "Start Trigger Name" is our default attribute.
            Dim defaultObject As Object = String.Empty
            If (instance.DefaultAttribute IsNot Nothing) Then
                defaultObject = instance.DefaultAttribute.GetValue()
            End If
            If Not String.IsNullOrWhiteSpace(defaultObject.ToString()) Then
                Dim endObject As Object = "ongoing/active"
                If instance.EndTime < AFTime.MaxValue Then
                    endObject = instance.EndTime.LocalTime
                End If
                gridNotificationInstances.Rows.Add(instance.StartTime.LocalTime, endObject, defaultObject)
            End If
        Next
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.Click
        Close()
    End Sub
End Class