using System.Diagnostics;

namespace Ben.Tools.Development
{
    public abstract class ATimer
    {
        #region Field(s)
        protected readonly Stopwatch _stopwatch;
        private readonly double _alarm;
        private bool _isRinging;
        #endregion

        #region Propertie(s)
        public abstract double ElapsedTime { get; }

        public double Alarm => _alarm;

        public double ReaminingTime => Alarm - ElapsedTime;

        public double RatioNormalized => ElapsedTime / Alarm;

        public double Percent => RatioNormalized * 100;
        #endregion

        #region Constructor(s)
        public ATimer(double alarm, bool isRinging = false)
        {
            _stopwatch = new Stopwatch();

            _stopwatch.Start();

            _alarm = alarm;
            _isRinging = isRinging;
        }
        #endregion

        #region Public Behaviour(s)
        public bool IsRingingReset()
        {
            bool isRinging = IsRinging();

            if (isRinging)
                Reset();

            return isRinging;
        }

        public bool IsRinging()
        {
            if (_isRinging)
                return true;

            _isRinging = ElapsedTime >= Alarm;

            return _isRinging;
        }

        public void Reset()
        {
            _stopwatch.Restart();

            _isRinging = false;
        }
        #endregion
    }
}