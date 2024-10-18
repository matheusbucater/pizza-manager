//===================================================================================================
// Nome: Matheus Pelegrini Bucater
// Data: 18/10/2024
//===================================================================================================

//===================================================================================================
// Bibliotecas
//===================================================================================================
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    //===============================================================================================
    // Declaração de Variáveis
    //===============================================================================================
    [SerializeField] private TMP_Text _dayText;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _ingridientText;
    [SerializeField] private TMP_Text _ingridientPerPizzaText;
    [SerializeField] private TMP_Text _ingridientCostText;
    [SerializeField] private TMP_Text _ovenLevelText;
    [SerializeField] private TMP_Text _ovenUpgradeText;
    [SerializeField] private TMP_Text _ovenCostText;
    [SerializeField] private TMP_Text _ovenNextUpgradeText;
    [SerializeField] private TMP_Text _customerLevelText;
    [SerializeField] private TMP_Text _customerUpgradeText;
    [SerializeField] private TMP_Text _customerCostText;
    [SerializeField] private TMP_Text _customerNextUpgradeText;
    [SerializeField] private TMP_Text _pizzaLevelText;
    [SerializeField] private TMP_Text _pizzaUpgradeText;
    [SerializeField] private TMP_Text _pizzaCostText;
    [SerializeField] private TMP_Text _pizzaNextUpgradeText;
    [SerializeField] private TMP_Text _nextDayText;

    [SerializeField] private Button _plus1IngridientButton;
    [SerializeField] private Button _plus5IngridientButton;
    [SerializeField] private Button _plus10IngridientButton;
    [SerializeField] private Button _maxIngridientButton;
    [SerializeField] private Button _ovenButton;
    [SerializeField] private Button _customerButton;
    [SerializeField] private Button _pizzaButton;


    // Update is called once per frame
    void Update()
    {
        DrawText();
        DrawButton();
    }

    //===============================================================================================
    // Métodos
    //===============================================================================================
    private void DrawText() {
        _dayText.text = "Dia " + GameRules.istance.gameData.Day;
        _moneyText.text = Math.Round(GameRules.istance.gameData.Money, 2) + " $";

        _ingridientText.text = "Você possui " + GameRules.istance.gameData.Ingredients + " Ingrediente(s)"; 
        _ingridientPerPizzaText.text = "* cada pizza leva " + GameRules.istance.gameData.IngredientsCostPerPizza + " ingrediente(s) para ser feita";

        _ingridientCostText.text = "Custo: " + GameRules.istance.gameData.MoneyCostPerIngredient + " $/Ingrediente";

        _ovenLevelText.text = "Level: " + GameRules.istance.gameData.UpgradeLevels[0];
        _ovenUpgradeText.text = "- " + Upgrade.GetUpgrade(0, GameRules.istance.gameData.UpgradeLevels[0]) + " pizzas/hora";
        _ovenCostText.text = Upgrade.GetCost(0, GameRules.istance.gameData.UpgradeLevels[0] + 1) +  " $";
        _ovenNextUpgradeText.text = "(" + Upgrade.GetUpgrade(0, GameRules.istance.gameData.UpgradeLevels[0] + 1) + " pizzas/hora)";

        _customerLevelText.text = "Level: " + GameRules.istance.gameData.UpgradeLevels[1];
        _customerUpgradeText.text = "- " + Upgrade.GetUpgrade(1, GameRules.istance.gameData.UpgradeLevels[1]) + " clientes/hora";
        _customerCostText.text = Upgrade.GetCost(1, GameRules.istance.gameData.UpgradeLevels[1] + 1) +  " $";
        _customerNextUpgradeText.text = "(" + Upgrade.GetUpgrade(1, GameRules.istance.gameData.UpgradeLevels[1] + 1) + " clientes/hora)";

        _pizzaLevelText.text = "Level: " + GameRules.istance.gameData.UpgradeLevels[2];
        _pizzaUpgradeText.text = "- " + Upgrade.GetUpgrade(2, GameRules.istance.gameData.UpgradeLevels[2]) + " $/pizza";
        _pizzaCostText.text = Upgrade.GetCost(2, GameRules.istance.gameData.UpgradeLevels[2] + 1) +  " $";
        _pizzaNextUpgradeText.text = "(" + Upgrade.GetUpgrade(2, GameRules.istance.gameData.UpgradeLevels[2] + 1) + " $/pizza)";

        _nextDayText.text = "Começar Dia " + (GameRules.istance.gameData.Day + 1);
    }

    private void DrawButton() {
        _plus1IngridientButton.interactable = GameRules.istance.gameData.Money >= GameRules.istance.gameData.MoneyCostPerIngredient;
        _plus5IngridientButton.interactable = GameRules.istance.gameData.Money >= 5 * GameRules.istance.gameData.MoneyCostPerIngredient;
        _plus10IngridientButton.interactable = GameRules.istance.gameData.Money >= 10 * GameRules.istance.gameData.MoneyCostPerIngredient;
        _maxIngridientButton.interactable = GameRules.istance.gameData.Money >= GameRules.istance.gameData.MoneyCostPerIngredient;

        _ovenButton.interactable = GameRules.istance.gameData.Money >= Upgrade.GetCost(0, GameRules.istance.gameData.UpgradeLevels[0] + 1);
        _customerButton.interactable = GameRules.istance.gameData.Money >= Upgrade.GetCost(1, GameRules.istance.gameData.UpgradeLevels[1] + 1);
        _pizzaButton.interactable = GameRules.istance.gameData.Money >= Upgrade.GetCost(2, GameRules.istance.gameData.UpgradeLevels[2] + 1);
    }

    public void ClickBuy1Ingridient() {
        GameRules.istance.gameData.Ingredients++;
        GameRules.istance.gameData.Money -= GameRules.istance.gameData.MoneyCostPerIngredient;
    }
    public void ClickBuy5Ingridients() {
        GameRules.istance.gameData.Ingredients += 5;
        GameRules.istance.gameData.Money -= 5 * GameRules.istance.gameData.MoneyCostPerIngredient;
    }
    public void ClickBuy10Ingridients() {
        GameRules.istance.gameData.Ingredients += 10;
        GameRules.istance.gameData.Money -= 10 * GameRules.istance.gameData.MoneyCostPerIngredient;
    }
    public void ClickBuyMaxIngridients() {
        int ingridientsCount = (int)GameRules.istance.gameData.Money / (int)GameRules.istance.gameData.MoneyCostPerIngredient;
        GameRules.istance.gameData.Ingredients += ingridientsCount;
        GameRules.istance.gameData.Money -= ingridientsCount * GameRules.istance.gameData.MoneyCostPerIngredient;
    }
    public void ClickBuyOvenUpgrade() {
        GameRules.istance.gameData.UpgradeLevels[0]++;
        GameRules.istance.gameData.Money -= Upgrade.GetCost(0, GameRules.istance.gameData.UpgradeLevels[0]);
    }
    public void ClickBuyCustomerUpgrade() {
        GameRules.istance.gameData.UpgradeLevels[1]++;
        GameRules.istance.gameData.Money -= Upgrade.GetCost(1, GameRules.istance.gameData.UpgradeLevels[1]);
    }
    public void ClickBuyPizzaUpgrade() {
        GameRules.istance.gameData.UpgradeLevels[2]++;
        GameRules.istance.gameData.Money -= Upgrade.GetCost(2, GameRules.istance.gameData.UpgradeLevels[2]);
    }
    public void ClickStartNextDay() {
        SceneManager.LoadScene("Main Scene");
    }
}
