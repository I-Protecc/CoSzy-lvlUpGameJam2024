using Godot;
using System;
using GameJamPlaceHolderName.Scripts.domain.miscellaneous;

public partial class DayNightManager : Node
{
    private DayTime _dayTimeEnum;
    [Export] public int DayNightSeconds = 10;
    [Export] public int DawnDuskSeconds = 5;
    
    private DirectionalLight2D _sun;
    private Timer _timer;

    public DayNightManager()
    {
        _dayTimeEnum = new DayTime();
    }

    public override void _Ready()
    {
        _sun = GetNode<DirectionalLight2D>("Sun");
        _timer = GetNode<Timer>("Timer");
        _timer.Timeout += OnTimerTimeout;

        _dayTimeEnum = DayTime.Day;
    }

    public override void _Process(double delta)
    {
        
    }

    public void OnTimerTimeout()
    {
        GD.Print("TIMEOUT");
        
        switch (_dayTimeEnum)
        {
            case DayTime.Day:
                _timer.WaitTime = DawnDuskSeconds;
                _dayTimeEnum = DayTime.Dusk;
                _timer.Start();
                GD.Print("Switching from day to dusk");
                
                break;
            case DayTime.Night:
                _timer.WaitTime = DawnDuskSeconds;
                _dayTimeEnum = DayTime.Dawn;
                _timer.Start();
                GD.Print("Switching from night to dawn");
                
                break;
            case DayTime.Dawn:
                _timer.WaitTime = DayNightSeconds;
                _dayTimeEnum = DayTime.Day;
                _timer.Start();
                GD.Print("Switching from dawn to day");
                
                break;
            case DayTime.Dusk:
                _timer.WaitTime = DayNightSeconds;
                _dayTimeEnum = DayTime.Night;
                _timer.Start();
                GD.Print("Switching from dusk to night");
                
                break;
        }
    }
}
