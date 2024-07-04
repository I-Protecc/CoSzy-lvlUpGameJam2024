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
	
	public Building Building { get; private set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Building = new Building(Health);
		_building = GetNode<StaticBody2D>("Body");
		_buildingSprite = _building.GetNode<Sprite2D>("BuildingPlaceHolder");
		_mouseChecker= _building.GetNode<Area2D>("Area2D") as MouseChecker;

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
	
}
