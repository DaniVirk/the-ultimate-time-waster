using UnityEngine;
using System.Timers;
using TMPro;
using UnityEngine.InputSystem;
using Object = System.Object;

public class TimerToText : MonoBehaviour
{
    private Timer _timer;
    private static long _counter;
    public TextMeshProUGUI timerText;
    private int _days;
    private int _hours;
    private int _minutes;
    private int _seconds;
    private Vector2 _mousePos;

    private void Start()
    {
        _mousePos = Mouse.current.position.ReadValue();
        
        Debug.Log(_mousePos);
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
        if (AnyKeyPressed())
        {
            Debug.Log("You moved!");
            _timer.Enabled = false;
        }
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

    private bool AnyKeyPressed()
    {
        if (Keyboard.current.anyKey.isPressed) return true;
        if (Mouse.current.leftButton.isPressed) return true;
        if (Mouse.current.rightButton.isPressed) return true;
        if (Mouse.current.position.ReadValue() != _mousePos) return true;
        
        return false;
    }
    
    private static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        _counter++;
    }
}