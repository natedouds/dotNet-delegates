using System;
using System.ComponentModel;
using System.Threading;

namespace delegates_sample
{
    public class BackgroundWorkers
    {
        internal void RunWorkers()
        {
            //Create background worker
            var backgroundWorker1 = new BackgroundWorker();
            //Add handler to DoWork Event
            backgroundWorker1.DoWork += BackgroundWorkerOnDoWork;
            //Add handler to OnCompleted Event
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            var backgroundWorker2 = new BackgroundWorker();
            backgroundWorker2.DoWork += BackgroundWorkerOnDoWork;
            backgroundWorker2.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            //Do Work async
            backgroundWorker1.RunWorkerAsync();

            backgroundWorker2.RunWorkerAsync();
        }

        internal void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine(sender.GetType().Name + " Completed");
        }

        internal void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs args)
        {
            var randomSleep = new Random().Next(5, 15);
            var randomMaxLoop = new Random().Next(80, 100);
            Console.WriteLine($"Starting {sender.GetType().Name}; \n\tsleep: {randomSleep}; \n\t\tloop: {randomMaxLoop} \n\t\t\tThread Id: {Thread.CurrentThread.ManagedThreadId}");
            for (var i = 1; i <= randomMaxLoop; i++)
            {
                Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId};\tWorking on item: " + i);
                Thread.Sleep(randomSleep);
            }
        }
    }
}
