using UnityEngine;
using System.Timers;
using TMPro;
using Object = System.Object;

public class TimerToText : MonoBehaviour
{
    private Timer _timer;
    private static long _counter;
    public TextMeshProUGUI timerText;
    private int _days = 0;
    private int _hours = 0;
    private int _minutes = 0;
    private int _seconds = 0;

    private void Start()
    {
        _timer = new Timer(1000);
        _timer.Elapsed += OnTimedEvent; // Hook up the event
        _timer.AutoReset = true; // Restart timer after each event
        _timer.Enabled = true; // Start the timer
    }

    private void OnDestroy()
    {
        _timer?.Dispose();
    }

    private void Update()
    {
        PrintTimerToText();
    }

    private void PrintTimerToText()
    {
        _days = (int)_counter / (24 * 3600);
        _hours = (int)(_counter % (24 * 3600)) / 3600;
        _minutes = (int)(_counter % 3600) / 60;
        _seconds = (int)_counter % 60;
        
        var timer = "";

        if (_days != 0)
        {
            timer += $"{_days}:";
        }

        if (_days != 0 || _hours != 0)
        {
            timer += $"{_hours:D2}:";
        }
        else if (_minutes != 0)
        {
            // Skip hours but still want mm:ss
            timer += $"{_minutes:D2}:";
        }

        if (_days != 0 || _hours != 0)
        {
            timer += $"{_minutes:D2}:";
        }

        timer += $"{_seconds:D2}";

        timerText.text = timer;
    }
    
    private static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        _counter++;
    }
}