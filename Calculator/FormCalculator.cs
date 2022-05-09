using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class FormCalculator : Form
    {
        public FormCalculator()
        {
            InitializeComponent();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ToolStripButtonPlay_Click(object sender, EventArgs e)
        {
            Scripting script = new Scripting();
            
            var expression = scintillaEditor.Text.Trim();
            script.Evaluate(expression, EvaluateFinishCallBack);
        }

        private void FormCalculator_Load(object sender, EventArgs e)
        {
            string text = global::Calculator.Properties.Resources.calculator;
            webBrowser1.DocumentText = text;
        }

        public void EvaluateFinishCallBack(string id, string expression, string result, string error)
        {
            if (expression != "")
            {
                object y = webBrowser1.Document.InvokeScript("addItem", new string[] { "1", result, expression });
            }
        }
    }
}
