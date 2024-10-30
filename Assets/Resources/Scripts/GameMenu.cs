using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    private float _menuMusicBaseVolume = 1f;
    private bool  _isMusicOff;

    [SerializeField] private AudioSource _menuMusic;
    [SerializeField] private AudioSource _toggleMusicSound;
    [SerializeField] private Image       _iconMusicOn;
    [SerializeField] private Image       _iconMusicOff;

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
        
    }

    public void ClickPlay() {
        SceneManager.LoadScene("UI Scene");
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
}
