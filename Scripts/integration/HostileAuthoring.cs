using GameJamPlaceHolderName.Scripts.domain;
using Godot;

namespace GameJamPlaceHolderName.Scripts.integration;

public partial class HostileAuthoring : Node2D
{
	
	[Export]
	public int Health;
	
	public HostileMovement HostileMovement;
	private CharacterBody2D _hostileWorker;
	private Node2D _hostileWorkerNode2D;
	private Area2D _area2D;
	
	public Hostile Hostile { get; private set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hostile = new Hostile(Health);
		HostileMovement = GetNode<CharacterBody2D>("Body") as HostileMovement;
		_hostileWorkerNode2D = GetNode<Node2D>(".");
		_hostileWorker = GetNode<CharacterBody2D>("Body");
		_area2D = GetNode<Area2D>("Body/Area2D");
		_area2D.AreaEntered += _OnAreaEntered;
		_moveToCore();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Hostile.IsDead) QueueFree();
	}
	
	public void TakeDamage(float damage)
	{
		Hostile.Damage(damage);
	}

	private void _OnAreaEntered(Area2D area)
	{
		if (area is not null  && area.GetParent().GetParent() is Node2D )
		{
			Node2D AttackedTarget = area.GetParent().GetParent<Node2D>();

			if (AttackedTarget.HasMethod("Damage")) AttackedTarget.Call("Damage");
		}
	}

	private void _moveToCore()
	{
		HostileMovement.MovementTarget = GameManager.Instance.CorePosition;
	}
}