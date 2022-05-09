using System;
using System.ComponentModel;

namespace Calculator
{
    public class Job
    {
        public static void DoJob(DoWorkEventHandler doJob = null, RunWorkerCompletedEventHandler afterJob = null)
        {
            try
            {
                BackgroundWorker bw = new BackgroundWorker {
                    WorkerSupportsCancellation = true
                };
                bw.WorkerReportsProgress = true;
                bw.DoWork += doJob;
                bw.RunWorkerCompleted += afterJob;
                bw.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                //ex.Message;
            }
        }
    }
}
