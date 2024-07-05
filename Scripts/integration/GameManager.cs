using Godot;
using System;
using Godot.Collections;

public partial class GameManager : Node
{
    public Node2D SelectedWorker;
    private Node2D _empty;
    
    public static GameManager Instance;

    public int Money;

    public int IncomeMultiplier = 1;
    public int PassiveIncome = 3;

    public int CurrentlyWorkingFarms;
    public int FarmProductionValue = 1;

    public int CurrentlyWorkingMines;
    public int MineProductionEfficiency;
    

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

    public void NewCycle()
    {
        CycleIncome();
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
        return _changeCurrentlyWorkingFarms(amountThatStoppedWorking);
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
        return _changeCurrentlyWorkingMines(amountThatStoppedWorking);
    }
    
}
