using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject _xpPanel;
    [SerializeField] private Image _lifeBar;
    [SerializeField] private TextMeshProUGUI _killText;
    [SerializeField] private TextMeshProUGUI _coolDownUltText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private int levelXpBar = 0;

    private void Update()
    {
        //_lifeBar.fillAmount = GameData.
        _coolDownUltText.SetText("88");
        _killText.SetText(GameData._kill.ToString());
        _scoreText.SetText(GameData._score.ToString());

        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            _xpPanel.SetActive(!_xpPanel.activeSelf);
        }
    }
}
