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
    [SerializeField] private Image _xpBar;
    [SerializeField] private TextMeshProUGUI _killText;
    [SerializeField] private TextMeshProUGUI _coolDownUltText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private int _currentLevelBar = 0;
    [SerializeField] List<int> levelXpBar = new List<int>();

    private bool _isPaused;
    private void Update()
    {
        _xpBar.fillAmount = (float)GameData._currentXp / (float)levelXpBar[_currentLevelBar];
        _coolDownUltText.SetText("88");
        _killText.SetText(GameData._kill.ToString());
        _scoreText.SetText("Score : " + GameData._score.ToString());

        #region Debug
        if (Input.GetKeyDown(KeyCode.O))
        {
            GameData._currentXp += 10;
        }
        #endregion

        #region Affichage XP
        if (_currentLevelBar+1 < levelXpBar.Count && GameData._currentXp >= levelXpBar[_currentLevelBar])
        { 
            GameData._currentXp = 0; 
            _currentLevelBar++;
        }
        if (_currentLevelBar+1 == levelXpBar.Count && GameData._currentXp >= levelXpBar[_currentLevelBar])
        { 
            GameData._currentXp = levelXpBar[_currentLevelBar];
        }
        #endregion

        #region Pause
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
        #endregion
    }
}
