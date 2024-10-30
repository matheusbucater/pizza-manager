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
using System.Collections;

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
    private double  _usedIngridients;
    private double  _moneyEarned;

    private float   _pizzaWait;
    private float   _customerWait;

    private float   _elapsedTime;
    private float   _startTime;
    private float   _speed;

    private Color32 _green;
    private Color32 _blue;
    private Color32 _red;

    private float   _menuMusicBaseVolume = 0.6f;
    private bool    _isMusicOff;

    private bool    _isDayRunning;

    [SerializeField] private TMP_Text    _dayText;
    [SerializeField] private TMP_Text    _hourText;
    [SerializeField] private TMP_Text    _moneyText;
    [SerializeField] private TMP_Text    _ingridientText;
    [SerializeField] private TMP_Text    _pizzaText;
    [SerializeField] private TMP_Text    _usedIngridientsText;
    [SerializeField] private TMP_Text    _pizzasSoldText;
    [SerializeField] private TMP_Text    _moneySpendText;
    [SerializeField] private TMP_Text    _moneyEarnedText;
    [SerializeField] private TMP_Text    _profitText;
    [SerializeField] private Button      _speed1X;
    [SerializeField] private Button      _speed2X;
    [SerializeField] private Button      _speed5X;
    [SerializeField] private AudioSource _pizzaSoldSound;
    [SerializeField] private AudioSource _velocityChangeSound;
    [SerializeField] private AudioSource _toggleMusicSound;
    [SerializeField] private AudioSource _finishDaySound;
    [SerializeField] private AudioSource _menuMusic;
    [SerializeField] private Image       _iconMusicOn;
    [SerializeField] private Image       _iconMusicOff;
    [SerializeField] private Canvas      _summaryCanvas;
    [SerializeField] private GameObject  _customerPrefab;
                     private GameObject  _activeCustomer;
                     private Animator    _animator;

    //===============================================================================================
    // Métodos
    //===============================================================================================
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

        _speed = 1;
        Time.timeScale = _baseSpeed;
        _startTime = Time.time;

        _blue = new Color32(26, 0, 132, 255);
        _green = new Color32(27, 141, 0, 255);
        _red = new Color32(255, 88, 88, 255);

        _pizzaWait = (float) (60 / GameData.GetOvenUpgrade());
        _customerWait = (float) (60 / GameData.GetCustomerUpgrade());

        _isDayRunning = true;
        _summaryCanvas.enabled = false;

        _soldPizzas = 0;
        _usedIngridients = 0;
        _moneyEarned = 0;

        StartCoroutine(SpawnCustomer());
    }
    void Update()
    {
        if (_isDayRunning) {
            _elapsedTime = Math.Abs(Time.time - _startTime);
            if (_elapsedTime / 60 > 8 || GameData.ingredients < GameData.ingredientsCostPerPizza) {
                // SceneManager.LoadScene("UI Scene");
                _isDayRunning = false;
            }

            if (_pizzaWait >= 0)
                _pizzaWait -= Time.deltaTime;
            if (_customerWait >= 0)
                _customerWait -= Time.deltaTime;


            if (_activeCustomer) {
                if (_customerWait >= 10 / GameData.GetCustomerUpgrade() && _customerWait <= 30 / GameData.GetCustomerUpgrade())
                    _animator.SetFloat("speed", 0);
                else
                    _animator.SetFloat("speed", 0.1f); 
            }

            if (_pizzaWait <= 0 && _customerWait <= 0) {
                StartCoroutine(DestroyCustomer());
                GameRules.BakePizza();
                _soldPizzas++;
                _usedIngridients += GameData.ingredientsCostPerPizza;
                _moneyEarned += GameData.GetPizzaUpgrade();
                PlaySound();
                StartCoroutine(SpawnCustomer());
                _pizzaWait = (float) (60 / GameData.GetOvenUpgrade());
                _customerWait = (float) (60 / GameData.GetCustomerUpgrade());
            }

            Time.timeScale = _speed * _baseSpeed;
            DrawHUD();
        }
        else {
            _summaryCanvas.enabled = true;
            DrawSummary();
        }
    }

    void OnDisable() {
        GameRules.moneySpend = 0;
    }

    private void DrawHUD() {
        DrawText();
        DrawButton();
    }
    private void DrawSummary() {
        double _profit = Math.Round(_moneyEarned - GameRules.moneySpend);
        _usedIngridientsText.text = (int)_usedIngridients + " Ingridientes usados";
        _pizzasSoldText.text = _soldPizzas + " Pizzas vendidas";
        _moneySpendText.text = GameRules.moneySpend + " $ gastos";
        _moneyEarnedText.text = Math.Round(_moneyEarned, 2) + " $ recebidos";

        _profitText.color = (_profit > 0) ? _green : _red;
        _profitText.text = Math.Round(_moneyEarned - GameRules.moneySpend, 2) + " $";
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

    private IEnumerator SpawnCustomer() {
        yield return new WaitForSeconds((float) (10 / GameData.GetCustomerUpgrade()));
        _activeCustomer = Instantiate(_customerPrefab) as GameObject;
        _animator = _activeCustomer.GetComponent<Animator>();
        _animator.SetFloat("speed", 0.1f);
    }
    private IEnumerator DestroyCustomer() {
        yield return new WaitForSeconds((float) (5 / GameData.GetCustomerUpgrade()));
        Destroy(_activeCustomer);
    }

    private void PlaySound() {
        _pizzaSoldSound.Play();
    }

    public void ClickSpeed1X() {
        _velocityChangeSound.Play();
        _speed = 1;
    }
    public void ClickSpeed2X() {
        _velocityChangeSound.Play();
        _speed = 2;
    }
    public void ClickSpeed5X() {
        _velocityChangeSound.Play();
        _speed = 5;
    }
    public void ClickSkip() {
        _velocityChangeSound.Play();
        BatchSummary _summary = GameRules.BakeBatch(8 - (_elapsedTime / 60));
        _soldPizzas += _summary.PizzasMade;
        _usedIngridients += _summary.IngridientsSpend;
        _moneyEarned += _summary.MoneyEarned;
        _isDayRunning = false;
    }
    public void ClickToggleMusic() {
        _isMusicOff = !_isMusicOff;
        _menuMusic.volume = (_isMusicOff ? 0 : 1) * _menuMusicBaseVolume;
        UpdateMusicIcon();
        Save();
        _toggleMusicSound.Play();
    }
    public void ClickContinue() {
        _finishDaySound.Play();
        StartCoroutine(WaitForFinishDaySound());
    }

    private void UpdateMusicIcon() {
        _iconMusicOn.enabled = !_isMusicOff;
        _iconMusicOff.enabled = _isMusicOff;
    }

    private IEnumerator WaitForFinishDaySound() {
        while (_finishDaySound.isPlaying) {
            yield return null;
        }
        SceneManager.LoadScene("UI Scene");
    }
}
