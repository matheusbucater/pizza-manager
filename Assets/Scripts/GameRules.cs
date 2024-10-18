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
    public void PizzaProduction(double hours) {
        int maxPizzaCount = (int)(gameData.Ingredients / gameData.IngredientsCostPerPizza);
        int pizzaCount =  (int)(hours * Math.Min(Upgrade.GetUpgrade(0, gameData.UpgradeLevels[0]), Upgrade.GetUpgrade(1, gameData.UpgradeLevels[1])));
        int actualPizzaCount = Math.Min(maxPizzaCount, pizzaCount);
        gameData.Money += actualPizzaCount * Upgrade.GetUpgrade(2, gameData.UpgradeLevels[2]);
        gameData.Ingredients -= actualPizzaCount * (int)gameData.IngredientsCostPerPizza;
        gameData.Day++;
    }
}
