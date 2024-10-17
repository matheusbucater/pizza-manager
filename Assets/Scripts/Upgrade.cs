//===================================================================================================
// Nome: Matheus Pelegrini Bucater
// Data: 17/10/2024
//===================================================================================================

//===================================================================================================
// Bibliotecas
//===================================================================================================
using System.Collections.Generic;

public class Upgrade
{
    //===============================================================================================
    // Declaração de Variáveis
    //===============================================================================================
    private static readonly double[] _ovenUpgrades = {0, };
    private static readonly double[] _ovenUpgradesCost = {0, };
    private static readonly double[] _customerUpgrades = {0, };
    private static readonly double[] _customerUpgradesCost = {0, };
    private static readonly double[] _pizzaUpgrades = {0, };
    private static readonly double[] _pizzaUpgradesCost = {0, };

    private static readonly Dictionary<int,double[]> _upgrades = new() {
        [0] = _ovenUpgrades,
        [1] = _customerUpgrades,
        [2] = _pizzaUpgrades
    };
    private static readonly Dictionary<int,double[]> _upgradesCost = new() {
        [0] = _ovenUpgradesCost,
        [1] = _customerUpgradesCost,
        [2] = _pizzaUpgradesCost
    };

    //===============================================================================================
    // Métodos
    //===============================================================================================
    public int GetMaxNumOfUpgrades(int upgradeIndex) {
        return _upgrades[upgradeIndex].Length - 1;
    }
    public bool IsUpgradeMax(int upgradeIndex, int levelIndex) {
        return levelIndex == GetMaxNumOfUpgrades(upgradeIndex);
    }
    public double GetUpgradeStat(int upgradeIndex, int levelIndex) {
        return _upgrades[upgradeIndex][levelIndex];
    }
    public double GetUpgradeCost(int upgradeIndex, int levelIndex) {
        return _upgradesCost[upgradeIndex][levelIndex];
    }

}
