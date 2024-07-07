using Godot;
using System;
using GameJamPlaceHolderName.Scripts.integration;

public partial class UILabel : Label
{
    public override void _Process(double delta)
    {
        Text = ("Money : " + GameManager.Instance.Money + "  Iron Ore: " + GameManager.Instance.GetAmountInInventory("Iron") + "Days passed: " + GameManager.Instance.DaysPassed);
    }
}
