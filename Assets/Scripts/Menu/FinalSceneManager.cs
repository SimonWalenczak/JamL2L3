using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _killText;

    [SerializeField] private GameObject _slider;
    
    public AudioManager _audioManager;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _scoreText.SetText("Score : " + GameData._score.ToString());
        _timeText.SetText("Time : " + GameData.time);
        _killText.SetText(GameData._kill.ToString());
    }   

    public void ReturnToMainMenu()
    {
        _audioSource.clip = _audioManager.AudioClips[0];
        _audioSource.Play();
        _slider.SetActive(true);
        StartCoroutine(SwitchScene());
    }

    IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
    