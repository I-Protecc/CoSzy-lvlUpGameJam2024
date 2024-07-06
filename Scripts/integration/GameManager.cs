using System;
using Godot;
using Godot.Collections;

namespace GameJamPlaceHolderName.Scripts.integration;

public partial class GameManager : Node
{
    public Node2D SelectedWorker;
    private Node2D _empty;
    
    public static GameManager Instance;

    public int Money;
    private Dictionary<String, int> _inventory = new Dictionary<string, int>();

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
    
    public void AddToInventory(string itemType, int amount)
    {
        if (_inventory.ContainsKey(itemType))
        {
            _inventory[itemType] += amount;
            GD.Print("Added " + amount + itemType + " to inventory");
        }
        else
            _inventory.Add(itemType, amount);
    }

    public void RemoveFromInventory(string itemType, int amount)
    {
        if (_inventory.ContainsKey(itemType))
        {
            _inventory[itemType] -= amount;
            _inventory[itemType] = Mathf.Clamp(_inventory[itemType], 0, 9999);
        }
    }

    public int GetAmountInInventory(string itemType)
    {
        return _inventory[itemType];
    }
}