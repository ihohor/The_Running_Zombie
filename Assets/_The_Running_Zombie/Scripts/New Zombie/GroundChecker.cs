using UnityEngine;

namespace NewZombie
{
	public class GroundChecker : MonoBehaviour
	{
		[SerializeField] private float _groundHeight = 0f;
		[field: SerializeField] public bool IsGrounded { get; private set; }

		public void CheckGround()
		{
			if(transform.position.y > _groundHeight)
				IsGrounded = false;
			else
				IsGrounded = true;
		}
	}
}