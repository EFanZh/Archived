using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using ImgProc.Shared;

namespace ImgProc
{
    internal class ImageProcessorTaskContext
    {
        public ImageProcessorTaskContext()
        {
            AutoResetEvent = new AutoResetEvent(false);
            ProcessingPluginQueue = new Queue<IProcessingPlugin>();
            Error = null;
        }

        public Bitmap Bitmap
        {
            get;
            set;
        }

        public Queue<IProcessingPlugin> ProcessingPluginQueue
        {
            get;
            private set;
        }

        public int TotalProcedureCount
        {
            get;
            set;
        }

        public IProcessingPlugin CurrentProcessingPlugin
        {
            get;
            set;
        }

        public object CurrentProcessingPluginUserState
        {
            get;
            set;
        }

        public AutoResetEvent AutoResetEvent
        {
            get;
            private set;
        }

        public Exception Error
        {
            get;
            set;
        }
    }
}
