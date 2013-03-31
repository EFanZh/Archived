using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using ImgProc.Shared;

namespace ImgProc
{
    internal class ImageProcessingManager
    {
        ImageProcessor imageProcessor = new ImageProcessor();

        Dictionary<object, AsyncOperation> userStateToLifetime = new Dictionary<object, AsyncOperation>();
        Dictionary<object, ImageProcessingManagerTaskContext> userStateToTaskContext = new Dictionary<object, ImageProcessingManagerTaskContext>();
        Dictionary<object, ProcessProgressForm> userStateToProcessProgressForm = new Dictionary<object, ProcessProgressForm>();
        Dictionary<object, object> imageProcessorUserStateToUserState = new Dictionary<object, object>();
        Dictionary<object, string> imageProcessorUserStateToInput = new Dictionary<object, string>();

        Action<Queue<KeyValuePair<string, string>>, IEnumerable<IProcessingPlugin>, IOutputPlugin, AsyncOperation> processWorkerDelegate;
        SendOrPostCallback processAsyncTaskProgressChangedDelegate;
        SendOrPostCallback processAsyncProgressChangedDelegate;
        SendOrPostCallback processAsyncStateChangedDelegate;
        SendOrPostCallback processAsyncCompletedDelegate;
        EventHandler<AsyncEventArgs> processingProgressForm_CancelProcessDelegate;

        private event EventHandler<ProcessTaskProgressChangedEventArgs> ProcessTaskProgressChanged;
        private event ProgressChangedEventHandler ProcessProgressChanged;
        private event EventHandler<ProcessStateChangedEventArgs> ProcessStateChanged;
        private event AsyncCompletedEventHandler ProcessCompleted;

        public ImageProcessingManager()
        {
            processWorkerDelegate = new Action<Queue<KeyValuePair<string, string>>, IEnumerable<IProcessingPlugin>, IOutputPlugin, AsyncOperation>(ProcessWorker);
            processAsyncTaskProgressChangedDelegate = new SendOrPostCallback(ProcessAsyncTaskProgressChanged);
            processAsyncProgressChangedDelegate = new SendOrPostCallback(ProcessAsyncProgressChanged);
            processAsyncStateChangedDelegate = new SendOrPostCallback(ProcessAsyncStateChanged);
            processAsyncCompletedDelegate = new SendOrPostCallback(ProcessAsyncCompleted);
            processingProgressForm_CancelProcessDelegate = new EventHandler<AsyncEventArgs>(processingProgressForm_CancelProcess);

            imageProcessor.ProcessProgressChanged += new ProgressChangedEventHandler(imageProcessor_ProcessProgressChanged);
            imageProcessor.ProcessCompleted += new AsyncCompletedEventHandler(imageProcessor_ProcessCompleted);

            ProcessTaskProgressChanged += new EventHandler<ProcessTaskProgressChangedEventArgs>(ImageProcessingManager_ProcessTaskProgressChanged);
            ProcessProgressChanged += new ProgressChangedEventHandler(ImageProcessingManager_ProcessProgressChanged);
            ProcessStateChanged += new EventHandler<ProcessStateChangedEventArgs>(ImageProcessingManager_ProcessStateChanged);
            ProcessCompleted += new AsyncCompletedEventHandler(ImageProcessingManager_ProcessCompleted);
        }

        private void processingProgressForm_CancelProcess(object sender, AsyncEventArgs e)
        {
            ProcessAsyncCancel(e.UserState);
        }

        private void imageProcessor_ProcessCompleted(object sender, AsyncCompletedEventArgs e)
        {
            object userState = imageProcessorUserStateToUserState[e.UserState];
            ImageProcessingManagerTaskContext imageProcessingManagerTaskContext = userStateToTaskContext[userState];
            if (!IsTaskCancelled(userState))
            {
                AsyncOperation asyncOp = userStateToLifetime[userState];
                if (e.Error == null)
                {
                    if (e.Cancelled)
                    {
                        asyncOp.Post(processAsyncStateChangedDelegate, new ProcessStateChangedEventArgs(userState, imageProcessorUserStateToInput[e.UserState], "取消"));
                    }
                    else
                    {
                        asyncOp.Post(processAsyncStateChangedDelegate, new ProcessStateChangedEventArgs(userState, imageProcessorUserStateToInput[e.UserState], "完成"));
                    }
                }
                else
                {
                    asyncOp.Post(processAsyncStateChangedDelegate, new ProcessStateChangedEventArgs(userState, imageProcessorUserStateToInput[e.UserState], "错误"));
                }
                lock ((imageProcessingManagerTaskContext.UserStateToTaskProgressPercentage) as ICollection)
                {
                    imageProcessingManagerTaskContext.UserStateToTaskProgressPercentage[e.UserState] = 100;
                }
                asyncOp.Post(processAsyncTaskProgressChangedDelegate, new ProcessTaskProgressChangedEventArgs(100, userState, e.UserState));
                asyncOp.Post(processAsyncProgressChangedDelegate, new ProgressChangedEventArgs(imageProcessingManagerTaskContext.ProcessProgressPercentage, userState));
            }
            lock (imageProcessingManagerTaskContext.CurrentProcessingPluginUserStates)
            {
                imageProcessingManagerTaskContext.CurrentProcessingPluginUserStates.Remove(e.UserState);
            }
            imageProcessingManagerTaskContext.Semaphore.Release();
        }

        private void imageProcessor_ProcessProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            object userState = imageProcessorUserStateToUserState[e.UserState];
            ImageProcessingManagerTaskContext imageProcessingManagerTaskContext = userStateToTaskContext[userState];
            AsyncOperation asyncOp = userStateToLifetime[userState];
            lock ((imageProcessingManagerTaskContext.UserStateToTaskProgressPercentage) as ICollection)
            {
                imageProcessingManagerTaskContext.UserStateToTaskProgressPercentage[e.UserState] = e.ProgressPercentage;
            }
            asyncOp.Post(processAsyncTaskProgressChangedDelegate, new ProcessTaskProgressChangedEventArgs(e.ProgressPercentage, userState, e.UserState));
            asyncOp.Post(processAsyncProgressChangedDelegate, new ProgressChangedEventArgs(imageProcessingManagerTaskContext.ProcessProgressPercentage, userState));
        }

        private bool IsTaskCancelled(object userState)
        {
            return !userStateToLifetime.ContainsKey(userState);
        }

        private void ProcessWorker(Queue<KeyValuePair<string, string>> taskQueue, IEnumerable<IProcessingPlugin> processingPlugins, IOutputPlugin outputPlugin, AsyncOperation asyncOp)
        {
            var imageProcessingManagerTaskContext = new ImageProcessingManagerTaskContext(taskQueue, asyncOp.UserSuppliedState);
            imageProcessingManagerTaskContext.TotalTaskCount = taskQueue.Count;

            lock ((userStateToTaskContext as ICollection).SyncRoot)
            {
                userStateToTaskContext[asyncOp.UserSuppliedState] = imageProcessingManagerTaskContext;
            }

            while (imageProcessingManagerTaskContext.TaskQueue.Count > 0)
            {
                imageProcessingManagerTaskContext.Semaphore.WaitOne();

                if (IsTaskCancelled(asyncOp.UserSuppliedState))
                {
                    break;
                }
                else
                {
                    var task = taskQueue.Dequeue();
                    asyncOp.Post(processAsyncStateChangedDelegate, new ProcessStateChangedEventArgs(asyncOp.UserSuppliedState, task.Key, "处理中"));
                    object userState = Guid.NewGuid();
                    lock ((imageProcessorUserStateToUserState as ICollection).SyncRoot)
                    {
                        imageProcessorUserStateToUserState[userState] = asyncOp.UserSuppliedState;
                    }
                    lock ((imageProcessorUserStateToInput as ICollection).SyncRoot)
                    {
                        imageProcessorUserStateToInput[userState] = task.Key;
                    }
                    lock (imageProcessingManagerTaskContext.CurrentProcessingPluginUserStates)
                    {
                        imageProcessingManagerTaskContext.CurrentProcessingPluginUserStates.Add(userState);
                    }
                    imageProcessor.ProcessAsync(task, processingPlugins, outputPlugin, userState);
                }
            }
            while (imageProcessingManagerTaskContext.CurrentProcessingPluginUserStates.Count > 0)
            {
                imageProcessingManagerTaskContext.Semaphore.WaitOne();
            }
            bool cancelled = IsTaskCancelled(asyncOp.UserSuppliedState);
            asyncOp.PostOperationCompleted(processAsyncCompletedDelegate, new AsyncCompletedEventArgs(null, cancelled, asyncOp.UserSuppliedState));
        }

        public void ProcessAsync(IDictionary<string, string> tasks, IEnumerable<IProcessingPlugin> processingPlugins, IOutputPlugin outputPlugin, object userState)
        {
            if (IsTaskCancelled(userState))
            {
                ProcessProgressForm processProgressForm = null;
                try
                {
                    AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(userState);
                    processProgressForm = new ProcessProgressForm(userState);
                    processProgressForm.CancelProcess += processingProgressForm_CancelProcessDelegate;
                    processProgressForm.AddListViewItems(tasks.Keys);

                    lock ((userStateToLifetime as ICollection).SyncRoot)
                    {
                        userStateToLifetime[userState] = asyncOp;
                    }

                    lock ((userStateToProcessProgressForm as ICollection).SyncRoot)
                    {
                        userStateToProcessProgressForm[userState] = processProgressForm;
                    }

                    processWorkerDelegate.BeginInvoke(new Queue<KeyValuePair<string, string>>(tasks), processingPlugins, outputPlugin, asyncOp, null, null);
                    processProgressForm.ShowDialog();
                }
                finally
                {
                    if (processProgressForm != null)
                    {
                        processProgressForm.Dispose();
                    }
                }
            }
            else
            {
                throw new ArgumentException("用户状态已存在。", "userState");
            }
        }

        public void ProcessAsyncCancel(object userState)
        {
            userStateToLifetime.Remove(userState);
            foreach (var userState2 in userStateToTaskContext[userState].CurrentProcessingPluginUserStates)
            {
                imageProcessor.ProcessAsyncCancel(userState2);
            }
        }

        private void ProcessAsyncTaskProgressChanged(object arg)
        {
            OnProcessTaskProgressChanged(arg as ProcessTaskProgressChangedEventArgs);
        }

        private void ProcessAsyncProgressChanged(object arg)
        {
            OnProcessProgressChanged(arg as ProgressChangedEventArgs);
        }

        private void ProcessAsyncStateChanged(object arg)
        {
            OnProcessStateChanged(arg as ProcessStateChangedEventArgs);
        }

        private void ProcessAsyncCompleted(object arg)
        {
            OnProcessCompleted(arg as AsyncCompletedEventArgs);
        }

        protected virtual void OnProcessTaskProgressChanged(ProcessTaskProgressChangedEventArgs e)
        {
            if (ProcessTaskProgressChanged != null)
            {
                ProcessTaskProgressChanged(this, e);
            }
        }

        protected virtual void OnProcessProgressChanged(ProgressChangedEventArgs e)
        {
            if (ProcessProgressChanged != null)
            {
                ProcessProgressChanged(this, e);
            }
        }

        protected virtual void OnProcessStateChanged(ProcessStateChangedEventArgs e)
        {
            if (ProcessStateChanged != null)
            {
                ProcessStateChanged(this, e);
            }
        }

        protected virtual void OnProcessCompleted(AsyncCompletedEventArgs e)
        {
            if (ProcessCompleted != null)
            {
                ProcessCompleted(this, e);
            }
        }

        private void ImageProcessingManager_ProcessTaskProgressChanged(object sender, ProcessTaskProgressChangedEventArgs e)
        {
            ProcessProgressForm processProgressForm = userStateToProcessProgressForm[e.UserState];
            processProgressForm.ChangeTaskProgress(imageProcessorUserStateToInput[e.TaskUserState], userStateToTaskContext[e.UserState].UserStateToTaskProgressPercentage[e.TaskUserState]);
        }

        private void ImageProcessingManager_ProcessProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProcessProgressForm processProgressForm = userStateToProcessProgressForm[e.UserState];
            processProgressForm.ChangeProgress(e.ProgressPercentage / 100.0);
        }

        private void ImageProcessingManager_ProcessStateChanged(object sender, ProcessStateChangedEventArgs e)
        {
            ProcessProgressForm processProgressForm = userStateToProcessProgressForm[e.UserState];
            processProgressForm.ChangeTaskState(e.Item, e.State);
        }

        private void ImageProcessingManager_ProcessCompleted(object sender, AsyncCompletedEventArgs e)
        {
            ProcessProgressForm processProgressForm = userStateToProcessProgressForm[e.UserState];
            ImageProcessingManagerTaskContext imageProcessingManagerTaskContext = userStateToTaskContext[e.UserState];
            if (e.Error == null)
            {
                if (e.Cancelled)
                {
                    foreach (var task in imageProcessingManagerTaskContext.TaskQueue)
                    {
                        processProgressForm.ChangeTaskState(task.Key, "取消");
                    }
                }
            }
            else
            {
                foreach (var task in imageProcessingManagerTaskContext.TaskQueue)
                {
                    processProgressForm.ChangeTaskState(task.Key, "错误");
                }
            }
            processProgressForm.ChangeProgress(1.0);
            processProgressForm.ProcessProgressCompleted();
        }
    }
}
