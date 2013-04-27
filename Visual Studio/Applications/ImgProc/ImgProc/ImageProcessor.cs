using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using ImgProc.Shared;

namespace ImgProc
{
    internal class ImageProcessor
    {
        GetBitmapManager getBitmapManager = new GetBitmapManager();

        /// <summary>
        /// ImageProcessor 异步操作的 UserState 和 AsyncOperation 的对应关系。
        /// </summary>
        Dictionary<object, AsyncOperation> userStateToLifetime = new Dictionary<object, AsyncOperation>();

        /// <summary>
        /// ImageProcessor 异步操作的 UserState 和处理任务上下文的对应关系
        /// </summary>
        Dictionary<object, ImageProcessorTaskContext> userStateToTaskContext = new Dictionary<object, ImageProcessorTaskContext>();

        /// <summary>
        /// 子任务异步操作的 UserState 和 ImageProcessor 异步操作的 UserState 的对应关系。
        /// </summary>
        Dictionary<object, object> taskUserStateToUserState = new Dictionary<object, object>();

        // Delegates
        Action<KeyValuePair<string, string>, IEnumerable<IProcessingPlugin>, IOutputPlugin, AsyncOperation> processWorkerDelegate;

        SendOrPostCallback processAsyncProgressChangedDelegate;
        SendOrPostCallback processAsyncCompletedDelegate;
        ProgressChangedEventHandler processingPlugin_ProcessProgressChangedDelegate;
        EventHandler<GetBitmapCompletedEventArgs> getBitmapCompletedDelegate;

        // Events
        public event ProgressChangedEventHandler ProcessProgressChanged;
        public event AsyncCompletedEventHandler ProcessCompleted;

        public ImageProcessor()
        {
            processWorkerDelegate = new Action<KeyValuePair<string, string>, IEnumerable<IProcessingPlugin>, IOutputPlugin, AsyncOperation>(ProcessWorker);
            processAsyncProgressChangedDelegate = new SendOrPostCallback(ProcessAsyncProgressChanged);
            processAsyncCompletedDelegate = new SendOrPostCallback(ProcessAsyncCompleted);
            processingPlugin_ProcessProgressChangedDelegate = new ProgressChangedEventHandler(processingPlugin_ProcessProgressChanged);
            getBitmapCompletedDelegate = new EventHandler<GetBitmapCompletedEventArgs>(getBitmapCompleted);

            getBitmapManager.GetBitmapCompleted += getBitmapCompletedDelegate;
        }

        private bool IsTaskCancelled(object userState)
        {
            return !userStateToLifetime.ContainsKey(userState);
        }

        /// <summary>
        /// 异步工作线程。
        /// </summary>
        /// <param name="task">输入路径到输出路径</param>
        /// <param name="processingPlugins">处理插件</param>
        /// <param name="outputPlugin">输出插件</param>
        /// <param name="asyncOp">跟踪异步操作的生存期。</param>
        private void ProcessWorker(KeyValuePair<string, string> task, IEnumerable<IProcessingPlugin> processingPlugins, IOutputPlugin outputPlugin, AsyncOperation asyncOp)
        {
            // 创建处理任务上下文对象。
            ImageProcessorTaskContext imageProcessorTaskContext = new ImageProcessorTaskContext();
            imageProcessorTaskContext.TotalProcedureCount = processingPlugins.Count() + 2;

            // 为 ProcessingPlugin 添加事件处理方法。
            foreach (var processingPlugin in processingPlugins)
            {
                // ! 重要，保证多任务处理事件唯一，也许以后可能有更好的方法。
                lock (processingPlugin)
                {
                    processingPlugin.ProcessProgressChanged -= processingPlugin_ProcessProgressChangedDelegate;
                    processingPlugin.ProcessCompleted -= getBitmapCompletedDelegate;
                    processingPlugin.ProcessProgressChanged += processingPlugin_ProcessProgressChangedDelegate;
                    processingPlugin.ProcessCompleted += getBitmapCompletedDelegate;
                }
                imageProcessorTaskContext.ProcessingPluginQueue.Enqueue(processingPlugin);
            }

            // 添加到对应关系中。
            lock ((userStateToTaskContext as ICollection).SyncRoot)
            {
                userStateToTaskContext[asyncOp.UserSuppliedState] = imageProcessorTaskContext;
            }

            // 创建 UserState 对象。
            object userState = Guid.NewGuid();

            // 添加到对应关系中。
            lock ((taskUserStateToUserState as ICollection).SyncRoot)
            {
                taskUserStateToUserState[userState] = asyncOp.UserSuppliedState;
            }

            // 开始获取 Bitmap。
            getBitmapManager.GetBitmapAsync(task.Key, userState);

            while (imageProcessorTaskContext.ProcessingPluginQueue.Count > 0)
            {
                // 等待上步操作完成。
                imageProcessorTaskContext.AutoResetEvent.WaitOne();

                if (IsTaskCancelled(asyncOp.UserSuppliedState) || imageProcessorTaskContext.Error != null)
                {
                    break;
                }
                else
                {
                    IProcessingPlugin processingPlugin = imageProcessorTaskContext.ProcessingPluginQueue.Dequeue();
                    userState = Guid.NewGuid();
                    imageProcessorTaskContext.CurrentProcessingPlugin = processingPlugin;
                    imageProcessorTaskContext.CurrentProcessingPluginUserState = userState;

                    lock ((taskUserStateToUserState as ICollection).SyncRoot)
                    {
                        taskUserStateToUserState[userState] = asyncOp.UserSuppliedState;
                    }
                    processingPlugin.ProcessAsync(imageProcessorTaskContext.Bitmap, userState);
                }
            }

            if (!IsTaskCancelled(asyncOp.UserSuppliedState) && imageProcessorTaskContext.Error == null)
            {
                // 等待最后处理完成。
                imageProcessorTaskContext.AutoResetEvent.WaitOne();

                // 输出。
                outputPlugin.Output(imageProcessorTaskContext.Bitmap, task.Value);
            }

            bool cancelled = IsTaskCancelled(asyncOp.UserSuppliedState);
            lock ((userStateToLifetime as ICollection).SyncRoot)
            {
                userStateToLifetime.Remove(asyncOp.UserSuppliedState);
            }
            asyncOp.PostOperationCompleted(processAsyncCompletedDelegate, new AsyncCompletedEventArgs(imageProcessorTaskContext.Error, cancelled, asyncOp.UserSuppliedState));
        }

        // GetBitmapManager 完成以及处理插件处理完成。
        private void getBitmapCompleted(object sender, GetBitmapCompletedEventArgs e)
        {
            object userState = taskUserStateToUserState[e.UserState];
            ImageProcessorTaskContext imageProcessorTaskContext = userStateToTaskContext[userState];

            if (!e.Cancelled && e.Error == null)
            {
                imageProcessorTaskContext.Bitmap = e.Bitmap;
                userStateToLifetime[userState].Post(processAsyncProgressChangedDelegate, new ProgressChangedEventArgs(Utilities.IntRound(100.0 * (imageProcessorTaskContext.TotalProcedureCount - imageProcessorTaskContext.ProcessingPluginQueue.Count - 1) / imageProcessorTaskContext.TotalProcedureCount), userState));
            }
            else
            {
                if (e.Error != null)
                {
                    imageProcessorTaskContext.Error = e.Error;
                }
            }

            imageProcessorTaskContext.AutoResetEvent.Set();
        }

        // 处理插件进度报告。
        private void processingPlugin_ProcessProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            object userState = taskUserStateToUserState[e.UserState];
            ImageProcessorTaskContext imageProcessorTaskContext = userStateToTaskContext[userState];
            if (!IsTaskCancelled(userState))
            {
                userStateToLifetime[userState].Post(processAsyncProgressChangedDelegate, new ProgressChangedEventArgs(Utilities.IntRound(100.0 * (imageProcessorTaskContext.TotalProcedureCount - imageProcessorTaskContext.ProcessingPluginQueue.Count + e.ProgressPercentage / 100.0 - 2) / imageProcessorTaskContext.TotalProcedureCount), userState));
            }
        }

        /// <summary>
        /// 异步处理图像。
        /// </summary>
        /// <param name="bitmap">要处理的图像</param>
        /// <param name="userState">用户状态</param>
        public void ProcessAsync(KeyValuePair<string, string> task, IEnumerable<IProcessingPlugin> processingPlugins, IOutputPlugin outputPlugin, object userState)
        {
            if (IsTaskCancelled(userState))
            {
                AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(userState);
                lock ((userStateToLifetime as ICollection).SyncRoot)
                {
                    userStateToLifetime[userState] = asyncOp;
                }
                processWorkerDelegate.BeginInvoke(task, processingPlugins, outputPlugin, asyncOp, null, null);
            }
            else
            {
                throw new ArgumentException("用户状态已存在。", "userState");
            }
        }

        /// <summary>
        /// 取消异步处理。
        /// </summary>
        /// <param name="userState">用户状态</param>
        public void ProcessAsyncCancel(object userState)
        {
            // 保证取消状态。
            lock ((userStateToLifetime as ICollection).SyncRoot)
            {
                userStateToLifetime.Remove(userState);
            }

            ImageProcessorTaskContext imageProcessorTaskContext = userStateToTaskContext[userState];
            imageProcessorTaskContext.CurrentProcessingPlugin.ProcessAsyncCancel(imageProcessorTaskContext.CurrentProcessingPluginUserState);
        }

        /// <summary>
        /// 异步引发 ProcessProgressChanged 事件。
        /// </summary>
        /// <param name="arg">为 ProcessProgressChanged 事件提供数据。</param>
        private void ProcessAsyncProgressChanged(object arg)
        {
            OnProcessProgressChanged(arg as ProgressChangedEventArgs);
        }

        /// <summary>
        /// 异步引发 ProcessCompleted 事件。
        /// </summary>
        /// <param name="arg">为 ProcessCompleted 事件提供数据。</param>
        private void ProcessAsyncCompleted(object arg)
        {
            OnProcessCompleted(arg as AsyncCompletedEventArgs);
        }

        /// <summary>
        /// 引发 ProcessProgressChanged 事件。
        /// </summary>
        /// <param name="e">为 ProcessProgressChanged 事件提供数据。</param>
        protected virtual void OnProcessProgressChanged(ProgressChangedEventArgs e)
        {
            if (ProcessProgressChanged != null)
            {
                ProcessProgressChanged(this, e);
            }
        }

        /// <summary>
        /// 引发 ProcessCompleted 事件。
        /// </summary>
        /// <param name="e">为 ProcessCompleted 事件提供数据。</param>
        protected virtual void OnProcessCompleted(AsyncCompletedEventArgs e)
        {
            if (ProcessCompleted != null)
            {
                ProcessCompleted(this, e);
            }
        }
    }
}
