using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UKH.Thread
{
    public abstract class UThread
    {
        Task task;
        public UThread()
        {
            task = new Task(ThreadProc);
            task.RunSynchronously();
        }
        abstract protected void ThreadProc();
    }
}
