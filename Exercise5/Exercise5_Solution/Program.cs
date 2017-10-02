using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using OSIsoft.AF.PI;
using OSIsoft.AF;
using OSIsoft.AF.Data;
using OSIsoft.AF.Time;

namespace Exercise5_Solution
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataArchive = new PIServers().DefaultPIServer;

            var points = GetOrCreatePIPoints(dataArchive, new string[] { "New York_Pressure", "New York_Temperature", "New York_Humidity" }, "AFSDKWS_");

            //Create data pipe
            PIDataPipe pipe = new PIDataPipe(AFDataPipeType.Snapshot);
            pipe.AddSignups(points);

            var counter = 0;
            do
            {
                while (!Console.KeyAvailable)
                {
                    CheckPipeEvents(pipe, ref counter);
                };
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            pipe.Close();
            pipe.Dispose();
        }

        private static IList<PIPoint> GetOrCreatePIPoints(PIServer dataArchive, IEnumerable<string> inputTagNames, string prefix)
        {
            // Build a set of output tag names by sticking the prefix on each input tag name.
            var outputTagNames = inputTagNames.Select(name => $"{prefix}{name}");

            // Let's see if the output tags already exist.  Part of the exercise is to notify the end-user.
            // It'd be nice to have this as a List instead of IList so we can later use AddRange.
            var outputTags = GetPIPoints(dataArchive, outputTagNames).ToList();
            foreach (var tag in outputTags)
            {
                Console.WriteLine($"Tag '{tag}' already exists.");
            }

            // Let's not think in terms of All Or None, as in either all are missing or none are.
            // Where's the fun in that?  Instead we challenge outselves to check to see if any
            // individual names are missing and only create that subset.
            var missingTagNames = outputTagNames.Except(outputTags.Select(tag => tag.Name));
            if (missingTagNames.Count() == 0)
            {
                // None were missing so we can return now.
                return outputTags;
            }

            // We have at least one, if not all, to create.

            // When we fetch the input tag, e.g. "New York_Temperature", we want to also fetch its point attributes.
            // Since all known inputs are "classic", we will work with that.
            // But we don't need the Tag (Name) since we must use brand new names to create the output tags.
            var classicAttrs = GetPointAttributes(dataArchive, "classic").Except(ExcludedPointAttributes).ToArray();


            // Simultaneously find tags and load the point attributes.
            // Note the points we want to find are those input tags where we do not have an existing output tag.
            var inputTags = GetPIPoints(dataArchive, 
                                        missingTagNames.Select(name => name.Substring(prefix.Length)),
                                        classicAttrs);

            // Prep a dictionary of tag definitions
            var definitions = new Dictionary<string, IDictionary<string, object>>();
            foreach (var tag in inputTags)
            {
                definitions[$"{prefix}{tag.Name}"] = tag.GetAttributes(classicAttrs);
            }

            // Make a bulk call to create all missing tags in one call.
            var createdPoints = dataArchive.CreatePIPoints(definitions);

            // Add the new tags to our output list.
            outputTags.AddRange(createdPoints.Results);

            return outputTags;
        }

        private static IList<PIPoint> GetPIPoints(PIServer dataArchive, IEnumerable<string> tagNames, IEnumerable<string> attributeNames = null)
        {
            return PIPoint.FindPIPoints(dataArchive, tagNames, attributeNames);
        }

        // https://techsupport.osisoft.com/Documentation/PI-AF-SDK/Html/T_OSIsoft_AF_PI_PIPointClass.htm

        // https://techsupport.osisoft.com/Documentation/PI-AF-SDK/Html/M_OSIsoft_AF_PI_PIPointClass_GetAttributes.htm

        
        // One way to list the needed point attributes would be to explicitly list them as they appear
        // in the PIPointsForPIAFSDKWorkshop spreadsheet.  This is fairly rigid and dependent upon 
        // the spreadsheet.
        private static string[] GetExplicitPointAttributes => new string[] { PICommonPointAttributes.Archiving
                                                                            , PICommonPointAttributes.CompressionDeviation
                                                                            , PICommonPointAttributes.CompressionPercentage
                                                                            , PICommonPointAttributes.CompressionMaximum
                                                                            , PICommonPointAttributes.CompressionMinimum
                                                                            , PICommonPointAttributes.ConversionFactor
                                                                            , PICommonPointAttributes.Descriptor
                                                                            , PICommonPointAttributes.ExceptionDeviation
                                                                            , PICommonPointAttributes.ExceptionPercentage
                                                                            , PICommonPointAttributes.ExceptionMaximum
                                                                            , PICommonPointAttributes.ExceptionMinimum
                                                                            , PICommonPointAttributes.ExtendedDescriptor
                                                                            , PICommonPointAttributes.FilterCode
                                                                            , PICommonPointAttributes.Location1
                                                                            , PICommonPointAttributes.Location2
                                                                            , PICommonPointAttributes.Location3
                                                                            , PICommonPointAttributes.Location4
                                                                            , PICommonPointAttributes.Location5
                                                                            , PICommonPointAttributes.PointSource
                                                                            , PICommonPointAttributes.PointType
                                                                            , PICommonPointAttributes.PointClassName
                                                                            , PICommonPointAttributes.Shutdown
                                                                            , PICommonPointAttributes.SquareRoot
                                                                            , PICommonPointAttributes.SourcePointID
                                                                            , PICommonPointAttributes.Step
                                                                            , PICommonPointAttributes.TotalCode
                                                                            , PICommonPointAttributes.TypicalValue
                                                                            , PICommonPointAttributes.UserInt1
                                                                            , PICommonPointAttributes.UserInt2
                                                                            , PICommonPointAttributes.UserReal1
                                                                            , PICommonPointAttributes.UserReal2
                                                                            , PICommonPointAttributes.Zero
                                                                            , PICommonPointAttributes.Span
                                                                            };

        // Another way to list the point attributes is to query all attributes for the "classic" point class.
        // This will return some that you don't want to copy with new tags, such as the Tag name, point ID,
        // create date, etc.  Therefore you will need another method that lists the point attributes to exclude.
        private static IList<string> GetPointAttributes(PIServer dataArchive, string ptClassName)
        {
            var ptclass = dataArchive.PointClasses[ptClassName];
            var dict = ptclass.GetAttributes();
            return dict.Keys.ToList();
        }

        // These point attributes are not to be copied when creating a new point.
        private static string[] ExcludedPointAttributes => new string[] { PICommonPointAttributes.Tag
                                                                        , PICommonPointAttributes.PointID
                                                                        , PICommonPointAttributes.RecordNumber
                                                                        , PICommonPointAttributes.CreationDate
                                                                        , PICommonPointAttributes.Creator
                                                                        , PICommonPointAttributes.ChangeDate
                                                                        , PICommonPointAttributes.Changer
                                                                        , PICommonPointAttributes.PointClassID
                                                                        , PICommonPointAttributes.PointClassRevision
                                                                        };

        private static void CheckPipeEvents(PIDataPipe pipe, ref int counter)
        {
            if (counter++ % 15 == 0)
            {
                Console.WriteLine("{0}Press ESC to stop{0}", Environment.NewLine);
            }
            Console.WriteLine("Check data pipe events at {0}", DateTime.Now);
            AFListResults<PIPoint, AFDataPipeEvent> pipeContents = pipe.GetUpdateEvents(1000);
            if (pipeContents.Count == 0)
            {
                Console.WriteLine("   No new values");
            }
            foreach (AFDataPipeEvent pipeEvent in pipeContents)
            {
                //user-friendly string: [point] [timestamp]  [value]
                string s = string.Format("    {0} \t{1} \t{2}"
                                        , pipeEvent.Value.PIPoint.Name
                                        , pipeEvent.Value.Timestamp
                                        , pipeEvent.Value.Value);
                Console.WriteLine(s);
            }
            // You are to monitor every second.
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

    }
}
