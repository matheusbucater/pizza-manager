//===================================================================================================================
// Autor(es):
// - Matheus Pelegrini Bucater
//===================================================================================================================

//===================================================================================================================
// Bibliotecas
//===================================================================================================================
using System;

public class GameData
{
    //===============================================================================================================
    // Declaração de Constantes
    //===============================================================================================================  
    private const double _ovenUpgradeBase        = 0.75;
    private const double _ovenUpgradeRate        = 1.2;
    private const double _ovenCostBase           = 32;
    private const double _ovenCostRate           = 1.01;

    private const double _customerUpgradeBase    = 0.625;
    private const double _customerUpgradeRate    = 1.1;
    private const double _customerCostBase       = 30;
    private const double _customerCostRate       = 1.02;

    private const double _pizzaUpgradeBase       = 12;
    private const double _pizzaUpgradeRate       = 15;
    private const double _pizzaCostBase          = 40;
    private const double _pizzaCostRate          = 1.11;

    //===============================================================================================================
    // Declaração de Variáveis
    //===============================================================================================================  
    public static int    day                     = 0;         // Contador de dias
    public static double money                   = 30.0;      // Quantidade de dinheiro que o player possui
    public static int    ovenLevel               = 0;         // Level do Upgrade de forno
    public static int    customerLevel           = 0;         // Level do Upgrade de cliente
    public static int    pizzaLevel              = 0;         // Level do Upgrade de pizza
    public static int    ingredients             = 0;         // Número de ingredientes que o player possui
    public static double moneyCostPerIngredient  = 2.0;       // Preço do ingrediente
    public static double ingredientsCostPerPizza = 3.0;       // Número ingredientes necessários para fazer uma pizza

    //===============================================================================================================
    // Métodos
    //===============================================================================================================
    public static double GetOvenUpgrade(int offsetLevel = 0) {
        return Math.Round(_ovenUpgradeBase + (_ovenUpgradeRate * (ovenLevel + offsetLevel)), 2);
    }
    public static double GetOvenCost(int offsetLevel = 0) {
        return Math.Round(_ovenCostBase * Math.Pow(_ovenCostRate, ovenLevel + offsetLevel), 2);
    }
    public static double GetCustomerUpgrade(int offsetLevel = 0) {
        return Math.Round(_customerUpgradeBase + ( _customerUpgradeRate * (customerLevel + offsetLevel)), 2);
    }
    public static double GetCustomerCost(int offsetLevel = 0) {
        return Math.Round(_customerCostBase * Math.Pow(_customerCostRate, customerLevel + offsetLevel), 2);
    }
    public static double GetPizzaUpgrade(int offsetLevel = 0) {
        return Math.Round(_pizzaUpgradeBase + (_pizzaUpgradeRate * (pizzaLevel + offsetLevel)), 2);
    }
    public static double GetPizzaCost(int offsetLevel = 0) {
        return Math.Round(_pizzaCostBase * Math.Pow(_pizzaCostRate, pizzaLevel + offsetLevel), 2);
    }
}
