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

Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Threading

Imports OSIsoft.AF.PI
Imports OSIsoft.AF
Imports OSIsoft.AF.Data
Imports OSIsoft.AF.Time

Namespace Exercise5_Solution

    Public Module Program

        Public Sub Main(args As String())

            ' Dim dataArchive = New PIServers().DefaultPIServer
            Dim dataArchive = New PIServers().Item("rdavin7250")

            Dim points = GetOrCreatePIPoints(dataArchive, New String() {"New York_Pressure", "New York_Temperature", "New York_Humidity"}, "AFSDKWS_")

            'Create data pipe
            Dim pipe As New PIDataPipe(AFDataPipeType.Snapshot)
            pipe.AddSignups(points)

            Dim counter = 0
            Do
                While Not Console.KeyAvailable
                    CheckPipeEvents(pipe, counter)
                End While
            Loop While Console.ReadKey(True).Key <> ConsoleKey.Escape

            pipe.Close()
            pipe.Dispose()

        End Sub

        Private Function GetOrCreatePIPoints(dataArchive As PIServer, inputTagNames As IEnumerable(Of String), prefix As String) As IList(Of PIPoint)
            ' Build a set of output tag names by sticking the prefix on each input tag name.
            Dim outputTagNames = inputTagNames.Select(Function(name) $"{prefix}{name}")

            ' Let's see if the output tags already exist.  Part of the exercise is to notify the end-user.
            ' It'd be nice to have this as a List instead of IList so we can later use AddRange.
            Dim outputTags = GetPIPoints(dataArchive, outputTagNames).ToList()
            For Each tag In outputTags
                Console.WriteLine($"Tag '{tag}' already exists.")
            Next

            ' Let's not think in terms of All Or None, as in either all are missing or none are.
            ' Where's the fun in that?  Instead we challenge outselves to check to see if any
            ' individual names are missing and only create that subset.
            Dim missingTagNames = outputTagNames.Except(outputTags.Select(Function(tag) tag.Name))
            If missingTagNames.Count() = 0 Then
                ' None were missing so we can return now.
                Return outputTags
            End If

            ' We have at least one, if not all, to create.

            ' When we fetch the input tag, e.g. "New York_Temperature", we want to also fetch its point attributes.
            ' Since all known inputs are "classic", we will work with that.
            ' But we don't need the Tag (Name) since we must use brand new names to create the output tags.
            Dim classicAttrs = GetPointAttributes(dataArchive, "classic").Except(ExcludedPointAttributes).ToArray()


            ' Simultaneously find tags and load the point attributes.
            ' Note the points we want to find are those input tags where we do not have an existing output tag.
            Dim inputTags = GetPIPoints(dataArchive, missingTagNames.Select(Function(name) name.Substring(prefix.Length)), classicAttrs)

            ' Prep a dictionary of tag definitions
            Dim definitions = New Dictionary(Of String, IDictionary(Of String, Object))()
            For Each tag In inputTags
                definitions($"{prefix}{tag.Name}") = tag.GetAttributes(classicAttrs)
            Next

            ' Make a bulk call to create all missing tags in one call.
            Dim createdPoints = dataArchive.CreatePIPoints(definitions)

            ' Add the new tags to our output list.
            outputTags.AddRange(createdPoints.Results)

            Return outputTags
        End Function

        Private Function GetPIPoints(dataArchive As PIServer, tagNames As IEnumerable(Of String), Optional attributeNames As IEnumerable(Of String) = Nothing) As IList(Of PIPoint)
            Return PIPoint.FindPIPoints(dataArchive, tagNames, attributeNames)
        End Function

        ' https://techsupport.osisoft.com/Documentation/PI-AF-SDK/Html/T_OSIsoft_AF_PI_PIPointClass.htm
        ' https://techsupport.osisoft.com/Documentation/PI-AF-SDK/Html/M_OSIsoft_AF_PI_PIPointClass_GetAttributes.htm

        ' One way to list the needed point attributes would be to explicitly list them as they appear
        ' in the PIPointsForPIAFSDKWorkshop spreadsheet.  This is fairly rigid and dependent upon 
        ' the spreadsheet.
        Public Function GetExplicitPointAttributes() As String()
            Return New String() {PICommonPointAttributes.Archiving,
                                 PICommonPointAttributes.CompressionDeviation,
                                 PICommonPointAttributes.CompressionPercentage,
                                 PICommonPointAttributes.CompressionMaximum,
                                 PICommonPointAttributes.CompressionMinimum,
                                 PICommonPointAttributes.ConversionFactor,
                                 PICommonPointAttributes.Descriptor,
                                 PICommonPointAttributes.ExceptionDeviation,
                                 PICommonPointAttributes.ExceptionPercentage,
                                 PICommonPointAttributes.ExceptionMaximum,
                                 PICommonPointAttributes.ExceptionMinimum,
                                 PICommonPointAttributes.ExtendedDescriptor,
                                 PICommonPointAttributes.FilterCode,
                                 PICommonPointAttributes.Location1,
                                 PICommonPointAttributes.Location2,
                                 PICommonPointAttributes.Location3,
                                 PICommonPointAttributes.Location4,
                                 PICommonPointAttributes.Location5,
                                 PICommonPointAttributes.PointSource,
                                 PICommonPointAttributes.PointType,
                                 PICommonPointAttributes.PointClassName,
                                 PICommonPointAttributes.Shutdown,
                                 PICommonPointAttributes.SquareRoot,
                                 PICommonPointAttributes.SourcePointID,
                                 PICommonPointAttributes.Step,
                                 PICommonPointAttributes.TotalCode,
                                 PICommonPointAttributes.TypicalValue,
                                 PICommonPointAttributes.UserInt1,
                                 PICommonPointAttributes.UserInt2,
                                 PICommonPointAttributes.UserReal1,
                                 PICommonPointAttributes.UserReal2,
                                 PICommonPointAttributes.Zero,
                                 PICommonPointAttributes.Span}
        End Function

        ' Another way to list the point attributes is to query all attributes for the "classic" point class.
        ' This will return some that you don't want to copy with new tags, such as the Tag name, point ID,
        ' create date, etc.  Therefore you will need another method that lists the point attributes to exclude.
        Private Function GetPointAttributes(dataArchive As PIServer, Optional ptClassName As String = "classic") As IList(Of String)
            Dim ptclass = dataArchive.PointClasses(ptClassName)
            Dim dict = ptclass.GetAttributes()
            Return dict.Keys.ToList()
        End Function

        ' These point attributes are Not to be copied when creating a New point.
        Private ReadOnly Property ExcludedPointAttributes() As String()
            Get
                Return New String() {PICommonPointAttributes.Tag,
                                     PICommonPointAttributes.PointID,
                                     PICommonPointAttributes.RecordNumber,
                                     PICommonPointAttributes.CreationDate,
                                     PICommonPointAttributes.Creator,
                                     PICommonPointAttributes.ChangeDate,
                                     PICommonPointAttributes.Changer,
                                     PICommonPointAttributes.PointClassID,
                                     PICommonPointAttributes.PointClassRevision}
            End Get
        End Property


        Private Sub CheckPipeEvents(pipe As PIDataPipe, ByRef counter As Integer)
            counter += 1
            If counter Mod 15 = 0 Then
                Console.WriteLine("{0}Press ESC to stop{0}", Environment.NewLine)
            End If
            Console.WriteLine("Check data pipe events at {0}", DateTime.Now)
            Dim pipeContents As AFListResults(Of PIPoint, AFDataPipeEvent) = pipe.GetUpdateEvents(1000)
            If pipeContents.Count = 0 Then
                Console.WriteLine("   No new values")
            End If
            For Each pipeEvent As AFDataPipeEvent In pipeContents
                'user-friendly string: [point] [timestamp]  [value]
                Dim s As String = String.Format("    {0} " & vbTab & "{1} " & vbTab & "{2}", pipeEvent.Value.PIPoint.Name, pipeEvent.Value.Timestamp, pipeEvent.Value.Value)
                Console.WriteLine(s)
            Next
            ' You are to monitor every second.
            Thread.Sleep(TimeSpan.FromSeconds(1))
        End Sub

    End Module

End Namespace
