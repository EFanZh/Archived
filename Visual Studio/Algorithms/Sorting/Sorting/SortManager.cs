using System;
using System.ComponentModel;
using System.Threading;

namespace Sorting
{
    internal abstract class SortManager
    {
        private AsyncOperation sort_async_operation;
        private Guid task_id;
        private Action<int[]> sort_delegate;
        private SendOrPostCallback compare_callback;
        private SendOrPostCallback set_value_callback;
        private SendOrPostCallback set_value_indirect_callback;
        private SendOrPostCallback swap_callback;
        private SendOrPostCallback sort_completed_callback;

        public event EventHandler<SortEventArgs> Compare;
        public event EventHandler<SortEventArgs> SetValue;
        public event EventHandler<SortEventArgs> SetValueIndirect;
        public event EventHandler<SortEventArgs> Swap;
        public event AsyncCompletedEventHandler SortCompleted;

        public SortManager()
        {
            sort_delegate = new Action<int[]>(Sort);
            compare_callback = new SendOrPostCallback(CompareCallBack);
            set_value_callback = new SendOrPostCallback(SetValueCallBack);
            set_value_indirect_callback = new SendOrPostCallback(SetValueIndirectCallBack);
            swap_callback = new SendOrPostCallback(SwapCallBack);
            sort_completed_callback = new SendOrPostCallback(SortCompletedCallback);
        }

        public int RestTime
        {
            get;
            set;
        }

        protected bool IsTaskCanceled
        {
            get
            {
                return task_id == Guid.Empty;
            }
        }

        public void SortAsync(int[] data)
        {
            if (IsTaskCanceled)
            {
                task_id = Guid.NewGuid();
                sort_async_operation = AsyncOperationManager.CreateOperation(task_id);
                sort_delegate.BeginInvoke(data, null, null);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void SortAsyncCancel()
        {
            task_id = Guid.Empty;
        }

        protected void Sort(int[] data)
        {
            Exception error = null;
            try
            {
                DoSort(data);
            }
            catch (Exception ex)
            {
                error = ex;
            }
            bool canceled = IsTaskCanceled;
            if (!canceled)
            {
                task_id = Guid.Empty;
            }
            sort_async_operation.PostOperationCompleted(sort_completed_callback, new AsyncCompletedEventArgs(error, canceled, sort_async_operation.UserSuppliedState));
        }

        protected abstract void DoSort(int[] data);

        private void Rest()
        {
            Thread.Sleep(RestTime);
        }

        protected void PostCompareCallback(int a, int b)
        {
            sort_async_operation.Post(compare_callback, new SortEventArgs(a, b));
            Rest();
        }

        private void CompareCallBack(object arg)
        {
            if (Compare != null)
            {
                Compare(this, arg as SortEventArgs);
            }
        }

        protected void PostSetValueCallback(int a, int b)
        {
            sort_async_operation.Post(set_value_callback, new SortEventArgs(a, b));
            Rest();
        }

        private void SetValueCallBack(object arg)
        {
            if (SetValue != null)
            {
                SetValue(this, arg as SortEventArgs);
            }
        }

        protected void PostSetValueIndirectCallback(int a, int b)
        {
            sort_async_operation.Post(set_value_indirect_callback, new SortEventArgs(a, b));
            Rest();
        }

        private void SetValueIndirectCallBack(object arg)
        {
            if (SetValueIndirect != null)
            {
                SetValueIndirect(this, arg as SortEventArgs);
            }
        }

        protected void PostSwapCallback(int a, int b)
        {
            sort_async_operation.Post(swap_callback, new SortEventArgs(a, b));
            Rest();
        }

        private void SwapCallBack(object arg)
        {
            if (Swap != null)
            {
                Swap(this, arg as SortEventArgs);
            }
        }

        private void SortCompletedCallback(object arg)
        {
            if (SortCompleted != null)
            {
                SortCompleted(this, arg as AsyncCompletedEventArgs);
            }
        }
    }
}
