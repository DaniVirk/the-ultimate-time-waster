using UnityEngine;
using System.Timers;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Object = System.Object;

public class TimerToText : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Button mainButton;
    
    private Timer _timer;
    
    private static long _counter;
    private Vector2 _mousePos;
    private bool _gameOver;

    private void Start()
    {
        _counter = 0;
        mainButton.gameObject.SetActive(false);
        _mousePos = Mouse.current.position.ReadValue();
        _timer = new Timer(1000);
        _timer.Elapsed += OnTimedEvent;
        _timer.AutoReset = true;
        _timer.Enabled = true;
    }

    private void OnDestroy()
    {
        _timer?.Dispose();
    }

    private void Update()
    {
        if (_gameOver) return;

        if (AnyKeyPressed() && _gameOver == false)
        {
            _timer.Enabled = false;
            mainButton.gameObject.SetActive(true);
            _gameOver = true;
        }
        PrintTimerToText();
    }

    private void PrintTimerToText()
    {
        var days = (int)_counter / (24 * 3600);
        var hours = (int)(_counter % (24 * 3600)) / 3600;
        var minutes = (int)(_counter % 3600) / 60;
        var seconds = (int)_counter % 60;
        
        var timer = "";

        if (days != 0)
        {
            timer += $"{days}:";
        }

        if (days != 0 || hours != 0)
        {
            timer += $"{hours:D2}:";
        }
        else if (minutes != 0)
        {
            timer += $"{minutes:D2}:";
        }

        if (days != 0 || hours != 0)
        {
            timer += $"{minutes:D2}:";
        }
        
        timerText.text = timer + $"{seconds:D2}";
    }

    private bool AnyKeyPressed()
    {
        if (Keyboard.current.anyKey.isPressed){ return true;}
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