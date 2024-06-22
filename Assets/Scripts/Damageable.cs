using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Damageable : MonoBehaviour
{
	[SerializeField] private float _maxHealth = 100;
	[SerializeField] private float _health = 100;
	[SerializeField] private bool _isAlive = true;

	[SerializeField] private bool isInvincible = false;
	[SerializeField] private float invincibilityTime = 0.5f;

	private Animator animator;
	private float timeSinceHit = 0;

	/// <summary>
	/// �ִ� ü�� 
	/// </summary>
	public float MaxHealth
	{
		get { return _maxHealth; }
		set { _maxHealth = value; }
	}

	/// <summary>
	/// ���� ü��
	/// </summary>
	public float Health
	{
		get { return _health; }
		set 
		{ 
			_health = value;
			if(_health <= 0)
			{
				IsAlive = false;
			}
		}
	}

	public bool IsAlive
	{
		get { return _isAlive; }
		set
		{
			_isAlive = value;
			//�״� �ִϸ��̼� ó��
			animator.SetBool(AnimationStrings.IsAlive, _isAlive);
		}
	}

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			GetHit(33);
		}

		// ���� ���� �����̴�.
		if (isInvincible)
		{
			// timeSinceHit�� ������ ���� �ð����� Ŀ�� ���
			if (timeSinceHit > invincibilityTime)
			{
				// ���� ���¸� false�� ��ȯ�ϰ�
				isInvincible = false;
				// timeSinceHit�� �ʱ�ȭ�Ѵ�.
				timeSinceHit = 0;
			}
			// �� �����Ӹ�Ÿ Time.deltaTime�� �����ش�.
			timeSinceHit += Time.deltaTime;
			return;
		}
	}

	/// <summary>
	/// �������� �߰��ϴ� �Լ�
	/// </summary>
	/// <param name="damage"></param>
	/// <returns></returns>
	public bool GetHit(int damage)
	{
		if (IsAlive && !isInvincible)
		{
			Health -= damage;
			isInvincible = true;
		}
		return false;
	}


}
