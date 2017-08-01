namespace vnc.Utilities.Time
{
    /// <summary> Keeps track of timed events and automatically trigger them.</summary>
    public abstract class Timer
    {
        #region Fields
        protected float _timer;
        protected float _maxTimer;
        protected bool isStarted = false;
        #endregion

        public Timer(float time)
        {
            _maxTimer = _timer = time;
        }

								#region Public
								/// <summary>
								/// Check if the time was already started
								/// </summary>
								public bool HasStarted
								{
												get
												{
																return isStarted;
												}
								}

								/// <summary>
								/// Starts the timer and must be called before Tick() or FixedTick().
								/// </summary>
								public void Start()
        {
            isStarted = true;
        }

        /// <summary>
        /// Ticks the timer using delta time (Update function)
        /// </summary>
        public void Tick()
        {
            if (!isStarted)
            {
                UnityEngine.Debug.LogError("Timer not started. Did you called 'Start()' method?");
                UnityEngine.Debug.Break();
            }
            Step(UnityEngine.Time.deltaTime);
        }

        /// <summary>
        /// Ticks the timer using fixed delta time (FixedUpdate function)
        /// </summary>
        public void FixedTick()
        {
            if (!isStarted)
            {
                UnityEngine.Debug.LogError("Timer not started. Did you called 'Start()' method?");
                UnityEngine.Debug.Break();
            }
            Step(UnityEngine.Time.fixedDeltaTime);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Step the timer 
        /// </summary>
        /// <param name="stepValue">The value of each step or tick</param>
        protected abstract void Step(float stepValue);
        #endregion
    }

}
