using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character.
	[SerializeField] private CineCameraShake m_CameraShake;

	[HideInInspector]
	public bool m_Grounded;            // Whether or not the player is grounded.
	[HideInInspector]
	public bool m_OnWall;
	public Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	public bool m_EnableMovement = true;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject == gameObject)
			return;

		bool wasGrounded = m_Grounded;
		m_Grounded = false;
		m_OnWall = false;

		int layer = collision.gameObject.layer;

		// contained
		if (m_WhatIsGround == (m_WhatIsGround | 1 << layer))
		{
			m_Grounded = true;
			if (!wasGrounded)
				OnLandEvent.Invoke();
		}
		else if (layer == LayerMask.NameToLayer("Wall"))
		{
			m_OnWall = true;
		}
	}


	public void Move(float move)
	{
		if (!m_EnableMovement)
			move = 0f;

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
	}

	public bool PushingAgainstWall(float horizontalMove)
	{
		if ((!m_FacingRight && horizontalMove < 0f)
			|| (m_FacingRight && horizontalMove > 0f))
			return true;

		return false;
	}

	public void Jump()
	{
		// Add a vertical force to the player.
		m_Grounded = false;
		m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

		AudioManager.instance.Play("Jump");
	}

	public void ShakeCam()
	{
		m_CameraShake.Shake(8f, 0.1f);
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void FreezMovement()
    {
		StartCoroutine(FreezeMovementForSecond(1.2f));
    }

	private IEnumerator FreezeMovementForSecond(float sec)
    {
		m_EnableMovement = false;
		yield return new WaitForSeconds(sec);
		m_EnableMovement = true;
	}
}