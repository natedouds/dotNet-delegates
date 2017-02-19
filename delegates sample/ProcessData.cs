using System;

namespace delegates_sample
{
    public class ProcessData
    {
        public void Process(int x, int y, BizRulesDelegate del)
        {
            var result = del?.Invoke(x, y);
            Console.WriteLine("ProcessData result: " + result);
        }

        public void ProcessAction(int x, int y, Action<int, int> action)
        {
            action?.Invoke(x, y);
            Console.WriteLine("ProcessAction has been processed");
        }

        public void ProcessActionAsync(int x, int y, Action<int, int> action)
        {
            action?.BeginInvoke(x, y, null, null);
            Console.WriteLine("ProcessActionAsync has been processed");
        }

        public void ProcessFunc(int x, int y, Func<int, int, int> func)
        {
            func?.BeginInvoke(x, y, null, null);
            Console.WriteLine("ProcessFunc has been processed");
        }
    }
}
