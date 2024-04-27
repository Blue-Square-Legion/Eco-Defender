using System;
using System.Timers;

/// <summary>
/// Debounce timeout. 
/// Start triggers on no Cooldown. 
/// Reset cooldown on each start attempt. 
/// </summary>
public class Debounce
{
    private Timer _timer;
    public Action OnEnd, OnStart;

    public Debounce(float CooldownTime = 0.5f)
    {
        _timer = new Timer(CooldownTime * 1000);
        _timer.AutoReset = false;

        _timer.Elapsed += TimerEnd;
    }

    public void Start()
    {
        if (_timer.Enabled)
        {
            _timer.Stop();
        }
        else
        {
            OnStart?.Invoke();
        }
        _timer.Start();
    }

    public void Stop()
    {
        _timer.Stop();
    }

    private void TimerEnd(object sender, ElapsedEventArgs e)
    {
        OnEnd?.Invoke();
    }
}
