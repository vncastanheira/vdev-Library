using System;
using System.Collections.Generic;
using UnityEngine;

namespace vnc.Tools
{
    /// <summary> Global event system </summary>
    public static class VncEventSystem
    {
        static Dictionary<Type, List<IVncEventListenerGeneric>> _events;

        static VncEventSystem()
        {
            _events = new Dictionary<Type, List<IVncEventListenerGeneric>>();
        }

        /// <summary>
        /// Start listening to events
        /// </summary>
        /// <typeparam name="VncEvent">Type of event to listen to</typeparam>
        /// <param name="listener">Register itself as event listener</param>
        public static void Listen<VncEvent>(this IVncEventListener<VncEvent> listener) where VncEvent : struct
        {
            Type eType = typeof(VncEvent);
            List<IVncEventListenerGeneric> _eventList;

            if (!_events.ContainsKey(eType))
            {
                _eventList = new List<IVncEventListenerGeneric>();
                _events.Add(eType, _eventList);
            }
            else
            {
                _eventList = _events[eType];
            }

            if (!_eventList.Contains(listener))
                _eventList.Add(listener);
        }

        /// <summary>
        /// Stop listening to events
        /// </summary>
        /// <typeparam name="VncEvent">Type of event to unlisten to</typeparam>
        /// <param name="listener">Remove itself as event listener</param>
        public static void Unlisten<VncEvent>(this IVncEventListener<VncEvent> listener) where VncEvent : struct
        {
            List<IVncEventListenerGeneric> _eventList;

            if (_events.TryGetValue(typeof(VncEvent), out _eventList))
            {
                _eventList.Remove(listener);
            }
            else
            {
                throw new UnityException("Cannot unlisten this listener. It doesn't exist.");
            }
        }

        /// <summary>
        /// Trigger an event
        /// </summary>
        /// <typeparam name="VncEvent">A generic struct</typeparam>
        /// <param name="vncEvent">Event message to send to all lintener</param>
        public static void Trigger<VncEvent>(VncEvent vncEvent) where VncEvent : struct
        {
            List<IVncEventListenerGeneric> _eventList;
            if (!_events.TryGetValue(typeof(VncEvent), out _eventList))
                throw new UnityException("'VncEvent' doesn't exist. Make sure to listen to it first.");

            foreach (IVncEventListener<VncEvent> e in _eventList)
                e.OnVncEvent(vncEvent);
        }
    }

    public interface IVncEventListenerGeneric { }

    /// <summary>
    /// Inherit this interface to start listening to events.
    /// Don't forget to call "this.Listen(...)"
    /// </summary>
    /// <typeparam name="VncEvent">Type of the event</typeparam>
    public interface IVncEventListener<VncEvent> : IVncEventListenerGeneric where VncEvent : struct
    {
        void OnVncEvent(VncEvent e);
    }

}
