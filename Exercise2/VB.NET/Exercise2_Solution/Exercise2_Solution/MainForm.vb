Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports OSIsoft.AF
Imports OSIsoft.AF.Asset
Imports OSIsoft.AF.UnitsOfMeasure
Imports OSIsoft.AF.Time
Imports OSIsoft.AF.Notification
Imports OSIsoft.AF.EventFrame
Imports OSIsoft.AF.Data
Imports OSIsoft.AF.Search

Public Class MainForm
    Inherits Form

    Private Enum Feature
        Temperature
        CloudCover
        WindSpeed
        Visibility
        Pressure
        Humidity
    End Enum

    Private Enum DataMethod
        RecordedValues
        InterpolatedValues
        Summary
    End Enum

    Private Shared ReadOnly Property _mapToUomName As IDictionary(Of Feature, String)
        Get
            Dim map = New Dictionary(Of Feature, String)
            map.Add(Feature.CloudCover, "percent")
            map.Add(Feature.Humidity, "percent")
            map.Add(Feature.Pressure, "millibar")
            map.Add(Feature.Temperature, "degree Celsius")
            map.Add(Feature.Visibility, "kilometer")
            map.Add(Feature.WindSpeed, "kilometer per hour")
            Return map
        End Get
    End Property

    Private Class MetaData
        Implements IEquatable(Of MetaData)

        Public Sub New()
            Element = Nothing
            FeatureText = "Temperature"
            EngUnit = Nothing
            DataMethod = DataMethod.RecordedValues
            StartTime = "y"
            EndTime = "*"
        End Sub

        Public Sub New(ByVal source As MetaData)
            Element = source.Element
            FeatureText = source.FeatureText
            EngUnit = source.EngUnit
            DataMethod = source.DataMethod
            StartTime = source.StartTime
            EndTime = source.EndTime
        End Sub

        Public Property Element As AFElement
        Public Property FeatureText As String
        Public Property EngUnit As UOM
        Public Property DataMethod As DataMethod
        Public Property StartTime As String
        Public Property EndTime As String

        Public ReadOnly Property Attribute As AFAttribute
            Get
                If (Element Is Nothing) OrElse String.IsNullOrWhiteSpace(FeatureText) Then
                    Return Nothing
                End If
                Return Element.Attributes(FeatureText)
            End Get
        End Property


        Public ReadOnly Property IsTimeRangeValid As Boolean
            Get
                Return ParseToAFTime(StartTime).HasValue AndAlso ParseToAFTime(EndTime).HasValue
            End Get
        End Property


        Public ReadOnly Property TimeRange As AFTimeRange?
            Get
                If IsTimeRangeValid Then
                    Return New AFTimeRange(StartTime, EndTime)
                End If
                Return Nothing
            End Get
        End Property

        Public ReadOnly Property IsValid As Boolean
            Get
                Return (Attribute IsNot Nothing) AndAlso IsTimeRangeValid
            End Get
        End Property

        Public Shadows Function Equals(ByVal other As MetaData) As Boolean Implements IEquatable(Of MetaData).Equals
            If (CType(other, Object) Is Nothing) Then
                Return False
            End If

            Return ((Me.Attribute = other.Attribute) _
                        AndAlso ((Me.DataMethod = other.DataMethod) _
                        AndAlso ((Me.EngUnit = other.EngUnit) _
                        AndAlso (Me.TimeRange = other.TimeRange))))
        End Function

        Public ReadOnly Property IsEmpty As Boolean
            Get
                Return Equals(New MetaData())
            End Get
        End Property

        Public Function Clone() As MetaData
            Return New MetaData(Me)
        End Function

        Public Overrides Function ToString() As String
            ' Let's be nice and show some context regarding the data values that were retrieved
            Dim sb = New StringBuilder
            sb.Append(Attribute)
            If (EngUnit IsNot Nothing) Then
                sb.Append($"; UOM={EngUnit.Abbreviation}")
            End If
            sb.Append($"; {DataMethod}")
            sb.Append($"; {TimeRange.Value}")
            Return sb.ToString()
        End Function
    End Class

    Private Selected As MetaData = New MetaData
    Private Const InitialMetaDataText As String = "Data Values"

    Public Sub New()
        InitializeComponent()

        lblMetaInfo.Text = InitialMetaDataText

        'To have text align correctly in listbox, we will use a Fixed Width font
        lboxDataValues.Font = New Font("Consolas", 9, FontStyle.Regular)

        FillIntervalPicks()
        cboxInterval.Enabled = False

        rbTemperature.Checked = True
        Selected.FeatureText = rbTemperature.Text
        FillUomPicks(Feature.Temperature)

        rbRecordedValues.Checked = True
        Selected.DataMethod = DataMethod.RecordedValues

        Selected.StartTime = tbStartTime.Text
        Selected.EndTime = tbEndTime.Text

        afDatabasePicker1.SystemPicker = piSystemPicker1
        afTreeView1.AFRoot = afDatabasePicker1.AFDatabase

        CheckAllButtons()
    End Sub

    Private ReadOnly Property AssetServer As PISystem
        Get
            Return piSystemPicker1.PISystem
        End Get
    End Property

    Private ReadOnly Property Database As AFDatabase
        Get
            Return afDatabasePicker1.AFDatabase
        End Get
    End Property

    Private Sub afDatabasePicker1_SelectionChange(ByVal sender As Object, ByVal e As OSIsoft.AF.UI.SelectionChangeEventArgs) Handles afDatabasePicker1.SelectionChange
        afTreeView1.AFRoot = Database?.Elements
        Me.CheckAllButtons()
    End Sub

    Private Sub afTreeView1_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles afTreeView1.AfterSelect
        Selected.Element = TryCast(afTreeView1.AFSelection, AFElement)
        CheckAllButtons()
    End Sub

    Private Sub rbFeature_CheckChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbWindSpeed.CheckedChanged, rbVisibility.CheckedChanged, rbTemperature.CheckedChanged, rbPressure.CheckedChanged, rbHumidity.CheckedChanged, rbCloudCover.CheckedChanged
        Dim radioButton = TryCast(sender, RadioButton)
        If (radioButton Is Nothing OrElse Not radioButton.Checked) Then
            Return
        End If

        ' Continue for the checked radio button
        Selected.FeatureText = radioButton.Text
        Dim feature = CType(GetByText(GetType(Feature), radioButton.Text), Feature)
        FillUomPicks(feature)
        CheckAllButtons()
    End Sub

    Private Sub FillUomPicks(ByVal feature As Feature)
        cboxUom.Items.Clear()
        Dim defaultUOM = AssetServer?.UOMDatabase.UOMs(_mapToUomName(feature))
        If (defaultUOM IsNot Nothing) Then
            cboxUom.Items.Add($"< default ( {defaultUOM.Abbreviation} ) >")
            cboxUom.SelectedIndex = 0
            For Each uom In defaultUOM.Class.UOMs
                cboxUom.Items.Add(uom)
            Next
        End If

    End Sub

    Private Sub rbMethod_CheckChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbSummary.CheckedChanged, rbRecordedValues.CheckedChanged, rbInterpolatedValues.CheckedChanged
        Dim radioButton = TryCast(sender, RadioButton)
        If (radioButton Is Nothing OrElse Not radioButton.Checked) Then
            Return
        End If

        ' Continue for the checked radio button
        Selected.DataMethod = CType(GetByText(GetType(DataMethod), radioButton.Text), DataMethod)
        cboxInterval.Enabled = (Me.Selected.DataMethod = DataMethod.InterpolatedValues)
        Me.CheckAllButtons()
    End Sub

    Private Shared Function GetByText(ByVal enumType As Type, ByVal text As String) As Object
        If (String.IsNullOrWhiteSpace(text)) Then Return 0
        Return System.Enum.Parse(enumType, RemoveBlanks(text))
    End Function

    Private Shared Function RemoveBlanks(ByVal text As String) As String
        Return text.Replace(" ", String.Empty)
    End Function

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
        If (textBox Is tbStartTime) Then
            Selected.StartTime = textBox.Text
        ElseIf (textBox Is tbEndTime) Then
            Selected.EndTime = textBox.Text
        End If

        CheckAllButtons()
    End Sub

    Private Sub TimeBoxValidate(ByVal textBox As TextBox)
        If (textBox Is Nothing) Then
            Return
        End If

        If ParseToAFTime(textBox.Text).HasValue Then
            textBox.BackColor = Color.White
            textBox.ForeColor = Color.Black
        Else
            textBox.BackColor = Color.LightYellow
            textBox.ForeColor = Color.Red
        End If

    End Sub

    Private Sub CheckAllButtons()
        btnViewElement.Enabled = (Selected.Attribute IsNot Nothing)
        btnGetData.Enabled = Selected.IsValid

        If (lblMetaInfo.Text = InitialMetaDataText) OrElse (lblMetaInfo.Text = Selected.ToString()) Then
            lblMetaInfo.ForeColor = Color.Black
        Else
            lblMetaInfo.ForeColor = Color.Red
        End If
    End Sub

    Private Sub btnGetData_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetData.Click
        lboxDataValues.Items.Clear()

        Dim data = Selected.Attribute.Data

        If (Selected.DataMethod = DataMethod.Summary) Then
            Dim summaryDict = data.Summary(Selected.TimeRange.Value, AFSummaryTypes.All, AFCalculationBasis.TimeWeighted, AFTimestampCalculation.Auto)
            For Each summary In summaryDict
                Dim value = summary.Value
                Dim uomAbbr As String = ""
                If (value.UOM IsNot Nothing) Then
                    uomAbbr = $" {value.UOM.Abbreviation}"
                End If
                lboxDataValues.Items.Add($"{summary.Key,17}{vbTab}{value.Value}{uomAbbr}{vbTab}{value.Timestamp}")
            Next
        Else
            Dim values = New AFValues
            If (Selected.DataMethod = DataMethod.InterpolatedValues) Then
                Dim interval = CType(cboxInterval.SelectedItem, TimeSpan)
                values = data.InterpolatedValues(Selected.TimeRange.Value, New AFTimeSpan(interval), Selected.EngUnit, Nothing, False)
            Else
                values = data.RecordedValues(Selected.TimeRange.Value, AFBoundaryType.Interpolated, Selected.EngUnit, Nothing, False, 0)
            End If

            For Each value In values
                Dim uomAbbr As String = ""
                If (value.UOM IsNot Nothing) Then
                    uomAbbr = $" {value.UOM.Abbreviation}"
                End If
                lboxDataValues.Items.Add($"{value.Timestamp.LocalTime}{vbTab}{value.Value:N3}{uomAbbr}")
            Next
        End If

        lblMetaInfo.Text = Selected.ToString()
        CheckAllButtons()
    End Sub

    Private Sub cboxUom_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboxUom.SelectedIndexChanged
        Dim cbox = TryCast(sender, ComboBox)
        If (cbox Is Nothing) Then
            Return
        End If

        If (cbox.SelectedIndex < 1) Then
            Selected.EngUnit = Nothing
        Else
            Selected.EngUnit = CType(cbox.SelectedItem, UOM)
        End If

        CheckAllButtons()
    End Sub

    Private Sub piSystemPicker1_SelectionChange(ByVal sender As Object, ByVal e As OSIsoft.AF.UI.SelectionChangeEventArgs) Handles piSystemPicker1.SelectionChange
        If (AssetServer Is Nothing) Then
            Return
        End If

        Dim feature = CType(GetByText(GetType(Feature), Selected.FeatureText), Feature)
        Me.FillUomPicks(feature)
    End Sub

    Private Sub btnViewElement_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnViewElement.Click
        Dim dialogForm = New AttributesForm(Selected.Element)
        dialogForm.ShowDialog(Me)
    End Sub

    Private Sub FillIntervalPicks()
        cboxInterval.Items.Clear
        cboxInterval.Items.Add(TimeSpan.FromMinutes(5))
        cboxInterval.Items.Add(TimeSpan.FromMinutes(15))
        cboxInterval.Items.Add(TimeSpan.FromHours(1))
        cboxInterval.SelectedIndex = 2
        cboxInterval.Items.Add(TimeSpan.FromHours(8))
        cboxInterval.Items.Add(TimeSpan.FromDays(1))
        cboxInterval.Items.Add(TimeSpan.FromDays(7))
    End Sub
End Class
