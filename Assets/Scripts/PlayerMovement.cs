using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private CharacterController controller = null;
	[SerializeField] float speed = 7f;
	[SerializeField] float sprintSpeed = 12f;
	[SerializeField] float rotationSpeed = 50f;
	[SerializeField] float maxStamina = 1;
	private float stamina = 1;

	[SerializeField] Transform playerCamera;
	Controls moveAction = null;

	private void Awake()
	{
		moveAction = new Controls();
	}
	private void OnEnable()
	{
		moveAction.Enable();
	}
	private void OnDisable()
	{
		moveAction.Disable();
	}
	
	private void Update()
	{
		Move();
	}

	private void Move()
	{
		Vector2 mouse = moveAction.Player.Look.ReadValue<Vector2>();
		float pitch = mouse.y * rotationSpeed * Time.deltaTime;
		float yaw = mouse.x * rotationSpeed * Time.deltaTime;
		pitch = Mathf.Clamp(pitch, -15, 37);

		playerCamera.transform.Rotate(-pitch, 0, 0, Space.Self);

		transform.Rotate(0, yaw, 0, Space.World); //rotate player
		MoveDirection();
	}

	private void MoveDirection()
	{
		Vector2 context = moveAction.Player.Move.ReadValue<Vector2>();
		Vector3 right = controller.transform.right;
		Vector3 forward = controller.transform.forward;
		right.y = 0f;
		forward.y = 0f;
		Vector3 movement = right.normalized * context.x + forward.normalized * context.y;
		controller.Move(movement * GetSpeed() * Time.deltaTime);
	}

	private float GetSpeed()
	{
		float newSpeed = speed;
		if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
		{
			stamina = stamina - .01f;
			newSpeed = sprintSpeed;
		}
		else if (!Input.GetKey(KeyCode.LeftShift))
		{
			stamina = stamina < maxStamina ? stamina + .01f : maxStamina;
			newSpeed = speed;
		}
		return newSpeed;
	}
}
