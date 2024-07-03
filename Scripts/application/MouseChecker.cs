using Godot;
using System;

public partial class MouseChecker : Area2D
{
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
}
