using GameJamPlaceHolderName.Scripts.domain;
using Godot;

namespace GameJamPlaceHolderName.Scripts.integration;

public partial class HostileAuthoring : Node2D
{
	
	[Export]
	public float Health;

	[Export] 
	public float DamageAmount;
	
	public HostileMovement HostileMovement;
	public UnitAttack UnitAttack;
	private CharacterBody2D _hostileWorker;
	private Node2D _hostileWorkerNode2D;
	private Area2D _attackArea;
	private Node2D _attackedTarget;
	
	public Hostile Hostile { get; private set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hostile = new Hostile(Health);
		
		HostileMovement = GetNode<CharacterBody2D>("Body") as HostileMovement;
		UnitAttack = GetNode<Area2D>("Body/AttackArea") as UnitAttack;
		_hostileWorkerNode2D = GetNode<Node2D>(".");
		_hostileWorker = GetNode<CharacterBody2D>("Body");
		_attackArea = GetNode<Area2D>("Body/AttackArea");

		//UnitAttack.
		
		if (HostileMovement != null) HostileMovement.MoveToCore();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (Hostile.IsDead) QueueFree();
		if(GameManager.Instance.StartOfNewDay) Kill();
		
	}
	
	public void Damage(float damage)
	{
		Hostile.Damage(damage);
		GD.Print("da enemy getting damaged " + damage + " hp, hp left: " + Hostile.Health);
	}

	public void Kill()
	{
		Hostile.Kill();
	}

	public float GetDamageAmount()
	{
		return DamageAmount;
	}
}