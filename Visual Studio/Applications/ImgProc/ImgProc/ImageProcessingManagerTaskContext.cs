using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace ImgProc
{
    internal class ImageProcessingManagerTaskContext
    {
        public ImageProcessingManagerTaskContext(Queue<KeyValuePair<string, string>> taskQueue, object userState)
        {
            TaskQueue = taskQueue;
            Semaphore = new Semaphore(Utilities.ProcessorCount, Utilities.ProcessorCount);
            UserStateToTaskProgressPercentage = new Dictionary<object, int>();
            CurrentProcessingPluginUserStates = new HashSet<object>();
        }

        public Queue<KeyValuePair<string, string>> TaskQueue
        {
            get;
            private set;
        }

        public Semaphore Semaphore
        {
            get;
            private set;
        }

        public Dictionary<object, int> UserStateToTaskProgressPercentage
        {
            get;
            private set;
        }

        public HashSet<object> CurrentProcessingPluginUserStates
        {
            get;
            private set;
        }

        public int TotalTaskCount
        {
            get;
            set;
        }

        public int ProcessProgressPercentage
        {
            get
            {
                double m = 0.0;
                lock ((UserStateToTaskProgressPercentage) as ICollection)
                {
                    foreach (var kvp in UserStateToTaskProgressPercentage)
                    {
                        m += kvp.Value;
                    }
                }
                return Utilities.IntRound(m / TotalTaskCount);
            }
        }
    }
}
