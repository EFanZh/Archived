using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using ImgProc.Shared;

namespace ImgProc
{
    internal class GetBitmapManager
    {
        Dictionary<object, AsyncOperation> userStateToLifetime = new Dictionary<object, AsyncOperation>();
        ISet<IInputPlugin> inputPlugins = Utilities.CreateInstanceSet<IInputPlugin>(PluginManager.InputPluginTypes);

        // Delegates
        Action<string, AsyncOperation> getBitmapWorkerDelegate;

        SendOrPostCallback getBitmapAsyncCompletedDelegate;

        public event EventHandler<GetBitmapCompletedEventArgs> GetBitmapCompleted;

        public GetBitmapManager()
        {
            getBitmapWorkerDelegate = new Action<string, AsyncOperation>(GetBitmapWorker);
            getBitmapAsyncCompletedDelegate = new SendOrPostCallback(GetBitmapAsyncCompleted);
        }

        private Bitmap GetBitmap(string path)
        {
            string extension = Path.GetExtension(path);

            // 找到支持扩展名的插件。
            var selectedInputPlugins = from inputPlugin in inputPlugins
                                       where (from supportedExtension in inputPlugin.SupportedExtensions
                                              where supportedExtension.Value.Contains(extension)
                                              select supportedExtension).Count() > 0
                                       select inputPlugin;

            foreach (var inputPlugin in selectedInputPlugins)
            {
                try
                {
                    Bitmap bitmap = inputPlugin.GetBitmap(path);
                    if (bitmap != null)
                    {
                        return bitmap;
                    }
                }
                catch (Exception)
                {
                }
            }

            // 尝试所有插件。
            foreach (var inputPlugin in inputPlugins.Except(selectedInputPlugins))
            {
                try
                {
                    Bitmap bitmap = inputPlugin.GetBitmap(path);
                    if (bitmap != null)
                    {
                        return bitmap;
                    }
                }
                catch (Exception)
                {
                }
            }
            throw new ArgumentException("不支持的图像格式。", "path");
        }

        private void GetBitmapWorker(string path, AsyncOperation asyncOp)
        {
            Exception error = null;
            bool cancelled = false;
            Bitmap bitmap = null;

            try
            {
                bitmap = GetBitmap(path);
            }
            catch (Exception ex)
            {
                error = ex;
            }

            cancelled = IsTaskCanceled(asyncOp.UserSuppliedState);
            GetBitmapAsyncCancel(asyncOp.UserSuppliedState);
            asyncOp.PostOperationCompleted(getBitmapAsyncCompletedDelegate, new GetBitmapCompletedEventArgs(error, cancelled, asyncOp.UserSuppliedState, bitmap));
        }

        public void GetBitmapAsync(string path, object userState)
        {
            if (IsTaskCanceled(userState))
            {
                AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(userState);
                lock ((userStateToLifetime as ICollection).SyncRoot)
                {
                    userStateToLifetime[userState] = asyncOp;
                }
                getBitmapWorkerDelegate.BeginInvoke(path, asyncOp, null, null);
            }
            else
            {
                throw new ArgumentException("用户状态已存在。", "userState");
            }
        }

        public void GetBitmapAsyncCancel(object userState)
        {
            lock ((userStateToLifetime as ICollection).SyncRoot)
            {
                userStateToLifetime.Remove(userState);
            }
        }

        private bool IsTaskCanceled(object userState)
        {
            return !userStateToLifetime.ContainsKey(userState);
        }

        private void GetBitmapAsyncCompleted(object arg)
        {
            OnGetBitmapCompleted(arg as GetBitmapCompletedEventArgs);
        }

        protected virtual void OnGetBitmapCompleted(GetBitmapCompletedEventArgs e)
        {
            if (GetBitmapCompleted != null)
            {
                GetBitmapCompleted(this, e);
            }
        }
    }
}
