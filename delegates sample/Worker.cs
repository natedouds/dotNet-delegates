using System;
using System.Threading;

namespace delegates_sample
{
    public class Worker
    {
        public event EventHandler<WorkPerformedEventArgs> WorkPerformed;
        public event EventHandler WorkHalfComplete;
        public event EventHandler WorkCompleted;

        public void DoWork(int hours, WorkType workType)
        {
            var halfWayPointRoundedUp = (hours - 1) / 2 + 1;
            for (int i = 1; i <= hours; i++)
            {
                OnWorkPerformed(i, workType);
                //Only doing this to show app working at runtime
                Thread.Sleep(200);

                if (i == halfWayPointRoundedUp)
                {
                    OnWorkHalfComplete();
                }
            }
            OnWorkCompleted();
        }

        private void OnWorkHalfComplete()
        {
            var del = WorkHalfComplete;
            del?.Invoke(this, EventArgs.Empty);
        }

        private void OnWorkCompleted()
        {
            var del = WorkCompleted;
            del?.Invoke(this, EventArgs.Empty);
            /*
             * Note: the above code is simply a shortcut to say:
             * if (del != null) {
             *      del(this, EventArgs.Empty);
             * }
             * Always check for null as this will prevent runtime errors due to lack of event listener bindings
             */
        }

        private void OnWorkPerformed(int i, WorkType workType)
        {
            var del = WorkPerformed;
            del?.Invoke(this, new WorkPerformedEventArgs(i, workType));
        }
    }
}
