using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using L.ServiceBases;

namespace L.Util
{
    public class ActionFactoryService:LServiceBase
    {
        private readonly Queue<Action> _actionsQueue = new Queue<Action>();
        public int MaxActionQueue { get; set; } = 66;
        public int MaxWork { get; set; } = 6;
        //Semaphore semaphore = new Semaphore(0, 10);
        private int _counter = 0; //Interlocked.Increment()

        private Thread _workHeader;

        public void PutAction(Action action)
        {
            lock (_actionsQueue)
            {
                if(_actionsQueue.Count>= MaxActionQueue)return;
                _actionsQueue.Enqueue(action);
            }
        }

        protected override bool OnOpen()
        {
            _workHeader = new Thread(() =>
            {
                while (IsRunning)
                {
                    var current = Interlocked.Increment(ref _counter);
                    if (current > MaxWork)
                    {
                        Interlocked.Decrement(ref _counter);
                        Thread.Sleep(1);
                        continue;
                    }

                    Action action = null;
                    lock (_actionsQueue)
                    {
                        if(_actionsQueue.Count>0) action = _actionsQueue.Dequeue();
                        if (action == null)
                        {
                            Interlocked.Decrement(ref _counter);
                        }
                        else
                        {
                            Task.Run(() =>
                            {
                                try{action?.Invoke();}
                                catch (Exception e){Loger.Log.Warn(e);}
                                Interlocked.Decrement(ref _counter);
                            });
                        }
                        if (_actionsQueue.Count > 0)continue;
                    }
                    Thread.Sleep(1);
                }
            });
            _workHeader.Start();

            return true;
        }

        protected override void OnDispose()
        {
            _workHeader?.Join(0);
        }
    }
}
