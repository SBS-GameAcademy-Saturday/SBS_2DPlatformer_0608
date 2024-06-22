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
	/// 최대 체력 
	/// </summary>
	public float MaxHealth
	{
		get { return _maxHealth; }
		set { _maxHealth = value; }
	}

	/// <summary>
	/// 현재 체력
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
			//죽는 애니메이션 처리
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

		// 현재 무적 상태이다.
		if (isInvincible)
		{
			// timeSinceHit이 지정된 무적 시간보다 커진 경우
			if (timeSinceHit > invincibilityTime)
			{
				// 무적 상태를 false로 변환하고
				isInvincible = false;
				// timeSinceHit를 초기화한다.
				timeSinceHit = 0;
			}
			// 매 프레임마타 Time.deltaTime을 더해준다.
			timeSinceHit += Time.deltaTime;
			return;
		}
	}

	/// <summary>
	/// 데미지를 추가하는 함수
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
