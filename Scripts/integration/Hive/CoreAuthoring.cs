using System.ComponentModel.DataAnnotations;
using GameJamPlaceHolderName.Scripts.domain;
using GameJamPlaceHolderName.Scripts.integration.Definitions;
using Godot;

namespace GameJamPlaceHolderName.Scripts.integration.Hive;

public partial class CoreAuthoring : Node
{
	public CoreDefiniton CoreDefiniton;
	public Core Core { get; private set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Core = new Core(CoreDefiniton.health);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Core.IsDead) QueueFree();
		
		
	}

	public void Damage(float damage)
	{
		Core.Damage(damage);
	}
}