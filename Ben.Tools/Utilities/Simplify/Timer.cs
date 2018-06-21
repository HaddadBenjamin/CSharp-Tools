namespace BenTools.Utilities.Simplify
{
     
    [System.Serializable]
    public sealed class Timer
    {
        public float Alarm { get; private set; }
        public float ElapsedTime { get; private set; }

        public Timer(float timer, bool alarmisRinging = true)
        {
            Alarm = timer;
            ElapsedTime = alarmisRinging ? Alarm : 0.0f;
        }
        
        public bool IsRingingUpdated()
        {
            Update();

            bool alarmIsRinging = IsRinging();

            if (alarmIsRinging)
                this.Reset();

            return alarmIsRinging;
        }

        public bool IsRinging() => ElapsedTime >= Alarm;

        public void Update()
        {
            //this.elapsedTime += TimeService.Instance.ElapsedTime;
        }

        public void Reset() => ElapsedTime = 0.0f;

        public float Ratio() => ElapsedTime / Alarm;

        public float GetTimeToWait() => Alarm - ElapsedTime;

        public void DecreaseElapsedTime(float elaspedTimeAdd)
        {
            ElapsedTime -= elaspedTimeAdd;

            if (ElapsedTime < 0.0f)
                ElapsedTime = 0.0f;
        }
        
    }
    
}
