using System;
using System.Windows.Forms;

using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using System.IO;
using System.Drawing;



namespace Calculator
{

    public class Tools
    {
        public void Show(string message)
        {
            MessageBox.Show(message);
        }

    }
}
