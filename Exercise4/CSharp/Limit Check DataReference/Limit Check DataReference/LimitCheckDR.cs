/***************************************************************************
   Copyright 2017 OSIsoft, LLC.
   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at
       http://www.apache.org/licenses/LICENSE-2.0
   
   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 ***************************************************************************/

//*********************************************************************************************************
// PIDevClub.DataReference.LimitCheck.dll
//---------------------------------------------------------------------------------------------------------
// This custom data reference will accept an input attribute, automatically find its related limit traits,
// and compare the value of the input attribute against the limits.  Note that there could be 0-4 different
// limit traits { Lo, Hi, LoLo, HiHi }.  
//
// An Int32 value is returned representing 1 of 5 possible states:
//      -2 : <= LoLo
//      -1 : <= Lo (but not LoLo)
//       0 : Normal, i.e. within limits
//      +1 : >= Hi (but not HiHi)
//      +2 : >= HiHi
//
// The result Attribute could be using Int32 data type, although a raw number lacks context.
// It is recommended to created an AFEnumerationSet based on the above codes with desired text.
// You may use different enumeration sets with different text, but the codes must be the same.
//---------------------------------------------------------------------------------------------------------
// HISTORY:
//         Developer             Date     Reason
// -------------------------  ----------  -----------------------------------------------------------------
// Rick Davin                 2017-09-28  Created for Exercise 4 in AF SDK online course. 
//*********************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.ComponentModel;
using OSIsoft.AF;
using OSIsoft.AF.Asset;
using OSIsoft.AF.Time;
using OSIsoft.AF.Data;
using OSIsoft.AF.UnitsOfMeasure;

namespace LimitCheckDR
{
    [Guid("8DA87B2A-38D9-4CF6-9BA0-C75314A6B5DC"),
     Serializable(),
     Description("Limit Check;Compares a measurement to its limit traits.")]
    public class LimitCheckDR : AFDataReference
    {
        // Rather than look up and validate the limit traits each instant data is requested,
        // we will keep a tiny cache of no more than 5 attributes in memory.
        // You are free to adjust the cache duration, though I would recommend no more than 2 minutes
        // and no less than 10 seconds.  If you insist on no caching, you could disable
        // the code in GetInputs or use a span of 0 seconds.
        // Bonus Challenge: You could make the duration be a setting in the ConfigString.
        private static TimeSpan _cacheRefreshDuration = TimeSpan.FromSeconds(30);

        private DateTime _lastCachedTime = DateTime.UtcNow.AddDays(-1);
        private AFAttributeList _cachedInputAttrs = new AFAttributeList();
        // The default measurement name is relative dot reference to the parent attribute.
        private const string DefaultMeasPath = "..";
        private string _measAttrName = DefaultMeasPath;

        private bool UseParentAttribute => string.IsNullOrWhiteSpace(_measAttrName) || _measAttrName == DefaultMeasPath;

        private AFAttributeTrait[] AllowedTraits => new AFAttributeTrait[] { AFAttributeTrait.LimitLo
                                                                           , AFAttributeTrait.LimitHi
                                                                           , AFAttributeTrait.LimitLoLo
                                                                           , AFAttributeTrait.LimitHiHi };

        private class TripState
        {
            public const int None = 0;
            public const int LowLow = -2;
            public const int Low = -1;
            public const int High = 1;
            public const int HighHigh = 2;
        }


        // https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/P_OSIsoft_AF_Asset_AFDataReference_ConfigString.htm
        public override string ConfigString
        {
            get
            {
                return $"MeasAttr={_measAttrName}";
            }
            set
            {
                // We will always force the cache to refresh anytime we change ConfigString.
                _lastCachedTime = DateTime.UtcNow.AddDays(-1);
                // We clear out any previously set values to their defaults.  
                // For now, we only have 1 to worry about but that could change in future versions.
                _measAttrName = DefaultMeasPath;
                // With only 1 name=value pair, we don't have a semi-colon delimiter, but the 
                // example shown will pretend there is one.  This allows for future expansion for more name=value pairs.
                var pairs = value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var pair in pairs)
                {
                    // Question: what to do if tokens.Count is not 2 as expected?
                    var tokens = pair.Split(new char[] { '=' }, StringSplitOptions.None);
                    switch (tokens[0].ToUpperInvariant())
                    {
                        case "MEASATTR":
                            // You will note we do very little validation on the _measAttrName.
                            // That should not be a concern of the ConfigString setter, whose only
                            // concern should be parsing the string.  We defer to other methods,
                            // such as GetInputs, to validate if the parsed string refers to valid
                            // AFObjects.
                            if (tokens.Length > 1 && !string.IsNullOrEmpty(tokens[1]))
                            {
                                _measAttrName = tokens[1].Trim();
                            }
                            break;
                        default:
                            throw new ArgumentException("Invalid parameter '{tokens[0]}'.");
                    }
                }
                // Important to persist changes back to database via this protected method call:
                SaveConfigChanges();
            }
        }


        // https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/M_OSIsoft_AF_Asset_AFDataReference_GetInputs.htm
        public override AFAttributeList GetInputs(object context)
        {
            // Internal time keeping is done by UTC so we completely avoid even the slightest hint of any DST issues.
            if ((DateTime.UtcNow - _lastCachedTime) < _cacheRefreshDuration)
            {
                return _cachedInputAttrs;
            }

            // PRO TIP: although _cachedInputAttrs is accessible to the entire class instance, you want to avoid
            // using _cachedInputAttrs.Add or AddRange in name of thread safety.  The safer route is to build
            // the list in its own local variable, and once that local list is completely created, you may then
            // assign that local variable to _cachedInputAttrs.
            // To help emphasize that, the local list is created in its own method.
            _cachedInputAttrs = CreateBrandNewInputList(context);

            // Since the above could throw an exception, we only want to mark _lastCachedTime AFTER a
            // successful creation of the inputs.
            _lastCachedTime = DateTime.UtcNow;

            return _cachedInputAttrs;
        }


        private AFAttributeList CreateBrandNewInputList(object context)
        {
            // PRO TIP:
            // See comments in GetInputs where you want to avoid _cachedInputAttrs.Add or AddRange.
            // For thread safety, use a brand new list that is local to this method.

            // Start with a brand new list
            var brandNewList = new AFAttributeList();

            AFAttribute measurement = null;

            // First and foremost we need a measurement attribute, which is the centerpoint to compare against limits.
            if (UseParentAttribute)
            {
                // https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/P_OSIsoft_AF_Asset_AFDataReference_Attribute.htm
                // https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/P_OSIsoft_AF_Asset_AFAttribute_Parent.htm
                measurement = Attribute.Parent;
                if (measurement == null)
                {
                    throw new Exception("Root-level attribute does not have a parent.  You must define 'MeasAttr=something' in the ConfigString.");
                }
            }
            else
            {
                // Let's offer some bit of name substitution.
                // However, the GetInputs method lacks any timeContext, which restricts @value substitution
                // to current values only.  This restriction is fine for static attributes.
                // https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/T_OSIsoft_AF_AFNameSubstitutionType.htm
                var path = SubstituteParameters(_measAttrName, this, context, timeContext: null);
                // Note that the final fetch of the measurement attribute is *relative* to the current Attribute.
                // https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/P_OSIsoft_AF_Asset_AFAttribute_Attributes.htm
                measurement = Attribute.Attributes[path];
                if (measurement == null)
                {
                    throw new Exception($"MeasAttr '{_measAttrName}' not found.  Check your ConfigString.");
                }
            }

            if (!IsNumericType(Type.GetTypeCode(measurement.Type)))
            {
                throw new Exception($"MeasAttr does not have a numeric Type.");
            }

            // If the list will have any items, the measurement will always be at Index 0.
            brandNewList.Add(measurement);

            // Let the CDR automatically fetch the associated limits.
            // These could come back in any order, plus some or all may be missing!
            // Geez, doesn't that make it fun and challenging!
            // https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/M_OSIsoft_AF_Asset_AFAttribute_GetAttributesByTrait.htm
            brandNewList.AddRange(measurement.GetAttributesByTrait(AllowedTraits));

            return brandNewList;
        }


        // In order for the Step property to be accessible from other apps such PI Vision or ProcessBook,
        // the CDR will need to support the ZeroAndSpan method.
        public override bool Step => true;


        // Your CDR does not necessarily have to override GetValues in order to support it.  
        // You may use the default implementation, which does not require additional coding on your part.
        // https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/P_OSIsoft_AF_Asset_AFDataReference_SupportedMethods.htm
        public override AFDataReferenceMethod SupportedMethods => AFDataReferenceMethod.GetValue 
                                                                | AFDataReferenceMethod.GetValues 
                                                                | AFDataReferenceMethod.ZeroAndSpan;


        // For Rich Data Access support, we will merely declare what we want supported, and then rely on the base implementation.
        // What that means is we don't have to write any code in order to support these methods.  Nice.  Very nice.
        // https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/P_OSIsoft_AF_Asset_AFDataReference_SupportedDataMethods.htm
        public override AFDataMethods SupportedDataMethods => DefaultSupportedDataMethods;


        // PRO TIP: the allows analytics and other clients to work more efficiently with a calcuation DR.
        protected override bool? IsSupportedDataMethod(AFDataMethods dataMethods) => IsDefaultSupportedDataMethod(dataMethods);
  

        // https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/M_OSIsoft_AF_Asset_AFDataReference_GetValue_1.htm
        public override AFValue GetValue(object context, object timeContext, AFAttributeList inputAttributes, AFValues inputValues)
        {
            // Important to note that the order of inputValues matches the order of inputAttributes.

            // Note that timeContext is an object. 
            // We need to examine it further in order to resolve it to an AFTime.
            var time = ToAFTime(timeContext);

            AFValue measurement = null;
            AFValue low = null;
            AFValue high = null;
            AFValue lowlow = null;
            AFValue highhigh = null;

            // https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/P_OSIsoft_AF_Asset_AFAttribute_Trait.htm
            // https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/T_OSIsoft_AF_Asset_AFAttributeTrait.htm
            for (var i = 0; i < inputAttributes.Count; i++)
            {
                if (i == 0)
                {
                    measurement = inputValues[i];
                }
                else if (inputAttributes[i].Trait == AFAttributeTrait.LimitLo)
                {
                    low = inputValues[i];
                }
                else if (inputAttributes[i].Trait == AFAttributeTrait.LimitHi)
                {
                    high = inputValues[i];
                }
                else if (inputAttributes[i].Trait == AFAttributeTrait.LimitLoLo)
                {
                    lowlow = inputValues[i];
                }
                else if (inputAttributes[i].Trait == AFAttributeTrait.LimitHiHi)
                {
                    highhigh = inputValues[i];
                }
            }
            // Remember any of the passed AFValues could be null if the limit trait is not defined.
            // This is a fact of life and reflects the many possibilities within a given process unit.
            return Calculation(time, measurement
                                   , low
                                   , high
                                   , lowlow
                                   , highhigh);
        }


        private AFValue Calculation(AFTime time, AFValue measurement, AFValue low, AFValue high, AFValue lowlow, AFValue highhigh)
        {
            // Our custom ToDouble returns double.NaN for null, missing, bad data, or bad conversions.
            var numericMeasurement = ToDouble(measurement);
            if (double.IsNaN(numericMeasurement))
            {
                // https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/M_OSIsoft_AF_Asset_AFValue_CreateSystemStateValue.htm
                return AFValue.CreateSystemStateValue(Attribute, AFSystemStateCode.NoResult, time);
            }
            // Remember: any or all of the limits could be null.  In those bad cases, a double.NaN is sent.
            var calc = Calculation(numericMeasurement, ToDouble(low), ToDouble(high), ToDouble(lowlow), ToDouble(highhigh));
            return new AFValue(Attribute, calc, time);
        }


        private static int Calculation(double measurement, double low, double high, double lowlow, double highhigh)
        {
            // If any limits were missing or bad, they will be a NaN here.
            // A NaN used in comparisons will return false, which is what we want below.  That is,
            // a comparison will return true only if the limit is not NaN and the limit has been met.
            if (measurement >= highhigh)
            {
                return TripState.HighHigh;
            }
            if (measurement <= lowlow)
            {
                return TripState.LowLow;
            }
            if (measurement >= high)
            {
                return TripState.High;
            }
            if (measurement <= low)
            {
                return TripState.Low;
            }
            return TripState.None;
        }


        private static AFTime ToAFTime(object timeContext)
        {
            if (timeContext is AFTime)
            {
                return (AFTime)timeContext;
            }
            else if (timeContext is AFTimeRange)
            {
                return ((AFTimeRange)timeContext).EndTime;
            }
            return AFTime.NowInWholeSeconds;
        }


        private static bool IsNumericType(TypeCode typeCode)
        {
            switch (typeCode)
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                default:
                    return false;
            }
        }


        private static double ToDouble(AFValue value)
        {
            if (value == null || !value.IsGood || !IsNumericType(value.ValueTypeCode))
            {
                return double.NaN;
            }
            // https://techsupport.osisoft.com/Documentation/PI-AF-SDK/html/M_OSIsoft_AF_Asset_AFValue_ValueAsDouble.htm
            return value.ValueAsDouble();
        }
    }
}
