//===================================================================================================
// Nome: Matheus Pelegrini Bucater
// Data: 18/10/2024
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
    // Declaração de Variáveis
    //===============================================================================================
    public static GameRules istance;
    public GameData gameData;
    
    private void Awake() {
        istance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameData = new(0, 30.0, new int[]{0,0,0}, 0, 2.0, 3.0);

    }

    // Update is called once per frame
    void Update()
    {

    }

    //===============================================================================================
    // Métodos
    //===============================================================================================
    public void Buy1Ingridient() {
        gameData.Ingredients++;
        gameData.Money -= gameData.MoneyCostPerIngredient;
    }
    public void Buy5Ingridients() {
        gameData.Ingredients += 5;
        gameData.Money -= 5 * gameData.MoneyCostPerIngredient;
    }
    public void Buy10Ingridients() {
        gameData.Ingredients += 10;
        gameData.Money -= 10 * gameData.MoneyCostPerIngredient;
    }
    public void BuyMaxIngridients() {
        int ingridientsCount = (int)gameData.Money / (int)gameData.MoneyCostPerIngredient;
        gameData.Ingredients += ingridientsCount;
        gameData.Money -= ingridientsCount * gameData.MoneyCostPerIngredient;
    }
    public void BuyOvenUpgrade() {
        gameData.UpgradeLevels[0]++;
        gameData.Money -= Upgrade.GetCost(0, gameData.UpgradeLevels[0]);
    }
    public void BuyCustomerUpgrade() {
        gameData.UpgradeLevels[1]++;
        gameData.Money -= Upgrade.GetCost(1, gameData.UpgradeLevels[1]);
    }
    public void BuyPizzaUpgrade() {
        gameData.UpgradeLevels[2]++;
        gameData.Money -= Upgrade.GetCost(2, gameData.UpgradeLevels[2]);
    }
    public void PizzaProduction(double hours) {
        int maxPizzaCount = (int)(gameData.Ingredients / gameData.IngredientsCostPerPizza);
        int pizzaCount =  (int)(hours * Math.Min(Upgrade.GetUpgrade(0, gameData.UpgradeLevels[0]), Upgrade.GetUpgrade(1, gameData.UpgradeLevels[1])));
        int actualPizzaCount = Math.Min(maxPizzaCount, pizzaCount);
        gameData.Money += actualPizzaCount * Upgrade.GetUpgrade(2, gameData.UpgradeLevels[2]);
        gameData.Ingredients -= actualPizzaCount * (int)gameData.IngredientsCostPerPizza;
        gameData.Day++;
    }
}
