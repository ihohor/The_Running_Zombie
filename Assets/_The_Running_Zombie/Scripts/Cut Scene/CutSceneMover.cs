using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

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

		_targetCameras[0].VirtualCamera.enabled = true;
	}

	private void Start()
	{
		_coroutine = StartCoroutine(MoveCamerasProcess());
	}

	private IEnumerator MoveCamerasProcess()
	{
		for (int i = 0; i < _targetCameras.Length; i++)
		{
			yield return new WaitForSeconds(_targetCameras[_currentCameraIndex].Delay);
			_currentCameraIndex++;
			EnablaCurrentCamera(_currentCameraIndex);
		}
	}

	private void EnablaCurrentCamera(int currentIndex)
	{
		foreach (var cam in _targetCameras)
			cam.VirtualCamera.enabled = false;

		_targetCameras[currentIndex].VirtualCamera.enabled = true;
	}
	
	public void EndCoroutine()
	{
		StopCoroutine(_coroutine);
	}
}
