using System;
using System.Collections.Generic;

namespace EventCallbacks
{
    public abstract class EventSystem
    {
        public abstract void CleanEventSystem();
    }

    /// <summary>
    /// The actual generic eventsystem
    /// </summary>
    /// <typeparam name="T">The type of the event for this system</typeparam>
    public class EventSystem<T> : EventSystem
    {
        static EventSystem<T> eventSystemInstance;
        Action<T> eventDelegate;

        public static event Action<T> EventTriggered
        {
            add
            {
                if (EventSystemInstance.eventDelegate == null)
                {
                    EventSystemManagement.RegisterNewEventSystem(EventSystemInstance);
                }

                EventSystemInstance.eventDelegate += value;
            }

            remove { EventSystemInstance.eventDelegate -= value; }
        }

        public static void FireEvent(T eventData)
        {
            EventSystemInstance.FireEvent_(eventData);
        }

        static void CleanCurrentEventSystem()
        {
            if (eventSystemInstance != null)
            {
                eventSystemInstance.CleanSubscribersList();
                // we set our instance to null, so we can check whether we have to create a new instance next time
                eventSystemInstance = null;
            }
        }

        static EventSystem<T> EventSystemInstance
        {
            get { return eventSystemInstance ?? (eventSystemInstance = new EventSystem<T>()); }
        }

        public override void CleanEventSystem()
        {
            // notice that we call a static method here
            CleanCurrentEventSystem();
        }

        void CleanSubscribersList()
        {
            eventDelegate = null;
        }

        void FireEvent_(T eventData)
        {
            if (eventDelegate != null)
            {
                eventDelegate(eventData);
            }
        }

        EventSystem()
        {
        }
    }

    /// <summary>
    /// a management class used to cleanup every used event system
    /// </summary>
    public static class EventSystemManagement
    {
        static readonly List<EventSystem> eventSystems = new List<EventSystem>();

        /// <summary>
        /// Registers a new event system instance
        /// </summary>
        /// <param name="eventSystem">The event system instance to register</param>
        public static void RegisterNewEventSystem(EventSystem eventSystem)
        {
            if (!eventSystems.Contains(eventSystem))
            {
                eventSystems.Add(eventSystem);
            }
        }

        /// <summary>
        /// removes the listeners of all registered event system instances and
        /// clears the list of registered event systems
        /// </summary>
        public static void CleanupEventSystem()
        {
            foreach (var eventSystem in eventSystems)
            {
                eventSystem.CleanEventSystem();
            }

            eventSystems.Clear();
        }
    }
}