using GameJamPlaceHolderName.Scripts.domain.miscellaneous;
using Godot;

namespace GameJamPlaceHolderName.Scripts.integration;

public partial class DayNightManager : Node
{
    private DayTime _dayTimeEnum;
    [Export] public int DayNightSeconds = 10;
    [Export] public int DawnDuskSeconds = 5;
    
    private DirectionalLight2D _sun;
    private Timer _timer;
    
    private float _t = 1f;

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
        _sun.Energy = 1f;
    }

    public override void _PhysicsProcess(double delta)
    {
        _t += (float)delta * 0.05f;
        
        if (_dayTimeEnum is DayTime.Dawn or DayTime.Dusk && _t <= 1)
            _sun.Energy += _t * (0.5f - _sun.Energy);

        if (_dayTimeEnum is DayTime.Day && _t <= 1)
            _sun.Energy += _t * (1f - _sun.Energy);
        
        if (_dayTimeEnum is DayTime.Night && _t <= 1)
            _sun.Energy += _t * (0f - _sun.Energy);
    }

    public void OnTimerTimeout()
    {
        GD.Print("TIMEOUT");
        
        switch (_dayTimeEnum)
        {
            case DayTime.Day:
                _timer.WaitTime = DawnDuskSeconds;
                _dayTimeEnum = DayTime.Dusk;
                _t = 0f;
                _timer.Start();
                GD.Print("Switching from day to dusk");
                
                break;
            case DayTime.Night:
                _timer.WaitTime = DawnDuskSeconds;
                _dayTimeEnum = DayTime.Dawn;
                _t = 0f;
                _timer.Start();
                GameManager.Instance.NewCycle();
                GD.Print("Switching from night to dawn");
                
                break;
            case DayTime.Dawn:
                _timer.WaitTime = DayNightSeconds;
                _dayTimeEnum = DayTime.Day;
                _t = 0f;
                _timer.Start();
                GameJamPlaceHolderName.Scripts.integration.GameManager.Instance.DaysPassed += 1;
                GD.Print("Switching from dawn to day");
                
                break;
            case DayTime.Dusk:
                _timer.WaitTime = DayNightSeconds;
                _dayTimeEnum = DayTime.Night;
                _t = 0f;
                _timer.Start();
                GameManager.Instance.StartAttackWave();
                GD.Print("Switching from dusk to night");
                
                break;
        }
    }
}