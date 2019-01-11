using System;
using UnityEngine;
using UnityEngine.UI;

public class AlarmClock : MonoBehaviour {

    public Text AlarmText;
    public string StartTime;
    public string AlarmTime;
    public string Format = "HH :mm";
    public bool AlarmActive = false;
    public bool CountDown = false;
    public int CountDownTime = 120;

    private AudioSource alarmSound;
    private DateTime _time = DateTime.Now;
    private DateTime _alarm = DateTime.MinValue;
    private bool ClockIsActive = true;

    // Use this for initialization
    void Start () {
        alarmSound = GetComponent<AudioSource>();

        InvokeRepeating("UpdateTime", 0.0f, 1.0f);

        //Try to parse time, if not a valid time use current time.
        if (!String.IsNullOrEmpty(StartTime)){
            DateTime.TryParse(StartTime, out _time);
        }

        //try to parse alarm time, if not a valid time use MinValue (alarm wont activate)
        if (!String.IsNullOrEmpty(AlarmTime))
        {
            DateTime.TryParse(AlarmTime, out _alarm);
        }

        if (CountDown)
        {
            GlobalCountDown.StartCountDown(TimeSpan.FromSeconds(CountDownTime));

            _time = new DateTime(GlobalCountDown.TimeLeft.Ticks);
            DateTime.TryParse("00:00", out _alarm);

            if (GlobalCountDown.TimeLeft == TimeSpan.Zero)
            {
                AlarmActive = true;
            }
        }
    }

    public void ActivateAlarm() {
        if (!AlarmActive)
        {
            print("oi");

            AlarmActive = true;
            if (alarmSound != null)
            {
                alarmSound.Play();
            }
        }
    }

    public void SilenceAlarm()
    {
        if (AlarmActive)
        {
            AlarmActive = false;
            if (alarmSound != null)
            {
                alarmSound.Stop();
            }
        }
    }

    void UpdateTime()
    {
        if (CountDown)
        {
            if (ClockIsActive)
            {
                _time = _time.AddSeconds(-1);
            }
            if (AlarmText)
            {
                AlarmText.text = _time.ToString(Format);
            }
            if (_time.Hour == 0
                && _time.Minute == 0
                && _time.Second == 0)
            {
                ActivateAlarm();
                ClockIsActive = false;
            }
        }
        else
        {
            _time = _time.AddSeconds(1);
            if (AlarmText)
            {
                AlarmText.text = _time.ToString(Format);
            }

            //if an alarm has been set
            if (_alarm > DateTime.MinValue)
            {
                if (_time.Hour == _alarm.Hour
                    && _time.Minute == _alarm.Minute
                    && _time.Second == _alarm.Second)
                {
                    ActivateAlarm();
                }
            }
        }

    }
}
