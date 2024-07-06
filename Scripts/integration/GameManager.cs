using System;
using Godot;
using Godot.Collections;

namespace GameJamPlaceHolderName.Scripts.integration;

public partial class GameManager : Node
{
    public Node2D SelectedWorker;
    public bool hasWorkerSelected;

    public Node2D SelectedBuilding;
    
    public static GameManager Instance;

    public int Money;
    private Dictionary<String, int> _inventory = new Dictionary<string, int>();

    public int IncomeMultiplier = 1;
    public int PassiveIncome = 0;

    public int CurrentlyWorkingFarms;
    public int FarmProductionValue = 1;

    public int CurrentlyWorkingMines;
    public int MineProductionEfficiency;
    
    public int DaysPassed;

    public Vector2 CorePosition;

    public override void _Ready()
    {
        Instance ??= this;
    }

    public Node2D GetSelectedWorker()
    {
        if (SelectedWorker is null) return null;
        GD.Print("Getting " + SelectedWorker.Name);
        return SelectedWorker;
    }

    public void SetSelectedWorker(Node2D worker)
    {
        SelectedWorker = worker;
        hasWorkerSelected = true;
        GD.Print("SELECTED " + worker.Name);
    }

    public void UnsetSelectedWorker()
    {
        SelectedWorker = null;
        hasWorkerSelected = false;
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

    public void NewCycle()
    {
        CycleIncome();
        GD.Print("stonks " + Money);
        GD.Print("currentlyWorkingFarms " + CurrentlyWorkingFarms);
        
    }

    private int _changeMoney(int moneyChange)
    {
        return Money = Math.Clamp(Money + moneyChange, 0, 9999);
    }

    public int SpendMoney(int moneyLoss)
    {
        return _changeMoney(-moneyLoss);
    }

    public int GainMoney(int moneyGain)
    {
        return _changeMoney(moneyGain);
    }

    public void CycleIncome()
    {
        GainMoney(PassiveIncome); // Join the "definitely not a pyramid scheme" group || fall back to make mistakes early game less punishing
        GainMoney(FarmProductionValue * CurrentlyWorkingFarms * IncomeMultiplier);
        // More Incomes here
    }
    
    private int _changeCurrentlyWorkingFarms(int workingFarms)
    {
        return CurrentlyWorkingFarms = Math.Clamp(CurrentlyWorkingFarms + workingFarms, 0, 9999);
    }

    public int FarmsStartedWorking(int amountThatStartedWorking)
    {
        return _changeCurrentlyWorkingFarms(amountThatStartedWorking);
    }

    public int FarmsStoppedWorking(int amountThatStoppedWorking)
    {
        return _changeCurrentlyWorkingFarms(-amountThatStoppedWorking);
    }
    
    private int _changeCurrentlyWorkingMines(int workingMine)
    {
        return CurrentlyWorkingMines = Math.Clamp(CurrentlyWorkingMines + workingMine, 0, 9999);
    }

    public int MineStartedWorking(int amountThatStartedWorking)
    {
        return _changeCurrentlyWorkingMines(amountThatStartedWorking);
    }

    public int MineStoppedWorking(int amountThatStoppedWorking)
    {
        return _changeCurrentlyWorkingMines(-amountThatStoppedWorking);
    }
}