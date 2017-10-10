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

namespace Exercise3_Solution
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
            // Writeable Properties
            public AFElement Element { get; set;}
            public string FeatureText { get; set; }
            public UOM EngUnit { get; set; } = null;
            public DataMethod DataMethod { get; set; } = DataMethod.RecordedValues;
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            // Readonly properties
            public AFAttribute Attribute => Element?.Attributes[FeatureText];
            public bool IsTimeRangeValid => ParseToAFTime(StartTime).HasValue && ParseToAFTime(EndTime).HasValue;
            public AFTimeRange? TimeRange => IsTimeRangeValid ? (AFTimeRange?)(new AFTimeRange(StartTime, EndTime)) : null;
            public bool IsValid => (Attribute != null) && IsTimeRangeValid;
            public bool IsEmpty => Equals(new MetaData());
            public MetaData Clone => new MetaData(this);

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

            public override string ToString()
            {
                // Let's be nice and show some context regarding the data values that were retrieved
                var sb = new StringBuilder();
                sb.Append(Attribute);
                if (EngUnit != null)
                    sb.Append($"; UOM={EngUnit.Abbreviation}");
                sb.Append($"; {DataMethod}");
                sb.Append($"; {TimeRange.Value}");
                return sb.ToString();
            }
        }

        private MetaData Selected = new MetaData();
        private const string InitialMetaText = "Data Values";

        public MainForm()
        {
            InitializeComponent();

            lblMetaInfo.Text = InitialMetaText;

            ////To have text align correctly in listbox, we will use a Fixed Width font
            //gridDataValues.Font = new Font("Consolas", 9, FontStyle.Regular);

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
            // Added for Exercise 3
            btnNotifications.Enabled = (Selected.Element != null);

            btnViewElement.Enabled = (Selected.Attribute != null);
            btnGetData.Enabled = Selected.IsValid;

            lblMetaInfo.ForeColor = (lblMetaInfo.Text == InitialMetaText || lblMetaInfo.Text == Selected.ToString()) ? Color.Black : Color.Red;
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            gridDataValues.Rows.Clear();

            var data = Selected.Attribute.Data;

            if (Selected.DataMethod == DataMethod.Summary)
            {
                SetupGrid("Summary Type", "Value", "Timestamp");
                var summaryDict = Selected.Attribute.Data.Summary(Selected.TimeRange.Value, AFSummaryTypes.All, AFCalculationBasis.TimeWeighted, AFTimestampCalculation.Auto);
                foreach (var summary in summaryDict)
                {
                    var value = summary.Value;
                    object pv = (value.UOM != null) ? $"{value.Value} {value.UOM.Abbreviation}" : value.Value;
                    gridDataValues.Rows.Add(summary.Key, pv, value.Timestamp);
                }
            }
            else
            {
                SetupGrid("Timestamp", "Value");
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
                    object pv = (value.UOM != null) ? $"{value.Value} {value.UOM.Abbreviation}" : value.Value;
                    gridDataValues.Rows.Add(value.Timestamp, pv);
                }
            }

            // Let's be nice and show some context regarding the data values that were retrieved
            lblMetaInfo.Text = Selected.ToString();

            CheckAllButtons();
        }

        private void SetupGrid(params string[] columnHeadings)
        {
            gridDataValues.ColumnCount = 0;
            for (var i = 0; i < columnHeadings.Length; i++)
            {
                gridDataValues.Columns.Add($"Column{i}", columnHeadings[i]);
                PrepColumn(i);
            }
        }

        private void PrepColumn(int columnIndex)
        {
            var col = gridDataValues.Columns[columnIndex];
            col.Width = 150;
            col.MinimumWidth = 80;

            var style = col.InheritedStyle;
            // The 2nd column (or Column[1]) just happens to be the Value column
            // for all DataMethods, so we will hardcode the right alignment here.
            if (string.Compare(col.HeaderText, "Value", ignoreCase: true) == 0)
            {
                style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else
            {
                style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            col.DefaultCellStyle = style;
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



        // How to show row number in the row header
        // http://stackoverflow.com/questions/9581626/show-row-number-in-row-header-of-a-datagridview
        private void gridDataValues_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid == null) return;
            PostPaintRowNumber(grid, e);
        }
        /// <summary>
        /// A sample method of drawing row numbers in a <see cref="DataGridView"/>'s row header.
        /// Intended to be called from the <see cref="RowPostPaint"/> event handler.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="e"></param>
        public static void PostPaintRowNumber(DataGridView grid, DataGridViewRowPostPaintEventArgs e)
        {
            // How to show row number in the row header
            // http://stackoverflow.com/questions/9581626/show-row-number-in-row-header-of-a-datagridview

            if (grid == null || e == null || e.RowIndex < 0)
                return;

            var id = (e.RowIndex + 1).ToString("N0");
            var font = new Font("Tahoma", 8.0f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);

            var centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Far;
            centerFormat.LineAlignment = StringAlignment.Center;

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth - 4, e.RowBounds.Height);

            e.Graphics.DrawString(id, font, SystemBrushes.InactiveCaptionText, headerBounds, centerFormat);
        }

        private void btnNotifications_Click(object sender, EventArgs e)
        {
            var dialogForm = new NotificationsForm(Selected.Element, Selected.StartTime, Selected.EndTime);
            dialogForm.ShowDialog(this);
        }
    }
}
