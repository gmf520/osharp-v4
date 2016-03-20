// -----------------------------------------------------------------------
//  <copyright file="IHubClient.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-20 15:56</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq.Expressions;

using Microsoft.AspNet.SignalR.Client;


namespace OSharp.Web.SignalR.Client
{
    /// <summary>
    /// Connection management and dispatch for clients.
    /// <para>Represents an established connection</para>
    /// <para>Dispose of the hub client to close the connection</para>
    /// </summary>
    public interface IHubClient<TCalls, TEvents> : IDisposable
    {
        /// <summary>
        /// Bind to a hub event. When the hub send a message of the given type, the handler will be invoked
        /// </summary>
        /// <param name="eventToBind">The event method exposed by the hub</param>
        /// <param name="handler">The method that should handle the event</param>
        void BindEventHandler(Expression<Func<TEvents, Action>> eventToBind, Action handler);

        /// <summary>
        /// Send a message to the hub, using a client-to-hub contract method
        /// </summary>
        /// <param name="call">Expression calling to hub. Use like <code>hub => hub.MyMethod("hello", "world")</code></param>
        void SendToHub(Expression<Action<TCalls>> call);

        /// <summary>
        /// Send a message to the hub, using a client-to-hub contract method.
        /// <para>Will synchronously retrieve a response from the hub</para>
        /// </summary>
        /// <param name="call">Expression calling to hub. Use like <code>var helloWorld = RequestFromHub&lt;string&gt;(hub => hub.GetGreeting("world"))</code></param>
        TResult RequestFromHub<TResult>(Expression<Func<TCalls, TResult>> call);

        #region BindEventHandler<> multi argument variants

        /// <summary>
        /// Bind to a hub event. When the hub send a message of the given type, the handler will be invoked
        /// </summary>
        /// <param name="eventToBind">The event method exposed by the hub</param>
        /// <param name="handler">The method that should handle the event</param>
        void BindEventHandler<T>(Expression<Func<TEvents, Action<T>>> eventToBind, Action<T> handler);

        /// <summary>
        /// Bind to a hub event. When the hub send a message of the given type, the handler will be invoked
        /// </summary>
        /// <param name="eventToBind">The event method exposed by the hub</param>
        /// <param name="handler">The method that should handle the event</param>
        void BindEventHandler<T1, T2>(Expression<Func<TEvents, Action<T1, T2>>> eventToBind, Action<T1, T2> handler);

        /// <summary>
        /// Bind to a hub event. When the hub send a message of the given type, the handler will be invoked
        /// </summary>
        /// <param name="eventToBind">The event method exposed by the hub</param>
        /// <param name="handler">The method that should handle the event</param>
        void BindEventHandler<T1, T2, T3>(
            Expression<Func<TEvents, Action<T1, T2, T3>>> eventToBind,
            Action<T1, T2, T3> handler);

        /// <summary>
        /// Bind to a hub event. When the hub send a message of the given type, the handler will be invoked
        /// </summary>
        /// <param name="eventToBind">The event method exposed by the hub</param>
        /// <param name="handler">The method that should handle the event</param>
        void BindEventHandler<T1, T2, T3, T4>(
            Expression<Func<TEvents, Action<T1, T2, T3, T4>>> eventToBind,
            Action<T1, T2, T3, T4> handler);

        #endregion BindEventHandler<> multi argument variants
    }


    /// <summary>
    /// Exposes diagnostic and advanced methods of Hub Clients
    /// </summary>
    public interface IHubClientDiagnostics
    {
        /// <summary>
        /// Remove existing connection and proxy, replace with a new set.
        /// </summary>
        void SwitchConnection(IHubProxy proxy, IConnection conn);
    }
}