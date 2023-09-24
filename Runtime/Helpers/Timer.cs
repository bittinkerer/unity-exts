using UnityEngine;

namespace Packages.Estenis.UnityExts_
{
    /// <summary>
    /// Timer will use a rate and wait for rate seconds to pass before returning true
    /// At which point the time to execute will be (currentTime + rate) 
    /// </summary>
    public class Timer
    {
        private float _timeToExecute;
        private readonly float _rateInSeconds;

        public Timer(float rate)
        {
            _timeToExecute = 0;
            _rateInSeconds = rate;
        }

        public bool IsTimeSoft =>
            Time.time > _timeToExecute;

        public void Reset() =>
            _timeToExecute = Time.time + _rateInSeconds;

        /// <summary>
        /// Returns true if _rate amount of seconds have passed since last called AND
        /// increments _timeToExecute to current time + rate IF time to execute
        /// </summary>
        public bool CheckTimeAndIncrement()
        {
            if (Time.time > _timeToExecute)
            {
                _timeToExecute = Time.time + _rateInSeconds;
                return true;
            }
            return false;
        }

    }
}
