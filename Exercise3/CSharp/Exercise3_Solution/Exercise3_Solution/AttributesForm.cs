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

namespace Exercise3_Solution
{
    public partial class AttributesForm : Form
    {
        private AFElement Element { get; }

        public AttributesForm(AFElement element)
        {
            Element = element;
            InitializeComponent();

            if (element != null)
            {
                lblElement.Text = element.GetPath(element.Database);
                afViewControl1.AFSetObject(element, null, null, null);
                // This should jump view to "Attributes" tab.
                afViewControl1.AFSelection = element.Attributes;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
