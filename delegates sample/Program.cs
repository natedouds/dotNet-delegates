using System;

//Remember, delegates are simply a way to pipe data from a -> b, where b is the handler method
namespace delegates_sample
{
    //delegate
    public delegate int BizRulesDelegate(int x, int y);

    class Program
    {
        static void Main(string[] args)
        {
            //Relatively standard delegate implementation
            RunStandardDelegates();

            //Custom lambda delegate implementation
            RunCustomLambdaDelegates();

            //Background worker implementation
            var backgroundWorkers = new BackgroundWorkers();
            backgroundWorkers.RunWorkers();

            Console.ReadKey();
        }

        private static void RunStandardDelegates()
        {
            //Create my worker class, that has events registered
            var worker = new Worker();

            //Add my event handlers (based on various events defined in worker)
            WireUpEventHandlers(worker);

            Console.WriteLine("Runing Standard Work");

            //Execute main function which contains method invoking the delegates
            worker.DoWork(6, WorkType.Read);

            Console.WriteLine("Finished Running StandardWork");
            Console.WriteLine("--------------------------------");
            Console.WriteLine();
        }

        private static void RunCustomLambdaDelegates()
        {
            //Create the new class
            var data = new ProcessData();

            RunDelegateByActionAsync(data);

            RunDelegateByDelegate(data);

            RunDelegateByAction(data);

            RunDelegateByFunc(data);
        }

        private static void RunDelegateByAction(ProcessData data)
        {
            //Pass the action delegate now
            Action<int, int> myMultiplyAction = (x, y) => Console.WriteLine("Multiply Action Result: " + (x * y));
            Console.WriteLine("Beginning RunDelegateByAction");
            data.ProcessAction(2, 3, myMultiplyAction);
            Console.WriteLine("Finished RunDelegateByAction");
        }

        private static void RunDelegateByActionAsync(ProcessData data)
        {
            //Pass the action delegate now
            Action<int, int> myAddAction = (x, y) =>
            {
                System.Threading.Thread.Sleep(3000);
                Console.WriteLine("Add Action Result: " + (x + y));
            };
            Console.WriteLine("Beginning RunDelegateByActionAsync");
            data.ProcessActionAsync(2, 3, myAddAction);
            Console.WriteLine("Finished RunDelegateByActionAsync");
        }

        private static void RunDelegateByFunc(ProcessData data)
        {
            //Create 2 funcs
            Func<int, int, int> funcAddDel = (x, y) => x + y;
            Func<int, int, int> funcMultiplyDel = (x, y) => x * y;

            //Pass the specific delegate that you want to the method, allowing for execution of that specific delegate
            Console.WriteLine("Beginning RunDelegateByFunc");
            data.ProcessFunc(2, 3, funcAddDel);
            data.ProcessFunc(2, 3, funcMultiplyDel);
            Console.WriteLine("Finished RunDelegateByFunc");
        }

        private static void RunDelegateByDelegate(ProcessData data)
        {
            //Create 2 custom instances of BizRulesDelegate
            BizRulesDelegate addDel = (x, y) => x + y;
            BizRulesDelegate multiplyDel = (x, y) => x * y;

            //Pass the specific delegate that you want to the method, allowing for execution of that specific delegate
            Console.WriteLine("Beginning RunDelegateByDelegate");
            data.Process(2, 3, addDel);
            data.Process(2, 3, multiplyDel);
            Console.WriteLine("Finished RunDelegateByDelegate");
        }

        private static void WireUpEventHandlers(Worker worker)
        {
            //Here the event (WorkPerformed) gets an event handler (Worker_WorkPerformed) via delegate inference
            worker.WorkPerformed += Worker_WorkPerformed;

            //This is the long version of the above code
            worker.WorkCompleted += new EventHandler(Worker_workCompleted);

            //Use anonymous lambda method
            //s,e are inline method parameters -> compiler infers types here
            //=> labmda operator
            //Console.WriteLine represents the method body (could be enclosed with { ... }, allowing for multiple lines
            worker.WorkHalfComplete += (s, e) => Console.WriteLine("You're over the half way point!");
        }

        private static void Worker_workCompleted(object sender, EventArgs e)
        {
            Console.WriteLine("Work Completed!");
        }

        private static void Worker_WorkPerformed(object sender, WorkPerformedEventArgs e)
        {
            Console.WriteLine("Hours worked: " + e.Hours + " " + e.WorkType);
        }
    }
}
