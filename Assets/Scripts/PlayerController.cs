using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
	[SerializeField] private float _walkSpeed = 5.0f;

	//이동 방향(플레이어의 키 입력에 따른)
	Vector2 moveInput;

	Rigidbody2D rb;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Input Action 컨트롤 예시
	//private void OnEnable()
	//{
	//	InputAction moveAction = playerInput.currentActionMap.FindAction("Move");
	//	moveAction.performed += OnMove;
	//	moveAction.canceled += OnMove;
	//}

	//private void OnDisable()
	//{
	//	InputAction moveAction = playerInput.currentActionMap.FindAction("Move");
	//	moveAction.performed -= OnMove;
	//	moveAction.canceled -= OnMove;
	//}

	private void FixedUpdate()
	{
		rb.velocity = new Vector3(moveInput.x * _walkSpeed, rb.velocity.y);
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		moveInput = context.ReadValue<Vector2>();


	}

}
