using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ImgProc.Shared
{
    public abstract class ProcessingPlugin : IProcessingPlugin
    {
        Dictionary<object, AsyncOperation> userStateToLifetime = new Dictionary<object, AsyncOperation>();

        // Delegates
        Action<Bitmap, AsyncOperation> processWorkerDelegate;

        SendOrPostCallback processAsyncProgressChangedDelegate;
        SendOrPostCallback processAsyncCompletedDelegate;

        public ProcessingPlugin()
        {
            processWorkerDelegate = new Action<Bitmap, AsyncOperation>(ProcessWorker);
            processAsyncProgressChangedDelegate = new SendOrPostCallback(ProcessAsyncProgressChanged);
            processAsyncCompletedDelegate = new SendOrPostCallback(ProcessAsyncCompleted);
        }

        #region IProcessingPlugin 成员

        public void ProcessAsync(Bitmap bitmap, object userState)
        {
            if (IsTaskCancelled(userState))
            {
                AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(userState);
                lock ((userStateToLifetime as ICollection).SyncRoot)
                {
                    userStateToLifetime[userState] = asyncOp;
                }
                processWorkerDelegate.BeginInvoke(bitmap, asyncOp, null, null);
            }
            else
            {
                throw new ArgumentException("用户状态已存在。", "userState");
            }
        }

        public void ProcessAsyncCancel(object userState)
        {
            lock ((userStateToLifetime as ICollection).SyncRoot)
            {
                userStateToLifetime.Remove(userState);
            }
        }

        public abstract Form ConfigForm
        {
            get;
            protected set;
        }

        public abstract IEnumerable<string> SetupPath
        {
            get;
            protected set;
        }

        public event ProgressChangedEventHandler ProcessProgressChanged;
        public event EventHandler<GetBitmapCompletedEventArgs> ProcessCompleted;

        #endregion IProcessingPlugin 成员

        #region IPlugin 成员

        public abstract string Name
        {
            get;
            protected set;
        }

        public abstract string AboutInfo
        {
            get;
            protected set;
        }

        #endregion IPlugin 成员

        #region IDisposable 成员

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable 成员

        protected bool IsTaskCancelled(object userState)
        {
            return !userStateToLifetime.ContainsKey(userState);
        }

        private void ProcessWorker(Bitmap bitmap, AsyncOperation asyncOp)
        {
            Bitmap newBitmap = null;
            Exception error = null;
            try
            {
                newBitmap = new Bitmap(Process(bitmap, asyncOp));
            }
            catch (Exception ex)
            {
                error = ex;
            }
            bool cancelled = IsTaskCancelled(asyncOp.UserSuppliedState);
            lock ((userStateToLifetime as ICollection).SyncRoot)
            {
                userStateToLifetime.Remove(asyncOp.UserSuppliedState);
            }
            DoProcessAsyncCompleted(asyncOp, new GetBitmapCompletedEventArgs(error, cancelled, asyncOp.UserSuppliedState, newBitmap));
        }

        protected abstract Bitmap Process(Bitmap bitmap, AsyncOperation asyncOp);

        protected void DoProcessAsyncProgressChanged(AsyncOperation asyncOp, ProgressChangedEventArgs e)
        {
            asyncOp.Post(processAsyncProgressChangedDelegate, e);
        }

        private void DoProcessAsyncCompleted(AsyncOperation asyncOp, GetBitmapCompletedEventArgs e)
        {
            asyncOp.PostOperationCompleted(processAsyncCompletedDelegate, e);
        }

        private void ProcessAsyncProgressChanged(object arg)
        {
            OnProcessProgressChanged(arg as ProgressChangedEventArgs);
        }

        private void ProcessAsyncCompleted(object arg)
        {
            OnProcessCompleted(arg as GetBitmapCompletedEventArgs);
        }

        protected virtual void OnProcessProgressChanged(ProgressChangedEventArgs e)
        {
            if (ProcessProgressChanged != null)
            {
                ProcessProgressChanged(this, e);
            }
        }

        protected virtual void OnProcessCompleted(GetBitmapCompletedEventArgs e)
        {
            if (ProcessCompleted != null)
            {
                ProcessCompleted(this, e);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (ConfigForm != null)
                {
                    ConfigForm.Dispose();
                }
            }
        }
    }
}
