using System;
using System.Collections.Generic;

namespace Utility.GameEventManager
{
    public static class EventManager 
    {
        static readonly Dictionary<Type, Action<IGameEvent>> s_Events = new Dictionary<Type, Action<IGameEvent>>();

        static readonly Dictionary<Delegate, Action<IGameEvent>> s_EventLookups = new Dictionary<Delegate, Action<IGameEvent>>();

        public static void AddListener<T>(Action<T> evt) where T : IGameEvent
        {
            if (!s_EventLookups.ContainsKey(evt))
            {
                Action<IGameEvent> newAction = (e) => evt((T)e);
                s_EventLookups[evt] = newAction;

                if (s_Events.TryGetValue(typeof(T), out Action<IGameEvent> internalAction))
                    s_Events[typeof(T)] = internalAction += newAction;
                else
                    s_Events[typeof(T)] = newAction;
            }
        }

        public static void RemoveListener<T>(Action<T> evt) where T : IGameEvent
        {
            if (s_EventLookups.TryGetValue(evt, out var action))
            {
                if (s_Events.TryGetValue(typeof(T), out var tempAction))
                {
                    tempAction -= action;
                    if (tempAction == null)
                        s_Events.Remove(typeof(T));
                    else
                        s_Events[typeof(T)] = tempAction;
                }

                s_EventLookups.Remove(evt);
            }
        }

        public static void Broadcast(IGameEvent evt)
        {
            if (s_Events.TryGetValue(evt.GetType(), out var action))
                action.Invoke(evt);
        }

        public static void Clear()
        {
            s_Events.Clear();
            s_EventLookups.Clear();
        }
    }
}

