using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _creditsPanel;
    [SerializeField] private GameObject _slider;

    public AudioManager _audioManager;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
    public void Play()
    {
        _audioSource.clip = _audioManager.AudioClips[1];
        _audioSource.Play();
        _slider.SetActive(true);
        StartCoroutine(LoadScene());
    }

    public void Credits()
    {
        _audioSource.clip = _audioManager.AudioClips[0];
        _audioSource.Play();
        _creditsPanel.SetActive(true);
    }
    
    public void QuitCredit()
    {
        _audioSource.clip = _audioManager.AudioClips[0];
        _audioSource.Play();
        _creditsPanel.SetActive(false);
    }
    public void Quit()
    {
        _audioSource.clip = _audioManager.AudioClips[0];
        _audioSource.Play();

#if UNITY_STANDALONE
        Application.Quit();
#endif
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
