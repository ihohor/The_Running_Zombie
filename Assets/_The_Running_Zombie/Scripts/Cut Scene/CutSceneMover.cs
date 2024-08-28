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
    [SerializeField] private List<AudioClip> _cameraSwitchSounds; // Lista d�wi�k�w
    [SerializeField] private AudioSource _audioSource; // AudioSource do odtwarzania d�wi�k�w

    private int _currentCameraIndex;
    private Coroutine _coroutine;

    private void Awake()
    {
        // Wy��cz wszystkie kamery na starcie
        foreach (var cam in _targetCameras)
            cam.VirtualCamera.enabled = false;

        // W��cz pierwsz� kamer�
        if (_targetCameras.Length > 0)
        {
            _targetCameras[0].VirtualCamera.enabled = true;
        }
    }

    private void Start()
    {
        // Je�li jest wi�cej ni� jedna kamera, rozpocznij proces ich zmiany
        if (_targetCameras.Length > 1)
        {
            _coroutine = StartCoroutine(MoveCamerasProcess());
        }
    }

    private IEnumerator MoveCamerasProcess()
    {
        // Prze��czaj kamery po okre�lonym czasie
        while (_currentCameraIndex < _targetCameras.Length - 1)
        {
            yield return new WaitForSeconds(_targetCameras[_currentCameraIndex].Delay);
            _currentCameraIndex++;
            EnableCurrentCamera(_currentCameraIndex);
        }

        // Po zako�czeniu czekaj 5 sekund i przejd� do nast�pnej sceny
        yield return new WaitForSeconds(5f);
        GoToGame();
    }

    private void EnableCurrentCamera(int currentIndex)
    {
        // Wy��cz wszystkie kamery
        foreach (var cam in _targetCameras)
            cam.VirtualCamera.enabled = false;

        // W��cz aktualn� kamer� i odtw�rz d�wi�k, je�li istnieje
        if (currentIndex >= 0 && currentIndex < _targetCameras.Length)
        {
            _targetCameras[currentIndex].VirtualCamera.enabled = true;
            PlayCameraSwitchSound(currentIndex);
        }
    }

    // Odtwarzaj d�wi�k przypisany do aktualnej kamery
    private void PlayCameraSwitchSound(int index)
    {
        if (index < _cameraSwitchSounds.Count && _audioSource != null)
        {
            // Odtw�rz d�wi�k z listy, je�li jest dost�pny
            AudioClip switchSound = _cameraSwitchSounds[index];
            if (switchSound != null)
            {
                _audioSource.PlayOneShot(switchSound);
            }
        }
    }

    public void EndCoroutine()
    {
        // Zatrzymaj proces zmiany kamer, je�li jest uruchomiony
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    public void GoToGame()
    {
        Debug.Log("Przechodz� do GameScene");
        SceneManager.LoadScene("GameScene");
    }
}
