namespace vnc.Utilities
{
    public class CooldownEvent : Timer
    {
        /// <summary>
        /// If cooldown is finished
        /// </summary>
        public bool IsReady
        {
            get { return _isReady; }
            private set { _isReady = value; }
        }
        bool _isReady;

        public CooldownEvent(float time) : base(time) { }

        /// <summary>
        /// Step the timer 
        /// </summary>
        /// <param name="stepValue">The value of each step or tick</param>
        protected override void Step(float stepValue)
        {
            if(_timer > 0)
                _timer -= stepValue;

            IsReady = _timer <= 0;
        }

        /// <summary>
        /// Restart the cooldown timer
        /// </summary>
        public void Reset()
        {
            _timer = _maxTimer;
        }
    }
}

