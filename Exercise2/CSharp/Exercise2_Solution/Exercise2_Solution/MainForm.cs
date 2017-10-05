using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OSIsoft.AF;
using OSIsoft.AF.Asset;
using OSIsoft.AF.UnitsOfMeasure;
using OSIsoft.AF.Time;
using OSIsoft.AF.Notification;
using OSIsoft.AF.EventFrame;
using OSIsoft.AF.Data;
using OSIsoft.AF.Search;

namespace Exercise2_Solution
{
    public partial class MainForm : Form
    {
        private enum Feature { Temperature, CloudCover, WindSpeed, Visibility, Pressure, Humidity }
        private enum DataMethod { RecordedValues, InterpolatedValues, Summary }

        private static IDictionary<Feature, string> _mapToUomName => new Dictionary<Feature, string>() { { Feature.CloudCover, "percent" }
                                                                                                       , { Feature.Humidity, "percent" }
                                                                                                       , { Feature.Pressure, "millibar" }
                                                                                                       , { Feature.Temperature, "degree Celsius" }
                                                                                                       , { Feature.Visibility, "kilometer" }
                                                                                                       , { Feature.WindSpeed, "kilometer per hour" }
                                                                                                       };

        private class MetaData : IEquatable<MetaData>
        {
            public MetaData()
            { }

            public MetaData(MetaData source)
            {
                Element = source.Element;
                FeatureText = source.FeatureText;
                EngUnit = source.EngUnit;
                DataMethod = source.DataMethod;
                StartTime = source.StartTime;
                EndTime = source.EndTime;
            }

            public AFElement Element { get; set;}
            public string FeatureText { get; set; }
            public UOM EngUnit { get; set; } = null;
            public DataMethod DataMethod { get; set; } = DataMethod.RecordedValues;
            public string StartTime { get; set; }
            public string EndTime { get; set; }

            public AFAttribute Attribute => Element?.Attributes[FeatureText];

            public bool IsTimeRangeValid => ParseToAFTime(StartTime).HasValue && ParseToAFTime(EndTime).HasValue;

            public AFTimeRange? TimeRange => IsTimeRangeValid ? (AFTimeRange?)(new AFTimeRange(StartTime, EndTime)) : null;

            public bool IsValid => (Attribute != null) && IsTimeRangeValid;

            public bool Equals(MetaData other)
            {
                if ((object)other == null)
                {
                    return false;
                }
                return (Attribute == other.Attribute)
                    && (DataMethod == other.DataMethod)
                    && (EngUnit == other.EngUnit)
                    && (TimeRange == other.TimeRange);
            }

            public bool IsEmpty => Equals(new MetaData());

            public MetaData Clone => new MetaData(this);
        }

        private MetaData Selected = new MetaData();
        private MetaData Retrieved = new MetaData();

        public MainForm()
        {
            InitializeComponent();

            //To have text align correctly in listbox, we will use a Fixed Width font
            lboxDataValues.Font = new Font("Consolas", 9, FontStyle.Regular);

            FillIntervalPicks();
            cboxInterval.Enabled = false;

            rbTemperature.Checked = true;
            Selected.FeatureText = rbTemperature.Text;
            FillUomPicks(Feature.Temperature);

            rbRecordedValues.Checked = true;
            Selected.DataMethod = DataMethod.RecordedValues;

            Selected.StartTime = tbStartTime.Text;
            Selected.EndTime = tbEndTime.Text;

            afDatabasePicker1.SystemPicker = piSystemPicker1;
            afTreeView1.AFRoot = afDatabasePicker1.AFDatabase;

            CheckAllButtons();
        }

        private PISystem AssetServer => piSystemPicker1.PISystem;
        private AFDatabase Database => afDatabasePicker1.AFDatabase;

        private void afDatabasePicker1_SelectionChange(object sender, OSIsoft.AF.UI.SelectionChangeEventArgs e)
        {
            afTreeView1.AFRoot = Database?.Elements;
            CheckAllButtons();
        }

        private void afTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Selected.Element = afTreeView1.AFSelection as AFElement;
            CheckAllButtons();
        }

        private void rbFeature_CheckChanged(object sender, EventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null || !radioButton.Checked)
                return;

            // Continue for the checked radio button
            Selected.FeatureText = radioButton.Text;
            var feature = (Feature)GetByText(typeof(Feature), radioButton.Text);
            FillUomPicks(feature);

            CheckAllButtons();
        }

        private void FillUomPicks(Feature feature)
        {
            cboxUom.Items.Clear();
            var defaultUOM = AssetServer?.UOMDatabase.UOMs[_mapToUomName[feature]];
            if (defaultUOM != null)
            {
                cboxUom.Items.Add($"< default ( {defaultUOM.Abbreviation} ) >");
                cboxUom.SelectedIndex = 0;
                foreach (var uom in defaultUOM.Class.UOMs)
                {
                    cboxUom.Items.Add(uom);
                }
            }
        }

        private void rbMethod_CheckChanged(object sender, EventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null || !radioButton.Checked)
            {
                return;
            }

            // Continue for the checked radio button
            Selected.DataMethod = (DataMethod)GetByText(typeof(DataMethod), radioButton.Text);
            cboxInterval.Enabled = (Selected.DataMethod == DataMethod.InterpolatedValues);
            CheckAllButtons();
        }

        
        private static object GetByText(Type enumType, string text) => Enum.Parse(enumType, RemoveBlanks(text));
        private static string RemoveBlanks(string text) => text.Replace(" ", string.Empty);


        // While this seems to be a likely candidate for an extension method,
        // there is a strong possibility that text could be null, which
        // doesn't work well with extension methods.
        public static AFTime? ParseToAFTime(string text)
        {
            try
            {
                var time = AFTime.MaxValue;
                // AFTime.TryParse
                // https://techsupport.osisoft.com/Documentation/PI-AF-SDK/Html/M_OSIsoft_AF_Time_AFTime_TryParse_2.htm
                // DateTime.TryParse
                // https://msdn.microsoft.com/en-us/library/ch92fbc1(v=vs.110).aspx
                if (AFTime.TryParse(text, out time))
                {
                    return time;
                }
            }
            catch
            { }
            return null;
        }
        private AFTime? GetStartTime() => ParseToAFTime(tbStartTime.Text);
        private AFTime? GetEndTime() => ParseToAFTime(tbEndTime.Text);

        private void TimeBox_KeyUp(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            TimeBoxValidate(textBox);
            if (textBox == tbStartTime)
            {
                Selected.StartTime = textBox.Text;
            }
            else if (textBox == tbEndTime)
            {
                Selected.EndTime = textBox.Text;
            }
            CheckAllButtons();
        }

        private void TimeBoxValidate(TextBox textBox)
        {
            if (textBox == null)
                return;

            if (ParseToAFTime(textBox.Text).HasValue)
            {
                textBox.BackColor = Color.White;
                textBox.ForeColor = Color.Black;
            }
            else
            {
                textBox.BackColor = Color.LightYellow;
                textBox.ForeColor = Color.Red;
            }
        }

        private void CheckAllButtons()
        {
            btnViewElement.Enabled = (Selected.Attribute != null);
            btnGetData.Enabled = Selected.IsValid;

            lblMetaInfo.ForeColor = (Selected.Equals(Retrieved) || Retrieved.IsEmpty) ? Color.Black : Color.Red;
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            lboxDataValues.Items.Clear();

            var data = Selected.Attribute.Data;

            if (Selected.DataMethod == DataMethod.Summary)
            {
                var summaryDict = Selected.Attribute.Data.Summary(Selected.TimeRange.Value, AFSummaryTypes.All, AFCalculationBasis.TimeWeighted, AFTimestampCalculation.Auto);
                foreach (var summary in summaryDict)
                {
                    var value = summary.Value;
                    var uomAbbr = (value.UOM != null) ? $" {value.UOM.Abbreviation}" : "";
                    lboxDataValues.Items.Add($"{summary.Key,17}\t{value.Value}{uomAbbr}\t{value.Timestamp}");
                }
            }
            else
            {
                var values = new AFValues();

                if (Selected.DataMethod == DataMethod.InterpolatedValues)
                {
                    var interval = (TimeSpan)cboxInterval.SelectedItem;
                    values = data.InterpolatedValues(Selected.TimeRange.Value, new AFTimeSpan(interval), Selected.EngUnit, null, false);
                }
                else
                {
                    values = data.RecordedValues(Selected.TimeRange.Value, AFBoundaryType.Interpolated, Selected.EngUnit, null, false, 0);
                }

                foreach (var value in values)
                {
                    var uomAbbr = (value.UOM != null) ? $" {value.UOM.Abbreviation}" : "";
                    lboxDataValues.Items.Add($"{value.Timestamp.LocalTime}\t{value.Value:N1}{uomAbbr}");
                }
            }

            // Let's be nice and show some context regarding the data values that were retrieved
            var sb = new StringBuilder();
            sb.Append(Selected.Attribute);
            if (Selected.EngUnit != null)
                sb.Append($"; UOM={Selected.EngUnit.Abbreviation}");
            sb.Append($"; {Selected.DataMethod}");
            sb.Append($"; {Selected.TimeRange.Value}");
            lblMetaInfo.Text = sb.ToString();

            Retrieved = Selected.Clone;
            CheckAllButtons();
        }

        private void cboxUom_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbox = sender as ComboBox;
            if (cbox == null)
                return;
            if (cbox.SelectedIndex < 1)
            {
                Selected.EngUnit = null;
            }
            else
            {
                Selected.EngUnit = (UOM)cbox.SelectedItem;
            }

            CheckAllButtons();
        }

        private void piSystemPicker1_SelectionChange(object sender, OSIsoft.AF.UI.SelectionChangeEventArgs e)
        {
            if (AssetServer == null)
                return;
            var feature = (Feature)GetByText(typeof(Feature), Selected.FeatureText);
            FillUomPicks(feature);
        }

        private void btnViewElement_Click(object sender, EventArgs e)
        {
            var dialogForm = new AttributesForm(Selected.Element);
            dialogForm.ShowDialog(this);
        }

        private void FillIntervalPicks()
        {
            cboxInterval.Items.Clear();
            cboxInterval.Items.Add(TimeSpan.FromMinutes(5));
            cboxInterval.Items.Add(TimeSpan.FromMinutes(15));
            cboxInterval.Items.Add(TimeSpan.FromHours(1));
            cboxInterval.SelectedIndex = 2;
            cboxInterval.Items.Add(TimeSpan.FromHours(8));
            cboxInterval.Items.Add(TimeSpan.FromDays(1));
            cboxInterval.Items.Add(TimeSpan.FromDays(7));
        }
    }
}
