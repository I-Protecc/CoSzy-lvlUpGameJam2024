using System;
using GameJamPlaceHolderName.Scripts.integration;
using Godot;

namespace GameJamPlaceHolderName.Prefabs;

public partial class UnitMovement : CharacterBody2D
{
	private float _movementSpeed = 200.0f;
	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	private WorkerAuthoring _worker;
	
	private NavigationAgent2D _navigationAgent;

	public Vector2 MovementTarget
	{
		get { return _navigationAgent.TargetPosition; }
		set { _navigationAgent.TargetPosition = value; }
	}

	public override void _Ready()
	{
		base._Ready();

		this.MouseEntered += _onMouseEntered;
		this.MouseExited += _onMouseExited;

		_navigationAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		_worker = GetParent<Node2D>() as WorkerAuthoring;
		
		_navigationAgent.PathDesiredDistance = 4.0f;
		_navigationAgent.TargetDesiredDistance = 2.0f;

		// Make sure to not await during _Ready.
		Callable.From(ActorSetup).CallDeferred();
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		Vector2 velocity = Velocity;
		Vector2 direction = Vector2.Zero;
		velocity.Y += Gravity * (float)delta;

		if (!_navigationAgent.IsNavigationFinished())
		{
			direction = _navigationAgent.GetNextPathPosition() - GlobalPosition;
			direction = direction.Normalized();
		}

		velocity.X = direction.X * _movementSpeed;
		Velocity = velocity;
		MoveAndCollide(Velocity * (float)delta);
	}

	private async void ActorSetup()
	{
		// Wait for the first physics frame so the NavigationServer can sync.
		await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
	}

	private void _onMouseEntered()
	{
		_worker.MouseInside = true;
	}
	
	private void _onMouseExited()
	{
		_worker.MouseInside = false;
	}
}