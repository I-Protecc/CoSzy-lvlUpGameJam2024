using Godot;

namespace GameJamPlaceHolderName.Scripts.integration;

public partial class UnitMovement : CharacterBody2D
{
	private NavigationAgent2D _navigationAgent;
	
	private RayCast2D _directionRay;

	private float _movementSpeed = 200.0f;

	private WorkerAuthoring _worker;

	public Vector2 MovementTarget
	{
		get => _navigationAgent.TargetPosition;
		set => _navigationAgent.TargetPosition = value;
	}

	public override void _Ready()
	{
		base._Ready();

		this.MouseEntered += _onMouseEntered;
		this.MouseExited += _onMouseExited;

		_navigationAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		_worker = GetParent<Node2D>() as WorkerAuthoring;

		_directionRay = GetNode<RayCast2D>("DirectionRay");
		
		_navigationAgent.PathDesiredDistance = 4.0f;
		_navigationAgent.TargetDesiredDistance = 2.0f;

		// Make sure to not await during _Ready.
		Callable.From(ActorSetup).CallDeferred();
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		if (_navigationAgent.IsNavigationFinished())
			return;

		Vector2 currentAgentPosition = GlobalTransform.Origin;
		Vector2 nextPathPosition = _navigationAgent.GetNextPathPosition();

		Vector2 direction = currentAgentPosition.DirectionTo(nextPathPosition);

		_directionRay.TargetPosition = direction / direction.Length() * 80; 

		Velocity = direction * _movementSpeed;
		MoveAndSlide();

		if (!_directionRay.IsColliding()) return;
		
		var collider = _directionRay.GetCollider();
			
		if (collider is not TileMap map) return;
			
		MapManager manager = map as MapManager;
		Vector2 hitPoint = map.ToLocal(_directionRay.GetCollisionPoint());
		Vector2I tilePos = map.LocalToMap(hitPoint);

		manager?.MineTile(tilePos);
	}

	private async void ActorSetup()
	{
		// Wait for the first physics frame so the NavigationServer can sync.
		await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
		//MovementTarget = _movementTargetPosition;
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