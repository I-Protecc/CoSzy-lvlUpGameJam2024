using GameJamPlaceHolderName.Scripts.domain;
using Godot;

namespace GameJamPlaceHolderName.Scripts.integration;


public enum WorkType
{
	Farm,
	Mine,
	//more to come if we have time 
}

public partial class BuildingAuthoring : Node2D
{
	[Export]
	public int Health;

	[Export] 
	public WorkType WorkType;
	
	private StaticBody2D _building; 
	private Sprite2D _buildingSprite;
	private MouseChecker _mouseChecker;
	private Area2D _area2D;

	private Node2D _employedWorker;
	
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

		if (_mouseChecker.Interacted)
		{
			_mouseChecker.Interacted = false;
			EmployWorker();
		}
	
		
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
	    GD.Print("Area was entered");
	    if (area == _employedWorker.GetNode<Area2D>("Body/InteractionArea"))
	    {
		   StartWork(); 
		   GD.Print("Now Work commences");
	    }
	    
	    
    }

	private void EmployWorker()
	{
		if (GameManager.Instance.GetSelectedWorker() is not null)
		{
			_employedWorker = GameManager.Instance.GetSelectedWorker();
			GD.Print("Worker Assigned");
		}
	}

	private void StartWork()
	{

		switch (WorkType)
		{
			case WorkType.Farm:
				
				break;
			default:
				GD.Print("No WorkType (Somehow)");
				break;
				
		}
		
	}
	
}



