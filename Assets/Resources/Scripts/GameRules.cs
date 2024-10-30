//===================================================================================================
// Autor(es):
// - Matheus Pelegrini Bucater
//===================================================================================================

//===================================================================================================
// Bibliotecas
//===================================================================================================
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRules : MonoBehaviour
{
    public static double moneySpend = 0;
    //===============================================================================================
    // Métodos
    //===============================================================================================
    public static void Buy1Ingridient() {
        GameData.ingredients++;
        GameData.money -= GameData.moneyCostPerIngredient;
        moneySpend += GameData.moneyCostPerIngredient;
    }
    public static void Buy5Ingridients() {
        GameData.ingredients += 5;
        GameData.money -= 5 * GameData.moneyCostPerIngredient;
        moneySpend += 5 * GameData.moneyCostPerIngredient;
    }
    public static void Buy10Ingridients() {
        GameData.ingredients += 10;
        GameData.money -= 10 * GameData.moneyCostPerIngredient;
        moneySpend += 10 * GameData.moneyCostPerIngredient;
    }
    public static void BuyMaxIngridients() {
        int _ingridientsCount = (int)GameData.money / (int)GameData.moneyCostPerIngredient;
        GameData.ingredients += _ingridientsCount;
        GameData.money -= _ingridientsCount * GameData.moneyCostPerIngredient;
        moneySpend += _ingridientsCount * GameData.moneyCostPerIngredient;
    }
    public static void BuyOvenUpgrade() {
        GameData.ovenLevel++;
        GameData.money -= GameData.GetOvenCost();
    }
    public static void BuyCustomerUpgrade() {
        GameData.customerLevel++;
        GameData.money -= GameData.GetCustomerCost();
    }
    public static void BuyPizzaUpgrade() {
        GameData.pizzaLevel++;
        GameData.money -= GameData.GetPizzaCost();
    }
    public static void StartNextDay() {
        GameData.day++;
        SceneManager.LoadScene("pizzaria");
    }
    public static void BakePizza() {
        GameData.money += GameData.GetPizzaUpgrade();
        GameData.ingredients -= (int) GameData.ingredientsCostPerPizza;
    }
    public static BatchSummary BakeBatch(double hours) {
        double _maxPizzaProductionPerHour = Math.Min(
            GameData.GetOvenUpgrade(),
            GameData.GetCustomerUpgrade()
        );
        double _maxPizzaByRemainigIngridients =
            (int) (GameData.ingredients / GameData.ingredientsCostPerPizza);

        int _totalPizzas = Math.Min(
            (int) (hours * _maxPizzaProductionPerHour),
            (int) _maxPizzaByRemainigIngridients
        );

        GameData.money += _totalPizzas * GameData.GetPizzaUpgrade();
        GameData.ingredients -= _totalPizzas * (int) GameData.ingredientsCostPerPizza;

        return new BatchSummary(_totalPizzas,
            _totalPizzas * GameData.ingredientsCostPerPizza,
            _totalPizzas * GameData.GetPizzaUpgrade()
        );
    }

}
public class BatchSummary
{
    int _pizzasMade;
    double _ingridientsSpend;
    double _moneyEarned;

    public BatchSummary(int pizzasMade, double ingridientsSpend, double moneyEarned)
    {
        _pizzasMade = pizzasMade;
        _ingridientsSpend = ingridientsSpend;
        _moneyEarned = moneyEarned;
    }

    public int PizzasMade {
        get => _pizzasMade;
    }
    public double IngridientsSpend {
        get => _ingridientsSpend;
    }
    public double MoneyEarned {
        get => _moneyEarned;
    }
}
