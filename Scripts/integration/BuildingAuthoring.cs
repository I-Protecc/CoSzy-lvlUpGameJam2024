using GameJamPlaceHolderName.Scripts.domain;
using Godot;

namespace GameJamPlaceHolderName.Scripts.integration;

public partial class BuildingAuthoring : Node2D
{
	[Export]
	public int Health;
	
	private StaticBody2D _building; 
	private Sprite2D _buildingSprite;
	private MouseChecker _mouseChecker;
	private Area2D _area2D;
	public Building Building { get; private set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Building = new Building(Health);
		_building = GetNode<StaticBody2D>("Body");
		_buildingSprite = GetNode<Sprite2D>("Body/BuildingPlaceHolder");
		_mouseChecker= GetNode<Area2D>("Body/Area2D") as MouseChecker;

		_area2D = GetNode<Area2D>("Body/Area2D");
		_area2D.AreaEntered += _OnAreaEntered;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Building.IsDestroyed) QueueFree();
		
	}

	public void Damage(float damage)
	{
		Building.Damage(damage);
	}

	public void Destroy()
	{
		Building.Destroy();
	}

	public void Repair(float healAmount)
	{
		Building.Repair(healAmount);
	}

	private void Highlighting()
	{
		if (_mouseChecker.MouseHovering)
		{
			//ShaderStuff
		}
	}
	private void _OnAreaEntered(Area2D area)
    {
    	// Replace with function body.
    }
}



