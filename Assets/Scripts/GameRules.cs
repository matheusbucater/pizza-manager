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
    //===============================================================================================
    // Métodos
    //===============================================================================================
    public static void Buy1Ingridient() {
        GameData.ingredients++;
        GameData.money -= GameData.moneyCostPerIngredient;
    }
    public static void Buy5Ingridients() {
        GameData.ingredients += 5;
        GameData.money -= 5 * GameData.moneyCostPerIngredient;
    }
    public static void Buy10Ingridients() {
        GameData.ingredients += 10;
        GameData.money -= 10 * GameData.moneyCostPerIngredient;
    }
    public static void BuyMaxIngridients() {
        int _ingridientsCount = (int)GameData.money / (int)GameData.moneyCostPerIngredient;
        GameData.ingredients += _ingridientsCount;
        GameData.money -= _ingridientsCount * GameData.moneyCostPerIngredient;
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
        SceneManager.LoadScene("Main Scene");
    }
    public static int UsedIngridients(double hours) {
        return (int) (SoldPizzas(hours) * GameData.ingredientsCostPerPizza);
    }
    public static int SoldPizzas(double hours) {
        double _pizzaProductionPerHour = Math.Min(GameData.GetOvenUpgrade(), GameData.GetCustomerUpgrade());
        return (int) (hours * _pizzaProductionPerHour); 
    }
    public static void DayProduction() {
        int _pizzas = Math.Min(
            (int) (GameData.ingredients / GameData.ingredientsCostPerPizza),
            SoldPizzas(8)
        );
        GameData.money += _pizzas * GameData.GetPizzaUpgrade();
        GameData.ingredients -= _pizzas * (int)GameData.ingredientsCostPerPizza;
    }
}
