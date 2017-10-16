'***************************************************************************
' Copyright 2017 OSIsoft, LLC.
'   Licensed under the Apache License, Version 2.0 (the "License");
'   you may not use this file except in compliance with the License.
'   You may obtain a copy Of the License at
'
'       http://www.apache.org/licenses/LICENSE-2.0
'   
'   Unless required by applicable law or agreed to in writing, software
'   distributed under the License is distributed on an "AS IS" BASIS,
'   WITHOUT WARRANTIES Or CONDITIONS Of ANY KIND, either express or implied.
'   See the License for the specific language governing permissions and
'   limitations under the License.
'****************************************************************************

'*********************************************************************************************************
' PIDevClub.DataReference.LimitCheck.dll
'---------------------------------------------------------------------------------------------------------
' This custom data reference will accept an input attribute, automatically find its related limit traits,
' and compare the value of the input attribute against the limits.  Note that there could be 0-4 different
' limit traits { Lo, Hi, LoLo, HiHi }.  
'
' An Int32 value is returned representing 1 of 5 possible states:
'      -2 : <= LoLo
'      -1 : <= Lo (but not LoLo)
'       0 : Normal, i.e. within limits
'      +1 : >= Hi (but not HiHi)
'      +2 : >= HiHi
'
' The result Attribute could be using Int32 data type, although a raw number lacks context.
' It is recommended to created an AFEnumerationSet based on the above codes with desired text.
' You may use different enumeration sets with different text, but the codes must be the same.
'---------------------------------------------------------------------------------------------------------
' HISTORY:
'         Developer             Date     Reason
' -------------------------  ----------  -----------------------------------------------------------------
' Rick Davin                 2017-09-28  Created in C# for Exercise 4 in AF SDK online course. 
' Rick Davin                 2017-10-03  Translated to VB.NET
'*********************************************************************************************************
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports OSIsoft.AF
Imports OSIsoft.AF.Asset
Imports OSIsoft.AF.Time
Imports OSIsoft.AF.Data
Imports OSIsoft.AF.UnitsOfMeasure

Namespace LimitCheckDR

    <Guid("8DA87B2A-38D9-4CF6-9BA0-C75314A6B5DC"),
     Serializable(),
     Description("Limit Check;Compares a measurement to its limit traits.")>
    Public Class LimitCheckDR
        Inherits AFDataReference

        ' Rather than look up and validate the limit traits each instant data is requested,
        ' we will keep a tiny cache of no more than 5 attributes in memory.
        ' You are free to adjust the cache duration, though I would recommend no more than 2 minutes
        ' and no less than 10 seconds.  If you insist on no caching, you could disable
        ' the code in GetInputs or use a span of 0 seconds.
        ' Bonus Challenge: You could make the duration be a setting in the ConfigString.
        Private Shared _cacheRefreshDuration As TimeSpan = TimeSpan.FromSeconds(30)

        Private _lastCachedTime As DateTime = DateTime.UtcNow.AddDays(-1)
        Private _cachedInputAttrs As AFAttributeList = New AFAttributeList
        ' The default measurement name is relative dot reference to the parent attribute.
        Private Const DefaultMeasPath As String = ".."
        Private _measAttrName As String = DefaultMeasPath

        Private ReadOnly Property UseParentAttribute As Boolean
            Get
                Return String.IsNullOrWhiteSpace(_measAttrName) OrElse _measAttrName = DefaultMeasPath
            End Get
        End Property

        Private ReadOnly Property AllowedTraits As AFAttributeTrait()
            Get
                Return New AFAttributeTrait() {AFAttributeTrait.LimitLo,
                                               AFAttributeTrait.LimitHi,
                                               AFAttributeTrait.LimitLoLo,
                                               AFAttributeTrait.LimitHiHi}
            End Get
        End Property


        Private Class TripState
            Public Const None As Integer = 0
            Public Const LowLow As Integer = -2
            Public Const Low As Integer = -1
            Public Const High As Integer = 1
            Public Const HighHigh As Integer = 2
        End Class


        ' https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/P_OSIsoft_AF_Asset_AFDataReference_ConfigString.htm
        Public Overrides Property ConfigString As String
            Get
                Return $"MeasAttr={_measAttrName}"
            End Get
            Set
                ' We will always force the cache to refresh anytime we change ConfigString.
                _lastCachedTime = DateTime.UtcNow.AddDays(-1)
                ' We clear out any previously set values to their defaults.  
                ' For now, we only have 1 to worry about but that could change in future versions.
                _measAttrName = DefaultMeasPath
                ' With only 1 name=value pair, we don't have a semi-colon delimiter, but the 
                ' example shown will pretend there is one.  This allows for future expansion for more name=value pairs.
                Dim pairs = Value.Split(New Char() {";"c}, StringSplitOptions.RemoveEmptyEntries)
                For Each pair In pairs
                    ' Question: what to do if tokens.Count is not 2 as expected?
                    Dim tokens = pair.Split(New Char() {"="c}, StringSplitOptions.None)
                    Select Case (tokens(0).ToUpperInvariant)
                        Case "MEASATTR"
                            ' You will note we do very little validation on the _measAttrName.
                            ' That should not be a concern of the ConfigString setter, whose only
                            ' concern should be parsing the string.  We defer to other methods,
                            ' such as GetInputs, to validate if the parsed string refers to valid
                            ' AFObjects.
                            If ((tokens.Length > 1) AndAlso Not String.IsNullOrEmpty(tokens(1))) Then
                                _measAttrName = tokens(1).Trim
                            End If

                        Case Else
                            Throw New ArgumentException("Invalid parameter '{tokens[0]}'.")
                    End Select

                Next
                ' Important to persist changes back to database via this protected method call:
                SaveConfigChanges()
            End Set
        End Property


        ' https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/M_OSIsoft_AF_Asset_AFDataReference_GetInputs.htm
        Public Overrides Function GetInputs(ByVal context As Object) As AFAttributeList
            ' Internal time keeping is done by UTC so we completely avoid even the slightest hint of any DST issues.
            If ((DateTime.UtcNow - Me._lastCachedTime) < _cacheRefreshDuration) Then
                Return _cachedInputAttrs
            End If

            ' PRO TIP: although _cachedInputAttrs is accessible to the entire class instance, you want to avoid
            ' using _cachedInputAttrs.Add or AddRange in name of thread safety.  The safer route is to build
            ' the list in its own local variable, and once that local list is completely created, you may then
            ' assign that local variable to _cachedInputAttrs.
            ' To help emphasize that, the local list is created in its own method.
            _cachedInputAttrs = CreateBrandNewInputList(context)

            ' Since the above could throw an exception, we only want to mark _lastCachedTime AFTER a
            ' successful creation of the inputs.
            _lastCachedTime = DateTime.UtcNow
            Return _cachedInputAttrs
        End Function

        Private Function CreateBrandNewInputList(ByVal context As Object) As AFAttributeList
            ' PRO TIP:
            ' See comments in GetInputs where you want to avoid _cachedInputAttrs.Add or AddRange.
            ' For thread safety, use a brand new list that is local to this method.
            ' Start with a brand new list
            Dim brandNewList As AFAttributeList = New AFAttributeList
            Dim measurement As AFAttribute = Nothing
            ' First and foremost we need a measurement attribute, which is the centerpoint to compare against limits.
            If UseParentAttribute Then
                ' https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/P_OSIsoft_AF_Asset_AFDataReference_Attribute.htm
                ' https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/P_OSIsoft_AF_Asset_AFAttribute_Parent.htm
                measurement = Attribute.Parent
                If (measurement Is Nothing) Then
                    Throw New Exception("Root-level attribute does not have a parent.  You must define 'MeasAttr=something' in the ConfigString.")
                End If
            Else
                ' Let's offer some bit of name substitution.
                ' However, the GetInputs method lacks any timeContext, which restricts @value substitution
                ' to current values only.  This restriction is fine for static attributes.
                ' https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/T_OSIsoft_AF_AFNameSubstitutionType.htm
                Dim path As String = SubstituteParameters(Me._measAttrName, Me, context, timeContext:=Nothing)
                ' Note that the final fetch of the measurement attribute is *relative* to the current Attribute.
                ' https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/P_OSIsoft_AF_Asset_AFAttribute_Attributes.htm
                measurement = Attribute.Attributes(path)
                If (measurement Is Nothing) Then
                    Throw New Exception($"MeasAttr '{_measAttrName}' not found.  Check your ConfigString.")
                End If

            End If

            If Not IsNumericType(Type.GetTypeCode(measurement.Type)) Then
                Throw New Exception("MeasAttr does not have a numeric Type.")
            End If

            ' If the list will have any items, the measurement will always be at Index 0.
            brandNewList.Add(measurement)
            ' Let the CDR automatically fetch the associated limits.
            ' These could come back in any order, plus some or all may be missing!
            ' Geez, doesn't that make it fun and challenging!
            ' https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/M_OSIsoft_AF_Asset_AFAttribute_GetAttributesByTrait.htm
            brandNewList.AddRange(measurement.GetAttributesByTrait(AllowedTraits))
            Return brandNewList
        End Function

        ' VB note: We want a Step property but Step is also a VB keyword used in For loops.
        ' Therefore, we must wrap our property name in brackets.
        Public Overrides ReadOnly Property [Step] As Boolean
            Get
                Return True
            End Get
        End Property


        Public Overrides ReadOnly Property SupportedMethods As AFDataReferenceMethod
            Get
                ' For Step property to be visible to other apps like PI Vision or ProcessBook,
                ' the CDR must support ZeroAndSpan.
                Return AFDataReferenceMethod.GetValue Or AFDataReferenceMethod.GetValues Or AFDataReferenceMethod.ZeroAndSpan
            End Get
        End Property


        Public Overrides ReadOnly Property SupportedDataMethods As AFDataMethods
            Get
                Return DefaultSupportedDataMethods
            End Get
        End Property

        ' PRO TIP: the allows analytics and other clients to work more efficiently with a calcuation DR.
        Protected Overrides Function IsSupportedDataMethod(ByVal dataMethods As AFDataMethods) As Boolean?
            Return IsDefaultSupportedDataMethod(dataMethods)
        End Function

        ' https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/M_OSIsoft_AF_Asset_AFDataReference_GetValue_1.htm
        Public Overrides Function GetValue(ByVal context As Object, ByVal timeContext As Object, ByVal inputAttributes As AFAttributeList, ByVal inputValues As AFValues) As AFValue
            ' Important to note that the order of inputValues matches the order of inputAttributes.
            ' Note that timeContext is an object. 
            ' We need to examine it further in order to resolve it to an AFTime.
            Dim time = ToAFTime(timeContext)
            Dim measurement As AFValue = Nothing
            Dim low As AFValue = Nothing
            Dim high As AFValue = Nothing
            Dim lowlow As AFValue = Nothing
            Dim highhigh As AFValue = Nothing
            ' https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/P_OSIsoft_AF_Asset_AFAttribute_Trait.htm
            ' https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/T_OSIsoft_AF_Asset_AFAttributeTrait.htm
            Dim i = 0
            Do While (i < inputAttributes.Count)
                If (i = 0) Then
                    measurement = inputValues(i)
                ElseIf AFAttributeTrait.LimitLo.Equals(inputAttributes(i).Trait) Then
                    low = inputValues(i)
                ElseIf AFAttributeTrait.LimitHi.Equals(inputAttributes(i).Trait) Then
                    high = inputValues(i)
                ElseIf AFAttributeTrait.LimitLoLo.Equals(inputAttributes(i).Trait) Then
                    lowlow = inputValues(i)
                ElseIf AFAttributeTrait.LimitHiHi.Equals(inputAttributes(i).Trait) Then
                    highhigh = inputValues(i)
                End If

                i = (i + 1)
            Loop

            ' Remember any of the passed AFValues could be null if the limit trait is not defined.
            ' This is a fact of life and reflects the many possibilities within a given process unit.
            Return Calculation(time, measurement, low, high, lowlow, highhigh)
        End Function

        Private Overloads Function Calculation(ByVal time As AFTime, ByVal measurement As AFValue, ByVal low As AFValue, ByVal high As AFValue, ByVal lowlow As AFValue, ByVal highhigh As AFValue) As AFValue
            ' Our custom ToDouble returns double.NaN for null, missing, bad data, or bad conversions.
            Dim numericMeasurement = ToDouble(measurement)
            If Double.IsNaN(numericMeasurement) Then
                ' https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/M_OSIsoft_AF_Asset_AFValue_CreateSystemStateValue.htm
                Return AFValue.CreateSystemStateValue(Attribute, AFSystemStateCode.NoResult, time)
            End If

            ' Remember: any or all of the limits could be null.  In those bad cases, a double.NaN is sent.
            Dim calc = Calculation(numericMeasurement, ToDouble(low), ToDouble(high), ToDouble(lowlow), ToDouble(highhigh))
            Return New AFValue(Attribute, calc, time)
        End Function

        Private Overloads Shared Function Calculation(ByVal measurement As Double, ByVal low As Double, ByVal high As Double, ByVal lowlow As Double, ByVal highhigh As Double) As Integer
            ' If any limits were missing or bad, they will be a NaN here.
            ' A NaN used in comparisons will return false, which is what we want below.  That is,
            ' a comparison will return true only if the limit is not NaN and the limit has been met.
            If (measurement >= highhigh) Then
                Return TripState.HighHigh
            End If

            If (measurement <= lowlow) Then
                Return TripState.LowLow
            End If

            If (measurement >= high) Then
                Return TripState.High
            End If

            If (measurement <= low) Then
                Return TripState.Low
            End If

            Return TripState.None
        End Function

        Private Function ToAFTime(ByVal timeContext As Object) As AFTime
            If (TypeOf timeContext Is AFTime) Then
                Return CType(timeContext, AFTime)
            ElseIf (TypeOf timeContext Is AFTimeRange) Then
                Dim baseElement As AFBaseElement = Attribute.Element
                Dim timeRange As AFTimeRange = CType(timeContext, AFTimeRange)
                If (TypeOf baseElement Is AFEventFrame) Then
                    Return timeRange.StartTime
                Else
                    Return timeRange.EndTime
                End If
            End If
            Return AFTime.NowInWholeSeconds
        End Function

        Private Shared Function IsNumericType(ByVal typeCode As TypeCode) As Boolean
            Select Case (typeCode)
                Case TypeCode.Byte,
                     TypeCode.Decimal,
                     TypeCode.Double,
                     TypeCode.Int16,
                     TypeCode.Int32,
                     TypeCode.Int64,
                     TypeCode.SByte,
                     TypeCode.Single,
                     TypeCode.UInt16,
                     TypeCode.UInt32,
                     TypeCode.UInt64
                    Return True
                Case Else
                    Return False
            End Select
        End Function

        Private Shared Function ToDouble(ByVal value As AFValue) As Double
            If ((value Is Nothing) _
                        OrElse (Not value.IsGood _
                        OrElse Not IsNumericType(value.ValueTypeCode))) Then
                Return Double.NaN
            End If

            ' https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/M_OSIsoft_AF_Asset_AFValue_ValueAsDouble.htm
            Return value.ValueAsDouble
        End Function
    End Class
End Namespace
