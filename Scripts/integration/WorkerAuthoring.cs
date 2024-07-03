using System;
using GameJamPlaceHolderName.Prefabs;
using GameJamPlaceHolderName.Scripts.domain;
using Godot;

namespace GameJamPlaceHolderName.Scripts.integration;

public partial class WorkerAuthoring : Node2D
{
    [Export]
    public int Health;
    
    public bool MouseInside = false;
    public bool Selected = false;
    
    public UnitMovement UnitMovement;
    private CharacterBody2D _worker;
    private Sprite2D _workerSprite;
    
    public Worker Worker { get; private set; }
	
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Worker = new Worker(Health);
        UnitMovement = GetNode<CharacterBody2D>("Body") as UnitMovement;
        _worker = GetNode<CharacterBody2D>("Body");
        _workerSprite = _worker.GetNode<Sprite2D>("WorkerPlaceHolder");
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
        {
            if (eventMouseButton.ButtonIndex == MouseButton.Left && MouseInside)
            {
                Selected = true;
                ((ShaderMaterial)_workerSprite.Material).SetShaderParameter("outlined", true);
            }
            else if (eventMouseButton.ButtonIndex == MouseButton.Left && !MouseInside)
            {
                Selected = false;
                ((ShaderMaterial)_workerSprite.Material).SetShaderParameter("outlined", false);
            }
            
            if(eventMouseButton.ButtonIndex == MouseButton.Right && Selected)
                UnitMovement.MovementTarget = GetGlobalMousePosition();
        }
    }
}