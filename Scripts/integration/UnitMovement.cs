using Godot;

namespace GameJamPlaceHolderName.Prefabs;

public partial class UnitMovement : CharacterBody2D
{
	private NavigationAgent2D _navigationAgent;

	private float _movementSpeed = 200.0f;

	public Vector2 MovementTarget
	{
		get { return _navigationAgent.TargetPosition; }
		set { _navigationAgent.TargetPosition = new Vector2(value.X, 0f); }
	}

	public override void _Ready()
	{
		base._Ready();

		_navigationAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		
		_navigationAgent.PathDesiredDistance = 4.0f;
		_navigationAgent.TargetDesiredDistance = 2.0f;

		// Make sure to not await during _Ready.
		Callable.From(ActorSetup).CallDeferred();
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		if (_navigationAgent.IsNavigationFinished())
		{
			return;
		}

		Vector2 currentAgentPosition = GlobalTransform.Origin;
		Vector2 nextPathPosition = _navigationAgent.GetNextPathPosition();

		Velocity = currentAgentPosition.DirectionTo(nextPathPosition) * _movementSpeed;
		MoveAndSlide();
	}

	private async void ActorSetup()
	{
		// Wait for the first physics frame so the NavigationServer can sync.
		await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
		//MovementTarget = _movementTargetPosition;
	}

	public void Ficken()
	{
		GD.Print("sex");
	}
}