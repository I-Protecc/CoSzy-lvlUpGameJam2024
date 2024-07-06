using Godot;
using System;

public partial class MouseChecker : Area2D
{
    public bool Selected; // L Click
    public bool Interacted; // R Click
    public bool MouseHovering { get; private set; }
    
    public override void _Ready()
    {
        this.MouseEntered += _onMouseEntered;
        this.MouseExited += _onMouseExited;
    }

    private void _onMouseEntered()
    {
        MouseHovering = true;
    }

    private void _onMouseExited()
    {
        MouseHovering = false;
    }



    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventMouseButton && @event.IsActionReleased("GeneralClick") && !@event.IsEcho())
        {
            if (eventMouseButton.ButtonIndex == MouseButton.Left && MouseHovering)
            {
                Selected = true;
            }
            else if (eventMouseButton.ButtonIndex == MouseButton.Left && !MouseHovering)
            {
                Selected = false;
            }

            if (eventMouseButton.ButtonIndex == MouseButton.Right && MouseHovering)
            {
                Interacted = true;
            }
        }
    }
}
