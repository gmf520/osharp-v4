using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading;

using Microsoft.AspNet.SignalR.Client;

using Newtonsoft.Json.Linq;

using OSharp.Web.SignalR.Client.Reflection;


namespace OSharp.Web.SignalR.Client
{
    /// <summary>
    /// Default SignalR hub client. 
    /// </summary>
    /// <typeparam name="TCalls">Contract of messages for which the Hub will listen</typeparam>
    /// <typeparam name="TEvents">Contract of messages the Hub might send</typeparam>
    public class HubClient<TCalls, TEvents> : IHubClient<TCalls, TEvents>, IHubClientDiagnostics
    {
        /// <summary> Events to be unbound on disconnect </summary>
        public readonly List<Action> DisposalActions;
        private IConnection _conn;
        private IHubProxy _proxy;

        /// <summary>
        /// Connect directly to a connected proxy
        /// </summary>
        public HubClient(IHubProxy proxy, IConnection conn)
        {
            _proxy = proxy;
            _conn = conn;

            DisposalActions = new List<Action>();
        }

        /// <summary>
        /// Replace connection and proxy. Use with caution!
        /// </summary>
        public void SwitchConnection(IHubProxy proxy, IConnection conn)
        {
            _conn = conn;
            _proxy = proxy;
            var oldActs = DisposalActions.ToArray();
            DisposalActions.Clear();
            foreach (var dispose in oldActs)
            {
                dispose();
            }
        }

        /// <summary>
        /// Disconnect and remove event bindings
        /// </summary>
        public void Dispose()
        {
            foreach (var dispose in DisposalActions)
            {
                dispose();
            }
            try
            {
                _conn.Stop();
            }
            catch
            {
                Ignore();
            }
        }

        /// <summary>
        /// Ignore an exception.
        /// </summary>
        private static void Ignore() { }

        static Action<IList<JToken>> Threaded(Action<IList<JToken>> action)
        {
            Action<IList<JToken>> threadedAction = list =>
            {
                var thread = new Thread(() => action(list));
                thread.Start();
            };
            return threadedAction;
        }

        /// <summary>
        /// Bind to a hub event. When the hub send a message of the given type, the handler will be invoked
        /// </summary>
        /// <param name="eventToBind">The event method exposed by the hub</param>
        /// <param name="handler">The method that should handle the event</param>
        public void BindEventHandler(Expression<Func<TEvents, Action>> eventToBind, Action handler)
        {
            var innerHandler = Threaded(args => handler());

            BindInnerHandler(eventToBind.GetBinding(), innerHandler);
        }

        /// <summary>
        /// Bind to a hub event. When the hub send a message of the given type, the handler will be invoked
        /// </summary>
        /// <param name="eventToBind">The event method exposed by the hub</param>
        /// <param name="handler">The method that should handle the event</param>
        public void BindEventHandler<T>(Expression<Func<TEvents, Action<T>>> eventToBind, Action<T> handler)
        {
            var innerHandler = Threaded(args => handler(Convert<T>(args[0])));

            BindInnerHandler(eventToBind.GetBinding(), innerHandler);
        }

        /// <summary>
        /// Bind to a hub event. When the hub send a message of the given type, the handler will be invoked
        /// </summary>
        /// <param name="eventToBind">The event method exposed by the hub</param>
        /// <param name="handler">The method that should handle the event</param>
        public void BindEventHandler<T1, T2>(
            Expression<Func<TEvents, Action<T1, T2>>> eventToBind, Action<T1, T2> handler)
        {
            var innerHandler = Threaded(args => handler(Convert<T1>(args[0]), Convert<T2>(args[1])));

            BindInnerHandler(eventToBind.GetBinding(), innerHandler);
        }

        /// <summary>
        /// Bind to a hub event. When the hub send a message of the given type, the handler will be invoked
        /// </summary>
        /// <param name="eventToBind">The event method exposed by the hub</param>
        /// <param name="handler">The method that should handle the event</param>
        public void BindEventHandler<T1, T2, T3>(
            Expression<Func<TEvents, Action<T1, T2, T3>>> eventToBind, Action<T1, T2, T3> handler)
        {
            var innerHandler = Threaded(args => handler(Convert<T1>(args[0]), Convert<T2>(args[1]), Convert<T3>(args[2])));

            BindInnerHandler(eventToBind.GetBinding(), innerHandler);
        }

        /// <summary>
        /// Bind to a hub event. When the hub send a message of the given type, the handler will be invoked
        /// </summary>
        /// <param name="eventToBind">The event method exposed by the hub</param>
        /// <param name="handler">The method that should handle the event</param>
        public void BindEventHandler<T1, T2, T3, T4>(
            Expression<Func<TEvents, Action<T1, T2, T3, T4>>> eventToBind, Action<T1, T2, T3, T4> handler)
        {
            var innerHandler = Threaded(args => handler(Convert<T1>(args[0]), Convert<T2>(args[1]), Convert<T3>(args[2]), Convert<T4>(args[3])));

            BindInnerHandler(eventToBind.GetBinding(), innerHandler);
        }

        /// <summary>
        /// Send a message to the hub, using a client-to-hub contract method
        /// </summary>
        /// <param name="call">Expression calling to hub. Use like <code>hub => hub.MyMethod("hello", "world")</code></param>
        public void SendToHub(Expression<Action<TCalls>> call)
        {
            var invocation = call.GetInvocation();

            WaitForConnection();

            var task = _proxy.Invoke(invocation.MethodName, invocation.ParameterValues);
            if (task == null) throw new Exception("Could not contact hub");
            task.Wait();
        }

        /// <summary>
        /// Send a message to the hub, using a client-to-hub contract method.
        /// <para>Will synchronously retrieve a response from the hub</para>
        /// </summary>
        /// <param name="call">Expression calling to hub. Use like <code>var helloWorld = RequestFromHub&lt;string&gt;(hub => hub.GetGreeting("world"))</code></param>
        public TResult RequestFromHub<TResult>(Expression<Func<TCalls, TResult>> call)
        {
            var invocation = call.GetInvocation();

            WaitForConnection();

            var task = _proxy.Invoke<TResult>(invocation.MethodName, invocation.ParameterValues);
            if (task == null) throw new Exception("Could not contact hub");
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// Bind a subscription to an event name, and prepare it for disposal.
        /// </summary>
        private void BindInnerHandler(MethodCallInfo eventToBind, Action<IList<JToken>> innerHandler)
        {
            var subscription = _proxy.Subscribe(eventToBind.MethodName);
            subscription.Received += innerHandler;
            DisposalActions.Add(() => {
                subscription.Received -= innerHandler;
            });
        }


        void WaitForConnection()
        {
            var sw = new Stopwatch();
            sw.Start();
            while (_conn.State != ConnectionState.Connected)
            {
                if (sw.Elapsed > _conn.TotalTransportConnectTimeout)
                {
                    throw new TimeoutException("Connection was disconnected during an attempted request");
                }
                Thread.Sleep(100);
            }
            sw.Stop();
        }

        private T Convert<T>(JToken obj)
        {
            // ReSharper disable once RedundantCast
            if (obj as object == null)
            {
                return default(T);
            }

            // Because newtonsoft json can't handle interfaces.
            if (!typeof(T).IsInterface && !typeof(T).IsAbstract)
            {
                return obj.ToObject<T>(_proxy.JsonSerializer);
            }
            var dummy = DynamicProxy.GetInstanceFor<T>();
            return (T)obj.ToObject(dummy.GetType(), _proxy.JsonSerializer);
        }

    }

}