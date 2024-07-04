using Godot;
using System;
using Godot.Collections;

public partial class GameManager : Node
{
    public Node2D SelectedWorker;
    private Node2D _empty;
    
    public static GameManager Instance;

    public int Money;

    public int DaysPassed;

    public override void _Ready()
    {
        _empty = new Node2D();
        Instance ??= this;
    }

    public Node2D GetSelectedWorker()
    {
        return SelectedWorker;
    }

    public void SetSelectedWorker(Node2D worker)
    {
        SelectedWorker = worker;
        GD.Print("SELECTED");
    }

    public void UnsetSelectedWorker()
    {
        SelectedWorker = _empty;
        GD.Print("UNSELECTED");
    }
}
