using System;
using System.Threading.Tasks;

namespace QIndependentStudios.Obex
{
    internal static class TaskEx
    {
        public static async Task WithTimeout(this Task task, int timeout)
        {
            var timeoutTask = Task.Delay(timeout);
            if (await Task.WhenAny(task, timeoutTask) == timeoutTask)
                throw new TimeoutException();
        }

        public static async Task<T> WithTimeout<T>(this Task<T> task, int timeout)
        {
            var timeoutTask = Task.Delay(timeout);
            if (await Task.WhenAny(task, timeoutTask) == timeoutTask)
                throw new TimeoutException();

            return task.Result;
        }
    }
}
