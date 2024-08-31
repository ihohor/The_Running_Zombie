using UnityEngine;

namespace NewZombie
{
	public class ZombieMover : MonoBehaviour
	{
		[SerializeField] private Vector2 _leftVector;
		[SerializeField] private Vector2 _rightVector;
		[SerializeField] private float _sideMoveSpeed = 10f;

		public void MoveLeft()
		{
			Move(_leftVector);
		}

		public void MoveRight()
		{
			Move(_rightVector);
		}
		
		private void Move(Vector2 direction)
		{
			transform.Translate(direction * _sideMoveSpeed);
		}
	}
}