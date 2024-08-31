using UnityEngine;

namespace NewZombie
{
	[RequireComponent(typeof(ZombieMover))]
	[RequireComponent(typeof(ZombieInput))]
	[RequireComponent(typeof(ZombieAnimator))]
	public class Zombie : MonoBehaviour
	{
		[SerializeField] private GroundChecker _groundChecker;
		
		private ZombieMover _mover;
		private ZombieInput _input;
		private ZombieAnimator _animator;

		private void Awake()
		{
			_mover = GetComponent<ZombieMover>();
			_input = GetComponent<ZombieInput>();
			_animator = GetComponent<ZombieAnimator>();
		}

		private void OnEnable()
		{
			_input.OnLeftDoubleClick += OnLeftDoubleClick;
			_input.OnRightDoubleClick += OnRightDoubleClick;
		}

		private void OnDisable()
		{
			_input.OnLeftDoubleClick -= OnLeftDoubleClick;
			_input.OnRightDoubleClick -= OnRightDoubleClick;
		}

		private void OnLeftDoubleClick()
		{
			if (_groundChecker.IsGrounded)
				_animator.PlayJumpLeftAnim();
		}

		private void OnRightDoubleClick()
		{
			if (_groundChecker.IsGrounded)
				_animator.PlayJumpRightAnim();
		}

		private void Update()
		{
			_groundChecker.CheckGround();
			
			if(!_groundChecker.IsGrounded) return;

			if (_input.IsLeftHold)
			{
				_mover.MoveLeft();
			}
			else if (_input.IsRightHold)
			{
				_mover.MoveRight();
			}
		}
	}
}