using System;

namespace delegates_sample
{
    public class WorkPerformedEventArgs : EventArgs
    {
        public WorkPerformedEventArgs(int hours, WorkType work)
        {
            Hours = hours;
            WorkType = work;
        }

        public int Hours { get; set; }
        public WorkType WorkType { get; set; }
    }
}
