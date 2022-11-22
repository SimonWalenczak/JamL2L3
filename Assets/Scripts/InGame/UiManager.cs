using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject _pausedMenu;
    [SerializeField] private GameObject _xpPanel;
    [SerializeField] private Image _lifeBar;
    [SerializeField] private TextMeshProUGUI _killText;
    [SerializeField] private TextMeshProUGUI _coolDownUltText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private int levelXpBar = 0;

    private bool _isPaused;
    private void Update()
    {
        //_lifeBar.fillAmount = GameData.
        _coolDownUltText.SetText("88");
        _killText.SetText(GameData._kill.ToString());
        _scoreText.SetText("Score : " + GameData._score.ToString());

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPaused)
            {
                Time.timeScale = 0;
                _isPaused = true;
            }
            else
            {
                Time.timeScale = 1;
                _isPaused = false;
            }

            
            _xpPanel.SetActive(!_xpPanel.activeSelf);
            _pausedMenu.SetActive(!_pausedMenu.activeSelf);
        }
    }
}
