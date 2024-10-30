//===================================================================================================
// Autor(es):
// - Matheus Pelegrini Bucater
//===================================================================================================

//===================================================================================================
// Bibliotecas
//===================================================================================================
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    //===============================================================================================
    // Declaração de Variáveis
    //===============================================================================================
    [SerializeField] private TMP_Text     _dayText;
    [SerializeField] private TMP_Text     _moneyText;
    [SerializeField] private TMP_Text     _ingridientText;
    [SerializeField] private TMP_Text     _ingridientPerPizzaText;
    [SerializeField] private TMP_Text     _ingridientCostText;
    [SerializeField] private TMP_Text     _ovenLevelText;
    [SerializeField] private TMP_Text     _ovenUpgradeText;
    [SerializeField] private TMP_Text     _ovenCostText;
    [SerializeField] private TMP_Text     _ovenNextUpgradeText;
    [SerializeField] private TMP_Text     _customerLevelText;
    [SerializeField] private TMP_Text     _customerUpgradeText;
    [SerializeField] private TMP_Text     _customerCostText;
    [SerializeField] private TMP_Text     _customerNextUpgradeText;
    [SerializeField] private TMP_Text     _pizzaLevelText;
    [SerializeField] private TMP_Text     _pizzaUpgradeText;
    [SerializeField] private TMP_Text     _pizzaCostText;
    [SerializeField] private TMP_Text     _pizzaNextUpgradeText;
    [SerializeField] private TMP_Text     _nextDayText;

    [SerializeField] private Button       _plus1IngridientButton;
    [SerializeField] private Button       _plus5IngridientButton;
    [SerializeField] private Button       _plus10IngridientButton;
    [SerializeField] private Button       _maxIngridientButton;
    [SerializeField] private Button       _ovenButton;
    [SerializeField] private Button       _customerButton;
    [SerializeField] private Button       _pizzaButton;

    [SerializeField] private AudioSource _buyIngridientSound;
    [SerializeField] private AudioSource _buyUpgradeSound;
    [SerializeField] private AudioSource _startNextDaySound;
    [SerializeField] private AudioSource _toggleMusicSound;
    [SerializeField] private AudioSource _menuMusic;

    [SerializeField] private Image       _iconMusicOn;
    [SerializeField] private Image       _iconMusicOff;

                     private float       _menuMusicBaseVolume = 0.3f;
                     private bool        _isMusicOff;

    private void Load() {   
        _isMusicOff = PlayerPrefs.GetInt("isMusicOff") == 1;
    }

    private void Save() {
        PlayerPrefs.SetInt("isMusicOff", _isMusicOff ? 1 : 0);
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("isMusicOff")) {
            PlayerPrefs.SetInt("isMusicOff", 0);
        }
        Load();
        UpdateMusicIcon();
        _menuMusic.volume = (_isMusicOff ? 0 : 1) * _menuMusicBaseVolume;
    }

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
        _dayText.text = "Dia " + GameData.day;
        _moneyText.text = Math.Round(GameData.money, 2) + " $";

        _ingridientText.text = "Você possui " + GameData.ingredients + " Ingrediente(s)";
        _ingridientPerPizzaText.text = "* cada pizza leva " + GameData.ingredientsCostPerPizza + " ingrediente(s) para ser feita";

        _ingridientCostText.text = "Custo: " + GameData.moneyCostPerIngredient + " $/Ingrediente";

        _ovenLevelText.text = "Level: " + GameData.ovenLevel;
        _ovenUpgradeText.text = "- " + GameData.GetOvenUpgrade() + " pizzas/hora";
        _ovenCostText.text = GameData.GetOvenCost(1) + " $";
        _ovenNextUpgradeText.text = "(" + GameData.GetOvenUpgrade(1) + " pizzas/hora)";

        _customerLevelText.text = "Level: " + GameData.customerLevel;
        _customerUpgradeText.text = "- " + GameData.GetCustomerUpgrade() + " clientes/hora";
        _customerCostText.text = GameData.GetCustomerCost(1) + " $";
        _customerNextUpgradeText.text = "(" + GameData.GetCustomerUpgrade(1) + " clientes/hora)";

        _pizzaLevelText.text = "Level: " + GameData.pizzaLevel;
        _pizzaUpgradeText.text = "- " + GameData.GetPizzaUpgrade() + " $/pizza";
        _pizzaCostText.text = GameData.GetPizzaCost(1) + " $";
        _pizzaNextUpgradeText.text = "(" + GameData.GetPizzaUpgrade(1) + " $/pizza)";

        _nextDayText.text = "Começar Dia " + (GameData.day + 1);
    }

    private void DrawButton() {
        _plus1IngridientButton.interactable = GameData.money >= GameData.moneyCostPerIngredient;
        _plus5IngridientButton.interactable = GameData.money >= 5 * GameData.moneyCostPerIngredient;
        _plus10IngridientButton.interactable = GameData.money >= 10 * GameData.moneyCostPerIngredient;
        _maxIngridientButton.interactable = GameData.money >= GameData.moneyCostPerIngredient;

        _ovenButton.interactable = GameData.money >= GameData.GetOvenCost(1);
        _customerButton.interactable = GameData.money >= GameData.GetCustomerCost(1);
        _pizzaButton.interactable = GameData.money >= GameData.GetPizzaCost(1);
    }

    public void ClickBuy1Ingridient() {
        GameRules.Buy1Ingridient();
        _buyIngridientSound.Play();
    }
    public void ClickBuy5Ingridients() {
        GameRules.Buy5Ingridients();
        _buyIngridientSound.Play();
    }
    public void ClickBuy10Ingridients() {
        GameRules.Buy10Ingridients();
        _buyIngridientSound.Play();
    }
    public void ClickBuyMaxIngridients() {
        GameRules.BuyMaxIngridients();
        _buyIngridientSound.Play();
    }
    public void ClickBuyOvenUpgrade() {
        GameRules.BuyOvenUpgrade();
        _buyUpgradeSound.Play();
    }
    public void ClickBuyCustomerUpgrade() {
        GameRules.BuyCustomerUpgrade();
        _buyUpgradeSound.Play();
    }
    public void ClickBuyPizzaUpgrade() {
        GameRules.BuyPizzaUpgrade();
        _buyUpgradeSound.Play();
    }
    public void ClickStartNextDay() {
        _startNextDaySound.Play();
        StartCoroutine(WaitForStartNextDaySound());
    }
    public void ClickToggleMusic() {
        _isMusicOff = !_isMusicOff;
        _menuMusic.volume = (_isMusicOff ? 0 : 1) * _menuMusicBaseVolume;
        UpdateMusicIcon();
        Save();
        _toggleMusicSound.Play();
    }

    private void UpdateMusicIcon() {
        _iconMusicOn.enabled = !_isMusicOff;
        _iconMusicOff.enabled = _isMusicOff;
    }

    private IEnumerator WaitForStartNextDaySound() {
        while (_startNextDaySound.isPlaying) {
            yield return null;
        }
        GameRules.StartNextDay();
    }
}
