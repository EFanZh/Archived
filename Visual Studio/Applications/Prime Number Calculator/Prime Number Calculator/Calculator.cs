using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace PrimeNumberCalculator
{
    internal class Calculator
    {
        private Guid task_id;
        private AsyncOperation async_operation;
        private SpinLock spin_lock_task_id = new SpinLock();
        private SpinLock spin_lock_list = new SpinLock();

        public event EventHandler OutputResult;
        public event AsyncCompletedEventHandler CalculateCompleted;

        public Calculator()
            : this(new List<long>() { 2, 3 })
        {
        }

        public Calculator(List<long> prime_number_list)
        {
            if (prime_number_list.Count < 2)
            {
                PrimeNumberList = new List<long>() { 2, 3 };
            }
            else
            {
                PrimeNumberList = prime_number_list;
            }
        }

        private bool IsTaskCanceled
        {
            get
            {
                return task_id == Guid.Empty;
            }
        }

        public IAsyncResult AsyncResult
        {
            get;
            private set;
        }

        public List<long> PrimeNumberList
        {
            get;
            private set;
        }

        public void CalculateAsync()
        {
            if (task_id != Guid.Empty)
            {
                throw new InvalidOperationException();
            }

            task_id = Guid.NewGuid();
            async_operation = AsyncOperationManager.CreateOperation(task_id);
            Action calc = () =>
            {
                Exception error = null;
                try
                {
                    CalculatePrimeNumbers();
                }
                catch (Exception ex)
                {
                    error = ex;
                }
                bool cancelled = IsTaskCanceled;
                bool lock_taken = false;
                spin_lock_task_id.Enter(ref lock_taken);
                task_id = Guid.Empty;
                spin_lock_task_id.Exit(false);
                async_operation.PostOperationCompleted((arg) =>
                {
                    if (CalculateCompleted != null)
                    {
                        CalculateCompleted(this, arg as AsyncCompletedEventArgs);
                    }
                }, new AsyncCompletedEventArgs(error, cancelled, async_operation.UserSuppliedState));
            };
            this.AsyncResult = calc.BeginInvoke(null, null);
        }

        private void CalculatePrimeNumbers()
        {
            Action output_result = () =>
            {
                async_operation.Post((arg) =>
                {
                    if (OutputResult != null)
                    {
                        OutputResult(this, arg as EventArgs);
                    }
                }, new EventArgs());
            };

            output_result();

            long i = PrimeNumberList.Last();
            int len = PrimeNumberList.Count;
            while (!IsTaskCanceled)
            {
                long k = (long)Math.Sqrt(i);
                for (int j = 1; j < len; j++)
                {
                    if (i % PrimeNumberList[j] == 0)
                    {
                        goto NEXT;
                    }
                }
                bool lock_taken = false;
                BeginAccessData(ref lock_taken);
                PrimeNumberList.Add(i);
                EndAccessData();
                len++;
                output_result();
            NEXT:
                i += 2;
            }
        }

        public void CancelCalculate()
        {
            bool lock_taken = false;
            spin_lock_task_id.Enter(ref lock_taken);
            task_id = Guid.Empty;
            spin_lock_task_id.Exit(false);
        }

        public void BeginAccessData(ref bool lock_taken)
        {
            spin_lock_list.Enter(ref lock_taken);
        }

        public void EndAccessData()
        {
            spin_lock_list.Exit(false);
        }
    }
}
