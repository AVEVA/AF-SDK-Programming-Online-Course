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

using OSIsoft.AF.Asset;
using OSIsoft.AF.Time;
using OSIsoft.AF.Search;
using OSIsoft.AF.EventFrame;
using OSIsoft.AF.Notification;

namespace Exercise3_Solution
{
    public partial class NotificationsForm : Form
    {
        private AFElement Element { get; }

        // Constructor requires an AFElement and 2 strings for start and end time.
        public NotificationsForm(AFElement element, string startTime, string endTime)
        {
            Element = element;

            InitializeComponent();

            tbStartTime.Text = startTime;
            tbEndTime.Text = endTime;

            TimeBoxValidate(tbStartTime);
            TimeBoxValidate(tbEndTime);

            if (Element != null)
            {
                Text += $" - {Element.Name}";
                var rules = Element.NotificationRules;
                lboxNotificationRules.Items.AddRange(rules.ToArray());
                if (lboxNotificationRules.Items.Count > 0)
                {
                    lboxNotificationRules.SelectedIndex = 0;
                }
            }

            CheckAllButtons();
            FindNotificationInstances();
        }

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
            CheckAllButtons();
            FindNotificationInstances();
        }

        private void TimeBoxValidate(TextBox textBox)
        {
            if (textBox == null)
                return;

            AFTime? time = ParseToAFTime(textBox.Text);
            textBox.Tag = time;

            if (time.HasValue)
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
            var startTime = tbStartTime.Tag as AFTime?;
            var endTime = tbEndTime.Tag as AFTime?;

            var okayToView = (Element != null) && startTime.HasValue && endTime.HasValue && (lboxNotificationRules.SelectedIndex >= 0);

            btnView.Enabled = okayToView;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            FindNotificationInstances();
        }

        private void lboxNotificationRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAllButtons();
            FindNotificationInstances();
        }

        //Get instances, which are Event Frames beginning with PI Notifications 2016
        private void FindNotificationInstances()
        {
            gridNotificationInstances.Rows.Clear();

            if (!btnView.Enabled)
                return;

            var rule = lboxNotificationRules.SelectedItem as AFNotificationRule;
            var startTime = tbStartTime.Tag as AFTime?;
            var endTime = tbEndTime.Tag as AFTime?;

            var database = Element.Database;

            //We echo the current notification rule search criteria, which includes literal "Criteria:"
            //but include the Target element in the filter.
            string query = string.Format("Element:'{0}' {1}", Element.GetPath(database), rule.Criteria);
            AFEventFrameSearch search = new AFEventFrameSearch(database, "", AFSearchMode.Overlapped, startTime.Value, endTime.Value, query);
            search.CacheTimeout = TimeSpan.FromMinutes(5);
            IEnumerable<AFEventFrame> instances = search.FindEventFrames(fullLoad: true);

            //Populate listbox with notification instances, which are just event frames in PI Notifications 2016 or later.
            foreach (AFEventFrame instance in instances)
            {
                // For multi-trigger analyses, the "Start Trigger Name" would be null on the parent event frame if there were multiple triggers.
                // We should suppress those.  Note the "Start Trigger Name" is our default attribute.
                object defaultObject = instance.DefaultAttribute?.GetValue() ?? (object)string.Empty;
                if (!string.IsNullOrWhiteSpace(defaultObject.ToString()))
                {
                    object endObject = (instance.EndTime == AFTime.MaxValue) ? (object)"ongoing/active" : instance.EndTime.LocalTime;
                    gridNotificationInstances.Rows.Add(instance.StartTime.LocalTime, endObject, defaultObject);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
