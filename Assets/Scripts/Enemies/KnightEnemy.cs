using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class KnightEnemy : MonoBehaviour
{
	public enum WalkableDirection
	{
		Right,
		Left,
	}

	[SerializeField] private float walkSpeed = 3.0f;
	[SerializeField] private DetectionZone hitBoxDetectionZone;

	public WalkableDirection WalkDirection
	{
		get { return _walkableDirection; }
		set
		{
			if(_walkableDirection != value)
			{
				transform.localScale =
					new Vector2(transform.localScale.x * -1, transform.localScale.y);
				switch (value)
				{
					case WalkableDirection.Left:
						walkDirectionVector = Vector2.left;
						break;
					case WalkableDirection.Right:
						walkDirectionVector = Vector2.right;
						break;
				}
			}
			_walkableDirection = value;
		}
	}

	[SerializeField] private bool _hasTarget = false;

	public bool HasTarget
	{
		get { return _hasTarget; }
		set
		{
			_hasTarget = value;
			animator.SetBool(AnimationStrings.HasTarget, _hasTarget);
		}
	}

	public bool CanMove
	{
		get { return animator.GetBool(AnimationStrings.CanMove); }
	}

	public float AttackCoolDown
	{
		get { return animator.GetFloat(AnimationStrings.AttackCoolDown); }
		set { animator.SetFloat(AnimationStrings.AttackCoolDown, Mathf.Max(value,0)); }
	}

	private Rigidbody2D rb;
	private Animator animator;
	private TouchingDirections touchingDirections;
	private Damageable damageable;

	private WalkableDirection _walkableDirection = WalkableDirection.Left;
	private Vector2 walkDirectionVector = Vector2.left;

	// Start is called before the first frame update
	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		touchingDirections = GetComponent<TouchingDirections>();
		damageable = GetComponent<Damageable>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!damageable.IsAlive)
			return;
		// �ش� Colldier ���� ���� ���� ������Ʈ�� �ִ�.
		HasTarget = hitBoxDetectionZone.detectedColliders.Count > 0;
        // AttackCoolDown�� 0���� ũ��
        if (AttackCoolDown > 0)
        {
			//Time.deltaTime : �� �����ӿ��� ���� ���������� �Ѿ�� �� ���� �ɸ��� �ð� ����
			AttackCoolDown -= Time.deltaTime;
        }
    }

	private void FixedUpdate()
	{
		if (!damageable.IsAlive)
			return;
		if (touchingDirections.IsOnWall && touchingDirections.IsGrounded)
        {
			FlipDirection();
		}
		if(CanMove)
			rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
		// �˹� ó��
	}

	public void FlipDirection()
	{
		switch (WalkDirection)
		{
			case WalkableDirection.Right:
				WalkDirection = WalkableDirection.Left;
				break;
			case WalkableDirection.Left:
				WalkDirection = WalkableDirection.Right;
				break;
		}
	}


}