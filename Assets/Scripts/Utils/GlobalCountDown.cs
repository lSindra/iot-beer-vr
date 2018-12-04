using System;

public static class GlobalCountDown
{
    static DateTime TimeStarted;
    static TimeSpan TotalTime;
    static bool Started = false;

    public static void StartCountDown(TimeSpan totalTime)
    {
        if (!Started)
        {
            TimeStarted = DateTime.UtcNow;
            TotalTime = totalTime;
            Started = true;
        }
    }

    public static void RestartCountDown(TimeSpan totalTime)
    {
        TimeStarted = DateTime.UtcNow;
        TotalTime = totalTime;
        Started = true;
    }

    public static TimeSpan TimeLeft
    {
        get
        {
            TimeSpan result = TotalTime - (DateTime.UtcNow - TimeStarted);
            return result.TotalSeconds <= 0 ? TimeSpan.Zero : result;
        }
    }

    //public static DateTime CurrentDateTime
    //{
    //    get
    //    {
    //        TimeSpan span = TimeStarted - DateTime.UtcNow;
    //        DateTime result = DateTime.UtcNow - TimeStarted;
    //        return result.CompareTo(DateTime.Zero) ? DateTime.Zero : result;
    //    }
    //}
}