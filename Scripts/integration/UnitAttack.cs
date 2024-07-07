using Godot;

namespace GameJamPlaceHolderName.Scripts.integration;

public partial class UnitAttack : Area2D
{
    [Export] public float WaitTime;
    
    private Area2D _area2D;
    private Timer _timer;
    private Node2D _attackedTarget;

    [Export]
    private float _damageAmount;

    public override void _Process(double delta)
    {
        var overlappingAreas = _area2D.GetOverlappingAreas();

        if (overlappingAreas.Count == 0)
        {
            _timer.Stop();
            return;
        }
        
        Area2D firstArea = overlappingAreas[0];
        
        if (firstArea.GetParent().GetParent() is Node2D)
        {
            GD.Print("target accepted");
            _attackedTarget= firstArea.GetParent().GetParent<Node2D>();
            if (HasMethod("GetDamageAmount"))
            {
                _damageAmount = (float)_attackedTarget.Call("GetDamageAmount");
            }
            
            if(_timer.TimeLeft == 0)
            {
                _timer.Start();
                GD.Print("timer started");
            }
        }
    }

    public override void _Ready()
    {
        _area2D = GetNode<Area2D>(".");
        _timer = GetNode<Timer>("Timer");

        _timer.Timeout += OnTimerTimeout;
        
        _timer.WaitTime = WaitTime;
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

    public float GetDamageAmount()
    {
        return _damageAmount;
    }
}
