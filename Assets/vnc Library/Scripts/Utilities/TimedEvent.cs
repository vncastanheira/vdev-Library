using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace vnc.Utilities
{

    public class TimedEvent : Timer
    {
        UnityEvent OnEventTrigger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time">Time in seconds between event triggering</param>
        /// <param name="trigger">The event to be triggered</param>
        public TimedEvent(float time, UnityAction trigger) : base(time)
        {
            OnEventTrigger = new UnityEvent();
            OnEventTrigger.AddListener(trigger);
        }

        #region Protected
        /// <summary>
        /// Step the timer 
        /// </summary>
        /// <param name="stepValue">The value of each step or tick</param>
        protected override void Step(float stepValue)
        {
            _timer -= stepValue;
            try
            {
                if (_timer <= 0)
                {
                    OnEventTrigger.Invoke();
                    _timer = _maxTimer;
                }
            }
            catch (System.NullReferenceException ex)
            {
                Debug.LogErrorFormat("Timer event is null.\n{0}", ex.Message);
            }
        }
        #endregion
    }

}
