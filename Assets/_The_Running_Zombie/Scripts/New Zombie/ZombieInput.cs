using System;
using UnityEngine;

namespace NewZombie
{
	public class ZombieInput : MonoBehaviour
	{
		[SerializeField] private CustomButton _leftButton;
		[SerializeField] private CustomButton _rightButton;

		public bool IsLeftHold => _leftButton.IsButtonHold;
		public bool IsRightHold => _rightButton.IsButtonHold;

		public event Action OnLeftDoubleClick;
		public event Action OnRightDoubleClick;

		private void OnEnable()
		{
			_leftButton.OnDoubleClick += LeftDoubleClick;
			_rightButton.OnDoubleClick += RightDoubleClick;
		}

		private void OnDisable()
		{
			_leftButton.OnDoubleClick -= LeftDoubleClick;
			_rightButton.OnDoubleClick -= RightDoubleClick;
		}

		private void LeftDoubleClick()
		{
			OnLeftDoubleClick?.Invoke();
		}

		private void RightDoubleClick()
		{
			OnRightDoubleClick?.Invoke();
		}
	}
}