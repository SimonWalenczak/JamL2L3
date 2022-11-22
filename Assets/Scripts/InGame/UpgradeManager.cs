using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameObject UpgradePanel;
    
    public PlayerController _playerController;
    public BasicAttack _basicAttack;
    public PlayerAttack _playerAttack;
    public UiManager _uiManager;

    [Header("Value Upgrade\n")] 
    [Header("Value ShootPower")]
    public GameObject ShootPowerButton;
    public float ShootPowerUpValue;
    public int ShootPowerIndex = 0;
    
    [Header("Value MultiplyScore")]
    public GameObject MultiplyScoreButton;
    public List<float> MultiplyScoreUpValue;
    public int MultiplyScoreIndex = 0;
    
    [Header("Value CoolDownReduction")]
    public GameObject CoolDownReductionButton;
    public float CoolDownReductionUpValue;
    public int CoolDownReductionIndex = 0;
    
    [Header("Value MoveSpeed")]
    public GameObject MoveSpeedButton;
    public float MoveSpeedUpValue;
    public int MoveSpeedIndex = 0;
    
    [Header("Value Weight")]
    public GameObject WeightButton;
    public float WeightUpValue;
    public int WeightIndex = 0;
    
    [Header("Value ShootSpeed")]
    public GameObject ShootSpeedButton;
    public float ShootSpeedUpValue;
    public int ShootSpeedIndex = 0;

    private void Update()
    {
        if (_uiManager._inUpgradeMode == true)
        {
            UpgradePanel.SetActive(true);
            Time.timeScale = 0;
        }
        
        if (ShootPowerIndex == 5)
            ShootPowerButton.SetActive(false);
        if (MultiplyScoreIndex == MultiplyScoreUpValue.Count)
            MultiplyScoreButton.SetActive(false);
        if (CoolDownReductionIndex == 3)
            CoolDownReductionButton.SetActive(false);
        if (MoveSpeedIndex == 5)
            MoveSpeedButton.SetActive(false);
        if (WeightIndex == 4)
            WeightButton.SetActive(false);
        if (ShootSpeedIndex == 3)
            ShootSpeedButton.SetActive(false);
    }
    public void ShootPowerUp()
    {
        if (ShootPowerIndex < 5)
        {
            _basicAttack.attackForce += ShootPowerUpValue;
            ShootPowerIndex++;
            UpgradePanel.SetActive(false);
            _uiManager._inUpgradeMode = false;
            Time.timeScale = 1;
        }
    }
    public void MultiplyScoreUp()
    {
        if (MultiplyScoreIndex < MultiplyScoreUpValue.Count)
        {
            MultiplyScoreIndex++;
            UpgradePanel.SetActive(false);
            _uiManager._inUpgradeMode = false;
            Time.timeScale = 1;
        }
    }
    public void CoolDownReductionUp()
    {
        if (MultiplyScoreIndex < 3)
        {
            _playerController._maxCurrentCoolDownUlt -= CoolDownReductionUpValue;
            CoolDownReductionIndex++;
            UpgradePanel.SetActive(false);
            _uiManager._inUpgradeMode = false;
            Time.timeScale = 1;
        }
    }
    public void MovementSpeedUp()
    {
        if (MoveSpeedIndex < 5)
        {
            _playerController._speed += MoveSpeedUpValue;
            MoveSpeedIndex++;
            UpgradePanel.SetActive(false);
            _uiManager._inUpgradeMode = false;
            Time.timeScale = 1;
        }
    }
    public void WeightUp()
    {
        if (WeightIndex < 4)
        {
            _playerController.invincibilityTime -= WeightUpValue;
            WeightIndex++;
            UpgradePanel.SetActive(false);
            _uiManager._inUpgradeMode = false;
            Time.timeScale = 1;
        }
    }
    public void ShootSpeedUp()
    {
        if (ShootSpeedIndex < 3)
        {
            _playerAttack.timeBetweenAttacks -= ShootSpeedUpValue;
            ShootSpeedIndex++;
            UpgradePanel.SetActive(false);
            _uiManager._inUpgradeMode = false;
            Time.timeScale = 1;
        }
    }
}
