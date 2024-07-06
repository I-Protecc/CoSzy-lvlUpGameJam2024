using Godot;

namespace GameJamPlaceHolderName.Scripts.integration;

public partial class UnitAttack : Area2D
{
    [Export] public float WaitTime;
    
    private Area2D _area2D;
    private Timer _timer;
    private Node2D _attackedTarget;

    private float _damageAmount;

    public override void _Ready()
    {
        _area2D = GetNode<Area2D>(".");
        _timer = GetNode<Timer>("Timer");

        _timer.Timeout += OnTimerTimeout;
        _area2D.AreaEntered += _OnAreaEntered;
        _area2D.AreaExited += _OnAreaExited;
        
        _timer.WaitTime = WaitTime;
    }
    
    private void _OnAreaEntered(Area2D area)
    {
        GD.Print("attack area entered");
        if (area is not null && area.GetParent().GetParent() is Node2D )
        {
            GD.Print("target accepted");
            _attackedTarget= area.GetParent().GetParent<Node2D>();
            if (HasMethod("GetDamageAmount"))
            {
                GD.Print("Trying to get damage amount");
                _damageAmount = (float)_attackedTarget.Call("GetDamageAmount");
            }
            _timer.Start();
            GD.Print("timer started");
        }
    }

    private void _OnAreaExited(Area2D area)
    {
        if (area == _attackedTarget)
        {
            GD.Print("Stopping Timer");
            _timer.Stop();
        }
    }

    private void OnTimerTimeout()
    {
        if (_attackedTarget.HasMethod("Damage"))
        {
            _attackedTarget.Call("Damage", _damageAmount); 
            GD.Print("damage method called");
        };
        GD.Print("tried dealing damage");
    }
}
