using System;
using GameJamPlaceHolderName.Prefabs;
using GameJamPlaceHolderName.Scripts.domain;
using Godot;

namespace GameJamPlaceHolderName.Scripts.integration;

public partial class WorkerAuthoring : Node2D
{
    [Export]
    public int Health;
    
    public UnitMovement UnitMovement;

    public Worker Worker { get; private set; }
	
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Worker = new Worker(Health);
        UnitMovement = GetNode<CharacterBody2D>("Body") as UnitMovement; 
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Worker.IsDead) QueueFree();
    }
    
    public void Damage(float damage)
    {
        Worker.Damage(damage);
    }
    
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventMouseButton)
            UnitMovement.MovementTarget = GetGlobalMousePosition();
    }
}