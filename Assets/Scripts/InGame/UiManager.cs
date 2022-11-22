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

    private void Update()
    {
        // _lifeBar.fillAmount =
        _killText.SetText(GameData._kill.ToString());
        _coolDownUltText.SetText("88");

        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            _xpPanel.SetActive(!_xpPanel.activeSelf);
        }
    }
}
