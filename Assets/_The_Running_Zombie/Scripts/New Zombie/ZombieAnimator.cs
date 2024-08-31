using DG.Tweening;
using UnityEngine;

namespace NewZombie
{
	public class ZombieAnimator : MonoBehaviour
	{
		[SerializeField] private float _jumpPower = 5f;
		[SerializeField] private float _jumpDuration = 1f;
		[SerializeField] private Vector3 _jumpTarget;
		[SerializeField] private Ease _jumpEase;
		
		public void PlayJumpLeftAnim()
		{
			PlayJump(-_jumpTarget);
		}
		
		public void PlayJumpRightAnim()
		{
			PlayJump(_jumpTarget);
		}
		
		private void PlayJump(Vector3 target)
		{
			transform.DOKill();
			transform.DOJump(transform.position + target, _jumpPower, 1, _jumpDuration)
				.SetEase(_jumpEase);
		}
	}
}