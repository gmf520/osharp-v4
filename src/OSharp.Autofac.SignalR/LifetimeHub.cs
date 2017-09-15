// -----------------------------------------------------------------------
//  <copyright file="LifetimeHub.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-04-02 14:16</last-date>
// -----------------------------------------------------------------------

using System;

using Microsoft.AspNet.SignalR;


namespace OSharp.Autofac.SignalR
{
    /// <summary>
    /// 带生命周期作用域的Hub基类
    /// </summary>
    public abstract class LifetimeHub : Hub, ILifetimeHub
    {
        /// <summary>
        /// 对象释放事件
        /// </summary>
        public event EventHandler OnDisposing;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                EventHandler handler = this.OnDisposing;
                if (handler != null)
                {
                    handler.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
    
    /// <summary>
    /// 带生命周期作用域的Hub基类
    /// </summary>
    public abstract class LifetimeHub<T> : Hub<T>, ILifetimeHub where T : class
    {
        /// <summary>
        /// 对象释放事件
        /// </summary>
        public event EventHandler OnDisposing;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                EventHandler handler = this.OnDisposing;
                if (handler != null)
                {
                    handler.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}