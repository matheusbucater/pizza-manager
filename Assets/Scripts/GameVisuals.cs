//===================================================================================================
// Autor(es):
// - Matheus Pelegrini Bucater
//===================================================================================================

//===================================================================================================
// Bibliotecas
//===================================================================================================
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameVisuals : MonoBehaviour
{
    //===============================================================================================
    // Declaração de Constantes
    //===============================================================================================
    private const float _baseSpeed = 10f;

    //===============================================================================================
    // Declaração de Variáveis
    //===============================================================================================
    private int     _soldPizzas;
    private float   _pizzaWait;
    private float   _customerWait;
    private float   _elapsedTime;
    private float   _startTime;
    private float   _speed;
    private Color32 _green;
    private Color32 _blue;

    [SerializeField] private TMP_Text _dayText;
    [SerializeField] private TMP_Text _hourText;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _ingridientText;
    [SerializeField] private TMP_Text _pizzaText;
    [SerializeField] private Button   _speed1X;
    [SerializeField] private Button   _speed2X;
    [SerializeField] private Button   _speed5X;

    //===============================================================================================
    // Métodos
    //===============================================================================================
    void Start()
    {
        _speed = 1;
        Time.timeScale = _baseSpeed;
        _startTime = Time.time;

        _blue = new Color32(26, 0, 132, 255);
        _green = new Color32(27, 141, 0, 255);

        _pizzaWait = (float) (60 / GameData.GetOvenUpgrade());
        _customerWait = (float) (60 / GameData.GetCustomerUpgrade());

        _soldPizzas = 0;
    }
    void Update()
    {
        _elapsedTime = Math.Abs(Time.time - _startTime);
        if (_elapsedTime / 60 > 8 || GameData.ingredients < GameData.ingredientsCostPerPizza) {
            SceneManager.LoadScene("UI Scene");
        }

        if (_pizzaWait >= 0)
            _pizzaWait -= Time.deltaTime;
        if (_customerWait >= 0)
            _customerWait -= Time.deltaTime;

        if (_pizzaWait <= 0 && _customerWait <= 0) {
            GameRules.BakePizza();
            _soldPizzas++;
            _pizzaWait = (float) (60 / GameData.GetOvenUpgrade());
            _customerWait = (float) (60 / GameData.GetCustomerUpgrade());
        }

        Time.timeScale = _speed * _baseSpeed;
        DrawHUD();
    }

    private void DrawHUD() {
        DrawText();
        DrawButton();
    }

    private void DrawText() {
        TimeSpan _timeInGame = TimeSpan.FromHours(8);
        _timeInGame += TimeSpan.FromMinutes(_elapsedTime);

        _dayText.text = "Dia " + GameData.day;
        _hourText.text = _timeInGame.ToString(@"hh\:mm");
        _moneyText.text = Math.Round(GameData.money, 2) + " $";
        _ingridientText.text = "- " + GameData.ingredients + " Ingredientes restantes";
        _pizzaText.text = "- " + _soldPizzas + " pizzas vendidas";
    }
    private void DrawButton() {
        switch (_speed)
        {
            case 1:
                _speed1X.image.color = _blue;
                _speed2X.image.color = _green;
                _speed5X.image.color = _green;
            break;
            case 2:
                _speed1X.image.color = _green;
                _speed2X.image.color = _blue;
                _speed5X.image.color = _green;
            break;
            case 5:
                _speed1X.image.color = _green;
                _speed2X.image.color = _green;
                _speed5X.image.color = _blue;
            break;
        }
    }

    public void ClickSpeed1X() {
        _speed = 1;
    }
    public void ClickSpeed2X() {
        _speed = 2;
    }
    public void ClickSpeed5X() {
        _speed = 5;
    }
    public void ClickSkip() {
        GameRules.BakeBatch(8 - (_elapsedTime / 60));
        SceneManager.LoadScene("UI Scene");
    }
}
