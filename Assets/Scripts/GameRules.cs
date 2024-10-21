//===================================================================================================
// Autor(es):
// - Matheus Pelegrini Bucater
//===================================================================================================

//===================================================================================================
// Bibliotecas
//===================================================================================================
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRules : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
    public void PizzaProduction(double hours) {
        // int maxPizzaCount = (int)(gameData.Ingredients / gameData.IngredientsCostPerPizza);
        // int pizzaCount =  (int)(hours * Math.Min(Upgrade.GetUpgrade(0, gameData.UpgradeLevels[0]), Upgrade.GetUpgrade(1, gameData.UpgradeLevels[1])));
        // int actualPizzaCount = Math.Min(maxPizzaCount, pizzaCount);
        // gameData.Money += actualPizzaCount * Upgrade.GetUpgrade(2, gameData.UpgradeLevels[2]);
        // gameData.Ingredients -= actualPizzaCount * (int)gameData.IngredientsCostPerPizza;
        // gameData.Day++;
    }
}
