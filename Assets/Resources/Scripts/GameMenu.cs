using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameMenu : MonoBehaviour
{
    private IEnumerator _typingCoroutine;

    private float  _menuMusicBaseVolume = 1f;
    private float  _typingDelay = 0.01f;
    private string _fullText;
    private bool   _isMusicOff;
    private bool   _isTypingDone;

    [SerializeField] private AudioSource _menuMusic;
    [SerializeField] private AudioSource _toggleMusicSound;
    [SerializeField] private AudioSource _playSound;
    [SerializeField] private AudioSource _okSound;
    [SerializeField] private Button      _okButton;
    [SerializeField] private Button      _typeAll;
    [SerializeField] private Image       _iconMusicOn;
    [SerializeField] private Image       _iconMusicOff;
    [SerializeField] private TMP_Text    _introText;
    [SerializeField] private TMP_Text    _skipTypingText;
    [SerializeField] private Canvas      _introCanvas;

    private void Load() {   
        _isMusicOff = PlayerPrefs.GetInt("isMusicOff") == 1;
    }

    private void Save() {
        PlayerPrefs.SetInt("isMusicOff", _isMusicOff ? 1 : 0);
    }

    void Start()
    {
        _introCanvas.enabled = false;
        _fullText = _introText.text;
        _introText.text = string.Empty;
        _okButton.gameObject.SetActive(false);
        _skipTypingText.enabled = true;

        _typingCoroutine = TypeText();

        if (!PlayerPrefs.HasKey("isMusicOff")) {
            PlayerPrefs.SetInt("isMusicOff", 0);
        }
        Load();
        UpdateMusicIcon();
        _menuMusic.volume = (_isMusicOff ? 0 : 1) * _menuMusicBaseVolume;
    }

    public void ClickPlay() {
        _playSound.Play();
        _menuMusic.volume = 0.3f * _menuMusicBaseVolume;
        _introCanvas.enabled = true;
        StartCoroutine(_typingCoroutine);
    }
    public void ClickToggleMusic() {
        _isMusicOff = !_isMusicOff;
        _menuMusic.volume = (_isMusicOff ? 0 : 1) * _menuMusicBaseVolume;
        UpdateMusicIcon();
        Save();
        _toggleMusicSound.Play();
    }
    public void ClickTypeAll() {
        _typeAll.interactable = false;
        StopCoroutine(_typingCoroutine);
        _introText.text = _fullText;
        _skipTypingText.enabled = false;
        _okButton.gameObject.SetActive(true);
    }
    public void ClickOk() {
        _okSound.Play();
        StartCoroutine(WaitForOkSound());
    }

    private void UpdateMusicIcon() {
        _iconMusicOn.enabled = !_isMusicOff;
        _iconMusicOff.enabled = _isMusicOff;
    }

    private IEnumerator WaitForOkSound() {
        while (_okSound.isPlaying) {
            yield return null;
        }
        SceneManager.LoadScene("UI Scene");
    }

    private IEnumerator TypeText() {
        foreach (char letter in _fullText) {
            _introText.text += letter;
            yield return new WaitForSeconds(_typingDelay);
        }
        _skipTypingText.enabled = false;
        _okButton.gameObject.SetActive(true);
    }
}
