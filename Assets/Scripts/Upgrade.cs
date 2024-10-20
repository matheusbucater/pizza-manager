//===================================================================================================
// Nome: Matheus Pelegrini Bucater
// Data: 17/10/2024
//===================================================================================================

//===================================================================================================
// Bibliotecas
//===================================================================================================
using System;

public static class Upgrade
{
    //===============================================================================================
    // Declaração de Constantes
    //===============================================================================================
    private const double _ovenUpgradeBase = 0.75;
    private const double _ovenUpgradeRate = 1.5;
    private const double _ovenCostBase = 30;
    private const double _ovenCostRate = 1.02;

    private const double _customerUpgradeBase = 0.625;
    private const double _customerUpgradeRate = 1.1;
    private const double _customerCostBase = 35;
    private const double _customerCostRate = 1.01;

    private const double _pizzaUpgradeBase = 9;
    private const double _pizzaUpgradeRate = 15;
    private const double _pizzaCostBase = 40;
    private const double _pizzaCostRate = 1.11;


    //===============================================================================================
    // Métodos
    //===============================================================================================
    private static double OvenUpgrade(int levelIndex) {
        return Math.Round(_ovenUpgradeBase + (_ovenUpgradeRate * levelIndex), 2);
    }
    private static double OvenCost(int levelIndex) {
        return Math.Round(_ovenCostBase * Math.Pow(_ovenCostRate, levelIndex), 2);
    }
    private static double CustomerUpgrade(int levelIndex) {
        return Math.Round(_customerUpgradeBase + ( _customerUpgradeRate * levelIndex), 2);
    }
    private static double CustomerCost(int levelIndex) {
        return Math.Round(_customerCostBase * Math.Pow(_customerCostRate, levelIndex), 2);
    }
    private static double PizzaUpgrade(int levelIndex) {
        return Math.Round(_pizzaUpgradeBase + (_pizzaUpgradeRate * levelIndex), 2);
    }
    private static double PizzaCost(int levelIndex) {
        return Math.Round(_pizzaCostBase * Math.Pow(_pizzaCostRate, levelIndex), 2);
    }

    public static double GetUpgrade(int upgradeIndex, int levelIndex) {
        return upgradeIndex switch
        {
            0 => OvenUpgrade(levelIndex),
            1 => CustomerUpgrade(levelIndex),
            2 => PizzaUpgrade(levelIndex),
            _ => throw new NotImplementedException(),
        };
    }
    public static double GetCost(int upgradeIndex, int levelIndex) {
        return upgradeIndex switch
        {
            0 => OvenCost(levelIndex),
            1 => CustomerCost(levelIndex),
            2 => PizzaCost(levelIndex),
            _ => throw new NotImplementedException(),
        };
    }
}
