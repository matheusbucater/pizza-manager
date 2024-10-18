//===================================================================================================
// Nome: Matheus Pelegrini Bucater
// Data: 17/10/2024
//===================================================================================================

//===================================================================================================
// Bibliotecas
//===================================================================================================
using System;
using System.Collections.Generic;

public static class Upgrade
{
    //===============================================================================================
    // Declaração de Variáveis
    //===============================================================================================


    //===============================================================================================
    // Métodos
    //===============================================================================================
    private static double OvenUpgrade(int levelIndex) {
        return Math.Round(3.75 * (levelIndex + 1), 2);
    }
    private static double OvenCost(int levelIndex) {
        return Math.Round(0.75 * Math.Pow(1.75, levelIndex), 2);
    }
    private static double CustomerUpgrade(int levelIndex) {
        return Math.Round(2.5 * (levelIndex + 1), 2);
    }
    private static double CustomerCost(int levelIndex) {
        return Math.Round(0.5 * Math.Pow(1.5, levelIndex), 2);
    }
    private static double PizzaUpgrade(int levelIndex) {
        return Math.Round(9 * Math.Pow(1.07,levelIndex), 2);
    }
    private static double PizzaCost(int levelIndex) {
        return Math.Round(18 * Math.Pow(1.07,levelIndex), 2);
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
