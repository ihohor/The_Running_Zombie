using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

namespace NewZombie
{
	[RequireComponent(typeof(Image))]
	public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		private const float TIME_TO_SEND_HOLDING_EVENT = 0.5f;
		private const float TIME_DOUBLE_CLICK_EVENT = 0.5f;

		private float _timer;
		private float _firstClickTime;

		private bool _isHoldingEventSended;

		public bool IsButtonHold { get; private set; }

		public event Action OnButtonDown;
		public event Action OnLongButtonHold;
		public event Action OnButtonUp;
		public event Action OnDoubleClick;

		protected PointerEventData CurrentEventData;

		public void OnPointerDown(PointerEventData eventData)
		{
			CurrentEventData = eventData;
			ButtonDown();
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			ButtonUp();
			CurrentEventData = eventData;
		}

		protected virtual void ButtonDown()
		{
			if (Time.timeSinceLevelLoad - _firstClickTime < TIME_DOUBLE_CLICK_EVENT)
				OnDoubleClick?.Invoke();

			_firstClickTime = Time.timeSinceLevelLoad;
			OnButtonDown?.Invoke();
			IsButtonHold = true;
		}

		protected virtual void ButtonUp()
		{
			_timer = 0;
			IsButtonHold = false;
			_isHoldingEventSended = false;
			OnButtonUp?.Invoke();
		}

		protected virtual void ButtonHold()
		{
			if (!_isHoldingEventSended)
			{
				_timer += Time.deltaTime;
				if (_timer > TIME_TO_SEND_HOLDING_EVENT)
				{
					OnLongButtonHold?.Invoke();
					_isHoldingEventSended = true;
				}
			}
		}

		private void Update()
		{
			if (IsButtonHold)
				ButtonHold();
		}
	}
}
