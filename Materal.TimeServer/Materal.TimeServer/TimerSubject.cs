using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Materal.TimeServer
{
    public class TimerSubject : ITimerSubject
    {
        private Timer _timer;
        private readonly ConcurrentBag<ITimerObserver> _observers;

        public TimerSubject(IEnumerable<ITimerObserver> observers)
        {
            _observers = new ConcurrentBag<ITimerObserver>(observers);
        }

        private void Execute(object stateInfo)
        {
            Parallel.ForEach(_observers, async item =>
            {
                if (item.NextExecuteTime > DateTime.Now) return;
                switch (item)
                {
                    case ISyncTimerObserver syncTimerObserver:
                        syncTimerObserver.Execute();
                        break;
                    case IAsyncTimerObserver asyncTimerObserver:
                        await asyncTimerObserver.ExecuteAsync();
                        break;
                }
                item.UpdateNextExecuteTime();
            });
            _timer.Change(1, 0);
        }

        public void Start()
        {
            _timer = new Timer(Execute);
            _timer.Change(1, 0);
        }

        public void Stop()
        {
            _timer?.Dispose();
        }
    }
}
