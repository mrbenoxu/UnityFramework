﻿using System;
using GameFramework;
using GameFramework.Event;

public class EventManager : SingletonMono<EventManager> 
{
    private IEventModule m_EventModule;

    public void Initialize()
    {
        m_EventModule = GameFrameworkEntry.GetModule<IEventModule>();
    }

    /// <summary>
    /// 获取事件处理函数的数量。
    /// </summary>
    /// <param name="id">事件类型编号。</param>
    /// <returns>事件处理函数的数量。</returns>
    public int Count(int id)
    {
        return m_EventModule.Count(id);
    }

    /// <summary>
    /// 检查是否存在事件处理函数。
    /// </summary>
    /// <param name="id">事件类型编号。</param>
    /// <param name="handler">要检查的事件处理函数。</param>
    /// <returns>是否存在事件处理函数。</returns>
    public bool Check(int id, EventHandler<GameEventArgs> handler)
    {
        return m_EventModule.Check(id, handler);
    }

    /// <summary>
    /// 订阅事件处理回调函数。
    /// </summary>
    /// <param name="id">事件类型编号。</param>
    /// <param name="handler">要订阅的事件处理回调函数。</param>
    public void Subscribe(int id, EventHandler<GameEventArgs> handler)
    {
        m_EventModule.Subscribe(id, handler);
    }

    /// <summary>
    /// 取消订阅事件处理回调函数。
    /// </summary>
    /// <param name="id">事件类型编号。</param>
    /// <param name="handler">要取消订阅的事件处理回调函数。</param>
    public void Unsubscribe(int id, EventHandler<GameEventArgs> handler)
    {
        m_EventModule.Unsubscribe(id, handler);
    }

    /// <summary>
    /// 设置默认事件处理函数。
    /// </summary>
    /// <param name="handler">要设置的默认事件处理函数。</param>
    public void SetDefaultHandler(EventHandler<GameEventArgs> handler)
    {
        m_EventModule.SetDefaultHandler(handler);
    }

    /// <summary>
    /// 抛出事件，这个操作是线程安全的，即使不在主线程中抛出，也可保证在主线程中回调事件处理函数，但事件会在抛出后的下一帧分发。
    /// </summary>
    /// <param name="sender">事件发送者。</param>
    /// <param name="e">事件内容。</param>
    public void Fire(object sender, GameEventArgs e)
    {
        m_EventModule.Fire(sender, e);
    }

    /// <summary>
    /// 抛出事件立即模式，这个操作不是线程安全的，事件会立刻分发。
    /// </summary>
    /// <param name="sender">事件发送者。</param>
    /// <param name="e">事件内容。</param>
    public void FireNow(object sender, GameEventArgs e)
    {
        m_EventModule.FireNow(sender, e);
    }

    protected override bool IsGlobalScope
    {
        get { return true; }
    }
}
