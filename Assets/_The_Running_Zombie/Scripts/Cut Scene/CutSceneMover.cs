using System;
using System.Collections;
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
    [SerializeField] private TargetCamera[] _targetCameras;

    private int _currentCameraIndex;
    private Coroutine _coroutine;

    private void Awake()
    {
        foreach (var cam in _targetCameras)
            cam.VirtualCamera.enabled = false;

        if (_targetCameras.Length > 0)
        {
            _targetCameras[0].VirtualCamera.enabled = true;
        }
    }

    private void Start()
    {
        if (_targetCameras.Length > 1)
        {
            _coroutine = StartCoroutine(MoveCamerasProcess());
        }
    }

    private IEnumerator MoveCamerasProcess()
    {
        while (_currentCameraIndex < _targetCameras.Length - 1)
        {
            yield return new WaitForSeconds(_targetCameras[_currentCameraIndex].Delay);
            _currentCameraIndex++;
            EnableCurrentCamera(_currentCameraIndex);
        }

        yield return new WaitForSeconds(5f);
        GoToGame();
    }

    private void EnableCurrentCamera(int currentIndex)
    {
        foreach (var cam in _targetCameras)
            cam.VirtualCamera.enabled = false;

        if (currentIndex >= 0 && currentIndex < _targetCameras.Length)
        {
            _targetCameras[currentIndex].VirtualCamera.enabled = true;
        }
    }

    public void EndCoroutine()
    {
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
