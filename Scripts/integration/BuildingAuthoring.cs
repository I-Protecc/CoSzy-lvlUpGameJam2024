using GameJamPlaceHolderName.Scripts.domain;
using Godot;

namespace GameJamPlaceHolderName.Scripts.integration;


public enum WorkType
{
	Farm,
	Mine,
	Wall,
	DefenseTower,
	//more to come if we have time 
}

public partial class BuildingAuthoring : Node2D
{
	[Export]
	public float Health;

	[Export] 
	public WorkType WorkType = WorkType.Farm;

	public bool MayBePlaced = true;

	private bool _isWorking;
	
	private StaticBody2D _building; 
	private Sprite2D _buildingSprite;
	private MouseChecker _mouseChecker;
	private Area2D _area2D;

	private Node2D _employedWorker;
	private WorkerAuthoring _employedWorkerAuthoring;
	
	public Building Building { get; private set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Building = new Building(Health);
		
		_building = GetNode<StaticBody2D>("Body");
		_mouseChecker= GetNode<Area2D>("Body/Area2D") as MouseChecker;

		if (this.WorkType is WorkType.Farm or WorkType.Farm)
		{
			_buildingSprite = GetNode<Sprite2D>("Body/BuildingPlaceHolder");
			_area2D = GetNode<Area2D>("Body/Area2D"); 
			_area2D.AreaEntered += _OnAreaEntered;
			_area2D.AreaExited += _OnAreaExited;
		}
		else if (this.WorkType is WorkType.Wall)
		{
			_area2D = GetNode<Area2D>("Body/Area2D"); 
			_buildingSprite = GetNode<Sprite2D>("Body/WallPlaceHolder");
			_area2D.AreaEntered += _OnAreaEntered;
			_area2D.AreaExited += _OnAreaExited;
		}
		else if (this.WorkType is WorkType.DefenseTower)
		{
			_area2D = GetNode<Area2D>("AttackArea");
			_buildingSprite = GetNode<Sprite2D>("TowerPlaceHolder");
			_area2D.AreaEntered += _OnAreaEntered;
			_area2D.AreaExited += _OnAreaExited;
		}
	} 

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Building.IsDestroyed)
		{
			if (_isWorking)
			{
				StopWork();
				
				_employedWorkerAuthoring.SwitchWorkerState(true);
				_employedWorkerAuthoring.Kill();
				
				GD.Print("Worker " + _employedWorker + " got killed");
			}
			
			QueueFree();
		}

		if (WorkType is not WorkType.DefenseTower && WorkType is not WorkType.Wall)
		{
			if(_mouseChecker.Interacted)
				return;
			_mouseChecker.Interacted = false;
			WorkerInteract();
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
		MayBePlaced = false;
		GD.Print("Area Entered");
	    
	    if (!_isWorking && GameManager.Instance.SelectedBuilding == this)
	    {
		    GD.Print("Area was entered");
		    if (_employedWorker != null)
		    {
			    if (area == _employedWorker.GetNode<Area2D>("Body/InteractionArea"))
			    {
				    _employedWorkerAuthoring = _employedWorker as WorkerAuthoring;
				    StartWork();
				    OnWorkerArrived();
				    GameManager.Instance.UnsetSelectedWorker();
				    GD.Print("Now Work commences");
			    }
		    }
	    }
    }

	private void _OnAreaExited(Area2D area)
	{
		MayBePlaced = true;
		GD.Print("Area exited");
	}
	
	private void WorkerInteract()
	{
		if (GameManager.Instance.GetSelectedWorker() is not null  && !_isWorking && GameManager.Instance.hasWorkerSelected)
		{
			_employedWorker = GameManager.Instance.GetSelectedWorker();
			GameManager.Instance.SelectedBuilding = this;
			
			GD.Print("Worker Assigned");
		}
		if (GameManager.Instance.GetSelectedWorker() is null && _isWorking && !GameManager.Instance.hasWorkerSelected )
		{
			GD.Print("the removing is done");
			RemoveEmployedWorker();
		}
		else
		{
			GD.Print("nuh-uh");
		}
	}

	private void StartWork()
	{
		_isWorking = true;
		GD.Print("is Workin? " + _isWorking);
		switch (WorkType)
		{
			case WorkType.Farm:
				GameManager.Instance.FarmsStartedWorking(1);
				break;
			case WorkType.Mine:
				GameManager.Instance.MineStartedWorking(1);
				break;
			case WorkType.Wall:
				break;
			default:
				GD.Print("No WorkType (Somehow)");
				break;
				
		}
		
	}
	private void StopWork()
	{
		_isWorking = false;
		
		switch (WorkType)
		{
			case WorkType.Farm:
				GameManager.Instance.FarmsStoppedWorking(1);
				break;
			case WorkType.Mine:
				GameManager.Instance.MineStoppedWorking(1);
				break;
			case WorkType.Wall:
				break;
			default:
				GD.Print("No WorkType (Somehow)");
				break;
				
		}
		
	}

	private void RemoveEmployedWorker()
	{
		OnWorkerLeave();
		StopWork();
	}

	private void OnWorkerArrived()
	{
		if (_employedWorker is WorkerAuthoring workerAuthoring) workerAuthoring.SwitchWorkerState(false);
	}

	private void OnWorkerLeave()
	{
		if (_employedWorker is WorkerAuthoring workerAuthoring) workerAuthoring.SwitchWorkerState(true);
	}
}



