using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace Calculator
{
    public delegate void TaskCompletedCallBack(string id, string expression, string result, string error = "");

    public class Scripting
    {
        public void Evaluate(string expression, TaskCompletedCallBack taskCompletedCallBack = null)
        {

            Job.DoJob(
                new DoWorkEventHandler(
                    delegate (object o, DoWorkEventArgs args)
                    {

                        string result = this.EvaluateExpression(expression);
                        args.Result = result;

                    }
                ),
                new RunWorkerCompletedEventHandler(
                    delegate (object o, RunWorkerCompletedEventArgs e)
                    {
                        if (e.Error != null)
                        {
                            //MessageBox.Show(e.Error.Message);
                        }
                        else if (e.Cancelled)
                        {
                            //MessageBox.Show("Canceled");
                        }
                        else
                        {
                            if (taskCompletedCallBack != null)
                            {
                                taskCompletedCallBack("1", expression, e.Result.ToString());
                            }
                        }
                    }
                )
            );

        }

        private string EvaluateExpression(string expression)
        {
            Script script = new Script("Lib.zip");
            return script.RunScript(expression);
        }
    }
}
