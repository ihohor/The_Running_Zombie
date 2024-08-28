using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public struct TargetCamera
{
    public CinemachineVirtualCamera VirtualCamera;
    public float Delay;
}

public class CutSceneMover : MonoBehaviour
{
    [SerializeField] private TargetCamera[] _targetCameras; // Tablica kamer
    [SerializeField] private List<AudioClip> _cameraSwitchSounds; // Lista dŸwiêków
    [SerializeField] private AudioSource _audioSource; // AudioSource do odtwarzania dŸwiêków

    private int _currentCameraIndex;
    private Coroutine _coroutine;

    private void Awake()
    {
        // Wy³¹cz wszystkie kamery na starcie
        foreach (var cam in _targetCameras)
            cam.VirtualCamera.enabled = false;

        // W³¹cz pierwsz¹ kamerê
        if (_targetCameras.Length > 0)
        {
            _targetCameras[0].VirtualCamera.enabled = true;
        }
    }

    private void Start()
    {
        // Jeœli jest wiêcej ni¿ jedna kamera, rozpocznij proces ich zmiany
        if (_targetCameras.Length > 1)
        {
            _coroutine = StartCoroutine(MoveCamerasProcess());
        }
    }

    private IEnumerator MoveCamerasProcess()
    {
        // Prze³¹czaj kamery po okreœlonym czasie
        while (_currentCameraIndex < _targetCameras.Length - 1)
        {
            yield return new WaitForSeconds(_targetCameras[_currentCameraIndex].Delay);
            _currentCameraIndex++;
            EnableCurrentCamera(_currentCameraIndex);
        }

        // Po zakoñczeniu czekaj 5 sekund i przejdŸ do nastêpnej sceny
        yield return new WaitForSeconds(5f);
        GoToGame();
    }

    private void EnableCurrentCamera(int currentIndex)
    {
        // Wy³¹cz wszystkie kamery
        foreach (var cam in _targetCameras)
            cam.VirtualCamera.enabled = false;

        // W³¹cz aktualn¹ kamerê i odtwórz dŸwiêk, jeœli istnieje
        if (currentIndex >= 0 && currentIndex < _targetCameras.Length)
        {
            _targetCameras[currentIndex].VirtualCamera.enabled = true;
            PlayCameraSwitchSound(currentIndex);
        }
    }

    // Odtwarzaj dŸwiêk przypisany do aktualnej kamery
    private void PlayCameraSwitchSound(int index)
    {
        if (index < _cameraSwitchSounds.Count && _audioSource != null)
        {
            // Odtwórz dŸwiêk z listy, jeœli jest dostêpny
            AudioClip switchSound = _cameraSwitchSounds[index];
            if (switchSound != null)
            {
                _audioSource.PlayOneShot(switchSound);
            }
        }
    }

    public void EndCoroutine()
    {
        // Zatrzymaj proces zmiany kamer, jeœli jest uruchomiony
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    public void GoToGame()
    {
        Debug.Log("Przechodzê do GameScene");
        SceneManager.LoadScene("GameScene");
    }
}
