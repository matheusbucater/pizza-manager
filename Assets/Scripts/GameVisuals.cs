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

    private float   _pizzaWait;
    private float   _customerWait;

    private float   _elapsedTime;
    private float   _startTime;
    private float   _speed;

    private Color32 _green;
    private Color32 _blue;

    private float   _menuMusicBaseVolume = 0.6f;
    private bool    _isMusicOff;

    [SerializeField] private TMP_Text    _dayText;
    [SerializeField] private TMP_Text    _hourText;
    [SerializeField] private TMP_Text    _moneyText;
    [SerializeField] private TMP_Text    _ingridientText;
    [SerializeField] private TMP_Text    _pizzaText;
    [SerializeField] private Button      _speed1X;
    [SerializeField] private Button      _speed2X;
    [SerializeField] private Button      _speed5X;
    [SerializeField] private AudioSource _pizzaSoldSound;
    [SerializeField] private AudioSource _velocityChangeSound;
    [SerializeField] private AudioSource _toggleMusicSound;
    [SerializeField] private AudioSource _menuMusic;
    [SerializeField] private Image       _iconMusicOn;
    [SerializeField] private Image       _iconMusicOff;
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

        _pizzaWait = (float) (60 / GameData.GetOvenUpgrade());
        _customerWait = (float) (60 / GameData.GetCustomerUpgrade());

        _soldPizzas = 0;
        StartCoroutine(SpawnCustomer());
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
            PlaySound();
            StartCoroutine(SpawnCustomer());
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
        StartCoroutine(WaitForVelocityChangeSound());
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

    private IEnumerator WaitForVelocityChangeSound() {
        while (_velocityChangeSound.isPlaying) {
            yield return null;
        }
        GameRules.BakeBatch(8 - (_elapsedTime / 60));
        SceneManager.LoadScene("UI Scene");
    }
}
