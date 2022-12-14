using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject _slideIn;
    public GameObject _slideOut;
    
    [SerializeField] private GameObject _pausedMenu;
    [SerializeField] private GameObject _xpPanel;
    [SerializeField] private Image _xpBar;
    [SerializeField] private TextMeshProUGUI _killText;
    [SerializeField] private TextMeshProUGUI _coolDownUltText;
    [SerializeField] private TextMeshProUGUI _pressSpaceText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private int _currentLevelBar = 0;
    [SerializeField] List<int> levelXpBar = new List<int>();

    private bool _isPaused;
    public bool _inUpgradeMode;
    
    [SerializeField] PlayerController _playerController;

    IEnumerator SlideIn()
    {
        yield return new WaitForSeconds(2);
        _slideIn.SetActive(false);
    }
    private void Start()
    {
        StartCoroutine(SlideIn());
    }

    private void Update()
    {
        _xpBar.fillAmount = (float)GameData._currentXp / (float)levelXpBar[_currentLevelBar];
        _killText.SetText(GameData._kill.ToString());
        _scoreText.SetText("Score : " + GameData._score.ToString());

        if (_playerController._currentCoolDownUlt <= 0)
        {
            _coolDownUltText.SetText("");
            _pressSpaceText.gameObject.SetActive(true);
        }
        else
        {
            _coolDownUltText.SetText(((int)(_playerController._currentCoolDownUlt)).ToString());
            _pressSpaceText.gameObject.SetActive(false);
        }

        #region Affichage XP
        if (_currentLevelBar+1 < levelXpBar.Count && GameData._currentXp >= levelXpBar[_currentLevelBar])
        {
            _inUpgradeMode = true;
            GameData._currentXp = 0; 
            _currentLevelBar++;
        }
        if (_currentLevelBar+1 == levelXpBar.Count && GameData._currentXp >= levelXpBar[_currentLevelBar])
        { 
            _inUpgradeMode = true;
            GameData._currentXp = levelXpBar[_currentLevelBar];
        }
        #endregion

        #region Pause
        if (!_inUpgradeMode && Input.GetKeyDown(KeyCode.Escape))
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
